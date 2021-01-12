using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CinemaModel.Models;
using Tomoiaga_AndreiBogdan_CinemaTAB.Models.CinemaViewModels;
using CinemaModel.Data;
using Tomoiaga_AndreiBogdan_CinemaTAB.Models;

namespace Tomoiaga_AndreiBogdan_CinemaTAB.Controllers
{
    public class HomeController : Controller
    {
        private readonly CinemaContext _context;
        public HomeController(CinemaContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Statistics()
        {
            IQueryable<POStudioGroup> data =
            from studio in _context.Studios
            group studio by studio.ParentOrganization into parentorgGroup
            select new POStudioGroup()
            {
                ParentOrganization = parentorgGroup.Key,
                StudioCount = parentorgGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Chat()
        {
            return View();
        }
    }
}
