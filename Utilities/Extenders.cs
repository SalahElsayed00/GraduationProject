﻿using System;
using GraduationProject.Enums;

namespace GraduationProject.Utilities
{
	public static class Extenders
	{
		public static string ToCustomString(this PeriodType periodType)
		{
			if (periodType == PeriodType.OneTime)
				return "One Time";

			return periodType.ToString();
		}
	}
}
