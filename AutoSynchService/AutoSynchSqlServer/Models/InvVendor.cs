using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class InvVendor
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public string? Company { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public int VendorTaxId { get; set; }
        public string? VendordTerms { get; set; }
        public decimal Commission { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public decimal? MinimumOrder { get; set; }
        public string? DefaultBillableDepartment { get; set; }
        public int PoDeliveryMethod { get; set; }
        public bool? IsActive { get; set; }
        public int BranchId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Mobile { get; set; }
        public int LedgerAccountId { get; set; }
        public bool IsDeleted { get; set; }
        public string? SaleTaxNumber { get; set; }
        public string? NtnNo { get; set; }
        public string? TaxType { get; set; }
        public string? Cnicno { get; set; }
    }
}
