using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelMS.Models;
using System.Text;

namespace HotelMS.Controllers
{
    public class HotelStaffsController : Controller
    {
        private HotelDataBaseEntities db = new HotelDataBaseEntities();

        private string checkHash = "c3bcf54a5b844d03700bc3440255d74a";

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: HotelStaffs
        public ActionResult Index()
        {
            var hotelStaff = db.HotelStaff.Include(h => h.EmployeesPositions);
            return View(hotelStaff.ToList());
        }

        // GET: HotelStaffs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelStaff hotelStaff = db.HotelStaff.Find(id);
            if (hotelStaff == null)
            {
                return HttpNotFound();
            }
            return View(hotelStaff);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: HotelStaffs/Create
        public ActionResult Create()
        {
            ViewBag.PositionCode = new SelectList(db.EmployeesPositions, "PositionCode", "PositionName");
            return View();
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // POST: HotelStaffs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffMail,Surname,Name,Patronymic,PositionCode,Salary,Schedule")] HotelStaff hotelStaff)
        {
            if (ModelState.IsValid)
            {
                db.HotelStaff.Add(hotelStaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionCode = new SelectList(db.EmployeesPositions, "PositionCode", "PositionName", hotelStaff.PositionCode);
            return View(hotelStaff);
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string login, string answer)
        {
            using (MD5 md5 = MD5.Create())
            {
                if (VerifyMd5Hash(md5, answer, checkHash) && 
                    Enumerable.Any(from element in db.HotelStaff
                                   where element.StaffMail == login
                                   select element))
                {
                    var cookie = new HttpCookie("Login");
                    cookie.Expires = DateTime.Now.AddHours(1);
                    cookie.Value = login;
                    Response.SetCookie(cookie);

                    cookie = new HttpCookie("Status");
                    cookie.Expires = DateTime.Now.AddHours(1);
                    cookie.Value = "Employee";
                    Response.SetCookie(cookie);
                }
            }

            return RedirectToAction("Index", "HotelRooms");
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: HotelStaffs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelStaff hotelStaff = db.HotelStaff.Find(id);
            if (hotelStaff == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionCode = new SelectList(db.EmployeesPositions, "PositionCode", "PositionName", hotelStaff.PositionCode);
            return View(hotelStaff);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // POST: HotelStaffs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffMail,Surname,Name,Patronymic,PositionCode,Salary,Schedule")] HotelStaff hotelStaff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelStaff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionCode = new SelectList(db.EmployeesPositions, "PositionCode", "PositionName", hotelStaff.PositionCode);
            return View(hotelStaff);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: HotelStaffs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelStaff hotelStaff = db.HotelStaff.Find(id);
            if (hotelStaff == null)
            {
                return HttpNotFound();
            }
            return View(hotelStaff);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // POST: HotelStaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HotelStaff hotelStaff = db.HotelStaff.Find(id);
            db.HotelStaff.Remove(hotelStaff);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return (0 == comparer.Compare(hashOfInput, hash));
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
