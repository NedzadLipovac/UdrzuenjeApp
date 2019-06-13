using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using UdruzenjeApp.Data;
using UdruzenjeApp.Models;
using UdruzenjeApp.Services;

namespace UdruzenjeApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {

            [Required(ErrorMessage = "Ime je obavezno polje")]
            [StringLength(50, MinimumLength = 2)]
            [RegularExpression(@"^[A-Ža-ž\s]+$")]
            public string Ime { get; set; }

            [Required(ErrorMessage = "Prezime je obavezno polje")]
            [StringLength(50, MinimumLength = 2)]
            [RegularExpression(@"^[A-Ža-ž\s]+$")]
            public string Prezime { get; set; }

            [Required(ErrorMessage = "Datum rođenja je obavezno polje")]
            [DataType(DataType.Date)]
            public DateTime DatumRodjenja { get; set; }

            public int? GradID { get; set; }
            [ForeignKey("GradID")]
            public Grad Grad { get; set; }
            public string Adresa { get; set; }

            public string SlikaURL { get; set; }
            [StringLength(25, MinimumLength = 2)]
            [RegularExpression(@"^[0-9]+$")]
            public string brojTelefona { get; set; }

            //**********************
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email,
                    Ime=Input.Ime,Prezime=Input.Prezime,DatumRodjenja=Input.DatumRodjenja,
                    Adresa=Input.Adresa,brojTelefona=Input.brojTelefona,GradID=Input.GradID
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var userRole = new IdentityUserRole<int>();
                    userRole.UserId = user.Id;
                    userRole.RoleId = 2;
                    db.UserRoles.Add(userRole);
                    db.SaveChanges();
                    db.Dispose();
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
