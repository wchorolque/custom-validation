using System;
using System.ComponentModel.DataAnnotations;
//using Cookbook.Recipes.Core.CustomValidation.MessageRepository;
using Cookbook.Recipes.Core.Data.Repository;

namespace Cookbook.Recipes.Core.CustomValidation
{
    public class UniqueUserValidator : ValidationAttribute
    {
        public IRepository Repository { get; set; }

        public UniqueUserValidator ()
        {
            this.Repository = new MockRepository ();
        }

        public override bool IsValid (object value)
        {
            string valueToTest = Convert.ToString (value);
            return this.Repository.IsUsernameUnique (valueToTest);
        }
    }
}

