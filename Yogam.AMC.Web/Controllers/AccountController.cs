using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Yogam.AMC.Data.Models;
using Yogam.AMC.Infrastructure;
using Yogam.AMC.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.IO;

namespace Yogam.AMC.Web.Controllers
{
    [Authorize]
    public class AccountController : NorthwindController
    {
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        internal AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        private UserRegistrationData GetUserRegistration()
        {
            if (Session["UserRegistrationData"] == null)
            {
                Session["UserRegistrationData"] = new UserRegistrationData();
            }

            return (UserRegistrationData)Session["UserRegistrationData"];
        }

        private void RemoveUserRegistration()
        {
            Session.Remove("UserRegistrationData");
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl, string type = "")
        {
            ViewBag.ReturnUrl = returnUrl;
            if (type == "vertical")
                return View("Login-Vertical");
            if (type == "horizontal")
                return View("Login-Horizontal");
            if (type == "inline")
                return View("Login-Inline");

            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
          

            return View();
        }

        [AllowAnonymous]
        public ActionResult RegisterSummary()
        {


            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                #region add by jp
                UserRegistrationData userRegistration = GetUserRegistration();
                
                userRegistration.ConfirmPassword = model.ConfirmPassword;
                userRegistration.Password = model.Password;
         
                userRegistration.Email = model.Email;
                userRegistration.UserName = model.UserName;
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "Male", Value = "Male" });
                items.Add(new SelectListItem { Text = "Female", Value = "Female" });
                TempData["Gender"] = items;
                ViewBag.Months = items;
                return View("RegisterStep2");
                #endregion

                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }



            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterStep2(RegisterViewModel2 model, string BtnPrevious, string BtnNext)
        {
            UserRegistrationData userRegistration = GetUserRegistration();
            if (BtnPrevious != null)
            {
                RegisterViewModel step1 = new RegisterViewModel();
                step1.UserName = userRegistration.UserName;
                step1.Email = userRegistration.Email;
                step1.Password = userRegistration.Password;
                step1.ConfirmPassword = userRegistration.ConfirmPassword;
                return View("Register", step1);
               
            }
            if(BtnNext != null)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                if (ModelState.IsValid)
                {
                    #region add by jp
                  
                    userRegistration.Country = model.Country;
                    userRegistration.DateOfBirth = model.DateOfBirth;
                    userRegistration.State = model.State;
                    userRegistration.Fistname = model.Fistname;
                    userRegistration.Gender = model.Gender;
                  
                    userRegistration.Lastname = model.Lastname;
                   
                    userRegistration.PhoneNumber = model.PhoneNumber;
                    userRegistration.Surname = model.Surname;

                    return View("RegisterStep3");
                    #endregion
                }
            }



            // If we got this far, something failed, redisplay form
            return View(model);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterStep3(RegisterViewModel3 model, string BtnPrevious, string BtnNext)
        {
            UserRegistrationData userRegistration = GetUserRegistration();
            if (BtnPrevious != null)
            {
                RegisterViewModel2 step2 = new RegisterViewModel2();
                step2.Country = userRegistration.Country;
                step2.DateOfBirth = userRegistration.DateOfBirth;
                step2.Fistname= userRegistration.Fistname;
                step2.Lastname = userRegistration.Lastname;
                step2.PhoneNumber = userRegistration.PhoneNumber;
                step2.State = userRegistration.State;
                step2.Surname = userRegistration.Surname;
                return View("RegisterStep2", step2);
            }

            if (BtnNext != null)
            {
                if (ModelState.IsValid)
                {
                    #region add by jp

                    var birthdateYear = userRegistration.DateOfBirth != null ? userRegistration.DateOfBirth.Value.Year.ToString().ToUpper() : DateTime.Now.Year.ToString().ToUpper();
                    userRegistration.City = model.City;
                   
                    userRegistration.IdentityNumber = model.IdentityNumber;
                   
                    userRegistration.Living = model.Living;
                    userRegistration.Nationality = model.Nationality;
                    userRegistration.YogamCardNumber = birthdateYear + userRegistration.DateOfBirth.Value.Month.ToString().ToUpper() + uniqueCode().ToUpper() + userRegistration.Fistname.Substring(0,2).ToUpper() + userRegistration.Lastname.Substring(0,2).ToUpper();
                    userRegistration.ValidFrom = DateTime.Now;
                    userRegistration.ValidTo = DateTime.Now.AddYears(5);
                    userRegistration.EnrollmentDate = DateTime.Now;
                    userRegistration.Status = Status.Enable;
                    userRegistration.StatusCode = StatusCode.RegistreOnly;

                    #endregion

                    var user = new ApplicationUser() { UserName = userRegistration.UserName , Country= userRegistration.Country, City=userRegistration.City, DateOfBirth=userRegistration.DateOfBirth,
                    Email=userRegistration.Email,Fistname=userRegistration.Fistname,Gender=userRegistration.Gender,IdentityNumber=userRegistration.IdentityNumber,
                    Lastname =userRegistration.Lastname,Living=userRegistration.Living,Nationality=userRegistration.Nationality,PhoneNumber=userRegistration.PhoneNumber,Surname=userRegistration.Surname, State=userRegistration.State,
                    YogamCardNumber = userRegistration.YogamCardNumber, ValidFrom= userRegistration.ValidFrom, ValidTo=userRegistration.ValidTo,EnrollmentDate=userRegistration.EnrollmentDate,Status=userRegistration.Status, StatusCode=userRegistration.StatusCode};
                    var result = await UserManager.CreateAsync(user, userRegistration.Password);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                       // return RedirectToAction("Index", "Home");
                        return View("RegisterSummary", userRegistration);
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }



            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult CountryLookup()
        {

            var result = CallRestJsonService<Dictionary<string, string>>("http://www.westclicks.com/webservices/?f=json", null, "GET", "", "");

            var countries = result.Select(pair => new CountryJsonObject { CountryCode = pair.Key, CountryName = pair.Value }).ToList();

         

            return Json(countries, JsonRequestBehavior.AllowGet);
        }
        //http://www.westclicks.com/webservices/?f=json&c=CM

        [AllowAnonymous]
        public ActionResult CountryStateLookup(string country ="CM")
        {

            var result = CallRestJsonService<Dictionary<string, string>>("http://www.westclicks.com/webservices/?f=json&c="+country, null, "GET", "", "");

            var countries = result.Select(pair => new CountryJsonObject { CountryCode = pair.Key, CountryName = pair.Value }).ToList();



            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult GenerateYogamCardPDF()
        {
            UserRegistrationData model = GetUserRegistration();
            //return new Rotativa.PartialViewAsPdf("_GenerateYogamCardPDFPartial", model) { FileName = "partialViewAsPdf.pdf" };
           return new Rotativa.ViewAsPdf("PrintYogamCard", model) { FileName = "YogamCard.pdf" };
            //return new Rotativa.ActionAsPdf("PrintYogamCard");
        }
        [AllowAnonymous]
        public  ActionResult PrintYogamCard()
        {
          var userId =  User.Identity.GetUserId();
        // var user=   await UserManager.FindByIdAsync(userId);
            UserRegistrationData userRegistration = GetUserRegistration();
            //if (user != null)
            //{
                
            //    userRegistration.Fistname = user.Fistname;
            //    userRegistration.Lastname = user.Lastname;
            //    userRegistration.YogamCardNumber = user.YogamCardNumber;
            //    userRegistration.ValidFrom = user.ValidFrom;
            //    userRegistration.ValidTo = user.ValidTo;
            //    userRegistration.Country = user.Country;
            //}

            return View(userRegistration);
        }
        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        private string uniqueCode()
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string ticks = DateTime.UtcNow.Ticks.ToString();
            var code = "";
            for (var i = 0; i < characters.Length; i += 2)
            {
                if ((i + 2) <= ticks.Length)
                {
                    var number = int.Parse(ticks.Substring(i, 2));
                    if (number > characters.Length - 1)
                    {
                        var one = double.Parse(number.ToString().Substring(0, 1));
                        var two = double.Parse(number.ToString().Substring(1, 1));
                        code += characters[Convert.ToInt32(one)];
                        code += characters[Convert.ToInt32(two)];
                    }
                    else
                        code += characters[number];
                }
            }
            return code;
        }

        private string uniqueCodeThread()
        {
             int count = 0;
             bool isrunning = true;
            Thread[] threads = new Thread[30];
            var code = "";

            while (isrunning)
            {
                string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#";
                string ticks = Thread.CurrentThread.ManagedThreadId.ToString() + DateTime.UtcNow.Ticks.ToString();

                if (count >= threads.Length)
                    count = 0;
                count++;

               
                for (var i = 0; i <= ticks.Length; i += 2)
                {
                    if ((i + 2) <= ticks.Length)
                    {
                        var number = int.Parse(ticks.Substring(i, 2));
                        if (number > characters.Length - 1)
                        {
                            var one = double.Parse(number.ToString().Substring(0, 1));
                            var two = double.Parse(number.ToString().Substring(1, 1));
                            code += characters[Convert.ToInt32(one)];
                            code += characters[Convert.ToInt32(two)];
                        }
                        else
                            code += characters[number];
                    }
                }

                if (count > characters.Length - 1)
                {
                    var t_count = count;
                    for (var i = 0; i < t_count.ToString().Length; i++)
                        code += characters[int.Parse(t_count.ToString().Substring(i, 1))];
                }
                else
                {
                    code += characters[count];
                }
                   
                Console.WriteLine(code);
               
            }

            return code;
        }


        private T CallRestJsonService<T>(string uri, object requestBodyObject, string method = "POST", string username = "", string password = "")
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            javaScriptSerializer.MaxJsonLength = 104857600; //200 MB unicode
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = method;
            request.Accept = "application/json";
            request.ContentType = "application/json; charset=utf-8";

            //Add basic authentication header if username is supplied
            if (!string.IsNullOrEmpty(username))
            {
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password)); ;
            }

            //Serialize request object as JSON and write to request body
            if (requestBodyObject != null)
            {
                var stringBuilder = new StringBuilder();
                javaScriptSerializer.Serialize(requestBodyObject, stringBuilder);
                var requestBody = stringBuilder.ToString();
                request.ContentLength = requestBody.Length;
                var streamWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                streamWriter.Write(requestBody);
                streamWriter.Close();
            }

            var response = request.GetResponse();

            if (response == null)
            {
                return default(T);
            }

            //Read JSON response stream and deserialize
            var streamReader = new System.IO.StreamReader(response.GetResponseStream());
            var responseContent = streamReader.ReadToEnd().Trim();
            var jsonObject = javaScriptSerializer.Deserialize<T>(responseContent);
            return jsonObject;
        }


    }
    public class CountryJsonObject
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
    }
}

    
