using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ServiseContext serviseContext;

        public HomeController(ServiseContext context)
        {
            serviseContext = context;
        }
        //login page/////
        [HttpPost]
        public IActionResult AdminLogin(string AdminName, string Password)
        {
            var admin = serviseContext.Admins.SingleOrDefault(a => a.AdminName == AdminName && a.Password == Password);
            if (admin != null)
            {
                
                return RedirectToAction("Start");
            }
            else
            {
                
                TempData["ErrorMessage"] = "Invalid credentials";
                return View("Login");
            }
        }

        [HttpPost]
        public IActionResult UserLogin(string UserName, string NationalId, string Password)
        {
            var user = serviseContext.Users.SingleOrDefault(u => u.UserName == UserName && u.NationalId == NationalId && u.Password == Password);
            if (user != null)
            {
              
                return RedirectToAction("UserIndex", user);
            }
            else
            {
                
                TempData["ErrorMessage"] = "Invalid credentials";
                return View("Login");
            }
        }
        //view data//
        public IActionResult index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Start()
        {
            ICollection<Admin> Admins = serviseContext.Admins.Take(5).ToList();
            return View(Admins);
        }

        //public IActionResult AdminDetails()
        //{
        //    return View();
        //}
        //view Admins
        //create admin
        public IActionResult CreateAdmin()
        {
            // return the create view to enter data
            return View();
        }
        [HttpPost]
        public IActionResult CreateAdmin(string name, string email, string password)
        {
            // Check for null or empty fields
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                // If any required field is missing, return the view with an error message
                TempData["ErrorMessage"] = "All fields are required.";
                return View(); // Return the view with the form so the user can correct the input
            }

            // Create a new Admin object
            Admin ins = new Admin
            {
                AdminName = name,
                Email = email,
                Password = password
            };

            // Add the Admin to the database and save changes
            serviseContext.Admins.Add(ins);
            serviseContext.SaveChanges();

            // Set a success message and redirect to the "Start" action
            TempData["SuccessMessage"] = "Admin created successfully!";
            return RedirectToAction("Start");
        }

        //create user//
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(string name, string nationalid, string address, string mternumber, string password, int adminid, string role)
        {
            // Check for null or empty fields
            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(nationalid) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(mternumber) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(role) ||
                adminid == 0)
            {
                
                TempData["ErrorMessage"] = "All fields are required.";
                return View(); 
            }

            // Create a new user object
            User user = new User
            {
                UserName = name,
                NationalId = nationalid,
                Address = address,
                MeterNumber = mternumber,
                Password = password,
                CreatedByAdminId = adminid,
                Role = role
            };

            // Add the user to the database and save changes
            serviseContext.Users.Add(user);
            serviseContext.SaveChanges();

            // Set a success message and redirect to the list of users
            TempData["SuccessMessage"] = "User created successfully!";
            return RedirectToAction("ViewAllUsers");
        }

        ///////////////////Edit Admin////////////////////
        [HttpGet]

        public ViewResult EditAdmin(int id)
        {
            var admin = serviseContext.Admins.Where(s => s.Id == id).FirstOrDefault();
            return View(admin);
        }

        [HttpPost]
        public IActionResult EditAdmin(Admin admin)
        {
            var currentAdmin = serviseContext.Admins.Where(s => s.Id == admin.Id).FirstOrDefault();
            currentAdmin.AdminName = admin.AdminName;
            currentAdmin.Email = admin.Email;
            currentAdmin.Password = admin.Password;
            serviseContext.SaveChanges();
            return RedirectToAction("Start");
        }

        //////////Delete///////////////////// 
        public IActionResult DeleteAdmin(int id)
        {
            var currentAdmin = serviseContext.Admins.Where(s => s.Id == id).FirstOrDefault();

            if (currentAdmin != null)
            {

                serviseContext.Admins.Remove(currentAdmin);

                serviseContext.SaveChanges();
            }

            return RedirectToAction("Start");
        }

        /// /////////////////////////////////////


        //public IActionResult UserIndex()
        //{

        //        var userId = HttpContext.Session.GetString("UserId");

        //        if (string.IsNullOrEmpty(userId))
        //        {
        //            return RedirectToAction("Login"); // Redirect if no user ID is found
        //        }

        //        var user = serviseContext.Users.FirstOrDefault(u => u.Id.ToString() == userId);

        //        if (user == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(user);

        //}
        //view users from adminPage
        [HttpGet]
        public IActionResult ViewAllUsers()
        {
            var users = serviseContext.Users.ToList(); // Fetch all users from the database
            return View(users); // Pass the list of users to the view
        }

        //delete user from admi Page
        [HttpPost]
        public IActionResult DeleteUserfromAdmin(int id)
        {
            var user = serviseContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            serviseContext.Users.Remove(user);
            serviseContext.SaveChanges();
            return RedirectToAction("ViewAllUsers");
        }
        //edit user from Admin
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = serviseContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                serviseContext.Users.Update(user);
                serviseContext.SaveChanges();
                return RedirectToAction("ViewAllUsers");
            }
            return View(user);
        }

        public IActionResult UserIndex(User user)
        {
            return View(user);
        }
        //reading from user
        [HttpGet]
        public IActionResult Reading()
        {
            return View(new MeterReading());
        }
        [HttpPost]
        [HttpPost]
        public IActionResult Reading(MeterReading model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = GetLoggedInUserId(); // Assuming you have a method to get the logged-in user's ID

                // Save the meter reading to the database
                serviseContext.MeterReadings.Add(model);
                serviseContext.SaveChanges();

                // Set the ViewBag with the new reading ID
                ViewBag.ReadingId = model.Id;

                // Calculate the bill based on the meter reading
                ViewBag.BillAmount = model.MeterReading1 * 0.25m;

                ViewBag.ShowPayButton = true;

                return View(model);
            }

            ViewBag.ShowPayButton = false;
            return View(model);
        }
        private int GetLoggedInUserId()
        {
            // Implement logic to get the logged-in user's ID
            // This might be something like:
            // return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return 1; // Placeholder value
        }
        [HttpGet]
        public IActionResult PayDetail()
        {
            // Get the current username from the logged-in user


            // Fetch the last 12 meter readings for the logged-in user
            var meterReadings = serviseContext.MeterReadings
                // Filter by the current username
                .OrderByDescending(mr => mr.ReadingDate) // Order by date to get the most recent readings
                .Take(12) // Take the last 12 readings
                .Select(mr => new PayingDetail
                {
                    UserId = mr.User.Id,
                    UserName = mr.User.UserName,
                    MeterNumber = mr.User.MeterNumber,
                    ReadingDate = mr.ReadingDate,
                    MeterReading = mr.MeterReading1,
                    BillAmount = mr.MeterReading1 * 0.25m
                })
                .ToList();

            if (meterReadings == null || meterReadings.Count == 0)
            {
                return NotFound(); // Return NotFound if no meter readings are found
            }

            return View(meterReadings);
        }

        //delete reading
        [HttpPost]
        public IActionResult DeleteReading(int id)
        {
            var meterReading = serviseContext.MeterReadings.FirstOrDefault(mr => mr.UserId == id);

            if (meterReading == null)
            {
                return NotFound();
            }

            serviseContext.MeterReadings.Remove(meterReading);
            serviseContext.SaveChanges();

            return RedirectToAction("PayDetail");
        }
        /// <summary>
        //
        /// </summary>
        /// <returns></returns>


        //[HttpPost]
        //public IActionResult PayNow(int id)
        //{
        //    // Implement your payment logic here

        //    // Redirect to a confirmation page or another appropriate action
        //    return RedirectToAction("PaymentSuccess");
        //}
        //paypill

        public IActionResult PayBill(int id)
        {
            // Fetch the meter reading details based on the provided ID
            var meterReading = serviseContext.MeterReadings
                .Include(mr => mr.User)
                .FirstOrDefault(mr => mr.Id == id);

            if (meterReading == null)
            {
                return NotFound();
            }

            // Calculate the bill amount
            var billAmount = meterReading.MeterReading1 * 0.25M;

            // Create a PayingDetail model to pass to the view
            var payingDetail = new PayingDetail
            {
                UserId = meterReading.UserId ?? 0, // Handle nullable UserId
                UserName = meterReading.User?.UserName ?? "Unknown", // Handle nullable UserName
                MeterNumber = meterReading.User?.MeterNumber ?? "Unknown", // Handle nullable MeterNumber
                ReadingDate = meterReading.ReadingDate,
                MeterReading = meterReading.MeterReading1,
                BillAmount = billAmount
            };

            // Pass the PayingDetail model to the view
            return View("PayBill", payingDetail);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}