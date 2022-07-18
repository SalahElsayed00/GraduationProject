using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationProject.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GraduationProject.Models.Shared
{
	public class Gender
	{
		public GenderType Id { get; set; }

		[Required, MaxLength(10), Column(TypeName = "varchar")]
		public string Name { get; set; }
	}
}
