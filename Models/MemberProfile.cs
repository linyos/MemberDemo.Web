using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberDemo.Web.Models
{
    public class MemberProfile : IValidatableObject
    {


        public int Id { get; set; }
        public string UserId { get; set; } = default!;

        [Display(Name = "暱稱")]
        [StringLength(50, ErrorMessage = "暱稱長度不能超過 50 個字元")]
        public string? NickName { get; set; }

        [Display(Name = "生日")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "地址")]
        [StringLength(200, ErrorMessage = "地址長度不能超過 200 個字元")]
        public string? Address { get; set; }

        public string? AvatarPath { get; set; }

        public ApplicationUser? User { get; set; }

        // 自訂驗證：生日不能是未來日期
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BirthDate.HasValue && BirthDate.Value.Date > DateTime.UtcNow.Date)
            {
                yield return new ValidationResult("生日不能是未來的日期。", new[] { nameof(BirthDate) });
            }

            // 若要強制暱稱為必填，可以啟用下面這段：
            // if (string.IsNullOrWhiteSpace(NickName))
            // {
            //     yield return new ValidationResult("暱稱為必填。", new[] { nameof(NickName) });
            // }
        }
    }
}