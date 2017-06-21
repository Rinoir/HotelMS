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
    public class GuestsPhoneNumbersController : Controller
    {
        private HotelDataBaseEntities1 db = new HotelDataBaseEntities1();

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: GuestsPhoneNumbers
        public ActionResult Index()
        {
            var guestsPhoneNumbers = db.GuestsPhoneNumbers.Include(g => g.HotelGuests).Include(g => g.PhoneNumbersTypes);
            return View(guestsPhoneNumbers.ToList());
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: GuestsPhoneNumbers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestsPhoneNumbers guestsPhoneNumbers = db.GuestsPhoneNumbers.Find(id);
            if (guestsPhoneNumbers == null)
            {
                return HttpNotFound();
            }
            return View(guestsPhoneNumbers);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: GuestsPhoneNumbers/Create
        public ActionResult Create()
        {
            ViewBag.GuestMail = new SelectList(db.HotelGuests, "GuestMail", "Surname");
            ViewBag.PhoneNumberTypeCode = new SelectList(db.PhoneNumbersTypes, "PhoneNumberTypeCode", "PhoneNumberTypeName");
            return View();
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // POST: GuestsPhoneNumbers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GuestMail,PhoneNumberTypeCode,PhoneNumber")] GuestsPhoneNumbers guestsPhoneNumbers, string id)
        {
            if (ModelState.IsValid)
            {
                guestsPhoneNumbers.GuestMail = id;

                db.GuestsPhoneNumbers.Add(guestsPhoneNumbers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuestMail = new SelectList(db.HotelGuests, "GuestMail", "Surname", guestsPhoneNumbers.GuestMail);
            ViewBag.PhoneNumberTypeCode = new SelectList(db.PhoneNumbersTypes, "PhoneNumberTypeCode", "PhoneNumberTypeName", guestsPhoneNumbers.PhoneNumberTypeCode);
            return View(guestsPhoneNumbers);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // GET: GuestsPhoneNumbers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuestsPhoneNumbers guestsPhoneNumbers = db.GuestsPhoneNumbers.Find(id);
            if (guestsPhoneNumbers == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuestMail = new SelectList(db.HotelGuests, "GuestMail", "Surname", guestsPhoneNumbers.GuestMail);
            ViewBag.PhoneNumberTypeCode = new SelectList(db.PhoneNumbersTypes, "PhoneNumberTypeCode", "PhoneNumberTypeName", guestsPhoneNumbers.PhoneNumberTypeCode);
            return View(guestsPhoneNumbers);
        }

        ///////////////////////////NOT YET READY///////////////////////////
        // POST: GuestsPhoneNumbers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GuestMail,PhoneNumberTypeCode,PhoneNumber")] GuestsPhoneNumbers guestsPhoneNumbers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guestsPhoneNumbers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuestMail = new SelectList(db.HotelGuests, "GuestMail", "Surname", guestsPhoneNumbers.GuestMail);
            ViewBag.PhoneNumberTypeCode = new SelectList(db.PhoneNumbersTypes, "PhoneNumberTypeCode", "PhoneNumberTypeName", guestsPhoneNumbers.PhoneNumberTypeCode);
            return View(guestsPhoneNumbers);
        }

        // GET: GuestsPhoneNumbers/Delete/5
        public ActionResult Delete(string number)
        {
            if (number == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var phone = from item in db.GuestsPhoneNumbers
                        where item.PhoneNumber == number
                        select item;

            if (phone == null)
            {
                return HttpNotFound();
            }

            return View(phone.First());
        }

        // POST: GuestsPhoneNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string number)
        {
            var phone = from item in db.GuestsPhoneNumbers
                        where item.PhoneNumber == number
                        select item;
            string tmp = phone.First().GuestMail;
            db.GuestsPhoneNumbers.Remove(phone.First());
            db.SaveChanges();
            return RedirectToAction("Details", "HotelGuests", new { id = tmp });
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
