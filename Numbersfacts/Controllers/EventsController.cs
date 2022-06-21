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
    public class EventsController : Controller
    {
        private readonly IBaseRespository<Fact> _numberRepository;
        private readonly DateYearClient _dateYearClient;

        public EventsController(DateYearClient dateYearClient, IBaseRespository<Fact> numberRepository)
        {
            _numberRepository = numberRepository;
            _dateYearClient = dateYearClient;
        }

        public ModelYear modelYear = new ModelYear();
        public ModelDate modelDate = new ModelDate();

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var events = new ModelOnThisDay();
            events = await OnDay();

            return View(events);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        public async Task<IActionResult> CreateYear(ModelYear model)
        {
            foreach (var item in model.CheckIt)
            {
                if (item.IsCheck == true)
                {
                    var number = new Fact();
                    number.Text = item.Name;
                    number.Year = model.YearforEvents;
                    await Year(number);
                }
            }

            return Redirect("/Events/EventsYear");

        }

        [HttpGet]
        public IActionResult EventsYear()
        {
            modelYear.CheckIt = new List<ModelYear>();
            return View(modelYear);
        }
        public static int index;

        public async Task<IActionResult> EventsYear(ModelYear model)
        {
            if (ModelState.IsValid)
            {
                Regex reg = new Regex(@"[0-9]+");
                MatchCollection matchestest = reg.Matches(Convert.ToString(model.YearforEvents));
                model.CheckIt = new List<ModelYear>();
                if (matchestest.Count == 1)
                {
                    int a = Convert.ToInt32(matchestest[0].Value);

                    if (a > 0 && a < 2023)
                    {
                        model.YearforEvents = matchestest[0].Value;
                        await Events_year(model);
                        model.CheckIt = new List<ModelYear>();
                        foreach (var it in model.Events)
                        {
                            model.CheckIt.Add(new ModelYear() { Name = it, IsCheck = false });
                        }
                        return View(model);
                    }
                }

            }
            ModelState.AddModelError(nameof(model.YearforEvents), $"Невірне значення дати | Різниця між першою та другою датою не повинна перевищувати 5років");
            return View(model);
        }

        [HttpGet]
        public IActionResult BirthsDate()
        {
            return View(modelDate);
        }

        public async Task<IActionResult> BirthsDate(ModelDate model)
        {
            if (ModelState.IsValid)
            {
                DateTime date;
                Regex regex = new Regex(@"[\d]+");
                MatchCollection matches = regex.Matches(model.Date);
                bool parse = DateTime.TryParse($"{matches[0]}.{matches[1]}.2020", out date);

                if (parse == true)
                {
                    model.Name = $"{matches[1]}/{matches[0]}";
                    await Births_date(model);
                    return View(model);
                }
                model.Births = null;
                ModelState.AddModelError(nameof(model.Date), "Невірне значення дати");

                return View(model);
            }
            else
            {
                return View(model);
            }
        }


        [HttpPost("eventsyear")]
        public async Task<ModelYear> Events_year(ModelYear model)
        {
            model.Events = await _dateYearClient.YearEvent(model.YearforEvents);
            return model;
        }

        [HttpPost("createyear")]
        public async Task<Fact> Year(Fact number)
        {
            await _numberRepository.Create(number);

            return number;
        }

        [HttpPost("birthsdate")]
        public async Task<ModelDate> Births_date(ModelDate model)
        {
            model.Births = await _dateYearClient.DateBirths(model.Name);
            return model;
        }

        //Event modelevent = new Event();
        [HttpGet("onthisday")]
        public async Task<ModelOnThisDay> OnDay()
        {
            DateTime now = DateTime.Now;
            string date = $"{now.Month}/{now.Day}";
            var modelfacts = await _dateYearClient.OnThisDay(date);
            return modelfacts;
        }

    }
}
