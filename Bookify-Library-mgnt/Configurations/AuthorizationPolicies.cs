﻿namespace Bookify_Library_mgnt.Configurations
{
    public static class AuthorizationPolicies
    {
        public static void AddCustomAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanDeleteBookPolicy", policy =>
                    policy.RequireClaim("CanDeleteBook", "true"));

                options.AddPolicy("CanManageUserClaimsPolicy", policy =>
                    policy.RequireClaim("CanManageUserClaims", "true"));
            });
        }
    }
}
