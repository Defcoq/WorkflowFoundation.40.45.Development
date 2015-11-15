namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System;
    using System.Web.Security;

    public class MembershipService : IMembershipService
    {
        #region Public Methods and Operators

        public void Approve(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user == null)
            {
                throw new ArgumentOutOfRangeException("userName");
            }

            user.IsApproved = true;
            Membership.UpdateUser(user);
        }

        public void CreateUser(
            string userName,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            out MembershipCreateStatus status)
        {
            Membership.CreateUser(
                userName, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }

        public void Delete(string userName)
        {
            Membership.DeleteUser(userName);
        }

        public MembershipUser GetUser(string name, bool userIsOnline)
        {
            return Membership.GetUser(name, userIsOnline);
        }

        public bool ValidateUser(string userName, string password)
        {
            return Membership.ValidateUser(userName, password);
        }

        #endregion
    }
}