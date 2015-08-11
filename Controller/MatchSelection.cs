using System;
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
            DataRow[] dr = _objUDT.CurrentDataSet.Tables[10].Select("Active=true");
            if(dr.Count()>0)
            {
                int index = listBox1.FindString(dr[0]["Name"].ToString());
                if (index != -1)
                {
                    listBox1.SetSelected(index, true);
                    UdtFilter filter = new UdtFilter();
                    filter.FilterColumn = "Name";
                    filter.FilterValue = listBox1.Text;
                    filter.TableIndex = 10;
                    if (!_objController.Udt.UdtFilters.ContainsKey("Active Match"))
                        _objController.Udt.UdtFilters.Add("Active Match", filter);
                    else
                        _objController.Udt.UdtFilters["Active Match"] = filter;
                    _objController.Udt.Notify("Active Match");
                }

            }
               
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            _objController.selectedMatch = listBox1.Text;

            UdtFilter filter= new UdtFilter();
            filter.FilterColumn = "Name";
            filter.FilterValue = listBox1.Text;
            filter.TableIndex = 10;
            if (!_objController.Udt.UdtFilters.ContainsKey("Active Match"))
                _objController.Udt.UdtFilters.Add("Active Match", filter);
            else
                _objController.Udt.UdtFilters["Active Match"] = filter;

            foreach (DataRowView item in listBox1.Items)
            {
                System.Diagnostics.Trace.WriteLine(item["Name"].ToString());

                if (item["Name"].ToString() == listBox1.Text)
                    _objUDT.UpdateUDT(10, new string[] { "Active" }, new string[] { "true" }, "Name", listBox1.Text);
                else
                    _objUDT.UpdateUDT(10, new string[] { "Active" }, new string[] { "false" }, "Name", item["Name"].ToString());
            }

            _objUDT.Notify("Active Match");
            
            this.Close();
        }
    }
}
