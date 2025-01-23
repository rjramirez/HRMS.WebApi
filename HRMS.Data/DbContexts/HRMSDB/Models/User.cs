using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContexts.HRMSDB.Models
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public int UserId { get; set; }
        public short UserRoleId { get; set; }
        public int EmployeeId { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }

        [ForeignKey(nameof(UserRoleId))]
        [InverseProperty("Users")]
        public virtual UserRole UserRole { get; set; }
    }
}
