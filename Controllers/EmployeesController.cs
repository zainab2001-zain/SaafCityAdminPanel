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
    public class EmployeesController : Controller
    {
        private SaafCity_Database_2Entities db = new SaafCity_Database_2Entities();


        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var employees = db.Employees.Include(e => e.Complaint).Include(e => e.Department);
            return View(await employees.ToListAsync());
        }
        public async Task<ActionResult> GarbageEmployees()
        {
            var employees = db.Employees
                .Include(e => e.Complaint)
                .Include(e => e.Department)
                .Where(e => e.Depart_ID == 1); // Replace 1 with the actual department ID for the garbage department

            return View(await employees.ToListAsync());
        }
        public async Task<ActionResult> SewageEmployees()
        {
            var employees = db.Employees
                .Include(e => e.Complaint)
                .Include(e => e.Department)
                .Where(e => e.Depart_ID == 2); // Replace 1 with the actual department ID for the garbage department

            return View(await employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.Complaint_ID = new SelectList(db.Complaints, "Complaint_ID", "Complaint_Loction");
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Employee_ID,Employee_Name,Employee_Phoneno,Employee_Email,Complaint_ID,Depart_ID,Job")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Complaint_ID = new SelectList(db.Complaints, "Complaint_ID", "Complaint_Loction", employee.Complaint_ID);
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name", employee.Depart_ID);
            return View(employee);
        }
      
        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Complaint_ID = new SelectList(db.Complaints, "Complaint_ID", "Complaint_Loction", employee.Complaint_ID);
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name", employee.Depart_ID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Employee_ID,Employee_Name,Employee_Phoneno,Employee_Email,Complaint_ID,Depart_ID,Job")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Complaint_ID = new SelectList(db.Complaints, "Complaint_ID", "Complaint_Loction", employee.Complaint_ID);
            ViewBag.Depart_ID = new SelectList(db.Departments, "Department_ID", "Department_Name", employee.Depart_ID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
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
