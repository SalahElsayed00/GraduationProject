using GraduationProject.Data;
using GraduationProject.DTOs.Response;
using GraduationProject.Models.Shared;
using GraduationProject.Utilities.CustomApiResponses;
using GraduationProject.Utilities.StaticStrings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.ApiControllers
{
    [Route("api")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Governorates()
        {
            var governorates = await _context.Governorates
                .Select(g => new ListItem(g.Id, g.Name))
                .ToArrayAsync();

            return new Success(governorates);
        }

        [HttpGet("[action]/{id:min(1)}")]
        public async Task<IActionResult> Cities(int id)
        {
            var cities = await _context.Cities
                .Where(c => c.GovernorateId == id)
                .Select(c => new ListItem(c.Id, c.Name))
                .ToArrayAsync();

            return new Success(cities);
        }

        [HttpGet("[action]/{id:min(1)}")]
        public async Task<IActionResult> Regions(int id)
        {
            var regions = await _context.Regions
                .Where(r => r.CityId == id)
                .Select(r => new ListItem(r.Id, r.Name))
                .ToArrayAsync();

            return new Success(regions);
        }

        [HttpGet("[action]")]
        public IActionResult Genders()
        {
            return new Success(GetGenders());
        }

        [HttpGet("[action]")]
        public IActionResult SocialStatus()
        {
            return new Success(GetSocialStatus());
        }

        [HttpGet("case-properties")]
        public async Task<IActionResult> CaseProperties()
        {

            var categories = _context.Categories.AsNoTracking().ToArrayAsync();
            var properties = new CaseProperties
            {
                Genders = GetGenders(),
                SocialStatus = GetSocialStatus(),
                Relationships = StaticValues.Relationships(),
                Periods = StaticValues.Periods(),
                Priorities = StaticValues.Priorities(),
                Categories = await categories,
            };

            return new Success(properties);
        }

        private IEnumerable<Gender> GetGenders()
        {
            var genders = StaticValues.Genders();
            return genders;
        }

        private IEnumerable<SocialStatus> GetSocialStatus()
        {
            var socialStatus = StaticValues.SocialStatus();
            return socialStatus;
        }
    }
}
