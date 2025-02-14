﻿using GraduationProject.Data;
using GraduationProject.Enums;
using GraduationProject.Utilities.StaticStrings;
using GraduationProject.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GraduationProject.MvcControllers
{
    [Authorize(Roles = Roles.Admin, AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Cases = _context.Cases.AsNoTracking().ToList(),
                Mediators = _context.Mediators.AsNoTracking().ToList(),
                CasePayments = _context.CasePayments.AsNoTracking().ToList(),
            };
            // data case
            var numOfCase = homeVM.Cases.Count();
            TempData["casecount"] = numOfCase;

            var numOfaccCase = homeVM.Cases.Where(m => m.StatusId == StatusType.Accepted).Count();
            TempData["caseacccount"] = numOfaccCase;

            var numOfrejCase = homeVM.Cases.Where(m => m.StatusId == StatusType.Rejected).Count();
            TempData["caserejcount"] = numOfrejCase;

            var numOfpenCase = homeVM.Cases.Where(m => m.StatusId == StatusType.Pending).Count();
            TempData["casepencount"] = numOfpenCase;

            var numOfUrgentpriorityCase = homeVM.Cases.Where(m => m.PriorityId == PriorityType.Urgent).Count();
            TempData["caseUrgentprioritycount"] = numOfUrgentpriorityCase;

            var numOfhighpriorityCase = homeVM.Cases.Where(m => m.PriorityId == PriorityType.High).Count();
            TempData["casehighprioritycount"] = numOfhighpriorityCase;

            // data mediator
            var numOfMediator = homeVM.Mediators.Count();
            TempData["mediatorcount"] = numOfMediator;

            var numOfaccMediator = homeVM.Mediators.Where(m => m.StatusId == StatusType.Accepted).Count();
            TempData["mediatoracccount"] = numOfaccCase;

            var numOfrejMediator = homeVM.Mediators.Where(m => m.StatusId == StatusType.Rejected).Count();
            TempData["mediatorrejcount"] = numOfrejCase;

            var numOfpenMediator = homeVM.Mediators.Where(m => m.StatusId == StatusType.Pending).Count();
            TempData["mediatorpencount"] = numOfpenCase;

            //casepayment
            var numberoftransaction = homeVM.CasePayments.Count();
            TempData["numberoftransaction"] = numberoftransaction;

            var sumofamount = homeVM.CasePayments.Sum(m => m.Amount);
            TempData["sumofamount"] = sumofamount;

            var countcase = homeVM.CasePayments.Select(c => c.CaseId).Distinct().Count();
            TempData["countcase"] = countcase;




            return View(homeVM);
        }
    }
}
