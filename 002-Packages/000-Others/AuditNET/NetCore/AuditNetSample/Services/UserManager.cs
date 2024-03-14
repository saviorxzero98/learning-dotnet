using Audit.Core;
using AuditNetSample.Exceptions;
using AuditNetSample.Models;
using System;
using System.Collections.Generic;

namespace AuditNetSample.Services
{
    public class UserManager
    {
        private static Dictionary<string, User> _userRepositories;
        protected Dictionary<string, User> UserRepositories
        {
            get
            {
                if (_userRepositories == null)
                {
                    _userRepositories = new Dictionary<string, User>();
                }
                return _userRepositories;
            }
            set
            {
                if (value != null)
                {
                    _userRepositories = value;
                }
            }
        }


        public UserManager()
        {
            UserRepositories = new Dictionary<string, User>();
        }

        public User GetUser(string userId)
        {
            if (string.IsNullOrEmpty(userId) || !_userRepositories.ContainsKey(userId))
            {
                throw new DataNotFoundException("User is not found");
            }
            return _userRepositories[userId];
        }


        public User AddUser(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException();
            }

            if (_userRepositories.ContainsKey(user.Id))
            {
                throw new DuplicateDataException("User is duplicate");
            }

            _userRepositories.Add(user.Id, user);
            return user;
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException();
            }

            if (_userRepositories.ContainsKey(user.Id))
            {
                _userRepositories[user.Id] = user;
            }
            else
            {
                throw new DataNotFoundException("User is not found");
            }
        }

        public void DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId) || !_userRepositories.ContainsKey(userId))
            {
                throw new DataNotFoundException("User is not found");
            }
            _userRepositories.Remove(userId);
        }
    }
}
