using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketsForCinema.Domain.DomainModels;
using TicketsForCinema.Domain.DTO;
using TicketsForCinema.Domain.Identity;

namespace TicketsForCinema.Web.Controllers {
    public class AccountController : Controller {
        private readonly UserManager<TicketsForCinemaUser> userManager;
        private readonly SignInManager<TicketsForCinemaUser> signInManager;
        public AccountController(UserManager<TicketsForCinemaUser> userManager, SignInManager<TicketsForCinemaUser> signInManager) {

            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Register() {
            UserRegistrationDTO model = new UserRegistrationDTO();
            return View(model);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationDTO request) {
            if (ModelState.IsValid) {
                var userCheck = await userManager.FindByEmailAsync(request.Email);
                if (userCheck == null) {
                    var user = new TicketsForCinemaUser {
                        FirstName = request.Name,
                        LastName = request.LastName,
                        UserName = request.Email,
                        NormalizedUserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserCart = new ShoppingCart()
                    };
                    var result = await userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded) {
                        return RedirectToAction("Login");
                    }
                    else {
                        if (result.Errors.Count() > 0) {
                            foreach (var error in result.Errors) {
                                ModelState.AddModelError("message", error.Description);
                            }
                        }
                        return View(request);
                    }
                }
                else {
                    ModelState.AddModelError("message", "Email already exists.");
                    return View(request);
                }
            }
            return View(request);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login() {
            UserLoginDTO model = new UserLoginDTO();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDTO model) {
            if (ModelState.IsValid) {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed) {
                    ModelState.AddModelError("message", "Email not confirmed yet");
                    return View(model);

                }
                if (await userManager.CheckPasswordAsync(user, model.Password) == false) {
                    ModelState.AddModelError("message", "Invalid credentials");
                    return View(model);

                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.Rememberme, true);

                if (result.Succeeded) {
                    await userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                    return RedirectToAction("Index", "Home");
                }
                else {
                    ModelState.AddModelError("message", "Invalid login attempt");
                    return View(model);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}