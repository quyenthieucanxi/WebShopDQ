using Microsoft.AspNetCore.Identity;

namespace WebShopDQ.App.Models
{
    public class Role : IdentityRole<Guid> { }

    public class RoleClaims : IdentityRoleClaim<Guid> { }

    public class UserRoles : IdentityUserRole<Guid> { }
}
