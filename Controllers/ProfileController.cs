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
            try
            {

                // // 移除 UserId 的驗證錯誤，因為我們會在後面手動設定
                // ModelState.Remove("UserId");

                // 驗證模型狀態
                if (!ModelState.IsValid)
                {
                    var allErrors = ModelState
                        .Where(kvp => kvp.Value.Errors.Count > 0)
                        .Select(kvp => new
                        {
                            Key = kvp.Key,
                            Errors = kvp.Value.Errors.Select(e => new { e.ErrorMessage, Exception = e.Exception?.Message })
                        }).ToList();

                    // 範例：寫到 TempData 讓 View 顯示（或用 _logger 記錄）
                    TempData["ModelStateErrors"] = string.Join(" | ", allErrors.Select(a => $"{a.Key}: {string.Join(",", a.Errors.Select(e => e.ErrorMessage + (e.Exception != null ? " (" + e.Exception + ")" : "")))}"));

                    return View(model);
                }

                // 取得當前使用者
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Challenge();
                }
                // 查找現有的個人資料
                var existingProfile = await _db.MemberProfiles
                .FirstOrDefaultAsync(s => s.UserId == user.Id);

                // 處理上傳的頭像
                if (avatar != null && avatar.Length > 0)
                {
                    var allowedTyppes = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var extension = Path.GetExtension(avatar.FileName).ToLowerInvariant();
                    if (!allowedTyppes.Contains(extension))
                    {
                        ModelState.AddModelError("avatar", "只允許上傳 JPG、PNG、GIF 格式的圖片");
                        return View(model);
                    }
                    // 驗證檔案大小 (例如: 5MB)
                    if (avatar.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("avatar", "檔案大小不能超過 5MB");
                        return View(model);
                    }
                    // 產生唯一檔名並儲存
                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var uploadsFolder = Path.Combine("wwwroot", "uploads", "avatars");
                    Directory.CreateDirectory(uploadsFolder);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await avatar.CopyToAsync(fileStream);
                    }
                    model.AvatarPath = $"/uploads/avatars/{fileName}";
                    // 刪除舊的頭像檔案
                    if (existingProfile?.AvatarPath != null)
                    {
                        var oldFilePath = Path.Combine("wwwroot", existingProfile.AvatarPath.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                }
                if (existingProfile == null)
                {
                    // 新增個人資料
                    model.UserId = user.Id;
                    _db.MemberProfiles.Add(model);
                }
                else
                {
                    existingProfile.NickName = model.NickName;
                    existingProfile.BirthDate = model.BirthDate;
                    existingProfile.Address = model.Address;
                    // 只有在有上傳新頭像時才更新路徑
                    if (!string.IsNullOrEmpty(model.AvatarPath))
                    {
                        existingProfile.AvatarPath = model.AvatarPath;
                    }

                    _db.MemberProfiles.Update(existingProfile);
                }
                await _db.SaveChangesAsync();
                TempData["SuccessMessage"] = "個人資料更新成功";
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                // 紀錄錯誤
                TempData["ErrorMessage"] = "更新個人資料時發生錯誤: " + ex.Message;
                return View(model);
            }


        }
    }
}