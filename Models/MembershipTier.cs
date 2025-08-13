using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemberDemo.Web.Models
{
    public class MembershipTier
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public int MinPoints { get; set; }

        public string Benefits { get; set; } = default!;

    }
}