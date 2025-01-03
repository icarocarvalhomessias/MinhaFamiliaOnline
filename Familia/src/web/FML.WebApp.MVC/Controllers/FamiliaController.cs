using Familia.WebApp.MVC.Controllers;
using FML.Core.Data;
using FML.WebApp.MVC.Services.Interface;
using FML.WebApp.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace FML.WebApp.MVC.Controllers
{
    [Authorize]
    public class FamiliaController : MainController
    {
        private readonly IFamiliaService _familiaService;

        public FamiliaController(IFamiliaService familiaService)
        {
            _familiaService = familiaService;
        }

        /// <summary>
        /// Displays the index page.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return await Table();
        }

        /// <summary>
        /// Displays the family tree view.
        /// </summary>
        [HttpGet]
        public IActionResult FamilyTree()
        {
            return View("FamilyTree");
        }

        /// <summary>
        /// Displays the table view with relatives.
        /// </summary>
        public async Task<IActionResult> Table()
        {
            var relatives = await _familiaService.GetRelatives();
            return View("Table", relatives);
        }

        /// <summary>
        /// Gets the family tree nodes as JSON.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetFamilyTreeNodes()
        {
            var relatives = await _familiaService.GetRelatives();

            var nodes = relatives.AsParallel().Select(relative => new
            {
                id = relative.Id,
                pids = relative.Spouse.HasValue ? new List<Guid> { relative.Spouse.Value } : new List<Guid>(),
                name = $"{relative.FirstName} {relative.LastName}",
                gender = relative.Gender.ToString().ToLower(),
                mid = relative.MotherId,
                fid = relative.FatherId,
                img = relative.FotoPerfil ?? "https://cdn.balkan.app/shared/4.jpg" // Replace with actual image path if available
            }).ToList();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                MaxDepth = 64,
                Converters = { new JsonStringEnumConverter() }
            };

            return Json(nodes, options);
        }


        /// <summary>
        /// Displays the edit view for a relative.
        /// </summary>
        public async Task<IActionResult> ParenteEdit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var relative = await _familiaService.GetRelativeById(id);
            if (relative == null)
            {
                return NotFound();
            }

            ViewBag.Relative = relative;
            await PopulateDropDownLists();

            return View("Create", relative);
        }



        /// <summary>
        /// Edits a relative.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(Relative model, IFormFile FotoFile)
        {
            if (!ModelState.IsValid)
            {
                LogModelErrors();
                await PopulateDropDownLists();
                return View("Create", model);
            }

            if (FotoFile is null)
            {
                await _familiaService.UpdateRelative(model, null, null);
                return RedirectToAction("Index");
            }

            await _familiaService.UpdateRelative(model, FotoFile.OpenReadStream(), FotoFile.FileName);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Displays the create view for a new relative.
        /// </summary>
        public async Task<IActionResult> Create()
        {
            var relatives = await _familiaService.GetRelatives();
            var homens = relatives.Where(x => x.Gender == Gender.Male).ToList();
            var mulheres = relatives.Where(x => x.Gender == Gender.Female).ToList();

            ViewBag.Homens = new SelectList(homens, "Id", "FullName");
            ViewBag.Mulheres = new SelectList(mulheres, "Id", "FullName");

            await PopulateDropDownLists();

            var relative = new Relative();

            return View("Create", relative);
        }

        /// <summary>
        /// Creates a new relative.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(Relative relative)
        {
            if (ModelState.IsValid)
            {
                await _familiaService.AddRelative(relative);
                return RedirectToAction(nameof(Index), new { familyId = relative.FamilyId });
            }

            await PopulateDropDownLists();
            return View(relative);
        }

        /// <summary>
        /// Removes a relative.
        /// </summary>
        public async Task<IActionResult> RemoveRelative(Relative relative)
        {
            await _familiaService.RemoveRelative(relative.Id);
            return RedirectToAction("Index");
        }



        /// <summary>
        /// Organizes the family tree.
        /// </summary>
        private FamilyTreeViewModel OrganizeFamilyTree(List<Relative> relatives)
        {
            var TESTE = relatives.Where(x => x.Id != Guid.Parse("1a9cedd7-4493-4c7b-c81c-08dd0d7371ad")
            && x.Id != Guid.Parse("9c6a8126-180b-40b2-65bf-08dd0d73fbc5")).ToList();

            var root = new FamilyTreeViewModel
            {
                Pessoa1 = TESTE.FirstOrDefault(r => r.Patriarch),
                Pessoa2 = TESTE.FirstOrDefault(r => r.Matriarch)
            };

            root.Id = GenerateId(root);

            AddChildren(root, TESTE);

            return root;
        }

        /// <summary>
        /// Adds children to the family tree node.
        /// </summary>
        private void AddChildren(FamilyTreeViewModel parent, List<Relative> relatives)
        {
            if (parent.Pessoa1 is null || parent.Pessoa2 is null)
            {
                return;
            }

            var children = relatives.Where(r =>
                (r.MotherId.HasValue && (r.MotherId == parent.Pessoa1.Id || r.MotherId == parent.Pessoa2.Id)) ||
                (r.FatherId.HasValue && (r.FatherId == parent.Pessoa1.Id || r.FatherId == parent.Pessoa2.Id))
            ).Distinct().ToList();

            foreach (var child in children)
            {
                var childNode = new FamilyTreeViewModel
                {
                    Pessoa1 = child,
                    Pessoa2 = relatives.FirstOrDefault(r => r.Id == child.Spouse)
                };

                childNode.Id = GenerateId(childNode);

                parent.Filhos.Add(childNode);
                AddChildren(childNode, relatives);
            }
        }

        /// <summary>
        /// Generates an ID for the family tree node.
        /// </summary>
        private string GenerateId(FamilyTreeViewModel familyTreeViewModel)
        {
            var id1 = familyTreeViewModel.Pessoa1?.Id.ToString() ?? string.Empty;
            var id2 = familyTreeViewModel.Pessoa2?.Id.ToString() ?? string.Empty;
            return id1 + id2;
        }

        /// <summary>
        /// Populates the dropdown lists for the view.
        /// </summary>
        private async Task PopulateDropDownLists()
        {
            var family = await _familiaService.GetRelatives();
            var homens = family.Where(x => x.Gender == Gender.Male).ToList();
            var mulheres = family.Where(x => x.Gender == Gender.Female).ToList();

            ViewBag.Homens = new SelectList(homens, "Id", "FirstName");
            ViewBag.Mulheres = new SelectList(mulheres, "Id", "FirstName");

            var familias = await _familiaService.GetFamilies();
            var houses = await _familiaService.GetHousesByFamilyId(familias.First().Id);

            ViewBag.Familias = new SelectList(familias, "Id", "Name");
            ViewBag.Casas = new SelectList(houses, "Id", "Name");
            ViewBag.Spouses = new SelectList(family, "Id", "FirstName");
        }

        /// <summary>
        /// Logs model errors to the console.
        /// </summary>
        private void LogModelErrors()
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Property: {state.Key} Error: {error.ErrorMessage}");
                }
            }
        }
    }
}
