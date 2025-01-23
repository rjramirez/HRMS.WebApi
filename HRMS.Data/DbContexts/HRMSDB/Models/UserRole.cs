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
        [StringLength(50)]
        public string UserRoleName { get; set; }
        [StringLength(100)]
        public string UserDescription { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string CreatedBy { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [InverseProperty(nameof(User.UserRole))]
        public virtual ICollection<User> Users { get; set; }
    }
}
