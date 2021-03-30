using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }


        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        //receive the teamid from teamnameviewcomponent. Take the context created above and sql out of it where teamid = teamid to match the tables
        //making teamid nullable so that at the home page it will show all bowlers and contact info
        //initiale the pageNum and pass that in as a parameter
        public IActionResult Index(long? teamid, string teamname, int pageNum = 0)
        {
            //store how many items we want per page
            int pageSize = 5;

            return View(new IndexViewModel
            {
                //passing in the dataset
                Bowlers = (context.Bowlers
                    .Where(m => m.TeamId == teamid || teamid == null)
                    .OrderBy(m => m.BowlerLastName)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()),

                //passing in the page numbering info
                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    //If no bowling team has been selected, then get the full count. Otherwise, only count the number
                    //from the bowling team that has been selected
                    TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == teamid).Count())
                },

                TeamCategory = teamname
            });               
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
    }
}
