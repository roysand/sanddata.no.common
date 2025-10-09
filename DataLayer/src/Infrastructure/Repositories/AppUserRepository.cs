using DataLayer.Application.Interface;
using DataLayer.Application.Interface.Repositories;
using DataLayer.Domain.Entities;
using DataLayer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Infrastructure.Repositories;

public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository<AppUser>
{
    public AppUserRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<AppUser?> GetWithChildren(string email, CancellationToken cancellationToken)
    {
        var appUser = await _context.AppUserSet
            .Where(x => Microsoft.EntityFrameworkCore.EF.Functions.Like(x.Email,email))
            .Include(r => r.AppUserRoles)!
            .ThenInclude(r => r.Role)
            .FirstOrDefaultAsync(cancellationToken);
        
        return appUser;
    }
}
