using Microsoft.AspNetCore.Identity;

namespace WebShopDQ.App.Models
{
    public class Roles : IdentityRole<Guid> { }

    public class RoleClaims : IdentityRoleClaim<Guid> { }

    public class UserRoles : IdentityUserRole<Guid> { }

}
