using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Yogam.AMC.Data.Models;
using Yogam.AMC.Infrastructure;
using Yogam.AMC.Infrastructure.Alerts;
using Yogam.AMC.Web.Models;

namespace Yogam.AMC.Web.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public UserProfileController(ApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        // GET: UserProfile
        public ActionResult Index(int? page)
        {
            var users = _context.Users.Where(u=>u.ParentUserId == _currentUser.User.Id || u.Id == _currentUser.User.Id).Project().To<UserRegistrationData>().OrderBy(x=>x.UserName);
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Edit(string id)
        {
            var model = _context.Users.Where(u => u.Id == id).Project().To<UserRegistrationData>().FirstOrDefault();
            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserRegistrationData form)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(form.Id);
            if (user != null)
            {
                user.Email = form.Email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (form.Password != string.Empty)
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(form.Password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(form.Password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded
                && form.Password != string.Empty && validPass.Succeeded))
                {
                    user.Country = form.Country;
                    user.City = form.City;
                    user.IdentityNumber = form.IdentityNumber;
                    user.Lastname = form.Lastname;
                    user.Fistname = form.Fistname;
                    user.Living = form.Living;
                    user.PhoneNumber = form.PhoneNumber;
                    user.Surname = form.Surname;
                    user.Nationality = form.Nationality;
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index").WithSuccess("Your profile has been update successfully");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
           
        }


    
        public ActionResult GenerateYogamCardPDF(string id)
        {
            var model  = _context.Users.Where(u=>u.Id == id).Project().To<UserRegistrationData>().FirstOrDefault();
            return new Rotativa.ViewAsPdf("PrintYogamCard", model) { FileName = "YogamCard.pdf" };
           
        }
    
       
    }
}