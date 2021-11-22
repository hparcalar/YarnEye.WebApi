using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YarnEye.WebApi.Models
{
    public class ActiveTesterModel{
        public int ActiveTesterId {get;set;}
        public string IpAddr { get; set; }
        public int ProdLineId { get; set; }
        public int TesterStatus { get; set; }
    }
}