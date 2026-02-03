// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using MiniTwit.Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MiniTwit.Web.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Author> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<Author> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        // OnPost-Method for the Logout page
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            // Sign out identity
            await _signInManager.SignOutAsync();
            
            // Sign out OAuth
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);      // "Identity.Application"
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);         // "Identity.External"
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); // default cookie
            
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect("/");
            }
            else
            {
                /* This needs to be a redirect so that the browser performs a new
                request and the identity for the user gets updated. */
                return LocalRedirect("/");
            }
        }
    }
}
