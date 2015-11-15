using System;
using System.ServiceModel;

namespace LibraryReservation
{
    public class ClientService : ILibraryReservation
    {
        public void RequestBook(ReservationRequest request)
        {
            ApplicationInterface.RequestBook(request);
        }

        public void RespondToRequest(ReservationResponse response)
        {
            ApplicationInterface.RespondToRequest(response);
        }
    }
}
