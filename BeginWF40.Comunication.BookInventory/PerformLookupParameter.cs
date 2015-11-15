using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace BookInventory
{

    public sealed class PerformLookupParameter : CodeActivity
    {
        // Define an activity input argument of type string
        public InArgument<String> Title { get; set; }
        public InArgument<String> Author { get; set; }
        public InArgument<String> ISBN { get; set; }
        public OutArgument<BookInfo[]> BookList { get; set; }


        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            // Obtain the runtime value of the Text input argument
            string author = Author.Get(context);
            string title = Title.Get(context);
            string isbn = ISBN.Get(context);
            BookInfo[] l = new BookInfo[4];
            l[0] = new BookInfo(title, author, isbn, "Available");
            l[1] = new BookInfo(title, author, isbn, "CheckedOut");
            l[2] = new BookInfo(title, author, isbn, "Missing");
            l[3] = new BookInfo(title, author, isbn, "Available");
            BookList.Set(context, l);

        }
    }
}
