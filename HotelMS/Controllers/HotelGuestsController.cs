﻿using System;
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
        private HotelDataBaseEntities db = new HotelDataBaseEntities();

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
                return RedirectToAction("Index");
            }

            ViewBag.PhoneNumberTypeCode = new SelectList(db.PhoneNumbersTypes, "PhoneNumberTypeCode", "PhoneNumberTypeName", guest.PhoneNumberTypeCode);
            return View(guest);
        }

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

        public ActionResult DeletePhone(string number)
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

        [HttpPost, ActionName("DeletePhone")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePhoneConfirmed(int id)
        {
            GuestsPhoneNumbers guestsPhoneNumbers = db.GuestsPhoneNumbers.Find(id);
            db.GuestsPhoneNumbers.Remove(guestsPhoneNumbers);
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