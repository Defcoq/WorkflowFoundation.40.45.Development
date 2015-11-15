using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace BookInventory
{
    /*****************************************************/
    // Define the service contract, IBookInventory
    // which consists of a single method, LookupBook() 
    /*****************************************************/
    [ServiceContract]
    public interface IBookInventory
    {
        [OperationContract]
        BookInfoList LookupBook(BookSearch request);
    }

    /*****************************************************/
    // Define the request message, BookSearch
    /*****************************************************/
    [MessageContract(IsWrapped = false)]
    public class BookSearch
    {
        private String _ISBN;
        private String _Title;
        private String _Author;

        public BookSearch()
        {
        }

        public BookSearch(String title, String author, String isbn)
        {
            _Title = title;
            _Author = author;
            _ISBN = isbn;
        }

        #region Public Properties
        [MessageBodyMember]
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        [MessageBodyMember]
        public String Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        [MessageBodyMember]
        public String ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; }
        }
        #endregion Public Properties
    }

    /*****************************************************/
    // Define the BookInfo class
    /*****************************************************/
    [MessageContract(IsWrapped = false)]
    public class BookInfo
    {
        private Guid _InventoryID;
        private String _ISBN;
        private String _Title;
        private String _Author;
        private String _Status;

        public BookInfo()
        {
        }

        public BookInfo(String title, String author, String isbn,
            String status)
        {
            _Title = title;
            _Author = author;
            _ISBN = isbn;
            _Status = status;
            _InventoryID = Guid.NewGuid();
        }

        #region Public Properties
        [MessageBodyMember]
        public Guid InventoryID
        {
            get { return _InventoryID; }
            set { _InventoryID = value; }
        }

        [MessageBodyMember]
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        [MessageBodyMember]
        public String Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        [MessageBodyMember]
        public String ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; }
        }

        [MessageBodyMember]
        public String status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        #endregion Public Properties
    }

    /*****************************************************/
    // Define the response message, BookInfoList, which
    // is a list of BookInfo classes
    /*****************************************************/
    [MessageContract(IsWrapped = false)]
    public class BookInfoList
    {
        private List<BookInfo> _BookList;

        public BookInfoList()
        {
            _BookList = new List<BookInfo>();
        }

        [MessageBodyMember]
        public List<BookInfo> BookList
        {
            get { return _BookList; }
        }
    }
}
