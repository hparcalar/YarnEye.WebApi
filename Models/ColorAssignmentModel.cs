using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YarnEye.WebApi.Models
{
    public class ColorAssignmentModel
    {
        public int AssignmentId { get; set; }
        public string AssignmentCode { get; set; }
        public byte[] SampleImage { get; set; }
        public decimal? SetHue { get; set; }
        public decimal? SetSaturation { get; set; }
        public decimal? SetValue { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
