using DocuSignLibrary;
using DocuSignLibrary.Models;
using DocuSignWebSample.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace DocuSignWebSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var myProfile = new DocuSignProfile(GetDocuSignCredentials());

            return View(myProfile.ListEnvelopes(10));
        }

        [HttpGet]
        public ActionResult CreateDocument()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDocument(Person person)
        {
            var documentGenerator = new DocuSignDocumentGenerator(GetDocuSignCredentials(), GetEmailTemplate(), GetDocuSignTemplate());

            documentGenerator.GenerateDocument(person.FullName, person.Email);

            return RedirectToAction("Status");
        }

        [HttpGet]
        public ActionResult Status()
        {
            return View();
        }

        private TemplateFacade GetDocuSignTemplate()
        {
            return new TemplateFacade(
                    ConfigurationManager.AppSettings["TemplateId"],
                    new List<string> { ConfigurationManager.AppSettings["RoleName"] }
                );
        }

        private EmailTemplate GetEmailTemplate()
        {
            return new EmailTemplate(
                    ConfigurationManager.AppSettings["EmailSubjectLine"],
                    ConfigurationManager.AppSettings["EmailMessage"]
                );
        }

        private Credentials GetDocuSignCredentials()
        {
            return new Credentials(
                    ConfigurationManager.AppSettings["UserName"],
                    ConfigurationManager.AppSettings["Password"],
                    ConfigurationManager.AppSettings["IntegratorKey"]
                );
        }
    }
}