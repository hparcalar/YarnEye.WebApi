using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YarnEye.WebApi.Models
{
    public class YarnCheckResultModel
    {
        public int ResultId { get; set; }
        public string SerialNo { get; set; }
        public int? ProdLineId { get; set; }
        public int? AssignmentId { get; set; }
        public int? TestResult { get; set; }
        public decimal? ColorHue { get; set; }
        public decimal? ColorSaturation { get; set; }
        public decimal? ColorValue { get; set; }
        public decimal? DiffHue { get; set; }
        public decimal? DiffSaturation { get; set; }
        public decimal? DiffValue { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
