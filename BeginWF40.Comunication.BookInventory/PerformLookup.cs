using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace BookInventory
{
    /*****************************************************/
    // This custom activity creates a BookInfoList class
    // which is a collection of BookInfo classes. It uses
    // the input parameters (BookSearch class) to "lookup"
    // the matching items. The BookInfoList class is
    // returned in the output parameter.
    /*****************************************************/

    public sealed class PerformLookup : CodeActivity
    {
        public InArgument<BookSearch> Search { get; set; }
        public OutArgument<BookInfoList> BookList { get; set; }

        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            string author = Search.Get(context).Author;
            string title = Search.Get(context).Title;
            string isbn = Search.Get(context).ISBN;
            BookInfoList l = new BookInfoList();
            l.BookList.Add(new BookInfo(title, author, isbn, "Available"));
            l.BookList.Add(new BookInfo(title, author, isbn, "CheckedOut"));
            l.BookList.Add(new BookInfo(title, author, isbn, "Missing"));
            l.BookList.Add(new BookInfo(title, author, isbn, "Available"));
            BookList.Set(context, l);

        }
    }
}
