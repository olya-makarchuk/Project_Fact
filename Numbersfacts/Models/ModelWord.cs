using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Numbersfacts.Models
{
    [Serializable]
    public class ModelWord
    {
        [Required(ErrorMessage = "Введіть слово")]
        public string WordInput { get; set; }
        public List<string> Texts { get; set; }
    }
}
