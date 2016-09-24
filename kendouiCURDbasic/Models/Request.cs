﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kendouiCURDbasic.Models
{
    public class Request
    {
        public int take { get; set; }
        public int skip { get; set; }
        public int page { get; set; }
        public List<Sort> sort { get; set; }
    }

    public class Sort
    {
        public string field { get; set; }
        public string dir { get; set; }
    }
}