using System;

namespace Cookbook.Recipes.Core.CustomValidation
{
    public interface IRepository
    {
        void AddUser (User user);
        bool IsUsernameUnique (string userName);
    }
}

