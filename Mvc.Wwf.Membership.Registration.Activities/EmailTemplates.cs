namespace Mvc.Wwf.Membership.Registration.Activities
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class EmailTemplates : ITemplateCache
    {
        #region Constants and Fields

        private static readonly Dictionary<string, ITemplateInfo> CachedTemplates =
            new Dictionary<string, ITemplateInfo>();

        private static ITemplateReader reader;

        private static ITemplateCache singletonCache;

        #endregion

        #region Constructors and Destructors

        public EmailTemplates()
        {
            Reader = new TemplateReader();
        }

        public EmailTemplates(ITemplateReader templateReader)
        {
            Reader = templateReader;
        }

        #endregion

        #region Public Properties

        public static ITemplateCache Cache
        {
            get
            {
                return singletonCache ?? (singletonCache = new EmailTemplates());
            }
        }

        public static ITemplateReader Reader
        {
            get
            {
                return reader ?? (reader = new TemplateReader());
            }
            set
            {
                reader = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Clear()
        {
            CachedTemplates.Clear();
        }

        public bool Contains(string path)
        {
            return CachedTemplates.ContainsKey(path);
        }

        public string Get(string path)
        {
            return this.Get(path, null);
        }

        public string Get(string path, Encoding encoding)
        {
            // Get the template info
            var template = Reader.GetTemplateInfo(path);

            if (template == null || !template.Exists)
            {
                throw new FileNotFoundException("Cannot open email template");
            }

            // Refresh the template if needed
            if (!this.Contains(template.Path) || this.IsStale(template))
            {
                LoadAndStoreTemplate(template, encoding);
            }
            else
            {
                // Load the template from the cache
                template = CachedTemplates[template.Path];
            }

            return template.Template;
        }

        public bool IsStale(ITemplateInfo template)
        {
            var storedTemplate = CachedTemplates[template.Path];
            return template.LastWriteTime > storedTemplate.LastWriteTime;
        }

        #endregion

        #region Methods

        private static void LoadAndStoreTemplate(ITemplateInfo template, Encoding encoding = null)
        {
            template.Template = encoding != null ? Reader.Read(template.Path, encoding) : Reader.Read(template.Path);

            if (CachedTemplates.ContainsKey(template.Path))
            {
                CachedTemplates[template.Path] = template;
            }
            else
            {
                CachedTemplates.Add(template.Path, template);
            }
        }

        #endregion
    }
}