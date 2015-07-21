﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using UDTProvider;

namespace Controller
{
    public partial class MatchSelection : Form
    {
        private UDTProvider.UDTProvider _objUDT;
        private Match _objController;
        public MatchSelection( Match objController)
        {
            InitializeComponent();
            _objController = objController;
            _objUDT = objController.Udt;
            DataSet dt = _objController.Udt.CurrentDataSet;
            var t = dt.Tables[10];
            listBox1.DataSource = t;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "ID";
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            _objController.selectedMatch = listBox1.Text;

            UdtFilter filter= new UdtFilter();
            filter.FilterColumn = "Name";
            filter.FilterValue = listBox1.Text;
            filter.TableIndex = 10;
            _objController.Udt.UdtFilters.Add("Active Match",filter);
            this.Close();
        }
    }
}
