using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContexts.HRMSDB.Models
{
    [Keyless]
    [Table("Employee")]
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string EmployeeEmail { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string LastName { get; set; }
        public int? SupervisorId { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        public int? UpdateBy { get; set; }
    }
}
