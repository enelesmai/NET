using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Services.Models
{
    public class Store
    {
        public Store() {
            Articles = new HashSet<Article>();
        }

        public int id {get;set;}
        public string name { get; set; }
        public string address { get; set; }
        public string Self
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
                    "api/stores/{0}", this.id);
            }
            set { }
        }

        public ICollection<Article> Articles { get; set; }
    }
}