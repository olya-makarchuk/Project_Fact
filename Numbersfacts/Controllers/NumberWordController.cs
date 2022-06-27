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
    public class NumberWordController : Controller
    {
        private readonly NumberClient _numberClient;
        private readonly TranslateClient _translateClient;
        private readonly WordClient _wordClient;

        public NumberWordController(NumberClient numberClient, TranslateClient translateClient, WordClient wordClient)
        {
            _numberClient = numberClient;
            _translateClient = translateClient;
            _wordClient = wordClient;
        }

        public ModelNumberFacts modelNumber = new ModelNumberFacts();

        [HttpGet]
        public IActionResult NumberFacts()
        {
            modelNumber.Facts = new List<string>();
            return View(modelNumber);
        }

        [HttpPost]
        public async Task<IActionResult> NumberFacts(ModelNumberFacts model)
        {
            model.Facts = new List<string>();
            if (ModelState.IsValid)
            {
                var res = await Number(model);
            }
            return View(model);
        }

        
        [HttpPost("numberfacts")]
        public async Task<ModelNumberFacts> Number(ModelNumberFacts model)
        {
            model.Facts = new List<string>();
            model.Facts.Add(await _numberClient.GetInfoMath(model.Number));
            model.Facts.Add(await _numberClient.GetInfoGeneral(model.Number));
            model.Facts = await Translate(model.Facts);
            return model;
        }

        [HttpGet("definition")]
        public async Task<List<ModelDefinition>> WordDefinitions(string word, string wordinput)
        {
            var list = new List<ModelDefinition>();
            list = await _wordClient.Definitions(word, wordinput);

            return list;
        }

        [HttpPost("translate")]
        public async Task<List<string>> Translate(List<string> text)
        {
            var list = new List<string>();
            list = await _translateClient.TextTransl(text);
            return list;
        }

        [HttpGet("translate")]
        public async Task<string> Translate(string text)
        {
            var transl = await _translateClient.TextTransl(text);
            return transl;
        }

        [HttpGet]
        public IActionResult Word()
        {
            var model = new ModelWord();
            model.Texts = new List<string>();
            return View(model);
        }


        public async Task<IActionResult> Word(ModelWord model)
        {
            string wordEN = await _translateClient.TextTransl(model.WordInput);
            model.Texts = new List<string>();
            var def = new List<ModelDefinition>();
            def = await WordDefinitions(wordEN, model.WordInput);
            foreach (var item in def)
            {
                model.Texts.Add(item.text);
            }
            model.Texts = await Translate(model.Texts);
            return View(model);
        }

    }
}
