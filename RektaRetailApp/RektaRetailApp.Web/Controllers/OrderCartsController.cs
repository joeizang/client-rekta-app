using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RektaRetailApp.Domain.Data;

namespace RektaRetailApp.Web.Controllers
{
    public class OrderCartsController : Controller
    {
        private readonly ILogger<OrderCartsController> _logger;
        private readonly RektaContext _db;

        public OrderCartsController(ILogger<OrderCartsController> logger, RektaContext db)
        {
            _logger = logger;
            _db = db;
        }
        // GET: OrderCartsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderCartsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderCartsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderCartsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderCartsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderCartsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderCartsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderCartsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
