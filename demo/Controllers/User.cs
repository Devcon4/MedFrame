using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo
{
    [Table("Users")]
    public class User
    {
        [Column("PersonId")]
        public int Id { get; set; }


        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string MiddleName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string DomainUserName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public List<UserRole> UserRoles { get; set; }


    }
}