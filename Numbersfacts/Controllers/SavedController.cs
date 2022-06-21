using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Numbersfacts.Clients;
using Numbersfacts.DAL.Interfaces;
using Numbersfacts.Domain;
using Numbersfacts.Models;
using Numbersfacts.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Numbersfacts.Controllers
{
    public class SavedController : Controller
    {
        private readonly IBaseRespository<Fact> _numberRepository;
        public SavedController(IBaseRespository<Fact> numberRepository)
        {
            _numberRepository = numberRepository;
        }

        static ModelSaved modelsaved = new ModelSaved();


        [HttpGet]
        public async Task<IActionResult> Save()
        {

            // list.Add(modelsaved.YearInput.Split(' '));
            if (modelsaved.YearInput != null)
            {
                var list = new List<string>();
                var mas = new string[2];
                mas = modelsaved.YearInput.Split(' ');
                ModelSaved model = new ModelSaved();
                var modelfacts = await Saved_facts();
                model.Events = new List<Fact>();

                if (mas.Length == 1)
                {
                    model.YearInput = mas[0];
                    foreach (Fact fact in modelfacts)
                    {
                        fact.Year = fact.Year.Replace(" ", string.Empty);
                        if (fact.Year == model.YearInput)///
                        {
                            model.Events.Add(fact);
                        }
                    }
                }

                if(mas.Length == 2)
                {
                    int start = Convert.ToInt32(mas[0]);
                    int end = Convert.ToInt32(mas[1]);
                    model.YearInput = $"{start} - {end}";
                    for (int i = start; i<end+1; i++)
                    {
                        foreach(Fact fact in modelfacts)
                        {
                            fact.Year = fact.Year.Replace(" ", string.Empty);
                            if(Convert.ToInt32(fact.Year) == i)
                            {
                                model.Events.Add(fact);
                            }
                        }
                    }
                }
                
                return View(model);
            }
            modelsaved.YearInput = null;
            return View(modelsaved);

        }

        public async Task<IActionResult> Save(ModelSaved model)
        {

            if (ModelState.IsValid)
            {
                Regex reg = new Regex(@"[0-9]+");
                MatchCollection matchestest = reg.Matches(Convert.ToString(model.YearInput));
                if (matchestest.Count == 1)
                {
                    int a = Convert.ToInt32(matchestest[0].Value);

                    if (a > 0 && a < 2023)
                    {
                        var modelfacts = await Saved_facts();
                        model.Events = new List<Fact>();
                        foreach (Fact fact in modelfacts)
                        {
                            fact.Year = fact.Year.Replace(" ", string.Empty);
                            if (fact.Year == matchestest[0].Value)
                            {
                                model.Events.Add(fact);
                            }
                        }
                        model.YearInput = matchestest[0].Value;
                        modelsaved.YearInput = model.YearInput;
                        return View(model);
                    }
                }

                if (matchestest.Count == 2)
                {
                    int start = Convert.ToInt32(matchestest[0].Value);
                    int end = Convert.ToInt32(matchestest[1].Value);
                    int rizn = end - start;
                    if (start > 0 && start < 2023 && start != end && end > 0 && end < 2023 && start < end && rizn < 11)
                    {
                        model.Events = new List<Fact>();
                        for (int i = start; i < end + 1; i++)
                        {
                            var modelfacts = await Saved_facts();
                            for (int j = 0; j < modelfacts.Count; j++)
                            {
                                modelfacts[j].Year = modelfacts[j].Year.Replace(" ", string.Empty);
                                if (modelfacts[j].Year == Convert.ToString(i))
                                {
                                    model.Events.Add(modelfacts[j]);
                                }
                            }
                        }
                        model.YearInput = $"{start} - {end}";
                        modelsaved.YearInput = $"{start} {end}";
                        return View(model);
                    }
                }


            }
            model.YearInput = null;
            ModelState.AddModelError(nameof(model.YearInput), $"Невірне значення дати | Різниця між першою та другою датою не повинна перевищувати 10 років");
            return View(model);
        }

        [HttpGet("savedfacts")]
        public async Task<List<Fact>> Saved_facts()
        {
            var modelfacts = await _numberRepository.Select();
            return modelfacts;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, string year)
        {
            //modelsaved.YearInput = year;
            Fact model = new Fact();
            model.Id = id;
            await Delete_events(model);
            //return Response.Redirect(Request.RawUrl);
            return Redirect("/Saved/Save");
        }


        [HttpDelete("delete_events")]
        public async Task<Fact> Delete_events(Fact model)
        {
            await _numberRepository.Delete(model);
            return model;
        }

        [HttpPut("update_events")]
        public async Task<Fact> Update_events(Fact model)
        {
            await _numberRepository.Update(model);
            return model;
        }


    }
}
