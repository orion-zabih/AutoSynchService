using System;
using System.Collections.Generic;

namespace AutoSynchService.Models
{
    public partial class InsStudentFeeMapping
    {
        public int Id { get; set; }
        public int AdmissionId { get; set; }
        public int FeeId { get; set; }
        public decimal Value { get; set; }
        public decimal TermValue { get; set; }
        public decimal AdditionalValue { get; set; }
        public decimal NetValue { get; set; }
        public bool? IsActive { get; set; }
    }
}
