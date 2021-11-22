using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YarnEye.WebApi.Models
{
    public class ActiveAssignerModel{
        public int ActiveAssignerId { get; set; }
        public string IpAddr { get; set; }
        public string SelectedLines { get; set; }
        public int AssignerStatus { get; set; }
    }
}