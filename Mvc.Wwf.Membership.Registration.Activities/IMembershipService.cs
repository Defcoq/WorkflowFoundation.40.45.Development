namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System.Web.Security;

    public interface IMembershipService
    {
        #region Public Methods and Operators

        void Approve(string userName);

        void CreateUser(
            string userName,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            out MembershipCreateStatus status);

        void Delete(string userName);

        MembershipUser GetUser(string name, bool userIsOnline);

        bool ValidateUser(string userName, string password);

        #endregion
    }
}