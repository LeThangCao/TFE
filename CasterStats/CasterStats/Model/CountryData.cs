﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CasterStats.Model
{
    public class CountryData
    {
        public string Country { get; set; }
        public int Count { get; set; }
        public string CountryCode { get; set; }
        public ImageSource Flag { get; set; }
    
    }
}
