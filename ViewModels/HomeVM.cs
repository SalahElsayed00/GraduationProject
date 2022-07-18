using GraduationProject.Models;
using GraduationProject.Models.CaseProperties;
using System.Collections.Generic;

namespace GraduationProject.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Case> Cases { get; set; }
        public IEnumerable<Mediator> Mediators { get; set; }
        public IEnumerable<CasePayment> CasePayments { get; set; }
    }
}
