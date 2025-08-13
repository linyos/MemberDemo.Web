using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MemberDemo.Web.Models
{
    public class ApplicationUser: IdentityUser
    {
         // 想直接放在 AspNetUsers 的附加欄位可加在這裡
        public int? CurrentPoints { get; set; }
    }
}