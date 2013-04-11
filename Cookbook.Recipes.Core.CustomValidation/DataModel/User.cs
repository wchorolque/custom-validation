using System;

namespace Cookbook.Recipes.Core.CustomValidation
{
    /// <summary>
    /// Contains details of the user being registered.
    /// </summary>
    public class User
    {
        #region Propiedades
        [UniqueUserValidator(ErrorMessage="User name already exists")]
        public String UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Password { get; set; }
        #endregion
        public User ()
        {
        }
    }
}

