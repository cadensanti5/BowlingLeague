using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Components
{
    public class TeamNameViewComponent : ViewComponent
    {
        private BowlingLeagueContext context;

        //constructor, creating the context of bowlingleaguecontext
        public TeamNameViewComponent(BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        public IViewComponentResult Invoke()
        {
            //this line is to help with the css styling on Default.cshtml to highlight the teamname once selected
            ViewBag.SelectedType = RouteData?.Values["teamname"];

            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
