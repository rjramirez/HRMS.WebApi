﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContexts.HRMSDB.Models
{
    [Table("ErrorLog")]
    public partial class ErrorLog
    {
        [Key]
        public int ErrorLogId { get; set; }
        [Required]
        [StringLength(4000)]
        public string Message { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        [Required]
        public string StackTrace { get; set; }
        [Required]
        [StringLength(500)]
        public string Path { get; set; }
        [Required]
        [StringLength(100)]
        public string StackTraceId { get; set; }
        [Required]
        [StringLength(100)]
        public string Source { get; set; }
        [Required]
        [StringLength(100)]
        public string UserIdentity { get; set; }
        [Required]
        [StringLength(50)]
        public string BuildVersion { get; set; }
    }
}
