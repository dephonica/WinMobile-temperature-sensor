using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace GSMTemperature
{
    class TempListener : IDisposable
    {
        public delegate void DlgOnDataReceived(string data);
        public DlgOnDataReceived OnDataReceived;

        private SerialPort _port = null;
        private string _activePort;

        private readonly StringBuilder _serialBuffer = new StringBuilder();

        public TempListener()
        {
        }

        public void Dispose()
        {
            ClosePort();
        }

        public static string[] EnumPorts()
        {
            var portList = SerialPort.GetPortNames().ToList();
            portList.Sort();
            return portList.ToArray();
        }

        public string ActivePort
        {
            get
            {
                return _activePort;
            }

            set
            {
                _activePort = value;
                ReopenPort();
            }
        }

        private void ClosePort()
        {
            try
            {
                if (_port != null)
                {
                    _port.Dispose();
                }
            }
            catch
            {
                // ignore
            }

            _port = null;
        }

        private void OpenPort(string portName)
        {
            if (_port != null)
            {
                ClosePort();
            }

            try
            {
                _port = new SerialPort(portName, 9600, Parity.None, 8, StopBits.One);
                _port.DataReceived += new SerialDataReceivedEventHandler(_port_DataReceived);
                _port.Open();
            }
            catch
            {
                try
                {
                    _port.Dispose();
                }
                catch
                {
                    // ignore
                }

                _port = null;
            }
        }

        void _port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var serialIn = _port.ReadExisting();

            if (OnDataReceived != null)
            {
                foreach (var c in serialIn)
                {
                    if (c == 0x0a || c == 0x0d)
                    {
                        if (_serialBuffer.Length > 0)
                        {
                            OnDataReceived(_serialBuffer.ToString());
                        }

                        _serialBuffer.Length = 0;
                    }
                    else
                    {
                        _serialBuffer.Append(c);
                    }
                }
            }
        }

        public void ReopenPort()
        {
            ClosePort();
            OpenPort(_activePort);
        }
    }
}
