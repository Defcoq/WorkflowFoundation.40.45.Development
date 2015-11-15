using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using LeadGenerator.Extensions;

namespace LeadGenerator.Activities
{

    public sealed class AddComment : CodeActivity
    {
        public InArgument<string> Comment { get; set; }
        public OutArgument<string> Comments { get; set; }
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddDefaultExtensionProvider(() => new CommentExtension());
        }
        protected override void Execute(CodeActivityContext context)
        {
            CommentExtension ext = context.GetExtension<CommentExtension>();
            ext.AddComment(Comment.Get(context));
            Comments.Set(context, ext.Comments);
        }




    }

    public sealed class GetComments : CodeActivity
    {
        public OutArgument<string> Comments { get; set; }
        protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);
            metadata.AddDefaultExtensionProvider(() => new CommentExtension());
        }
        protected override void Execute(CodeActivityContext context)
        {
            CommentExtension ext = context.GetExtension<CommentExtension>();
            Comments.Set(context, ext.Comments);
        }
    }

}
