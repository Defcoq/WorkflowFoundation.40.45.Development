using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace LibraryReservation
{
    /*****************************************************/
    // Define the Branch data structure
    /*****************************************************/
    public class Branch
    {
        public String BranchName { get; set; }
        public String Address { get; set; }
        public Guid BranchID { get; set; }

        #region Constructors
        public Branch()
        {
        }

        public Branch(String name, String address)
        {
            BranchName = name;
            Address = address;
            BranchID = Guid.NewGuid();
        }

        public Branch(String name, String address, Guid id)
        {
            BranchName = name;
            Address = address;
            BranchID = id;
        }

        public Branch(String name, String address, String id)
        {
            BranchName = name;
            Address = address;
            BranchID = new Guid(id);
        }
        #endregion Constructors
    }

    /*****************************************************/
    // Define the request message, ReservationRequest
    /*****************************************************/
    [MessageContract(IsWrapped = false)]
    public class ReservationRequest
    {
        private String _ISBN;
        private String _Title;
        private String _Author;
        private Guid _RequestID;
        private Branch _Requester;
        private Guid _InstanceID;

        #region Constructors
        public ReservationRequest()
        {
        }

        public ReservationRequest(String title, String author, String isbn,
            Branch requestor)
        {
            _Title = title;
            _Author = author;
            _ISBN = isbn;
            _Requester = requestor;
            _RequestID = Guid.NewGuid();
        }

        public ReservationRequest(String title, String author, String isbn,
            Branch requestor, Guid id)
        {
            _Title = title;
            _Author = author;
            _ISBN = isbn;
            _Requester = requestor;
            _RequestID = id;
        }
        #endregion Constructors

        #region Public Properties
        [MessageBodyMember]
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        [MessageBodyMember]
        public String ISBN
        {
            get { return _ISBN; }
            set { _ISBN = value; }
        }

        [MessageBodyMember]
        public String Author
        {
            get { return _Author; }
            set { _Author = value; }
        }

        [MessageBodyMember]
        public Guid RequestID
        {
            get { return _RequestID; }
            set { _RequestID = value; }
        }

        [MessageBodyMember]
        public Branch Requester
        {
            get { return _Requester; }
            set { _Requester = value; }
        }

        [MessageBodyMember]
        public Guid InstanceID
        {
            get { return _InstanceID; }
            set { _InstanceID = value; }
        }
        #endregion Public Properties
    }

    /*****************************************************/
    // Define the request message, ReservationResponse
    /*****************************************************/
    [MessageContract(IsWrapped = false)]
    public class ReservationResponse
    {
        private bool _Reserved;
        private Branch _Provider;
        private Guid _RequestID;

        #region Constructors
        public ReservationResponse()
        {
        }

        public ReservationResponse(ReservationRequest request, bool reserved,
            Branch provider)
        {
            _RequestID = request.RequestID;
            _Reserved = reserved;
            _Provider = provider;
        }
        #endregion Constructors

        #region Public Properties
        [MessageBodyMember]
        public bool Reserved
        {
            get { return _Reserved; }
            set { _Reserved = value; }
        }

        [MessageBodyMember]
        public Branch Provider
        {
            get { return _Provider; }
            set { _Provider = value; }
        }

        [MessageBodyMember]
        public Guid RequestID
        {
            get { return _RequestID; }
            set { _RequestID = value; }
        }
        #endregion Public Properties
    }

    /*****************************************************/
    // Define the service contract, ILibraryReservation
    // which consists of two methods, RequestBook() and
    // RespondToRequest()
    /*****************************************************/
    [ServiceContract]
    public interface ILibraryReservation
    {
       [OperationContract(IsOneWay = true)]
        void RequestBook(ReservationRequest request);

      [OperationContract(IsOneWay = true)]
        void RespondToRequest(ReservationResponse response);
    }
}
