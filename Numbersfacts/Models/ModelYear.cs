using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Numbersfacts.Models
{
    [Serializable]
    public class ModelYear
    {
        [Required(ErrorMessage ="Введіть рік")]
        [Range(1,2023, ErrorMessage ="Введіть значення у межах від 0 до 2023")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Введіть коректне значення")]
        public string YearforEvents { get; set; }
        public List<string> Events { get; set; }
        public List<ModelYear> CheckIt { get; set; }
        public string Name { get; set; }
        public bool IsCheck { get; set; }
    }
}
