using System;
using System.ComponentModel.DataAnnotations;
using Cookbook.Recipes.Core.CustomValidation.MessageRepository;
using Cookbook.Recipes.Core.Data.Repository;
using System.Threading;

namespace Cookbook.Recipes.Core.CustomValidation
{
    public class UniqueUserValidator : ValidationAttribute
    {
        public IRepository Repository { get; set; }
        public IMessageRepository MessageRepo { get; set; }

        public UniqueUserValidator ()
        {
            this.Repository = new MockRepository ();
            this.MessageRepo = new XmlMessageRepository ();
        }

        public override bool IsValid (object value)
        {
            string valueToTest = Convert.ToString (value);
            return this.Repository.IsUsernameUnique (valueToTest);
        }

        public override string FormatErrorMessage (string name)
        {
            string locale = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            return this.MessageRepo.GetMessages (locale) [this.ErrorMessage];
        }
    }
}

