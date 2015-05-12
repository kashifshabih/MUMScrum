using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using edu.mum.mumscrum.DAL;
using edu.mum.mumscrum.Models;

namespace edu.mum.mumscrum.Controllers
{
    public class ProductBacklogController : Controller
    {
        //private MUMScrumContext db = new MUMScrumContext();

        private IProductBacklogRepository productBacklogRepository;

        public ProductBacklogController()
        {
            this.productBacklogRepository = new ProductBacklogRepository(new MUMScrumContext());
        }

        public ProductBacklogController(IProductBacklogRepository productBacklogRepository)
        {
            this.productBacklogRepository = productBacklogRepository;
        }


        // GET: ProductBacklog
        public ActionResult Index()
        {
            //return View(db.ProductBacklogs.ToList());
            return View(productBacklogRepository.GetProductBacklogs());
        }

        // GET: ProductBacklog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //ProductBacklog productBacklog = db.ProductBacklogs.Find(id);
            ProductBacklog productBacklog = productBacklogRepository.GetProductBacklogByID(id);
            
            if (productBacklog == null)
            {
                return HttpNotFound();
            }
            
            return View(productBacklog);
        }

        // GET: ProductBacklog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductBacklog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate")] ProductBacklog productBacklog)
        {
            if (ModelState.IsValid)
            {
                productBacklog.CreatedBy = 1; // this has to be the id of the person to be logged in
                productBacklog.CreatedDate = DateTime.Now;
                
                //db.ProductBacklogs.Add(productBacklog);
                //db.SaveChanges();

                productBacklogRepository.InsertProductBacklog(productBacklog);
                productBacklogRepository.Save();
                
                return RedirectToAction("Index");
            }

            return View(productBacklog);
        }

        // GET: ProductBacklog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ProductBacklog productBacklog = db.ProductBacklogs.Find(id);
            ProductBacklog productBacklog = productBacklogRepository.GetProductBacklogByID(id);
            
            if (productBacklog == null)
            {
                return HttpNotFound();
            }
            return View(productBacklog);
        }

        // POST: ProductBacklog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate")] ProductBacklog productBacklog)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(productBacklog).State = EntityState.Modified;
                //db.SaveChanges();

                productBacklogRepository.UpdateProductBacklog(productBacklog);
                productBacklogRepository.Save();

                return RedirectToAction("Index");
            }
            return View(productBacklog);
        }

        // GET: ProductBacklog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ProductBacklog productBacklog = db.ProductBacklogs.Find(id);
            ProductBacklog productBacklog = productBacklogRepository.GetProductBacklogByID(id);
                        
            if (productBacklog == null)
            {
                return HttpNotFound();
            }
            return View(productBacklog);
        }

        // POST: ProductBacklog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //ProductBacklog productBacklog = db.ProductBacklogs.Find(id);
            //db.ProductBacklogs.Remove(productBacklog);
            //db.SaveChanges();

            ProductBacklog productBacklog = productBacklogRepository.GetProductBacklogByID(id);
            productBacklogRepository.DeleteProductBacklog(id);
            productBacklogRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                productBacklogRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
