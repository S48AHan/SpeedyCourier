using SpeedyCouriers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SpeedyCouriers.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        SpeedyCouriersEntities db = new SpeedyCouriersEntities();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserView()
        {
            return View();
        }

       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CreateParcel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateParcel(receiverInfo receiver)
        {
            if (ModelState.IsValid)
            {
                if (receiver.phoneNumber.StartsWith("01"))
                {
                    receiver.receiveDate = System.DateTime.Today;
                    try
                    {
                        db.receiverInfoes.Add(receiver);

                        db.SaveChanges();
                    }
                    catch (Exception e) {
                        ViewBag.Message = String.Format("Error!Phone number not valid.");
                    }

                    int uID = (int)Session["userID"];
                    OrderInfo order = new OrderInfo();
                    order.orderStatus = "Not Done";

                    order.Order_userID = uID;
                    order.Order_receiverID = receiver.receiverID;
                    order.totalCost = receiver.productPrice + 70;

                    try
                    {
                        db.OrderInfoes.Add(order);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewBag.Message = String.Format("Error!Phone number not valid.");
                    }
                   

                    ViewBag.Message = String.Format("Data inserted successfully!");
                    return View();


                }
                else
                {
                    ViewBag.Message = String.Format("Error!Phone number not valid.");

                }
            }
            return View();


        }




        public ActionResult OrderDetails(string searchStatus)
        {

            var id = (int)Session["userID"];
            List<OrderInfo> result = db.OrderInfoes.Where(x => x.Order_userID == id && x.orderStatus.Contains(searchStatus)).ToList();
            return View(result);



        }
    }
}