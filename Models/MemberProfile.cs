using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberDemo.Web.Models
{
    public class MemberProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = default!;
        public string? NickName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? AvatarPath { get; set; }
        public ApplicationUser? User { get; set; }
    }
}