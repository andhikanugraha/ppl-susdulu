using SusDulu.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SusDulu.Helpers
{
    public class SusDuluMembershipProvider : MembershipProvider
    {
        private UsersContext context = new UsersContext();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            User user = context.GetUser(username, oldPassword);
            if (user != null)
            {
                user.SetUnhashedPassword(newPassword);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();

                return true;
            }
            else
            {
                throw new MembershipPasswordException("Wrong old password.");
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (GetUserNameByEmail(email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            var user = GetUser(username, true);

            if (user == null)
            {
                var userObj = new User(username, password);

                using (var usersContext = new UsersContext())
                {
                    context.Users.Add(userObj);
                }

                status = MembershipCreateStatus.Success;

                return WrapUser(userObj);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;

                return null;
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = context.GetUser(username);
            return WrapUser(user);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var user = context.GetUser(providerUserKey);
            return WrapUser(user);
        }

        private MembershipUser WrapUser(User user)
        {
            if (user != null)
            {
                return new MembershipUser("SusDuluMembershipProvider", user.Email, user.ID, user.Email,
                                        string.Empty, string.Empty,
                                        true, false, DateTime.MinValue,
                                        DateTime.MinValue,
                                        DateTime.MinValue,
                                        DateTime.Now, DateTime.Now);
            }
            else
            {
                return null;
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            var user = context.GetUser(email);
            if (user != null)
            {
                return user.Email;
            }
            else
            {
                return string.Empty;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            return context.ValidateUser(username, password);
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }
    }
}