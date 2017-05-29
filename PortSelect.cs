using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GSMTemperature
{
    public partial class PortSelect : Form
    {
        public delegate void DlgPortChanged(string port);
        public DlgPortChanged OnPortChanged;

        public PortSelect()
        {
            InitializeComponent();
        }

        private void PortSelect_Load(object sender, EventArgs e)
        {
            listPorts.Items.Clear();

            var ports = TempListener.EnumPorts();

            foreach (var port in ports)
            {
                listPorts.Items.Add(new ListViewItem(port));
            }

            if (ConfigurationManager.AppSettings.ContainsKey("ComPort"))
            {
                var comPort = ConfigurationManager.AppSettings["ComPort"];
                foreach (var item in listPorts.Items.Cast<ListViewItem>())
                {
                    if (item.Text == comPort)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        item.Selected = false;
                    }
                }
            }

            if (ConfigurationManager.AppSettings.ContainsKey("PhoneNumber"))
            {
                textPhone.Text = ConfigurationManager.AppSettings["PhoneNumber"];
            }
        }

        private void menuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (listPorts.SelectedIndices.Count > 0)
            {
                if (OnPortChanged != null)
                {
                    OnPortChanged(listPorts.Items[listPorts.SelectedIndices[0]].Text);
                }
            }

            ConfigurationManager.AppSettings["PhoneNumber"] = textPhone.Text;
            ConfigurationManager.Save();

            Close();
        }
    }
}