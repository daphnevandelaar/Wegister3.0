using System;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace WegisterUI.Services
{
    public class CurrentUserServiceInit : ICurrentUserService
    {
        public CurrentUser CreateSession()
        {
            return new CurrentUser(new Guid().ToString(), new Guid().ToString());
        }
    }
}