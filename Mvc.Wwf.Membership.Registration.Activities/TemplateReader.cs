namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System.IO;
    using System.Text;

    public class TemplateReader : ITemplateReader
    {
        #region Public Methods and Operators

        public ITemplateInfo GetTemplateInfo(string path)
        {
            var fileInfo = new FileInfo(path);
            return new TemplateInfo
                { Exists = fileInfo.Exists, LastWriteTime = fileInfo.LastWriteTime, Path = Path.GetFullPath(path) };
        }

        public string Read(string path)
        {
            return Read(path, null);
        }

        public string Read(string path, Encoding encoding)
        {
            return encoding != null ? File.ReadAllText(path, encoding) : File.ReadAllText(path);
        }

        #endregion
    }
}