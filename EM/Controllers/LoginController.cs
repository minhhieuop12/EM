using EM.Data;
using EM.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EM.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;

        public LoginController(UserManager<AppUser> userManager, AppDbContext context, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginProcess(LoginModel account)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(account.email, account.password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(account.email);
                    if (user != null)
                    {
                        var list = _context.Employees.ToList();
                         return View("~/Views/Employee/Index.cshtml",list);
                    }
                }
                ModelState.AddModelError("", "Invalid Username and Password");
            }
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProcess(CreateModel model)
        {
            int tmp = _context.Employees.Count();
            tmp++;
            var user = new AppUser
            {
                UserName = model.email,
                Email = model.email,
                Id = "User" + tmp.ToString(),
            };
            var result = await _userManager.CreateAsync(user,model.password);
            if (result.Succeeded)
            {
                Employee newEm = new Employee();
                newEm.name = model.name;
                newEm.address= model.address;
                newEm.age = model.age;
                newEm.subject = model.subject;
                newEm.id = "User" + tmp.ToString();
                _context.Employees.Add(newEm);
                _context.SaveChanges();
            }
            return RedirectToAction("Login", "Login");
        }
    }
}
