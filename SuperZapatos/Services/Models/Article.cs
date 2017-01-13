using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.ComponentModel;

namespace Services.Models
{
    public class article
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        [DisplayName("shelf")]
        public int total_in_shelf { get; set; }
        [DisplayName("vault")]
        public int total_in_vault { get; set; }
        [DisplayName("store")]
        public string store_name { get; set; }
    }

    public class Article 
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        [DisplayName("total in shelf")]
        public int total_in_shelf { get; set; }
        [DisplayName("total in vault")]
        public int total_in_vault { get; set; }
        public string Self
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
                    "api/articles/{0}", this.id);
            }
            set { }
        }
        public virtual Store Store { get; set; }
    }

}