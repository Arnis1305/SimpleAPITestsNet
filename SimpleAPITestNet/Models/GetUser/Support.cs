using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAPITestNet.Models.GetUser
{
    public class Support
    {
        public Support() { }
        public string url { get; set; }
        public string text { get; set; }

        public Support(string url, string text)
        {
            this.url = url;
            this.text = text;
        }

        public override bool Equals(object? obj)
        {
            return obj is Support support &&
                   url == support.url &&
                   text == support.text;
        }
    }
}
