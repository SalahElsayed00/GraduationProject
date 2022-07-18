using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GraduationProject.Models.Location
{
	public class Region
	{
		public int Id { get; set; }

		[Required, MaxLength(250), Column(TypeName = "varchar")]
		public string Name { get; set; }

		[Required, MaxLength(250)]
		public string Name_AR { get; set; }

		public City City { get; set; }
		public int CityId { get; set; }

		public Region()
		{

		}

		public Region(int id)
		{
			Id = id;
		}
	}
}
