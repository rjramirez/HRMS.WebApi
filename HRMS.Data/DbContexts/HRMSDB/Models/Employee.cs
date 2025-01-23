using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DBContexts.HRMSDB.Models
{
    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int EmployeeNumber { get; set; }
        [Required]
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
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdateBy { get; set; }
    }
}
