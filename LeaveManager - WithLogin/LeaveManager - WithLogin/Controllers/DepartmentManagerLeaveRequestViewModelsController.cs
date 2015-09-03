using LeaveManager.Models;
using LeaveManager___WithLogin.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LeaveManager.Controllers
{
    [Authorize(Roles = "DepartmentManager")]
    public class DepartmentManagerLeaveRequestViewModelsController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DepartmentManagerLeaveRequestViewModels
        private string getUserById(int id)
        {
            var tmp = db.Employees.Find(id);
            return tmp.employeeName;
        }
        private Employee getEemployeebyID(int id)
        {
            return db.Employees.Find(id);
        }

        public ActionResult Index()
        {
            List<DepartmentManagerLeaveRequestViewModel> requstsList = new List<DepartmentManagerLeaveRequestViewModel>();
            List<LeaveRequest> tmp = new List<LeaveRequest>();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());
            foreach (LeaveRequest req in db.LeaveRequests) {
                tmp.Add(req);
            }

            foreach (LeaveRequest req in tmp)
            {
                string name = getUserById(req.employeeID);
                if (req.deliveryManagerStatus.requestStatusName.Equals("Approved") && (req.departmentManagerStatus.requestStatusName.Equals("Pending")) && name.Equals(currentUser.Name))
                {

                    requstsList.Add( new DepartmentManagerLeaveRequestViewModel
                    {
                        allDayEvent = req.allDayEvent,
                        departmentManager = getEemployeebyID(req.departmentManagerID),
                        departmentManagerStatus = req.departmentManagerStatus,
                        employee = getEemployeebyID(req.employeeID),
                        endTime = req.endTime,
                        description = req.Description,
                        leaveReason = req.leaveReason,
                        startTime = req.startTime,
                        departmentManagerComment = req.departmentManagerComment,
                        leaveRequestID = req.leaveRequestID,
                        employeeID = req.employeeID,
                        departmentManagerID = req.departmentManagerID,
                        departmentManagerStatusID = req.departmentManagerID

                    });
                }



            }

            return View(requstsList.AsQueryable());
        }

        // GET: DepartmentManagerLeaveRequestViewModels/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DepartmentManagerLeaveRequestViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DepartmentManagerLeaveRequestViewModels/Create
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

        public ActionResult ProcessRequest(int id)
        {
            LeaveRequest leaveRequest = db.LeaveRequests.Find(id);



            DepartmentManagerLeaveRequestViewModel viewModel = new DepartmentManagerLeaveRequestViewModel()
            {

                allDayEvent = leaveRequest.allDayEvent,
                departmentManager = leaveRequest.departmentManager,
                departmentManagerStatus = leaveRequest.departmentManagerStatus,
                employee = leaveRequest.employee,
                endTime = leaveRequest.endTime,
                description = leaveRequest.Description,
                leaveReason = leaveRequest.leaveReason,
                startTime = leaveRequest.startTime,
                departmentManagerComment = leaveRequest.departmentManagerComment,
                leaveRequestID = leaveRequest.leaveRequestID

            };

            // ViewBag.possibleStatuses = new SelectList(db.RequestStatus, "requestStatusID", "requestStatusName",GetRequestStatusByName(leaveRequest.departmentManagerStatus.requestStatusName).requestStatusID);
            ViewBag.departmentManagerStatusID = new SelectList(db.RequestStatus, "requestStatusID", "requestStatusName", GetRequestStatusByName(leaveRequest.departmentManagerStatus.requestStatusName).requestStatusID);

            return View("DepartmentManagerProcessRequest", viewModel);
        }

        [HttpPost]
        public ActionResult ProcessRequest([Bind(Include = "leaveRequestID,departmentManagerComment,departmentManagerStatusID")] DepartmentManagerLeaveRequestViewModel departmentManagerLeaveRequestViewModel)
        {
            if (ModelState.IsValid)
            {

                var requestToUpdate = db.LeaveRequests.Find(departmentManagerLeaveRequestViewModel.leaveRequestID);
                requestToUpdate.departmentManagerComment = departmentManagerLeaveRequestViewModel.departmentManagerComment;
                requestToUpdate.departmentManagerStatus = db.RequestStatus.Find(departmentManagerLeaveRequestViewModel.departmentManagerStatusID);

                db.SaveChanges();


                return RedirectToAction("Index");
            }

            return View();
        }

        public RequestStatus GetRequestStatusByName(string requestStatusName)
        {

            RequestStatus request = db.RequestStatus.Single(r => r.requestStatusName == requestStatusName);
            return request;
        }
        // GET: DepartmentManagerLeaveRequestViewModels/Edit/5
        public ActionResult Edit(int id)
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

        // POST: DepartmentManagerLeaveRequestViewModels/Edit/5
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

        // GET: DepartmentManagerLeaveRequestViewModels/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DepartmentManagerLeaveRequestViewModels/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
