﻿using FluentValidation.Results;
using FML.Core.Mediator;
using FML.Core.Messages.Integrations;
using FML.Familiares.API.Application.Commands;
using FML.MessageBus;

namespace FML.Familiares.API.BackgroundServices
{
    public class FamiliarIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public FamiliarIntegrationHandler(
                        IServiceProvider serviceProvider,
                        IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<FamiliarRegistradoIntegrationEvent, ResponseMessage>(async request =>
                await RegistrarFamiliar(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegistrarFamiliar(FamiliarRegistradoIntegrationEvent message)
        {
            var falimiarCommand = new RegistrarFamiliarCommand(message.Id, message.Nome, message.Email, message.BirthDate, message.Gender);
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(falimiarCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
