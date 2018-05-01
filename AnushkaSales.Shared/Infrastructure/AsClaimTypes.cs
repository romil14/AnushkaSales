using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AnushkaSales.Shared.Infrastructure
{
    public class AsClaimTypes
    {
        public const string UserId = "UserId";

        public const string UserName = "UserName";

        public const string IsAdmin = "IsAdmin";

        public const string LoginId = "LoginId";

        public static Claim GetClaim(string claimType, string value, string valueType)
        {
            return new Claim(claimType, value, valueType);
        }
    }
}
