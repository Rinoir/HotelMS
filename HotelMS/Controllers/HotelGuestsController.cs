using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelMS.Models;

namespace HotelMS.Controllers
{
    public class HotelGuestsController : Controller
    {
        private HotelDataBaseEntities1 db = new HotelDataBaseEntities1();

        // GET: HotelGuests
        public ActionResult Index()
        {
            return View(db.HotelGuests.ToList());
        }

        // GET: HotelGuests/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelGuests hotelGuests = db.HotelGuests.Find(id);
            if (hotelGuests == null)
            {
                return HttpNotFound();
            }
            return View(hotelGuests);
        }

        public ActionResult AddPhone()
        {
            ViewBag.PhoneNumberTypeCode = new SelectList(db.PhoneNumbersTypes, "PhoneNumberTypeCode", "PhoneNumberTypeName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPhone([Bind(Include = "Id,GuestMail,PhoneNumberTypeCode,PhoneNumber")] GuestsPhoneNumbers phone, string login)
        {
            if (login == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            phone.GuestMail = login;

            db.GuestsPhoneNumbers.Add(phone);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = login });
        }

        // GET: HotelGuests/Create
        public ActionResult Create()
        {
            ViewBag.PhoneNumberTypeCode = new SelectList(db.PhoneNumbersTypes, "PhoneNumberTypeCode", "PhoneNumberTypeName");
            return View();
        }

        // POST: HotelGuests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GuestMail,Surname,Name,Patronymic,PassportSerialNumber,PassportNumber,PhoneNumberTypeCode,PhoneNumber")] GuestModel guest)
        {
            if (ModelState.IsValid)
            {
                var hotelGuests = new HotelGuests()
                {
                    GuestMail = guest.GuestMail,
                    Surname = guest.Surname,
                    Name = guest.Name,
                    Patronymic = guest.Patronymic
                };

                var guestPassports = new GuestPassports()
                {
                    GuestMail = guest.GuestMail,
                    PassportSerialNumber = guest.PassportSerialNumber,
                    PassportNumber = guest.PassportNumber
                };

                var guestPhoneNumbers = new GuestsPhoneNumbers()
                {
                    GuestMail = guest.GuestMail,
                    PhoneNumberTypeCode = guest.PhoneNumberTypeCode,
                    PhoneNumber = guest.PhoneNumber
                };

                hotelGuests.GuestPassports.Add(guestPassports);
                hotelGuests.GuestsPhoneNumbers.Add(guestPhoneNumbers);

                db.HotelGuests.Add(hotelGuests);
                db.SaveChanges();

                SignIn(hotelGuests.GuestMail);
                
                return RedirectToAction("Index", "HotelRooms");
            }

            ViewBag.PhoneNumberTypeCode = new SelectList(db.PhoneNumbersTypes, "PhoneNumberTypeCode", "PhoneNumberTypeName", guest.PhoneNumberTypeCode);
            return View(guest);
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string login)
        {

            if (Enumerable.Any(from element in db.HotelGuests
                               where element.GuestMail == login
                               select element))
            {
                var cookie = new HttpCookie("Login");
                cookie.Expires = DateTime.Now.AddHours(1);
                cookie.Value = login;
                Response.SetCookie(cookie);

                cookie = new HttpCookie("Status");
                cookie.Expires = DateTime.Now.AddHours(1);
                cookie.Value = "Guest";
                Response.SetCookie(cookie);
            }

            return RedirectToAction("Index", "HotelRooms");
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: HotelGuests/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelGuests hotelGuests = db.HotelGuests.Find(id);
            if (hotelGuests == null)
            {
                return HttpNotFound();
            }
            return View(hotelGuests);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // POST: HotelGuests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GuestMail,Surname,Name,Patronymic")] HotelGuests hotelGuests)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelGuests).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotelGuests);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: HotelGuests/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelGuests hotelGuests = db.HotelGuests.Find(id);
            if (hotelGuests == null)
            {
                return HttpNotFound();
            }
            return View(hotelGuests);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // POST: HotelGuests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HotelGuests hotelGuests = db.HotelGuests.Find(id);
            db.HotelGuests.Remove(hotelGuests);
            db.SaveChanges();
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
