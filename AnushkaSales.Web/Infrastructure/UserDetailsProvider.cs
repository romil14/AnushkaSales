using AnushkaSales.Shared.Infrastructure;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace AnushkaSales.Web.Infrastructure
{
    public interface IUserDetailsProvider
    {
        void SetClaim(string claimType, string value, string valueType);

        void SetClaims(IEnumerable<Claim> claims);
        int UserId { get; }

        string UserName { get; }

        bool IsAdmin { get; }

        string LoginId { get; }
    }

    public class UserDetailsProvider : IUserDetailsProvider
    {
        private readonly IAuthenticationManager manager;

        public UserDetailsProvider(IAuthenticationManager manager)
        {
            this.manager = manager;
        }

        public string LoginId
        {
            get
            {
                return this.GetClaimValue(AsClaimTypes.LoginId);
            }
        }

        public int UserId
        {
            get
            {
                if (this.manager.User != null)
                {
                    var claim = this.manager.User.Claims.FirstOrDefault(c => c.Type == AsClaimTypes.UserId);
                    if (claim != null)
                    {
                        return Convert.ToInt32(claim.Value);
                    }
                }

                return 0;
            }
        }

        public string UserName
        {
            get
            {
                return this.GetClaimValue(AsClaimTypes.UserName);
            }
        }

        public bool IsAdmin
        {
            get
            {
                return this.GetClaimValue(AsClaimTypes.IsAdmin).Equals("True");
            }
        }



        public void SetClaim(string claimType, string value, string valueType)
        {
            if (this.manager.User != null)
            {
                var identity = this.manager.User.Identity;
                if (identity is ClaimsIdentity)
                {
                    var claimIdentity = identity as ClaimsIdentity;
                    if (claimIdentity.HasClaim(claim => claim.Type.Equals(claimType)))
                    {
                        var claim = claimIdentity.Claims.First(c => c.Type.Equals(claimType));
                        claimIdentity.RemoveClaim(claim);
                    }

                    claimIdentity.AddClaim(new Claim(claimType, value, valueType));
                    this.manager.SignOut(identity.AuthenticationType);

                    // TODO: This may not be persistent
                    this.manager.AuthenticationResponseGrant = new AuthenticationResponseGrant(
                        new ClaimsPrincipal(claimIdentity),
                        new AuthenticationProperties { IsPersistent = true });
                }
            }
        }

        public void SetClaims(IEnumerable<Claim> claims)
        {
            if (this.manager.User != null)
            {
                var identity = this.manager.User.Identity; // this.manager.User.Identity;
                if (identity is ClaimsIdentity)
                {
                    var claimIdentity = identity as ClaimsIdentity;

                    foreach (var claim in claims)
                    {
                        if (claimIdentity.HasClaim(c => c.Type.Equals(claim.Type, StringComparison.OrdinalIgnoreCase)))
                        {
                            var existingClaim = claimIdentity.Claims.First(c => c.Type.Equals(claim.Type, StringComparison.OrdinalIgnoreCase));
                            claimIdentity.RemoveClaim(existingClaim);
                        }
                    }

                    foreach (var claim in claims)
                    {
                        claimIdentity.AddClaim(claim);
                    }

                    this.manager.SignOut(identity.AuthenticationType);

                    // TODO : This may not be persistent
                    this.manager.AuthenticationResponseGrant = new AuthenticationResponseGrant(
                        new ClaimsPrincipal(claimIdentity),
                        new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddMinutes(60) });
                }
            }
        }

        private string GetClaimValue(string claimType)
        {
            if (this.manager.User == null)
            {
                return string.Empty;
            }

            var claim = this.manager.User.Claims.FirstOrDefault(c => c.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));
            return claim != null ? claim.Value : string.Empty;
        }
    }
}