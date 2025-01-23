using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContexts.HRMSDB.Models
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            EmployeeRoles = new HashSet<EmployeeRole>();
        }

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
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [InverseProperty(nameof(EmployeeRole.Role))]
        public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; }
    }
}
