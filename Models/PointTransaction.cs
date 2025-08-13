using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberDemo.Web.Models
{
    public class PointTransaction
    {
        public int Id { get; set; }
        public string UserId  { get; set; } = default!;

        public int Delta  { get; set; }

        public string  Reason  { get; set; }

        public DateTime DATETIME2  { get; set; } = DateTime.Now;

        public ApplicationUser? User { get; set; }
    }
}