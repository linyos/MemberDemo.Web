using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MemberDemo.Web.Data;
using MemberDemo.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Azure.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MemberDemo.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;


        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _db = dbContext;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _db.MemberProfiles.FirstOrDefaultAsync(p => p.UserId == user!.Id)
                ?? new MemberProfile { UserId = user!.Id };

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(MemberProfile model, IFormFile? avatar)
        {
             if (!ModelState.IsValid) return View(model);

        var exists = await _db.MemberProfiles.FirstOrDefaultAsync(p => p.UserId == model.UserId);
        if (avatar != null && avatar.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(avatar.FileName)}";
            var path = Path.Combine("wwwroot", "uploads", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            using var fs = System.IO.File.Create(path);
            await avatar.CopyToAsync(fs);
            model.AvatarPath = $"/uploads/{fileName}";
        }

        if (exists == null) _db.MemberProfiles.Add(model);
        else
        {
            exists.NickName = model.NickName;
            exists.BirthDate = model.BirthDate;
            exists.Address = model.Address;
            if (model.AvatarPath != null) exists.AvatarPath = model.AvatarPath;
        }
        await _db.SaveChangesAsync();
        TempData["Msg"] = "資料已更新";
        return RedirectToAction(nameof(Index));
        }
    }  
}