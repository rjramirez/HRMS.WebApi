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
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("EmployeeRoles")]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty("EmployeeRoles")]
        public virtual Role Role { get; set; }
    }
}
