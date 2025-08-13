using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MemberDemo.Web.Services
{
    public interface IPointService
    {
         Task AddAsync(string userId, int delta, string reason);
    }
}