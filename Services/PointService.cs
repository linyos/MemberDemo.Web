
using MemberDemo.Web.Data;
using MemberDemo.Web.Models;
namespace MemberDemo.Web.Services
{
    public class PointService:IPointService
    {
         private readonly ApplicationDbContext _db;
        public PointService(ApplicationDbContext db) => _db = db;

        public async Task AddAsync(string userId, int delta, string reason)
        {
              _db.PointTransactions.Add(new PointTransaction
              {
                  UserId = userId,
                  Delta = delta,
                  Reason = reason,
                DATETIME2 = DateTime.Now
            });

            var user = await _db.Users.FindAsync(userId);
            if (user != null)
            {
                user.CurrentPoints = (user.CurrentPoints ?? 0) + delta;
            }

            await _db.SaveChangesAsync();
        }
    }
}