﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PUL.GS.Models
{
    public class Player
    {
        public string app_id { get; set; }
        public string identifier { get; set; }
        public string language { get; set; }
        public string timezone { get; set; }
        public string game_version { get; set; }
        public string device_os { get; set; }
        public int device_type { get; set; }
        public string device_model { get; set; }
        public string tags { get; set; }
    }
}
