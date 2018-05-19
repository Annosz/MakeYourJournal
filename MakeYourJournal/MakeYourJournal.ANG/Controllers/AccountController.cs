using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeYourJournal.ANG.Dtos;
using MakeYourJournal.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MakeYourJournal.ANG.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
        }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;


        [HttpGet]
        public IActionResult GetIdentity()
        {
            var user = new IdentityUserModel()
            {
                IsSignedIn = User.Identity.IsAuthenticated,
                Name = User.Identity.Name
            };
            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginUserDto model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Json(result);
                }
                if (result.IsLockedOut)
                {
                    return Json("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Json(result);
                }
            }

            // If we got this far, something failed, redisplay form
            var invalidresult = new
            {
                Succeeded = false,
                Description = "Something failed."
            };
            return Json(invalidresult);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterUserDTO registerUserDTO)
        {
            var user = new ApplicationUser
            { UserName = registerUserDTO.UserName, Email = registerUserDTO.Email };
            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}