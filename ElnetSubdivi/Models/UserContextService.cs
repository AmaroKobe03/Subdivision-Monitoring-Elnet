// UserContextService.cs
using Microsoft.AspNetCore.Http;
using System;

namespace ElnetSubdivi.Services
{
    public class UserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.Session.GetString("UserId");
        public string Username => _httpContextAccessor.HttpContext?.Session.GetString("Username");
        public string TypeOfUser => _httpContextAccessor.HttpContext?.Session.GetString("TypeOfUser");
        public string FullName => _httpContextAccessor.HttpContext?.Session.GetString("FullName");
        public string MiddleName => _httpContextAccessor.HttpContext?.Session.GetString("MiddleName");
        public string DateOfBirth => _httpContextAccessor.HttpContext?.Session.GetString("DateOfBirth");
        public string Gender => _httpContextAccessor.HttpContext?.Session.GetString("Gender");
        public string Phone => _httpContextAccessor.HttpContext?.Session.GetString("Phone");
        public string Email => _httpContextAccessor.HttpContext?.Session.GetString("Email");
        public string ValidId => _httpContextAccessor.HttpContext?.Session.GetString("ValidId");
        public string Address => _httpContextAccessor.HttpContext?.Session.GetString("Address");
        public string ProfilePicture => _httpContextAccessor.HttpContext?.Session.GetString("ProfilePicture");
        public string CreatedAt => _httpContextAccessor.HttpContext?.Session.GetString("CreatedAt");
    }
}
