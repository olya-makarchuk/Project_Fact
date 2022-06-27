using System.Collections.Generic;

namespace Numbersfacts.Models.ApiModels
{

    public class Event
    {
        public string text { get; set; }
        public string year { get; set; }

        public List<Page> pages { get; set; }
    }
    public class Page
    {
        public Originalimage originalimage { get; set; }
    }
    public class Originalimage
    {
        public string source { get; set; }
    }

    public class ModelOnThisDay
    {
        public List<Event> events { get; set; }
    }


}
