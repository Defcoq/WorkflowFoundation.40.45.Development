using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Controls;

namespace LibraryReservation
{
    public class ListBoxTextWriter : TextWriter
    {
        const string textClosed = "This TextWriter must be opened before use";

        private Encoding _encoding;
        private bool _isOpen = false;
        private ListBox _listBox;

        public ListBoxTextWriter()
        {
            // Get the static list box
            _listBox = ApplicationInterface._app.GetEventListBox();
            if (_listBox != null)
                _isOpen = true;
        }

        public ListBoxTextWriter(ListBox listBox)
        {
            this._listBox = listBox;
            this._isOpen = true;
        }

        public override Encoding Encoding
        {
            get
            {

                if (_encoding == null)
                {
                    _encoding = new UnicodeEncoding(false, false);
                }
                return _encoding;
            }
        }

        public override void Close()
        {
            this.Dispose(true);
        }

        protected override void Dispose(bool disposing)
        {
            this._isOpen = false;
            base.Dispose(disposing);
        }

        public override string ToString()
        {
            return "";
        }

        public override void Write(char value)
        {
            if (!this._isOpen)
                throw new ApplicationException(textClosed); ;

            this._listBox.Dispatcher.BeginInvoke
                (new Action(() => this._listBox.Items.Add(value.ToString())));
        }

        public override void Write(string value)
        {
            if (!this._isOpen)
                throw new ApplicationException(textClosed); ;

            if (value != null)
                this._listBox.Dispatcher.BeginInvoke
                    (new Action(() => this._listBox.Items.Add(value)));
        }

        public override void Write(char[] buffer, int index, int count)
        {
            String toAdd = "";

            if (!this._isOpen)
                throw new ApplicationException(textClosed); ;

            if (buffer == null || index < 0 || count < 0)
                throw new ArgumentOutOfRangeException("buffer");


            if ((buffer.Length - index) < count)
                throw new ArgumentException("The buffer is too small");

            for (int i = 0; i < count; i++)
                toAdd += buffer[i];

            this._listBox.Dispatcher.BeginInvoke
                (new Action(() => this._listBox.Items.Add(toAdd)));
        }
    }
}
