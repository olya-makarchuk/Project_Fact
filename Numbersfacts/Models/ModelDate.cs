using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Numbersfacts.Models
{
    [Serializable]
    public class ModelDate 
    {
        [RegularExpression(@"[\d]{2}\/[\d]{2}", ErrorMessage ="Невірний формат вводу")]
        [Required(ErrorMessage = "Введіть дату")]
        public string Date { get; set; }
        public List<string> Births { get; set; }
        public string Name { get; set; }
    }
}
