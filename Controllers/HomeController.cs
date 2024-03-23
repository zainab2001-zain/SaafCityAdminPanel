using saafcity_fyp.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace saafcity_fyp.Controllers
{
    public class HomeController : Controller
    {

        private SaafCity_Database_2Entities db = new SaafCity_Database_2Entities();
        public ActionResult Dashboard()
        {
            ViewBag.smessage = TempData["message"];

            var complaintsCount = db.Complaints.Count();
           var complaints = db.Complaints.Where(c => c.Depart_ID != null).Count();
            var unverifiedcomplaints = db.Complaints.Where(c => c.Depart_ID == null).Count();
            ViewBag.noofComplaints = complaints;
            ViewBag.noofunverifiedComplaints = unverifiedcomplaints;
            ViewBag.ComplaintsCount = complaintsCount;

            return View();
        }

        public ActionResult GetImage(int Complaint_ID)
        {
            try
            {
                using (var db = new SaafCity_Database_2Entities())
                {
                    // Retrieve the image data from the database based on the complaint's ID
                    var complaint = db.Complaints.FirstOrDefault(c => c.Complaint_ID == Complaint_ID);

                    if (complaint != null && complaint.Complaint_Image != null)
                    {
                        // Get the image data as a byte array
                        byte[] imageData = complaint.Complaint_Image;

                        // Generate a unique filename for the image
                        string fileName = Guid.NewGuid().ToString("N") + ".jpeg";

                        // Define the file path where the image will be saved
                        string filePath = Server.MapPath("~/Content/images/") + fileName;

                        // Save the image file to the specified path
                        System.IO.File.WriteAllBytes(filePath, imageData);

                        // Return the image file path
                        return File("/Content/images/" + fileName, "image/jpeg");
                    }
                    else
                    {
                        // Handle the case when the image is not found
                        return HttpNotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval process
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public ActionResult UpdateComplaintStatus(int complaintId, string currentStatus)
        {
            try
            {
                using (var db = new SaafCity_Database_2Entities())
                {
                    // Retrieve the complaint from the database based on the complaint ID
                    var complaint = db.Complaints.FirstOrDefault(c => c.Complaint_ID == complaintId);

                    if (complaint != null)
                    {
                        // Update the Complaint_Status based on its current value
                        if (currentStatus == "Transport Send")
                        {
                            complaint.Complaint_Status = "Verification";
                        }
                        if (currentStatus == "In-Progress")
                        {
                            complaint.Complaint_Status = "Transport Send";
                        }
                        else if (currentStatus == "Verification")
                        {
                            complaint.Complaint_Status = "Completed";
                        }

                        db.SaveChanges();
                    }
                }
                // Redirect back to the TotalComplaints action
                return RedirectToAction("TotalComplaints");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the update process
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        public ActionResult TotalComplaints()
            {
            // Fetch the complaints from the database
            List<Complaint> complaints = db.Complaints.ToList();
            ViewBag.noofComplaints = complaints;
            // Pass the complaints to the view
            return View(complaints);
            
            }
        public ActionResult VerifiedComplaints()
        {
            // Fetch the complaints from the database where Department_ID is not null
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID != null).ToList();
            ViewBag.noofComplaints = complaints;
            // Pass the complaints to the view
            return View(complaints);
        }
        public ActionResult UnVerifiedComplaints()
        {
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID == null  ).ToList();
            ViewBag.noofComplaints = complaints;
            // Pass the complaints to the view
            return View(complaints);
        }



        public ActionResult Settings()
        {
            ViewBag.Title = "Settings";
           
            return View();
        }
        
        public ActionResult homepage_Garbage()
        {
            ViewBag.Title = "homepage_Garbage";
            var completedcomplaints = db.Complaints.Where(c=>c.Complaint_Status== "Completed" && c.Depart_ID == 1).Count();
            var complaints = db.Complaints.Where(c => c.Depart_ID == 1).Count();
            var inprogress_complaints = db.Complaints.Where(c => (c.Complaint_Status == "In-Progress" || c.Complaint_Status== "Transport Send") && c.Depart_ID==1).Count();
            var verified_complaints = db.Complaints.Where(c => (c.Complaint_Status == "Verification" && c.Depart_ID == 1)).Count();
            var rejected_complaints = db.Complaints.Where(c => (c.Complaint_Status == "Rejected" && c.Depart_ID == 1)).Count();
            var numberOfEmployeesInGarbageDepartment = db.Departments.Where(c => c.Department_ID == 1).Select(c => c.No_Of_Employees).FirstOrDefault();

            ViewBag.ComplaintsCount = complaints;
            ViewBag.CompletedComplaints = completedcomplaints;
            ViewBag.InProgressComplaints =inprogress_complaints;
            ViewBag.VerifiedComplaints = verified_complaints;
            ViewBag.RejectedComplaints = rejected_complaints;
            ViewBag.NoOfEmployees = numberOfEmployeesInGarbageDepartment;

            return View();
        }
        public ActionResult homepage_Sewage()
        {
            ViewBag.Title = "homepage_Sewage";
            var complaints = db.Complaints.Where(c => c.Depart_ID == 2).Count();
            var completedcomplaints = db.Complaints.Where(c => c.Complaint_Status == "Completed" && c.Depart_ID == 2).Count();
            var inprogress_complaints = db.Complaints.Where(c => (c.Complaint_Status == "In-Progress" || c.Complaint_Status == "Transport Send") && c.Depart_ID == 2).Count();
            var verified_complaints = db.Complaints.Where(c => (c.Complaint_Status == "Verification" && c.Depart_ID == 2)).Count();
            var rejected_complaints = db.Complaints.Where(c => (c.Complaint_Status == "Rejected" && c.Depart_ID == 2)).Count();
            var numberOfEmployeesInGarbageDepartment = db.Departments.Where(c => c.Department_ID == 2).Select(c => c.No_Of_Employees).FirstOrDefault();
            ViewBag.ComplaintsCount = complaints;
            ViewBag.CompletedComplaints = completedcomplaints;
            ViewBag.InProgressComplaints = inprogress_complaints;
            ViewBag.VerifiedComplaints = verified_complaints;
            ViewBag.RejectedComplaints = rejected_complaints;
            ViewBag.NoOfEmployees = numberOfEmployeesInGarbageDepartment;
            return View();
        }
        public ActionResult done_comsewage()
        {
            ViewBag.Title = "done_comsewage";
            // Fetch the complaints from the database where Department_ID is not null
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID == 2 && c.Complaint_Status.Equals("Completed")).ToList();
            ViewBag.noofComplaints = complaints;

            return View(complaints);
        }
        public ActionResult done_comgarbage()
        {
            ViewBag.Title = "done_comgarbage";
            // Fetch the complaints from the database where Department_ID is not null
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID == 1 && c.Complaint_Status.Equals("Completed")).ToList();
            ViewBag.noofComplaints = complaints;

            return View(complaints);
        }
        public ActionResult in_prog_garbage()
        {
            ViewBag.Title = "in_prog_garbage";
            
            // Fetch the complaints from the database where Department_ID is not null
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID == 1 && c.Complaint_Status.Equals("In-Progress")).ToList();
            ViewBag.noofComplaints = complaints;

            return View(complaints);
        }
        public ActionResult in_prog_sewage()
        {
            ViewBag.Title = "in_prog_sewage";
            // Fetch the complaints from the database where Department_ID is not null
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID == 2 && c.Complaint_Status.Equals("In-Progress")).ToList();
            ViewBag.noofComplaints = complaints;

            return View(complaints);
        }
        public ActionResult Total_complaints_garbage()
        {
            ViewBag.Title = "Total_complaints_garbage";
            // Fetch the complaints from the database where Department_ID is not null
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID ==1).ToList();
            ViewBag.noofComplaints = complaints;
            return View(complaints);
        }
        public ActionResult Total_complaints_sewage()
        {
            ViewBag.Title = "Total_complaints_sewage";
            // Fetch the complaints from the database where Department_ID is not null
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID == 2).ToList();
            ViewBag.noofComplaints = complaints;
            return View(complaints);
        }
        public ActionResult unver_com_garbage()
        {
            ViewBag.Title = "unver_com_garbage";

            return View();
        }
        public ActionResult var_com_garbage()
        {
            ViewBag.Title = "var_com_garbage";
            // Fetch the complaints from the database where Department_ID is not null
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID == 1 && c.Complaint_Status.Equals("Verification")).ToList();
            ViewBag.noofComplaints = complaints;
            return View(complaints);
        }
        public ActionResult unver_com_sewage()
        {
            ViewBag.Title = "unver_com_sewage";

            return View();
        }
        public ActionResult var_com_sewage()
        {
            ViewBag.Title = " var_com_sewage";
           
            // Fetch the complaints from the database where Department_ID is not null
            List<Complaint> complaints = db.Complaints.Where(c => c.Depart_ID == 2 && c.Complaint_Status.Equals("Verification")).ToList();
            ViewBag.noofComplaints = complaints;
            return View(complaints);
            
        }
    }


}
