using SpeedyCouriers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SpeedyCouriers.Controllers
{
    public class AdminController : Controller
    {

        SpeedyCouriersEntities db = new SpeedyCouriersEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Profile()
        {
            var userDetails = db.userInformations.FirstOrDefault();
            return View();

        }

        public ActionResult TransactionDetails()
        {
            var transInfo = db.OrderInfoes.ToList();

            return View(transInfo);
        }

        public ActionResult CreateOrder()
        {
            var orderInfo = db.OrderInfoes.ToList();

            return View(orderInfo);
        }

        public ActionResult AssignDeliveryMan(String delManstatus)
        {

            List<DeliveryMan> deliveryManDetails = db.DeliveryMen.Where(x => x.delmanStatus.Contains(delManstatus)).ToList();

            return View(deliveryManDetails);

        }

        [HttpPost]
        public ActionResult deliveryMan(int orderID, int delmanID)
        {
            TransactionInfo ti = new TransactionInfo();
            DeliveryInfo di = new DeliveryInfo();
            ti.tranOrderID = orderID;
            ti.tranUserID = delmanID;
            di.DeliveryStatus = "Not Done";
            di.PayStatus = "Not Done";
            var userdetails = db.TransactionInfoes.Where(x=>x.tranOrderID==ti.tranOrderID).FirstOrDefault();
            if (userdetails != null)
                ViewBag.Message = String.Format("Error!OrderAlready Assigned!");

            else {
                        db.TransactionInfoes.Add(ti);
                        db.SaveChanges();

                
            }

           


            var changeDelmanStatusQuery = from delman in db.DeliveryMen
                                          where delman.delmanID == delmanID
                                          select delman;
            foreach (DeliveryMan delman in changeDelmanStatusQuery)
            {
                delman.delmanStatus = "Busy";

            }
            db.SaveChanges();

            var changeOrderStatusQuery = from orderStat in db.OrderInfoes
                                         where orderStat.orderID == orderID
                                         select orderStat;
            foreach (OrderInfo orderStat in changeOrderStatusQuery)
            {
                orderStat.orderStatus = "Processing";

            }
            db.SaveChanges();
            return Json("Data Inserted Succesfully", JsonRequestBehavior.AllowGet);

        }


        public ActionResult DeliveryManList()
        {

            var sql = "select * from DeliveryMan";
            List<DeliveryMan> DelManInfo = db.DeliveryMen.SqlQuery(sql).ToList();
            return View(DelManInfo);

        }


    }
}