using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo
{
    public class UserRole
    {
        [Column("UserRoleId")]
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [StringLength(10), Column(TypeName = "VARCHAR")]
        public string RoleCode { get; set; }
        public Role Role { get; set; }

        [Column(TypeName = "DATE")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime? EndDate { get; set; }

    }
}