using GraduationProject.Enums;

namespace GraduationProject.Models
{
	public class Donation
	{
		public int Id { get; set; }

		public int DonatorId { get; set; }
		public Donator Donator { get; set; }

		public int Amount { get; set; }

		public Enums.DonationType DonationTypeId { get; set; }
		public DonationType DonationType { get; set; }

		public int BenefactorId { get; set; }

		public bool IsAnonymous { get; set; }

		public byte[] TransactionImage { get; set; }
	}
}
