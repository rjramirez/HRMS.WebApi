using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContexts.HRMSDB.Models
{
    [Table("EmployeeRole")]
    public partial class EmployeeRole
    {
        [Key]
        public short RoleId { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string RoleName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string RoleDescription { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
