using SpeedyCouriers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyCouriers.Controllers
{
    public class DeliverymanController : Controller
    {
        SpeedyCouriersEntities db = new SpeedyCouriersEntities();
        // GET: Deliveryman
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoggedInDeliveryMan() { 
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
        public ActionResult SelectDeliveryMan(int delmanID)
        {
            DeliveryInfo di = new DeliveryInfo();
            di.DeliveryID = delmanID;
            
            var changeDelmanStatusQuery = from delman in db.DeliveryInfoes
                                          where delman.DeliveryID == delmanID
                                          select delman;
            foreach (DeliveryInfo delman in changeDelmanStatusQuery)
            {
                delman.DeliveryStatus = "Done";
                delman.PayStatus = "Done";

            }
            db.SaveChanges();

            return View();
        }
        public ActionResult Delivery( )

        {
            var id = (int)Session["delmanID"];
            List<TransactionInfo> DeliveryDetails = db.TransactionInfoes.Where(x => x.tranUserID == id).ToList();
            return View(DeliveryDetails);
        }
    }
}