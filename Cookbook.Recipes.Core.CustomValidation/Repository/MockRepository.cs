using System;
using System.Collections.Generic;
using Cookbook.Recipes.Core.CustomValidation;

namespace Cookbook.Recipes.Core.Data.Repository
{
    public class MockRepository : IRepository
    {
        #region Variables
        private static List<User> _users;
        #endregion

        public MockRepository ()
        {
            _users = new List<User> ();
        }

        #region IRepository Members
        /// <summary>
        /// Adds a new user to the collection
        /// </summary>
        /// <param name="user">User.</param>
        public void AddUser (User user)
        {
            _users.Add (user);
        }
        /// <summary>
        /// Check whether a user withthe username already exists
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns><c>true</c> 
        public bool IsUsernameUnique (string userName)
        {
            bool exists = _users.Exists (u => u.UserName == userName);

            return !exists;
        }
        #endregion
    }
}

