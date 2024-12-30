using FML.Core.Data;
using FML.Familiares.API.Clients;
using FML.Familiares.API.Data.Repository.Interface;
using FML.Familiares.API.Models;
using FML.Familiares.API.Services.Interface;

namespace FML.Familiares.API.Services
{
    public class RelativeService : IRelativeService
    {
        private readonly IRelativeRepository _relativeRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IHouseRepository _houseRepository;
        private readonly IFileHttp _fileHttp;

        public RelativeService(
            IRelativeRepository relativeRepository, 
            IFamilyRepository familyRepository, 
            IHouseRepository houseRepository,
            IFileHttp fileHttp)
        {
            _relativeRepository = relativeRepository;
            _familyRepository = familyRepository;
            _houseRepository = houseRepository;
            _fileHttp = fileHttp;
        }


        public async Task<IEnumerable<Relative>> GetRelatives()
        {
            var id = Guid.Parse("417d7e43-fe2f-44d6-a8c4-4070e841ad53");
            var relatives = await _relativeRepository.GetRelativesByFamilyId(id);

            foreach (var relative in relatives.Where(x => x.FotoId != null))
            {
                var url = await _fileHttp.ImageUrlAsync(relative.FotoId.Value);
                relative.FotoPerfil = url;
            }

            return relatives;
        }

        public async Task<Relative> GetRelativeById(Guid relativeId)
        {
            var relative =  await _relativeRepository.GetRelativeById(relativeId);

            if(relative.FotoId != null)
            {
                var url = await _fileHttp.ImageUrlAsync(relative.FotoId.Value);
                relative.FotoPerfil = url;
            }

            return relative;
        }

        public async Task<Guid> Add()
        {
            var familia = new Family();
            familia.Id = Guid.NewGuid();
            familia.Name = "Carvalho";
            familia.StartDate = new DateTime(1960, 1, 1);
            familia.IsActive = true;
            familia.History = "Familia Carvalho";

            await _familyRepository.AddFamily(familia);

            #region Casas 

            var casasCarvalho = new List<House>();

            var carvalho = new House
            {
                Id = Guid.NewGuid(),
                Name = "Carvalho",
                FamilyId = familia.Id,
                IsActive = true
            };

            var carvalhoMessias = new House
            {
                Id = Guid.NewGuid(),
                Name = "Carvalho Messias",
                FamilyId = familia.Id,
                IsActive = true
            };

            casasCarvalho.Add(carvalhoMessias);

            var carvalhoOliveira = new House
            {
                Id = Guid.NewGuid(),
                Name = "Carvalho Oliveira",
                FamilyId = familia.Id,
                IsActive = true
            };

            casasCarvalho.Add(carvalhoOliveira);

            var carvalhoValverde = new House
            {
                Id = Guid.NewGuid(),
                Name = "Carvalho Valverde",
                FamilyId = familia.Id,
                IsActive = true
            };

            casasCarvalho.Add(carvalhoValverde);

            var carvalhoFerreira = new House
            {
                Id = Guid.NewGuid(),
                Name = "Carvalho Ferreira",
                FamilyId = familia.Id,
                IsActive = true
            };

            casasCarvalho.Add(carvalhoFerreira);

            var carvalhoJunqueira = new House
            {
                Id = Guid.NewGuid(),
                Name = "Carvalho Junqueira",
                FamilyId = familia.Id,
                IsActive = true
            };

            casasCarvalho.Add(carvalhoJunqueira);

            foreach (var casa in casasCarvalho)
            {
                await _houseRepository.AddHouse(casa);
            }

            await _familyRepository.UpdateFamily(familia);

            #endregion

            Relative chicada, mariaJovita;
            GetChicada(carvalho, out chicada, out mariaJovita);

            Relative inha, mario, cacalo, lidia, rosane, joseEugenio, marcelo, marlene, icaro, lidiaMara, natasha, marcela, huncas, yuri, raissa;
            CarvalhoMessias(carvalhoMessias, chicada, mariaJovita, out inha, out mario, out cacalo, out lidia, out rosane, out joseEugenio, out marcelo, out marlene, out icaro, out lidiaMara, out natasha, out marcela, out huncas, out yuri, out raissa);

            Relative miguel, lidinha, andre, lidiane, arthur, luizGustavo, marcusVinicius, isabela;
            CarvalhoOliveira(carvalhoOliveira, out miguel, out lidinha, out andre, out lidiane, out arthur, out luizGustavo, out marcusVinicius, out isabela);

            Relative roseli, jurandyr, belisa, eduardo, vital, renata, manuela, benicio;
            CarvalhoValverde(carvalhoValverde, chicada, mariaJovita, out roseli, out jurandyr, out belisa, out eduardo, out vital, out renata, out manuela, out benicio);

            Relative tina, renataAscencio, rodrigo, patricia, paulo, daniela, adrianoMaretti, julia, duda, guilherme, drizinho, henrique, pauloFerreira;
            CarvalhoFerreira(carvalhoFerreira, chicada, mariaJovita, out tina, out renataAscencio, out rodrigo, out patricia, out paulo, out daniela, out adrianoMaretti, out julia, out duda, out guilherme, out drizinho, out henrique, out pauloFerreira);

            Relative dalila, airthon, mayra, adrianoDorna, bruna, leonardo, marina, gustavo, otavio;
            CarvalhoJunqueira(carvalhoJunqueira, chicada, mariaJovita, out dalila, out airthon, out mayra, out adrianoDorna, out bruna, out leonardo, out marina, out gustavo, out otavio);

            Final(inha, cacalo, icaro, lidiaMara, natasha, marcela, lidinha, arthur, luizGustavo, roseli, jurandyr, belisa, eduardo, manuela, benicio, tina, renataAscencio, rodrigo, daniela, adrianoMaretti, julia, duda, guilherme, drizinho, henrique, mayra, adrianoDorna, marina, gustavo, otavio);

            var relatives = new List<Relative>
            {
                chicada, mariaJovita,
                inha, mario,

                tina, pauloFerreira , jurandyr, roseli, lidinha, miguel, dalila, airthon,

                marcelo, marlene, cacalo, lidia, rosane, joseEugenio,
                daniela, adrianoMaretti, paulo, patricia, renataAscencio, rodrigo,
                eduardo, belisa, renata, vital,
                andre, lidiane,
                leonardo, bruna, mayra, adrianoDorna,

                icaro, lidiaMara, marcela, natasha, huncas, yuri, raissa,
                guilherme, henrique, drizinho, duda, julia,
                benicio, manuela,
                arthur, luizGustavo, marcusVinicius, isabela,
                marina, gustavo, otavio
            };

            foreach (var relative in relatives)
            {
                relative.FamilyId = familia.Id;
                relative.HouseId = relative.House.Id;

                _relativeRepository.AddRelative(relative);
            }

            if (!await _relativeRepository.UnitOfWork.Commit())
            {
                Console.WriteLine("Erro ao salvar os dados");
            }


            return familia.Id;
        }

        public async Task<bool> Update(UpdateRelativeModel relative)
        {
            if (!string.IsNullOrEmpty(relative.FotoFileBase64))
            {
                var bytes = Convert.FromBase64String(relative.FotoFileBase64);
                var fileName = $"{relative.Relative.Id}.jpg";
                var response = await _fileHttp.UploadPhotoAsync(bytes, fileName);

                if (response.IsSuccessStatusCode)
                {
                    var id = await response.Content.ReadAsStringAsync();
                    relative.Relative.FotoId = Guid.Parse(id.Trim('"'));

                }
                else
                {
                    // Handle error
                    return false;
                }
            }

            return await _relativeRepository.UpdateRelative(relative.Relative);
        }





        public async Task<bool> AddRelative(Relative relative)
        {
            return await _familyRepository.AddRelative(relative);
        }

        public async Task<bool> RemoveRelative(Guid relativeId)
        {
            return await _relativeRepository.RemoveRelative(relativeId);
        }


        #region Private Methods


        private static void CarvalhoJunqueira(House carvalhoJunqueira, Relative chicada, Relative mariaJovita, out Relative dalila, out Relative airthon, out Relative mayra, out Relative adrianoDorna, out Relative bruna, out Relative leonardo, out Relative marina, out Relative gustavo, out Relative otavio)
        {
            dalila = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Dalila",
                LastName = "Carvalho Junqueira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoJunqueira,
                IsActive = false,
                FatherId = chicada.Id,
                MotherId = mariaJovita.Id
            };
            airthon = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Airthon",
                LastName = "Junqueira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoJunqueira,
                IsActive = false
            };
            airthon.Spouse = dalila.Id;
            dalila.Spouse = airthon.Id;

            mayra = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Mayra",
                LastName = "Carvalho Junqueira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoJunqueira,
                IsActive = false,
                FatherId = airthon.Id,
                MotherId = dalila.Id
            };
            adrianoDorna = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Adriano",
                LastName = "Dorna",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoJunqueira,
                IsActive = false
            };
            adrianoDorna.Spouse = mayra.Id;
            mayra.Spouse = adrianoDorna.Id;

            bruna = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Bruna",
                LastName = "Carvalho",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoJunqueira,
                IsActive = false
            };
            leonardo = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Leonardo",
                LastName = "Carvalho Junqueira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoJunqueira,
                IsActive = false,
                FatherId = airthon.Id,
                MotherId = dalila.Id
            };
            leonardo.Spouse = bruna.Id;
            bruna.Spouse = leonardo.Id;


            marina = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Marina",
                LastName = "Carvalho Junqueira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoJunqueira,
                IsActive = false,
                FatherId = leonardo.Id,
                MotherId = bruna.Id
            };
            gustavo = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Gustavo",
                LastName = "Carvalho Junqueira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoJunqueira,
                IsActive = false,
                FatherId = adrianoDorna.Id,
                MotherId = mayra.Id
            };
            otavio = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Otavio",
                LastName = "Carvalho Junqueira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoJunqueira,
                IsActive = false,
                FatherId = adrianoDorna.Id,
                MotherId = mayra.Id
            };
        }

        private static void CarvalhoFerreira(House carvalhoFerreira, Relative chicada, Relative mariaJovita, out Relative tina, out Relative renataAscencio, out Relative rodrigo, out Relative patricia, out Relative paulo, out Relative daniela, out Relative adrianoMaretti, out Relative julia, out Relative duda, out Relative guilherme, out Relative drizinho, out Relative henrique, out Relative pauloFerreira)
        {
            pauloFerreira = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Paulo",
                LastName = "Ferreira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsAlive = false
            };

            tina = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Tina",
                LastName = "Carvalho Ferreira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = true,
                FatherId = chicada.Id,
                MotherId = mariaJovita.Id
            };
            tina.Spouse = pauloFerreira.Id;
            pauloFerreira.Spouse = tina.Id;

            renataAscencio = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Renata",
                LastName = "Ascêncio",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false
            };
            rodrigo = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Rodrigo",
                LastName = "Carvalho Ferreira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false,
                FatherId = pauloFerreira.Id,
                MotherId = tina.Id
            };
            rodrigo.Spouse = renataAscencio.Id;
            renataAscencio.Spouse = rodrigo.Id;

            patricia = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Patricia",
                LastName = "Carvalho Ferreira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false,
            };
            paulo = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Paulo",
                LastName = "Carvalho Ferreira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false,
                FatherId = pauloFerreira.Id,
                MotherId = tina.Id
            };
            paulo.Spouse = patricia.Id;
            patricia.Spouse = paulo.Id;

            daniela = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Daniela",
                LastName = "Carvalho Maretti",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false,
                FatherId = pauloFerreira.Id,
                MotherId = tina.Id
            };
            adrianoMaretti = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Adriano",
                LastName = "Maretti",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false
            };
            daniela.Spouse = adrianoMaretti.Id;
            adrianoMaretti.Spouse = daniela.Id;


            julia = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Julia",
                LastName = "Carvalho Ferreira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false,
                FatherId = rodrigo.Id,
                MotherId = renataAscencio.Id
            };
            duda = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Duda",
                LastName = "Carvalho Ferreira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false,
                FatherId = paulo.Id,
                MotherId = patricia.Id
            };
            guilherme = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Guilherme",
                LastName = "Carvalho Ferreira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false,
                FatherId = paulo.Id,
                MotherId = patricia.Id
            };
            drizinho = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Drizinho",
                LastName = "Carvalho Maretti",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false,
                FatherId = adrianoMaretti.Id,
                MotherId = daniela.Id
            };
            henrique = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Henrique",
                LastName = "Carvalho Maretti",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoFerreira,
                IsActive = false,
                FatherId = adrianoMaretti.Id,
                MotherId = daniela.Id
            };
        }

        private static void CarvalhoValverde(House carvalhoValverde, Relative chicada, Relative mariaJovita, out Relative roseli, out Relative jurandyr, out Relative belisa, out Relative eduardo, out Relative vital, out Relative renata, out Relative manuela, out Relative benicio)
        {
            roseli = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Roseli",
                LastName = "Valverde Carvalho",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoValverde,
                IsActive = false
            };
            jurandyr = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jurandyr",
                LastName = "Valverde Carvalho",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoValverde,
                IsActive = false,
                FatherId = chicada.Id,
                MotherId = mariaJovita.Id
            };
            jurandyr.Spouse = roseli.Id;
            roseli.Spouse = jurandyr.Id;

            belisa = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Belisa",
                LastName = "Valverde Carvalho",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoValverde,
                IsActive = false
            };
            eduardo = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Eduardo",
                LastName = "Valverde Carvalho",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoValverde,
                IsActive = false,
                FatherId = jurandyr.Id,
                MotherId = roseli.Id
            };
            eduardo.Spouse = belisa.Id;
            belisa.Spouse = eduardo.Id;

            vital = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Vital",
                LastName = "Valverde Carvalho",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoValverde,
                IsActive = false
            };
            renata = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Renata",
                LastName = "Valverde Carvalho",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoValverde,
                IsActive = false,
                FatherId = jurandyr.Id,
                MotherId = roseli.Id
            };
            renata.Spouse = vital.Id;
            vital.Spouse = renata.Id;

            manuela = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Manuela",
                LastName = "Valverde Carvalho",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoValverde,
                FatherId = vital.Id,
                MotherId = renata.Id
            };
            benicio = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Benicio",
                LastName = "Valverde Carvalho",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoValverde,
                FatherId = eduardo.Id,
                MotherId = belisa.Id
            };
        }

        private static void CarvalhoOliveira(House carvalhoOliveira, out Relative miguel, out Relative lidinha, out Relative andre, out Relative lidiane, out Relative arthur, out Relative luizGustavo, out Relative marcusVinicius, out Relative isabela)
        {
            miguel = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Miguel",
                LastName = "Oliveira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoOliveira,
                IsActive = false
            };
            lidinha = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Lidinha",
                LastName = "Carvalho Oliveira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoOliveira,
                IsActive = false
            };
            lidinha.Spouse = miguel.Id;
            miguel.Spouse = lidinha.Id;

            andre = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Andre",
                LastName = "Carvalho Oliveira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoOliveira,
                FatherId = miguel.Id,
                MotherId = lidinha.Id
            };
            lidiane = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Lidiane",
                LastName = "Carvalho Oliveira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoOliveira,
                FatherId = miguel.Id,
                MotherId = lidinha.Id
            };
            arthur = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Arthur",
                LastName = "Carvalho Oliveira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoOliveira,
                MotherId = lidiane.Id
            };
            luizGustavo = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Luiz Gustavo",
                LastName = "Carvalho Oliveira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoOliveira,
                MotherId = lidiane.Id
            };
            marcusVinicius = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Marcus Vinicius",
                LastName = "Carvalho Oliveira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoOliveira,
                MotherId = lidiane.Id
            };
            isabela = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Isabela",
                LastName = "Carvalho Oliveira",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoOliveira,
                FatherId = andre.Id
            };
        }

        private static void CarvalhoMessias(House carvalhoMessias, Relative chicada, Relative mariaJovita, out Relative inha, out Relative mario, out Relative cacalo, out Relative lidia, out Relative rosane, out Relative joseEugenio, out Relative marcelo, out Relative marlene, out Relative icaro, out Relative lidiaMara, out Relative natasha, out Relative marcela, out Relative huncas, out Relative yuri, out Relative raissa)
        {
            inha = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Inha",
                LastName = "Carvalho Messias",
                BirthDate = new DateTime(1950, 1, 1),
                House = carvalhoMessias,
                FatherId = chicada.Id,
                MotherId = mariaJovita.Id,
                Gender = Gender.Female,
            };
            mario = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Mario",
                LastName = "Messias",
                BirthDate = new DateTime(1950, 1, 1),
                House = carvalhoMessias,
                IsActive = false
            };
            inha.Spouse = mario.Id;
            mario.Spouse = inha.Id;

            cacalo = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Cacalo",
                LastName = "Carvalho Messias",
                BirthDate = new DateTime(1970, 1, 1),
                House = carvalhoMessias,
                FatherId = mario.Id,
                MotherId = inha.Id
            };
            lidia = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Lidia",
                LastName = "Carvalho Messias",
                BirthDate = new DateTime(1970, 1, 1),
                House = carvalhoMessias,
                FatherId = mario.Id,
                MotherId = inha.Id
            };
            rosane = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Rosane",
                LastName = "Carvalho Monteiro",
                BirthDate = new DateTime(1970, 1, 1),
                House = carvalhoMessias,
                FatherId = mario.Id,
                MotherId = inha.Id
            };
            joseEugenio = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Jose Eugenio",
                LastName = "Monteiro",
                BirthDate = new DateTime(1970, 1, 1),
                House = carvalhoMessias,
                IsActive = false
            };
            rosane.Spouse = joseEugenio.Id;
            joseEugenio.Spouse = rosane.Id;

            marcelo = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Marcelo",
                LastName = "Carvalho Messias",
                BirthDate = new DateTime(1970, 1, 1),
                House = carvalhoMessias,
                FatherId = mario.Id,
                MotherId = inha.Id
            };
            marlene = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Marlene",
                LastName = "Brito",
                BirthDate = new DateTime(1970, 1, 1),
                House = carvalhoMessias,
                IsActive = false
            };
            marcelo.Spouse = marlene.Id;
            marlene.Spouse = marcelo.Id;

            icaro = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Icaro",
                LastName = "Brito de Carvalho Messias",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoMessias,
                FatherId = marcelo.Id,
                MotherId = marlene.Id
            };
            lidiaMara = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Lidia Mara",
                LastName = "Pereira Aguiar",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoMessias,
                IsActive = false
            };
            lidiaMara.Spouse = icaro.Id;
            icaro.Spouse = lidiaMara.Id;

            natasha = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Natasha",
                LastName = "Brito de Carvalho Messias",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoMessias,
                FatherId = marcelo.Id,
                MotherId = marlene.Id
            };
            marcela = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Marcela",
                LastName = "Brito de Carvalho Messias",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoMessias,
                FatherId = marcelo.Id,
                MotherId = marlene.Id
            };
            huncas = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Huncas",
                LastName = "Carvalho Monteiro",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoMessias,
                FatherId = joseEugenio.Id,
                MotherId = rosane.Id
            };
            yuri = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Yuri",
                LastName = "Carvalho Monteiro",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoMessias,
                FatherId = joseEugenio.Id,
                MotherId = rosane.Id
            };
            raissa = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Raissa",
                LastName = "Carvalho Monteiro",
                BirthDate = new DateTime(1990, 1, 1),
                House = carvalhoMessias,
                FatherId = joseEugenio.Id,
                MotherId = rosane.Id
            };
        }

        private static void GetChicada(House carvalho, out Relative chicada, out Relative mariaJovita)
        {
            chicada = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Chicada",
                LastName = "Carvalho",
                BirthDate = new DateTime(1930, 1, 1),
                House = carvalho,
                Patriarch = true,
                Gender = Gender.Male,
                FamilyId = carvalho.FamilyId,
                IsActive = false
            };
            mariaJovita = new Relative()
            {
                Id = Guid.NewGuid(),
                FirstName = "Maria Jovita",
                LastName = "Carvalho",
                BirthDate = new DateTime(1930, 1, 1),
                FamilyId = carvalho.FamilyId,
                House = carvalho,
                Matriarch = true,
                Gender = Gender.Female,
                IsActive = false
            };

            chicada.Spouse = mariaJovita.Id;
            mariaJovita.Spouse = chicada.Id;
        }

        private static void Final(Relative inha, Relative cacalo, Relative icaro, Relative lidiaMara, Relative natasha, Relative marcela, Relative lidinha, Relative arthur, Relative luizGustavo, Relative roseli, Relative jurandyr, Relative belisa, Relative eduardo, Relative manuela, Relative benicio, Relative tina, Relative renataAscencio, Relative rodrigo, Relative daniela, Relative adrianoMaretti, Relative julia, Relative duda, Relative guilherme, Relative drizinho, Relative henrique, Relative mayra, Relative adrianoDorna, Relative marina, Relative gustavo, Relative otavio)
        {
            mayra.SecretSanta = true;
            mayra.Phone = "35 98416-2072";
            mayra.LinkName = "https://www.4shared.com/s/frJ5GQ--qku";

            adrianoDorna.SecretSanta = true;
            adrianoDorna.Phone = "35 98416-2072";
            adrianoDorna.LinkName = "https://www.4shared.com/s/fnywX4ftege";

            gustavo.SecretSanta = true;
            gustavo.Phone = "35 98416-2072";
            gustavo.LinkName = "https://www.4shared.com/s/f2kzDKgEpku";

            otavio.SecretSanta = true;
            otavio.Phone = "35 98416-2072";
            otavio.LinkName = "https://www.4shared.com/s/f3Kt_lLg5ku";

            daniela.SecretSanta = true;
            daniela.Phone = "35 99132-1023";
            daniela.LinkName = "https://www.4shared.com/s/f817LopWzjq";

            drizinho.SecretSanta = true;
            drizinho.Phone = "35 98851-0305";
            drizinho.LinkName = "https://www.4shared.com/s/fYdAiX2Osku";

            henrique.SecretSanta = true;
            henrique.Phone = "35 99670-0003";
            henrique.LinkName = "https://www.4shared.com/s/fqsU817LJku";

            natasha.SecretSanta = true;
            natasha.Phone = "1 857 289-6282";
            natasha.LinkName = "https://www.4shared.com/s/fdSX8S8vXku";

            cacalo.SecretSanta = true;
            cacalo.Phone = "35 98865-8438";
            cacalo.LinkName = "https://www.4shared.com/s/fXDizycDmku";

            inha.SecretSanta = true;
            inha.Phone = "35 98865-8438";
            inha.LinkName = "https://www.4shared.com/s/fSbUBXRTIge";

            jurandyr.SecretSanta = true;
            jurandyr.Phone = "35 99958 7625";
            jurandyr.LinkName = "https://www.4shared.com/s/fbWn0UZZHku";

            roseli.SecretSanta = true;
            roseli.Phone = "35 99977-5328";
            roseli.LinkName = "https://www.4shared.com/s/fAINCk69ifa";

            manuela.SecretSanta = true;
            manuela.Phone = "13 998837-7952";
            manuela.LinkName = "https://www.4shared.com/s/f8P-w8FCLjq";

            marina.SecretSanta = true;
            marina.Phone = "35 99773-4377";
            marina.LinkName = "https://www.4shared.com/s/f-nlsftRxku";

            tina.SecretSanta = true;
            tina.Phone = "35 98896-6049";
            tina.LinkName = "https://www.4shared.com/s/fXRU487uLku";

            marcela.SecretSanta = true;
            marcela.Phone = "35 99107-8315";
            marcela.LinkName = "https://www.4shared.com/s/fjGOez6Nmku";

            benicio.SecretSanta = true;
            benicio.Phone = "19 997146-3545";
            benicio.LinkName = "https://www.4shared.com/s/fpAQC46Duge";

            belisa.SecretSanta = true;
            belisa.Phone = "19 997146-3545";
            belisa.LinkName = "https://www.4shared.com/s/fU4DfWhyIku";

            eduardo.SecretSanta = true;
            eduardo.Phone = "19 99904-7854";
            eduardo.LinkName = "https://www.4shared.com/s/ftPHpSOAXge";

            rodrigo.SecretSanta = true;
            rodrigo.Phone = "35 99977-3050";
            rodrigo.LinkName = "https://www.4shared.com/s/fr6ZHqnF6jq";

            renataAscencio.SecretSanta = true;
            renataAscencio.Phone = "35 99977-5279";
            renataAscencio.LinkName = "https://www.4shared.com/s/fOM_Ol8aPjq";

            julia.SecretSanta = true;
            julia.Phone = "35 99977-5279";
            julia.LinkName = "https://www.4shared.com/s/fKkAP0grtge";

            lidinha.SecretSanta = true;
            lidinha.Phone = "35 98869-6794";
            lidinha.LinkName = "https://www.4shared.com/s/fN8ToXBQIjq";

            arthur.SecretSanta = true;
            arthur.Phone = "35 98869-6794";
            arthur.LinkName = "https://www.4shared.com/s/fglD4K3uFge";

            luizGustavo.SecretSanta = true;
            luizGustavo.Phone = "35 98869-6794";
            luizGustavo.LinkName = "https://www.4shared.com/s/fieTlJRjFku";

            icaro.SecretSanta = true;
            icaro.Phone = "35 98436-9551";
            icaro.LinkName = "https://www.4shared.com/s/fpr9mL_PFjq";

            lidiaMara.SecretSanta = true;
            lidiaMara.Phone = "44 9173-3999";
            lidiaMara.LinkName = "https://www.4shared.com/s/fbV0LCnq9ku";

            duda.SecretSanta = true;
            duda.Phone = "35 99236-5971";
            duda.LinkName = "https://www.4shared.com/s/fYbXEIC-Uku";

            guilherme.SecretSanta = true;
            guilherme.Phone = "35 99112-8337";
            guilherme.LinkName = "https://www.4shared.com/s/fsSriT0OXfa";

            adrianoMaretti.SecretSanta = true;
            adrianoMaretti.Phone = "35 98805-3000";
            adrianoMaretti.LinkName = "https://www.4shared.com/s/fYB2J4TFejq";
        }

        



        #endregion
    }
}
