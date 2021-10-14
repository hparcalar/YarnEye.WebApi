using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YarnEye.WebApi.Models
{
    public class ProdLineModel
    {
        public int ProdLineId { get; set; }
        public string ProdLineCode { get; set; }
        public string ProdLineName { get; set; }
        public int? OrderNo { get; set; }
        public int? AssignmentId { get; set; }
    }
}
