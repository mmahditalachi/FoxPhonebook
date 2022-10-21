using FoxPhonebook.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace FoxPhonebook.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId => Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId"), out var userId) ? userId : null;
    }
}
