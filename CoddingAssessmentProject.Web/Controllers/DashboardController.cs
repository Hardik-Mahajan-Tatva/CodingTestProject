

using CoddingAssessmentProject.Services.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace CoddingAssessmentProject.Web.Controllers
{
    public class DashboardController : Controller
    {
        #region  Constructor
        public DashboardController()
        {

        }
        #endregion

        #region Dashboard
        [CustomAuthorize]
        public IActionResult Index()
        {
            return View();
        }
        #endregion
    }
}
