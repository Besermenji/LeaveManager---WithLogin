using LeaveManager.Models;
using LeaveManager___WithLogin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManager.Controllers
{
    [Authorize(Roles = "DeliveryManager")]
    public class DeliveryManagerViewModelController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        private Employee getEemployeebyID(int id)
        {
            return db.Employees.Find(id);
        }
        private string getUserNameById(int id)
        {
            var tmp = db.Employees.Find(id);
            return tmp.employeeName;
        }

        // GET: DeliveryManagerViewModel
        public ActionResult Index()
        {
            List<DeliveryManagerLeaveRequestViewModel> deliveryManagerViewModels = new List<DeliveryManagerLeaveRequestViewModel>();
            List<LeaveRequest> tmp = new List<LeaveRequest>();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            foreach (LeaveRequest req in db.LeaveRequests) {
                tmp.Add(req);
            }


            
            foreach (LeaveRequest request in tmp) {
                string name = getUserNameById(request.employeeID);
                if (request.deliveryManagerStatus.requestStatusName.Equals("Pending") && name.Equals(currentUser.Name)) 
                {
                    deliveryManagerViewModels.Add(new DeliveryManagerLeaveRequestViewModel
                    {

                        allDayEvent = request.allDayEvent,
                        deliveryManager = getEemployeebyID(request.deliveryManagerID),
                        deliveryManagerComment = request.deliveryManagerComment,
                        deliveryManagerStatus = request.deliveryManagerStatus,
                        Description = request.Description,
                        employee = getEemployeebyID(request.employeeID),
                        endTime = request.endTime,
                        leaveReason = request.leaveReason,
                        startTime = request.startTime,
                        LeaveRequestId = request.leaveRequestID,
                        DeliveryManagerID = request.deliveryManagerID,
                        EmployeeID = request.employeeID,
                        LeaveReasonID = request.leaveReasonID,
                        deliveryManagerStatusID = request.deliveryManagerStatus.requestStatusID
                        

                    });
                }

            }
            return View(deliveryManagerViewModels);
        }

        // GET: DeliveryManagerViewModel/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeliveryManagerViewModel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryManagerViewModel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DeliveryManagerViewModel/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View();
        }

        // POST: DeliveryManagerViewModel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProcessRequest(int id) {


            var request = db.LeaveRequests.Find(id);
            ViewData["EmployeeName"] = getUserNameById(request.employeeID);
            ViewData["allDay"] = request.allDayEvent ? "YES" : "NO";
            ViewData["startDate"] = request.startTime;
            ViewData["endDate"] = request.endTime;
            ViewData["leaveReason"] = request.leaveReason.leaveReasonName;
            ViewData["description"] = request.Description;
            

            return View();

        }
        [HttpPost]
        public ActionResult ProcessRequest(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                
                string description = Convert.ToString(collection["deliveryManagerComment"]);
                string status = Convert.ToString(collection["status"]);
                db.LeaveRequests.Find(id).deliveryManagerComment = description;
                db.LeaveRequests.Find(id).deliveryManagerStatus = db.RequestStatus.Single(s => s.requestStatusName.Equals(status));
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult RequestInfo()
        {
            return View();
        }
    }
}
