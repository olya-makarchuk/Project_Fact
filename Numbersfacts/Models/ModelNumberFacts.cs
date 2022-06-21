using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Numbersfacts.Models
{
    [Serializable]
    public class ModelNumberFacts
    {
        [Required(ErrorMessage ="Введіть число")]
        [RegularExpression(@"[0-9]+", ErrorMessage ="Введіть коректне значення")]
        public string Number { get; set; }
        public List<string> Facts { get; set; }
            
    }
}
