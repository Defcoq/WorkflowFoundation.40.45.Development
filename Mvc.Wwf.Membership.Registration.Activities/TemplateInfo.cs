namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System;

    public class TemplateInfo : ITemplateInfo
    {
        #region Public Properties

        public bool Exists { get; set; }

        public DateTime LastWriteTime { get; set; }

        public string Path { get; set; }

        public string Template { get; set; }

        #endregion
    }
}