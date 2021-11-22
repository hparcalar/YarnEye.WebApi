using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YarnEye.WebApi.Context
{
    public class YarnEyeContext : DbContext
    {
        public DbSet<ProdLine> ProdLine { get; set; }
        public DbSet<ColorAssignment> ColorAssignment { get; set; }
        public DbSet<YarnCheckResult> YarnCheckResult { get; set; }
        public DbSet<ActiveAssigner> ActiveAssigner { get; set; }
        public DbSet<ActiveTester> ActiveTester { get; set; }

        public YarnEyeContext() : base() { }
        public YarnEyeContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.UseSerialColumns();

        public new void Dispose()
        {
            base.Dispose();
        }
    }

    public class ProdLine
    {
        [Key]
        public int ProdLineId { get; set; }
        public string ProdLineCode { get; set; }
        public string ProdLineName { get; set; }
        public int? OrderNo { get; set; }
        public int? AssignmentId { get; set; }
        public ColorAssignment ColorAssignment { get; set; }

    }

    public class ColorAssignment
    {
        [Key]
        public int AssignmentId { get; set; }
        public string AssignmentCode { get; set; }
        public byte[] SampleImage { get; set; }
        public decimal? SetHue { get; set; }
        public decimal? SetSaturation { get; set; }
        public decimal? SetValue { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class ActiveAssigner{
        [Key]
        public int ActiveAssignerId { get; set; }
        public string IpAddr { get; set; }
        public string SelectedLines { get; set; }
        public int AssignerStatus { get; set; }
    }

    public class ActiveTester{
        [Key]
        public int ActiveTesterId {get;set;}
        public string IpAddr { get; set; }
        public int ProdLineId { get; set; }
        public int TesterStatus { get; set; }
    }

    public class YarnCheckResult
    {
        [Key]
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
        public ProdLine ProdLine { get; set; }
        public ColorAssignment ColorAssignment { get; set; }
    }
}
