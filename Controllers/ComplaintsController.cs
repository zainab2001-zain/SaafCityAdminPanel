using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using saafcity_fyp.Models;

namespace saafcity_fyp.Controllers
{
    public class ComplaintsController : Controller
    {
        private SaafCity_Database_2Entities db = new SaafCity_Database_2Entities();

        // GET: Complaints
        public async Task<ActionResult> Index()
        {
            var complaints = db.Complaints.Include(c => c.Complainnat).Include(c => c.Department);
            return View(await complaints.ToListAsync());
        }

        // GET: Complaints/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = await db.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // GET: Complaints/Create
        public ActionResult Create()
        {
            ViewBag.Complainant_ID = new SelectList(db.Complainnats, "Complainant_ID", "Complainant_Name");
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name");
            return View();
        }

        // POST: Complaints/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Complaint_ID,Complaint_Time,Complaint_Loction,Complaint_Status,Complaint_Image,Verifiction_Image,Depart_ID,Comments,Complainant_ID")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Complaints.Add(complaint);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Complainant_ID = new SelectList(db.Complainnats, "Complainant_ID", "Complainant_Name", complaint.Complainant_Email);
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name", complaint.Depart_ID);
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = await db.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.Complainant_ID = new SelectList(db.Complainnats, "Complainant_ID", "Complainant_Name", complaint.Complainant_Email);
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name", complaint.Depart_ID);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Complaint_ID,Complaint_Time,Complaint_Loction,Complaint_Status,Complaint_Image,Verifiction_Image,Depart_ID,Comments,Complainant_ID")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Complainant_ID = new SelectList(db.Complainnats, "Complainant_ID", "Complainant_Name", complaint.Complainant_Email);
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name", complaint.Depart_ID);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = await db.Complaints.FindAsync(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Complaint complaint = await db.Complaints.FindAsync(id);
            db.Complaints.Remove(complaint);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
