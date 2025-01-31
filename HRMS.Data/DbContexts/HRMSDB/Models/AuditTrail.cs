﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContexts.HRMSDB.Models
{
    [Table("AuditTrail")]
    public partial class AuditTrail
    {
        public AuditTrail()
        {
            AuditTrailDetails = new HashSet<AuditTrailDetail>();
        }

        [Key]
        public long AuditTrailId { get; set; }
        [Required]
        [StringLength(200)]
        public string TransactionBy { get; set; }
        public DateTime TransactionDate { get; set; }

        [InverseProperty(nameof(AuditTrailDetail.AuditTrail))]
        public virtual ICollection<AuditTrailDetail> AuditTrailDetails { get; set; }
    }
}
