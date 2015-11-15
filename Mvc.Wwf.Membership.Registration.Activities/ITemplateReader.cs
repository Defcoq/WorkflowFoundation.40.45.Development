namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System.Text;

    public interface ITemplateReader
    {
        #region Public Methods and Operators

        ITemplateInfo GetTemplateInfo(string path);

        string Read(string path);

        string Read(string path, Encoding encoding);

        #endregion
    }
}