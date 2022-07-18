﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GraduationProject.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GraduationProject.Models.CaseProperties
{
	public class Period
	{
		public PeriodType Id { get; set; }

		[Required, MaxLength(50), Column(TypeName = "varchar")]
		public string Name { get; set; }
	}
}
