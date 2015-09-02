﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LeaveManager.Models;

namespace LeaveManager___WithLogin.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Name
        {
            get
            {

                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public string Username
        {
            get
            {

                string fName = FirstName.ToLower()[0].ToString();
                string lName = LastName.ToLower();
                return string.Format("{0}.{1}", fName, lName);
            }
        }
        //public int employeeID { get; set; }
        //public virtual Employee employee { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<RolesForEmployee> RolesForEmployees { get; set; }

        public System.Data.Entity.DbSet<EmployeeRole> EmployeeRoles { get; set; }

        public System.Data.Entity.DbSet<LeaveReason> LeaveReasons { get; set; }

        public System.Data.Entity.DbSet<RequestStatus> RequestStatus { get; set; }

        public System.Data.Entity.DbSet<LeaveRequest> LeaveRequests { get; set; }

        public System.Data.Entity.DbSet<DeliveryManagerLeaveRequestViewModel> DeliveryManagerLeaveRequestViewModels { get; set; }
    }
}