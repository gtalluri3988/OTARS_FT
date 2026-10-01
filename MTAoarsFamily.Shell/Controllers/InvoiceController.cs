using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MTAoarsFamily.ViewModels.Shared;
using MTAoarsFamily.Services.Interfaces;
using MTAoarsFamily.Utilities.Mvc;

namespace MTAoarsFamily.Shell.Controllers {
    [MTAoarsFamilyAuthorize]
    public class InvoiceController : Controller {

        IInvoiceService invoiceService;

        public InvoiceController(IInvoiceService invoiceService) {
            this.invoiceService = invoiceService;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult PagedList(GridPageViewModel input) {
            var model = invoiceService.GetInvoiceList(input);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(long id) {
            var model = invoiceService.GetInvoiceDetails(id);
            return PartialView(model);
        }

    }// class
}// namespace
