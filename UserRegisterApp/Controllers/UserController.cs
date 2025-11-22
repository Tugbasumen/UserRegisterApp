using Microsoft.AspNetCore.Mvc;
using UserRegisterApp.Data;
using UserRegisterApp.Models;

namespace UserRegisterApp.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UserController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string fileName = null;

            if (model.ProfileImage != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(model.ProfileImage.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ProfileImage", "Sadece JPG, JPEG veya PNG formatında dosya yükleyebilirsiniz.");
                    return View(model);
                }

                string folder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                fileName = Guid.NewGuid() + extension;
                string path = Path.Combine(folder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                ProfileImagePath = fileName != null ? "/images/" + fileName : null
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"“{user.Name}” isimli kullanıcı başarıyla eklendi.";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            var model = new UserViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Password = ""
            };

            ViewBag.UserId = user.Id;
            ViewBag.ProfileImagePath = user.ProfileImagePath;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserViewModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(model.Name))
                user.Name = model.Name;

            if (!string.IsNullOrWhiteSpace(model.Email))
                user.Email = model.Email;

            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            if (model.ProfileImage != null)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(model.ProfileImage.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ProfileImage", "Sadece JPG, JPEG veya PNG formatında dosya yükleyebilirsiniz.");
                    ViewBag.UserId = user.Id;
                    ViewBag.ProfileImagePath = user.ProfileImagePath;
                    return View(model);
                }

                string folder = Path.Combine(_env.WebRootPath, "images");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid() + extension;
                string path = Path.Combine(folder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ProfileImage.CopyToAsync(stream);
                }

                user.ProfileImagePath = "/images/" + fileName;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"“{user.Name}” isimli kullanıcı başarıyla güncellendi.";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"“{user.Name}” isimli kullanıcı başarıyla silindi.";

            return RedirectToAction("Index");
        }
    }
}
