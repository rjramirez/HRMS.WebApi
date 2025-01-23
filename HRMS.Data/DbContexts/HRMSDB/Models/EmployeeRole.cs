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
        public int EmployeeRoleId { get; set; }
        public int EmployeeId { get; set; }
        public short RoleId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdatedBy { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("EmployeeRoles")]
        public virtual Role Role { get; set; }
    }
}
