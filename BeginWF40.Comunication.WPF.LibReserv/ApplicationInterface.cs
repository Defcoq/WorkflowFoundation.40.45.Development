using System;
using System.Windows.Controls;
using System.Activities;
using BeginWF40.Comunication.WPF.LibReserv;

namespace LibraryReservation
{
    public static class ApplicationInterface
    {
        public static MainWindow _app { get; set; }

        public static void AddEvent(String status)
        {
            if (_app != null)
            {
                new ListBoxTextWriter(_app.GetEventListBox()).WriteLine(status);
            }
        }

        public static void RequestBook(ReservationRequest request)
        {
            if (_app != null)
                _app.RequestBook(request);
        }

        public static void RespondToRequest(ReservationResponse response)
        {
            if (_app != null)
                _app.RespondToRequest(response);
        }

        public static void NewRequest(ReservationRequest request)
        {
            if (_app != null)
                _app.AddNewRequest(request);
        }
    }
}
