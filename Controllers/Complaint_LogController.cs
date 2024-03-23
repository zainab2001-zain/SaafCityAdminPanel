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
    public class Complaint_LogController : Controller
    {
        private SaafCity_Database_2Entities db = new SaafCity_Database_2Entities();

        // GET: Complaint_Log
        public async Task<ActionResult> Index()
        {
            var complaint_Log = db.Complaint_Log.Include(c => c.Complaint).Include(c => c.Department).Include(c => c.Employee);
            return View(await complaint_Log.ToListAsync());
        }

        // GET: Complaint_Log/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint_Log complaint_Log = await db.Complaint_Log.FindAsync(id);
            if (complaint_Log == null)
            {
                return HttpNotFound();
            }
            return View(complaint_Log);
        }

        // GET: Complaint_Log/Create
        public ActionResult Create()
        {
            ViewBag.Complaint_ID = new SelectList(db.Complaints, "Complaint_ID", "Complaint_Loction");
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name");
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name");
            return View();
        }

        // POST: Complaint_Log/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Serial_No,Complaint_ID,Log_Time,Log_Date,Complaint_Loction,Complaint_Status,Employee_ID,Depart_ID,Comments")] Complaint_Log complaint_Log)
        {
            if (ModelState.IsValid)
            {
                db.Complaint_Log.Add(complaint_Log);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Complaint_ID = new SelectList(db.Complaints, "Complaint_ID", "Complaint_Loction", complaint_Log.Complaint_ID);
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name", complaint_Log.Depart_ID);
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name", complaint_Log.Employee_ID);
            return View(complaint_Log);
        }

        // GET: Complaint_Log/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint_Log complaint_Log = await db.Complaint_Log.FindAsync(id);
            if (complaint_Log == null)
            {
                return HttpNotFound();
            }
            ViewBag.Complaint_ID = new SelectList(db.Complaints, "Complaint_ID", "Complaint_Loction", complaint_Log.Complaint_ID);
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name", complaint_Log.Depart_ID);
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name", complaint_Log.Employee_ID);
            return View(complaint_Log);
        }

        // POST: Complaint_Log/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Serial_No,Complaint_ID,Log_Time,Log_Date,Complaint_Loction,Complaint_Status,Employee_ID,Depart_ID,Comments")] Complaint_Log complaint_Log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint_Log).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Complaint_ID = new SelectList(db.Complaints, "Complaint_ID", "Complaint_Loction", complaint_Log.Complaint_ID);
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name", complaint_Log.Depart_ID);
            ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_Name", complaint_Log.Employee_ID);
            return View(complaint_Log);
        }

        // GET: Complaint_Log/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint_Log complaint_Log = await db.Complaint_Log.FindAsync(id);
            if (complaint_Log == null)
            {
                return HttpNotFound();
            }
            return View(complaint_Log);
        }

        // POST: Complaint_Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Complaint_Log complaint_Log = await db.Complaint_Log.FindAsync(id);
            db.Complaint_Log.Remove(complaint_Log);
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
