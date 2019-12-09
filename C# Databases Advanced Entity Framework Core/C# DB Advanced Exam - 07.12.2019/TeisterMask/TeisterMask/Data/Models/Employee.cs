using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeisterMask.Data.Models
{
    public class Employee
    {
        public Employee()
        {
            this.EmployeesTasks = new HashSet<EmployeeTask>();
        }
        public int Id { get; set; }
        [StringLength(40,MinimumLength = 3),Required]
        [RegularExpression(@"^[A-z0-9]+$")]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$"),Required]
        public string Phone { get; set; }

        public ICollection<EmployeeTask>EmployeesTasks { get; set; }
    }
}
