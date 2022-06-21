using System.Collections.Generic;

namespace Numbersfacts.Models.ApiModels
{
    public class Births
    {
        public IList<ModelDateBirths> births { get; set; }
    }

    public class ModelDateBirths
    {
        public string text { get; set; }
        public int year { get; set; }

    }
}
