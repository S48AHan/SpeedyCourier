using SpeedyCouriers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyCouriers.Controllers
{
    public class HomeController : Controller
    {
        SpeedyCouriersEntities db = new SpeedyCouriersEntities();
        public ActionResult Index()
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

        public ActionResult LogIn()
        {
            //List<Account> users = db.Accounts.ToList();
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult DeliverymanLogin() { 
        return View();
        }



        [HttpPost]
        public ActionResult Autherize(SpeedyCouriers.Models.userInformation userModel)
        {
            using (db)
            {
                var userDetails = db.userInformations.Where(x => x.Email == userModel.Email && x.Userpassword == userModel.Userpassword).FirstOrDefault();

                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("LogIn", userModel);
                }
                else if (userDetails.UserRole == "Admin") {
                    Session["userID"] = userDetails.userID;
                    Session["Name"] = userDetails.Name.ToString();
                    Session["Email"] = userDetails.Email.ToString();
                    Session["UserRole"] = userDetails.UserRole.ToString();
                    return RedirectToAction("Index", "Admin");
                }

                else
                {
                    Session["userID"] = userDetails.userID;
                    Session["Name"] = userDetails.Name.ToString();
                    Session["Email"] = userDetails.Email.ToString();
                    Session["UserRole"] = userDetails.UserRole.ToString();
                    return RedirectToAction("UserView", "User");
                }
            }


        }
        [HttpPost]
        public ActionResult AutherizeDel(SpeedyCouriers.Models.DeliveryMan userModel)
        {
            using (db)
            {
                var userDetails = db.DeliveryMen.Where(x => x.delmanEmail == userModel.delmanEmail && x.delmanPass == userModel.delmanPass).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong Email or password.";
                    return View("DeliverymanLogin",userModel);
                }
                
                else
                {
                    Session["delmanID"] = userDetails.delmanID;
                    Session["delmanName"] = userDetails.delmanName.ToString();
                    Session["delmanEmail"] = userDetails.delmanEmail.ToString();
                    Session["delmanPhone"] = userDetails.delmanPhone.ToString();
                    Session["delmanAddress"] = userDetails.delmanAddress.ToString();
                    Session["delmanStatus"] = userDetails.delmanStatus.ToString(); 


                    return RedirectToAction("LoggedInDeliveryMan", "Deliveryman");

                   
                }
            }
            /*Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.ClearHeaders();
            Response.AddHeader("Cache-Control", "no-cache, no-store,max-age-0,must-revalidate ");
            Response.AddHeader("Pragma", "no-cache");*/


        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DelBack() {
            return RedirectToAction("LoggedInDeliveryMan", "Deliveryman");
        }
        public ActionResult UserBack()
        {
            return RedirectToAction("Index", "User");
        }
        public ActionResult DeliverymanLogOut()
        {
            int userId = (int)Session["delmanID"];
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }




        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "Name, userPassword, Email,UserRole")] userInformation guest)
        {
            if (ModelState.IsValid)
            {
                db.userInformations.Add(guest);
                db.SaveChanges();
                return View();
            }

            return View();
        }

        public ActionResult DelRegister()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DelRegister([Bind(Include = "delmanName, delmanPass, delmanEmail,delmanPhone,delmanAddress,delmanStatus")] DeliveryMan guest)
        {
            if (ModelState.IsValid)
            {
                db.DeliveryMen.Add(guest);
                db.SaveChanges();
                ViewBag.Message = String.Format("Successfully Added");
                return View();
                
            }
            else
                ViewBag.Message = String.Format("Error!");

            return View();
        }
    }
}