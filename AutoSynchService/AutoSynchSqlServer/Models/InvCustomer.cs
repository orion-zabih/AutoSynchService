using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvCustomer
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? FatherName { get; set; }
        public string? Company { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public decimal DiscPercent { get; set; }
        public decimal AcctBalance { get; set; }
        public decimal AcctMaxBalance { get; set; }
        public bool TaxExempt { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Contact1 { get; set; }
        public string? Contact2 { get; set; }
        public string? Terms { get; set; }
        public bool? IsActive { get; set; }
        public string? ImageName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int BranchId { get; set; }
        public int RouteId { get; set; }
        public bool IsDeleted { get; set; }
        public string? Ntnno { get; set; }
        public string? Gstno { get; set; }
        public string? Cnicno { get; set; }
    }
}
