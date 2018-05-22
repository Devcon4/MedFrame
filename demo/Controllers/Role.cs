using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace demo
{
    [Table("Role")]
	public class Role
	{
		[Key, StringLength(10), Column(TypeName = "VARCHAR")]
		public string Code { get; set; }

		[StringLength(50), Column(TypeName = "VARCHAR")]
		public string Name { get; set; }

		public bool Enabled { get; set; }

		List<UserRole> UserRoles { get; set; }
	}
}