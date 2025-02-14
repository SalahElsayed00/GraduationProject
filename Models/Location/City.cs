﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GraduationProject.Models.Location
{
	public class City
	{
		public int Id { get; set; }

		[Required, MaxLength(250), Column(TypeName = "varchar")]
		public string Name { get; set; }

		[Required, MaxLength(250)]
		public string Name_AR { get; set; }

		public Governorate Governorate { get; set; }
		public int GovernorateId { get; set; }

		public ICollection<Region> Regions { get; set; }

		public City()
		{

		}

		public City(int id)
		{
			Id = id;
		}

		public City(string name, string name_AR, int governorateId)
		{
			Name = name;
			Name_AR = name_AR;
			GovernorateId = governorateId;
		}
	}
}
