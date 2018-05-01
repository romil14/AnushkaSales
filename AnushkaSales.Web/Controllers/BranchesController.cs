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

namespace AnushkaSales.Web.Controllers
{
    [Authorize]
    public class BranchesController : Controller
    {
        //private AppDbContext db = new AppDbContext();
        IBranchRepository branchRepository;

        private readonly IUserDetailsProvider userDetailsProvider;
        public BranchesController(IBranchRepository branchRepository, IUserDetailsProvider userDetailsProvider)
        {
            this.branchRepository = branchRepository;
            this.userDetailsProvider = userDetailsProvider;
        }

        // GET: Branches
        public ActionResult Index()
        {
            var userName = this.userDetailsProvider.UserName;
            //return View(db.Branches.ToList());
            var result = branchRepository.All();
            return View(result);
        }

        // GET: Branches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Branch branch = db.Branches.Find(id);
            Branch branch = branchRepository.FindById(Convert.ToInt32(id));
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // GET: Branches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BranchName,BranchAddress,BranchCity,BranchPinCode")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                //db.Branches.Add(branch);
                //db.SaveChanges();
                branchRepository.Add(branch);
                return RedirectToAction("Index");
            }

            return View(branch);
        }

        // GET: Branches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Branch branch = db.Branches.Find(id);
            Branch branch = branchRepository.FindById(Convert.ToInt32(id));
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BranchName,BranchAddress,BranchCity,BranchPinCode")] Branch branch)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(branch).State = EntityState.Modified;
                //db.SaveChanges();
                branchRepository.Update(branch);
                
                return RedirectToAction("Index");
            }
            return View(branch);
        }

        // GET: Branches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Branch branch = db.Branches.Find(id);
            Branch branch = branchRepository.FindById(Convert.ToInt32(id));
            if (branch == null)
            {
                return HttpNotFound();
            }
            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Branch branch = db.Branches.Find(id);
            //db.Branches.Remove(branch);
            //db.SaveChanges();
            Branch branch = branchRepository.FindById(Convert.ToInt32(id));
            branchRepository.Delete(branch);
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                branchRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
