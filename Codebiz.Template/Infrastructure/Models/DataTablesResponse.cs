using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class DataTablesResponse
    {
        public IDictionary<string, object> additionalParameters { get; set; }
        public object data { get; set; }
        public int draw { get; set; }
        public string error { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
    }
}
