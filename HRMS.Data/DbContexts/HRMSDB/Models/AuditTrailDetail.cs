﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContexts.HRMSDB.Models
{
    [Table("AuditTrailDetail")]
    public partial class AuditTrailDetail
    {
        [Key]
        public long AuditTrailDetailId { get; set; }
        public long AuditTrailId { get; set; }
        [Required]
        public string EntityId { get; set; }
        [Required]
        [StringLength(50)]
        public string TableName { get; set; }
        [Required]
        [StringLength(50)]
        public string EntityField { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        [StringLength(20)]
        public string Action { get; set; }

        [ForeignKey(nameof(AuditTrailId))]
        [InverseProperty("AuditTrailDetails")]
        public virtual AuditTrail AuditTrail { get; set; }
    }
}
