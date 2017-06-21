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
    public class OrdersRegistrationsController : Controller
    {
        private HotelDataBaseEntities1 db = new HotelDataBaseEntities1();

        // GET: OrdersRegistrations
        public ActionResult Index()
        {
            var ordersRegistration = from item in db.OrdersRegistration
                                     where item.OrderStatus == 2
                                     select item;
            return View(ordersRegistration.ToList());
        }

        // GET: OrdersRegistrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersRegistration ordersRegistration = db.OrdersRegistration.Find(id);
            if (ordersRegistration == null)
            {
                return HttpNotFound();
            }
            return View(ordersRegistration);
        }

        // GET: OrdersRegistrations/Create
        public ActionResult Create(int roomNumber)
        {
            string profileName = Request.Cookies["Login"].Value;

            if (!Enumerable.Any(from item in db.OrdersRegistration
                               where item.GuestMail.Equals(profileName)
                               select item))
            {
                var order = new OrdersRegistration()
                {
                    GuestMail = profileName,
                    RoomNumber = roomNumber,
                    BookingDate = DateTime.Now,
                    ArrivalDate = DateTime.Now,
                    LeavingDate = DateTime.Now,
                    PaymentMethodCode = 1,
                    OrderStatus = 1
                };

                db.OrdersRegistration.Add(order);
                db.SaveChanges();

                return RedirectToAction("Edit", new { login = profileName });
            }

            return RedirectToAction("Index", "HotelRooms");
        }

        // GET: OrdersRegistrations/Edit/5
        public ActionResult Edit(string login)
        {
            if (login == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = from item in db.OrdersRegistration
                             where item.GuestMail.Equals(login) &&
                             item.OrderStatus.Equals(1)
                             select item;
            if (order.Count() != 0)
            {
                var ordersRegistration = order.First();
                ViewBag.PaymentMethodCode = new SelectList(db.PaymentMethods, "PaymentMethodCode", "PaymentMethodName", ordersRegistration.PaymentMethodCode);
                return View(ordersRegistration);
            }

            return RedirectToAction("Index", "HotelRooms");
        }

        // POST: OrdersRegistrations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GuestMail,RoomNumber,BookingDate,ArrivalDate,LeavingDate,PaymentMethodCode,OrderStatus")] OrdersRegistration ordersRegistration)
        {
            if (ModelState.IsValid)
            {
                ordersRegistration.OrderStatus = 2;
                db.Entry(ordersRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "HotelRooms");
            }

            return RedirectToAction("Edit", new { login = ordersRegistration.GuestMail });
        }

        public ActionResult Confirm(int id)
        {
            var order = db.OrdersRegistration.Find(id);

            var registration = new HotelsRoomRegistration()
            {
                GuestMail = order.GuestMail,
                StaffMail = Request.Cookies["Login"].Value,
                BookedRoomNumber = order.RoomNumber,
                BookingDate = order.BookingDate,
                ArrivalDate = order.ArrivalDate,
                LeavingDate = order.LeavingDate,
                PaymentMethodCode = order.PaymentMethodCode,
                OrderStatus = 3
            };

            db.HotelsRoomRegistration.Add(registration);
            db.OrdersRegistration.Remove(order);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: OrdersRegistrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdersRegistration ordersRegistration = db.OrdersRegistration.Find(id);
            if (ordersRegistration == null)
            {
                return HttpNotFound();
            }
            db.OrdersRegistration.Remove(ordersRegistration);
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
