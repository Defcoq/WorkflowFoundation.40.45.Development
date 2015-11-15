namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System;

    public interface ITemplateInfo
    {
        bool Exists { get; set; }

        DateTime LastWriteTime { get; set; }

        string Path { get; set; }

        string Template { get; set; }
    }
}