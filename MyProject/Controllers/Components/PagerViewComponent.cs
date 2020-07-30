using Microsoft.AspNetCore.Mvc;
using MyProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {

        public Task<IViewComponentResult> InvokeAsync(PaingBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
