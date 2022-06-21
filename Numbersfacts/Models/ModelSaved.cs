using Numbersfacts.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Numbersfacts.Models
{
    [Serializable]
    public class ModelSaved
    {
        [Required(ErrorMessage = "Введіть рік")]

        // [RegularExpression(@"[0-9]+", ErrorMessage = "Введіть коректне значення")]
        //[Range(1, 2023, ErrorMessage = "Введіть значення у межах від 0 до 2023")]
        public string YearInput { get; set; }
        public List<Fact> Events { get; set; }
    }
}
