using System;
using System.Collections.Generic;

namespace Cookbook.Recipes.Core.CustomValidation.MessageRepository
{
    public interface IMessageRepository
    {
        IDictionary<string, string> GetMessages (string locale);
    }
}

