namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System.Text;

    public interface ITemplateCache
    {
        #region Public Methods and Operators

        void Clear();

        bool Contains(string path);

        string Get(string path);

        string Get(string path, Encoding encoding);

        #endregion
    }
}