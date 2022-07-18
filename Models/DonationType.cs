using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
	public class DonationType
	{
		public Enums.DonationType Id { get; set; }

		[Required, MaxLength(20), Column(TypeName = "varchar")]
		public string Name { get; set; }
	}
}
