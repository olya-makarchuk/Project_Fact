using System.Collections.Generic;

namespace Numbersfacts.Models.ApiModels
{
    public class Birth
    {
        public string text { get; set; }
        public int year { get; set; }
    }

    public class Event
    {
        public string text { get; set; }
        public int year { get; set; }
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
    public class Holiday
    {
        public string text { get; set; }
    }

    public class ModelOnThisDay
    {
        public List<Birth> births { get; set; }
        public List<Event> events { get; set; }
        public List<Holiday> holidays { get; set; }
    }



}
