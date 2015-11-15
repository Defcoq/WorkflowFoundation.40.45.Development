namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System;
    using System.Activities;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Loads the mail message body using the HTML template and merges arguments
    /// </summary>
    public sealed class PrepareMailMessage : CodeActivity
    {
        #region Constants and Fields

        private const int CancelUrlIndex = 2;

        private const string CancelUrlToken = "{CancelUrl}";

        private const int InstanceIdIndex = 0;

        private const string InstanceIdToken = "{InstanceId}";

        private const int StylesUrlIndex = 3;

        private const string StylesUrlToken = "{StylesUrl}";

        private const int TemplateIndex = 4;

        private const string TemplateIndexToken = "{TemplateIndex}";

        private const int VerificationUrlIndex = 1;

        private const string VerificationUrlToken = "{VerificationUrl}";

        #endregion

        #region Public Properties

        [RequiredArgument]
        public InArgument<RegistrationData> Data { get; set; }

        [RequiredArgument]
        public InArgument<int> EmailTemplateIndex { get; set; }

        [DefaultValue(null)]
        public InArgument<Encoding> Encoding { get; set; }

        public OutArgument<string> MessageBody { get; set; }

        #endregion

        #region Methods

        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            // Require a "mockable" interface as an extension
            metadata.RequireExtension<ITemplateCache>();

            // If the host has not provided one, this will create a concrete implementation
            metadata.AddDefaultExtensionProvider(() => new EmailTemplates());
            base.CacheMetadata(metadata);
        }

        /// <summary>
        /// When implemented in a derived class, performs the execution of the activity.
        /// </summary>
        /// <param name="context">The execution context under which the activity executes.</param>
        protected override void Execute(CodeActivityContext context)
        {
            var data = this.Data.Get(context);
            ValidateData(data);

            var index = this.EmailTemplateIndex.Get(context);
            ValidateIndex(data, index);

            // Get the template cache extension
            var templateCache = context.GetExtension<ITemplateCache>();

            var encoding = this.Encoding.Get(context);

            var emailTemplate = encoding != null
                                    ? templateCache.Get(data.EmailTemplates.ElementAt(index), encoding)
                                    : templateCache.Get(data.EmailTemplates.ElementAt(index));

            ValidateEmailTemplate(emailTemplate);

            var args = new object[5];

            args[InstanceIdIndex] = context.WorkflowInstanceId.ToString();
            args[VerificationUrlIndex] = data.VerificationUrl;
            args[CancelUrlIndex] = data.CancelUrl;
            args[StylesUrlIndex] = data.StylesUrl;
            args[TemplateIndex] = this.EmailTemplateIndex.Get(context).ToString(CultureInfo.InvariantCulture);

            var body = data.BodyArguments != null
                           ? string.Format(emailTemplate, (object[])data.BodyArguments)
                           : string.Format(emailTemplate, new object[1]);

            this.MessageBody.Set(context, ReplaceTokens(body, args));
        }

        private static string ReplaceTokens(string source, IList<object> args)
        {
            return
                source.Replace(InstanceIdToken, (string)args[InstanceIdIndex]).Replace(
                    VerificationUrlToken, (string)args[VerificationUrlIndex]).Replace(
                        CancelUrlToken, (string)args[CancelUrlIndex]).Replace(
                            StylesUrlToken, (string)args[StylesUrlIndex]).Replace(
                                TemplateIndexToken, (string)args[TemplateIndex]);
        }

        private static void ValidateData(RegistrationData data)
        {
            if (data.EmailTemplates == null)
            {
                throw new InvalidOperationException("No email templates supplied");
            }
        }

        private static void ValidateEmailTemplate(string emailTemplate)
        {
            if (string.IsNullOrWhiteSpace(emailTemplate))
            {
                throw new InvalidOperationException("Email template is empty");
            }
        }

        private static void ValidateIndex(RegistrationData data, int index)
        {
            if (index < 0 || index > data.EmailTemplates.Count())
            {
                // ReSharper disable NotResolvedInText
                throw new ArgumentOutOfRangeException("EmailTemplateIndex");
                // ReSharper restore NotResolvedInText
            }
        }

        #endregion
    }
}