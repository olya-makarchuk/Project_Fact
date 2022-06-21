using System.Collections.Generic;

namespace Numbersfacts.Models.ApiModels
{
    public class Data
    {
        public List<Language> languages { get; set; }
    }

    public class Language
    {
        public string language { get; set; }
    }

    public class ModelLanguage
    {
        public Data data { get; set; }
    }
}
