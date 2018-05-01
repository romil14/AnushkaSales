using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AnushkaSales.Model;
using AnushkaSales.Model.Models;
using AnushkaSales.Model.Repositorys;
using AnushkaSales.Web.Infrastructure;
using System.Security.Claims;
using AnushkaSales.Shared.Infrastructure;

namespace AnushkaSales.Web.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserDetailsProvider userDetailsProvider;
        public CustomersController(ICustomerRepository customerRepository, IUserDetailsProvider userDetailsProvider)
        {
            this._customerRepository = customerRepository;
            this.userDetailsProvider = userDetailsProvider;
        }

        // GET: Customers
        public ActionResult Index()
        {
            
            this.userDetailsProvider.SetClaims(new List<Claim>
            {
                AsClaimTypes.GetClaim(AsClaimTypes.UserName, "Romil-test", ClaimValueTypes.String),
                AsClaimTypes.GetClaim(AsClaimTypes.LoginId, "Romil@test.com", ClaimValueTypes.String),
                AsClaimTypes.GetClaim(AsClaimTypes.UserId, "1", ClaimValueTypes.Integer32),
                AsClaimTypes.GetClaim(AsClaimTypes.IsAdmin, "True", ClaimValueTypes.Boolean)
            });

           var result = _customerRepository.All();
            return View(result);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Customer customer = db.Customers.Find(id);
            Customer customer = _customerRepository.FindById(Convert.ToInt32(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,MobileNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //db.Customers.Add(customer);
                //db.SaveChanges();
                _customerRepository.Add(customer);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Customer customer = db.Customers.Find(id);
            Customer customer = _customerRepository.FindById(Convert.ToInt32(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,MobileNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(customer).State = EntityState.Modified;
                //db.SaveChanges();
                _customerRepository.Update(customer);
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Customer customer = db.Customers.Find(id);
            Customer customer = _customerRepository.FindById(Convert.ToInt32(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Customer customer = db.Customers.Find(id);            
            //db.Customers.Remove(customer);
            //db.SaveChanges();
            Customer customer = _customerRepository.FindById(Convert.ToInt32(id));
            _customerRepository.Delete(customer);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _customerRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
