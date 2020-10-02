using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Notification
    {
        public string app_id { get; set; }
        public Language contents { get; set; }
        public Language headings { get; set; }
        public string url { get; set; }
        //public string[] include_player_ids { get; set; }
        public string[] included_segments { get; set; }
    }

    public class Language
    {
        public string en { get; set; }
    }
}
