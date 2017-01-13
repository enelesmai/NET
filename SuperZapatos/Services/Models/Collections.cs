using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Services.Models
{
    public static class ServiceError {

        public static Dictionary<int, string> Response = new Dictionary<int, string>()
            {
	            {400, "Bad request"},
	            {401, "Not authorized"},
	            {404, "Record not found"},
	            {500, "Server Error"}
	        };

    }

    [DataContract]
    public class ApiCollection
    {
        [DataMember]
        public bool success { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int total_elements { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int error_code { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string error_msg { get; set; }
    }

    [DataContract]
    public class StoreApiCollection : ApiCollection {
        [DataMember(EmitDefaultValue = false)]
        public Store store { get; set; }
    }

    [DataContract]
    public class StoresApiCollection : ApiCollection
    {
        [DataMember(EmitDefaultValue = false)]
        public Store[] stores { get; set; }
    }

    [DataContract]
    public class ArticleApiCollection : ApiCollection
    {
        [DataMember(EmitDefaultValue = false)]
        public Article article { get; set; }
    }

    [DataContract]
    public class ArticlesApiCollection : ApiCollection
    {
        [DataMember(EmitDefaultValue = false)]
        public article[] articles { get; set; }
    }
}