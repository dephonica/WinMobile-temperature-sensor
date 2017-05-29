using Win32;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Globalization;
using Microsoft.WindowsMobile.Status;
using Microsoft.WindowsMobile.PocketOutlook;
using System.Runtime.InteropServices;

namespace GSMTemperature
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// This function resets a system timer that controls whether or not the
        /// device will automatically go into a suspended state.
        /// </summary>
        [DllImport("CoreDll.dll")]
        public static extern void SystemIdleTimerReset();

        [DllImport("CoreDLL")]
        public static extern int GetSystemPowerStatusEx2(
             SYSTEM_POWER_STATUS_EX2 statusInfo,
             int length,
             int getLatest
          );

        private const int MaxPowerOffPeriodSeconds = 10;

        private readonly TempListener _tempListener = new TempListener();
        
        private readonly Timer _powerCheckTimer = new Timer();
        private DateTime _lastPowerSupplyDetected = DateTime.Now;
        private bool _isExternalPowerOn = true;

        private DateTime _lastTemperatureReceived = DateTime.MinValue;

        private double _tempMin = 999999, _tempMax = -999999, _tempAvg = 0, _tempCur = 0;
        private int _tempAvgCount = 0;

        private DateTime _nextTemperatureSend = DateTime.Now + TimeSpan.FromHours(6);
        private DateTime _lastTemperatureAlert = DateTime.MinValue;

        private bool _isSensorActive = true;

        private Timer _sensorReconnectTimer = new Timer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DisableDeviceSleep();

            _powerCheckTimer.Interval = 1000;
            _powerCheckTimer.Tick += new EventHandler(_powerCheckTimer_Tick);
            _powerCheckTimer.Enabled = true;

            _tempListener.OnDataReceived = SerialDataReceived;

            _sensorReconnectTimer.Interval = 10000;
            _sensorReconnectTimer.Tick += new EventHandler(_sensorReconnectTimer_Tick);
            _sensorReconnectTimer.Enabled = false;

            if (ConfigurationManager.AppSettings.ContainsKey("ComPort"))
            {
                _tempListener.ActivePort = ConfigurationManager.AppSettings["ComPort"];
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            EnableDeviceSleep();
            _powerCheckTimer.Dispose();

            base.OnClosing(e);

            _tempListener.Dispose();
        }

        public void Update(Delegate callback)
        {
            if (IsDisposed)
                return;

            try
            {
                if (InvokeRequired)
                    Invoke(callback);
                else
                    callback.Method.Invoke(this, null);
            }
            catch
            {
                // ignore
            }
        }

        void _sensorReconnectTimer_Tick(object sender, EventArgs e)
        {
            _tempListener.ReopenPort();
        }

        private void SendTemperatures()
        {
            SendMessage(string.Format("Min: {0:F2}, Max: {1:F2}, Avg: {2:F2}, Cur: {3:F2}",
                _tempMin, _tempMax, _tempAvg / _tempAvgCount, _tempCur));

            _tempMin = 999999;
            _tempMax = -999999;
            _tempAvg = 0;
            _tempCur = 0;
            _tempAvgCount = 0;
        }

        public static SYSTEM_POWER_STATUS_EX2 GetSystemPowerStatus()
        {
            SYSTEM_POWER_STATUS_EX2 retVal = new SYSTEM_POWER_STATUS_EX2();
            int result = GetSystemPowerStatusEx2(retVal, Marshal.SizeOf(retVal), 0);
            return retVal;
        }

        void _powerCheckTimer_Tick(object sender, EventArgs e)
        {
            // Sensor states handling
            if (DateTime.Now - _lastTemperatureReceived > TimeSpan.FromSeconds(10))
            {
                labelActive.Text = "Sensor: not active";

                if (_isSensorActive)
                {
                    _sensorReconnectTimer.Enabled = true;
                    SendMessage("Connection with sensor was lost");
                }

                _isSensorActive = false;
            }
            else
            {
                labelActive.Text = "Sensor: active";
                labelLastActive.Text = _lastTemperatureReceived.ToShortDateString() + " " +
                    _lastTemperatureReceived.ToShortTimeString();

                if (_isSensorActive == false)
                {
                    _sensorReconnectTimer.Enabled = false;
                    SendMessage("Connection with sensor is restored");
                }

                _isSensorActive = true;
            }

            // Warning about temperature is too low
            if (_tempCur < 15.0 && 
                DateTime.Now - _lastTemperatureAlert > TimeSpan.FromMinutes(30) &&
                _isSensorActive)
            {
                _lastTemperatureAlert = DateTime.Now;
                SendMessage("Temperature too low: " + _tempCur + " oC");
            }

            // Periodical temperature statistics
            if (DateTime.Now > _nextTemperatureSend)
            {
                _nextTemperatureSend = DateTime.Now + TimeSpan.FromHours(6);
                SendTemperatures();
            }

            // Handle AC power states
            var sysPowerStatus = GetSystemPowerStatus();

            if (sysPowerStatus.ACLineStatus == ACLineStatus.AC_LINE_ONLINE)
            {
                _lastPowerSupplyDetected = DateTime.Now;
                labelPower.Text = "Power: AC";
            }
            else
            {
                labelPower.Text = "Power: accumulator";
            }

            if (DateTime.Now - _lastPowerSupplyDetected > 
                TimeSpan.FromSeconds(MaxPowerOffPeriodSeconds))
            {
                if (_isExternalPowerOn)
                {
                    _isExternalPowerOn = false;
                    SendMessage("AC power was lost");
                }
            }
            else
            {
                if (_isExternalPowerOn == false)
                {
                    _isExternalPowerOn = true;
                    SendMessage("AC power is restored");
                }
            }
        }

        private void SerialDataReceived(string stringData)
        {
            Update(new EventHandler(delegate
                {
                    _lastTemperatureReceived = DateTime.Now;

                    try
                    {
                        var temperature = double.Parse(stringData, CultureInfo.InvariantCulture);
                        labelCurrentTemp.Text = temperature.ToString() + " oC";

                        if (temperature < _tempMin) _tempMin = temperature;
                        if (temperature > _tempMax) _tempMax = temperature;
                        _tempAvg += temperature;
                        _tempAvgCount++;

                        _tempCur = temperature;

                        labelMinTemp.Text = _tempMin.ToString() + " oC";
                        labelMaxTemp.Text = _tempMax.ToString() + " oC";
                    }
                    catch
                    {
                        // ignore
                    }
                }));
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            SendTemperatures();
        }

        private void SendMessage(string text)
        {
            if (ConfigurationManager.AppSettings.ContainsKey("PhoneNumber") == false)
            {
                return;
            }

            try
            {
                SmsMessage sms = new SmsMessage()
                {
                    Body = text
                };

                sms.To.Add(new Recipient(ConfigurationManager.AppSettings["PhoneNumber"]));
                sms.Send();
            }
            catch (Exception ex)
            {
                // ignore
                MessageBox.Show(ex.Message);
            }
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            var portSelectForm = new PortSelect();
            portSelectForm.OnPortChanged = OnPortChanged;
            portSelectForm.ShowDialog();
        }

        private void OnPortChanged(string newPort)
        {
            ConfigurationManager.AppSettings["ComPort"] = newPort;
            ConfigurationManager.Save();

            if (_tempListener.ActivePort != newPort)
            {
                _tempListener.ActivePort = newPort;
            }
        }

        private static int nDisableSleepCalls = 0;
        private static System.Threading.Timer preventSleepTimer = null;

        public static void DisableDeviceSleep()
        {
            nDisableSleepCalls++;
            if (nDisableSleepCalls == 1)
            {
                // start a 30-second periodic timer
                preventSleepTimer = new System.Threading.Timer(new System.Threading.TimerCallback(PokeDeviceToKeepAwake),
                    null, 0, 30 * 1000);
            }
        }

        public static void EnableDeviceSleep()
        {
            nDisableSleepCalls--;
            if (nDisableSleepCalls == 0)
            {
                if (preventSleepTimer != null)
                {
                    preventSleepTimer.Dispose();
                    preventSleepTimer = null;
                }
            }
        }

        private static void PokeDeviceToKeepAwake(object extra)
        {
            try
            {
                SystemIdleTimerReset();
            }
            catch
            {
                // ignore
            }
        }
    }
}