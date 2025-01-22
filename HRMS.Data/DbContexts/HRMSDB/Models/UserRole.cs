using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContexts.HRMSDB.Models
{
    [Table("UserRole")]
    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public short UserRoleId { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string UserRoleName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string UserRoleDescription { get; set; }
        public bool Active { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty(nameof(User.UserRole))]
        public virtual ICollection<User> Users { get; set; }
    }
}
