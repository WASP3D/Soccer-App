using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDTProvider;

namespace Controller
{
    public partial class MatchPart : Form
    {
        private UDTProvider.UDTProvider _objUDT;
        private Match _objController;

        public MatchPart(Match objController)
        {
            InitializeComponent();
            _objController = objController;
           _objUDT = objController.Udt;
            DataSet dt = _objController.Udt.CurrentDataSet;
            var t = dt.Tables[11];
            listBox1.DataSource = t;
            listBox1.DisplayMember = "Name";
            listBox1.ValueMember = "ID";
            DataRow[] dr = _objUDT.CurrentDataSet.Tables[11].Select("Active=true");
            if (dr.Count() > 0)
            {
                int index = listBox1.FindString(dr[0]["Name"].ToString());
                if (index != -1)
                {
                    UdtFilter filter = new UdtFilter();
                    filter.FilterColumn = "Name";
                    filter.FilterValue = listBox1.Text;
                    filter.TableIndex = 11;
                    if (!_objController.Udt.UdtFilters.ContainsKey("Match Part"))
                        _objController.Udt.UdtFilters.Add("Match Part", filter);
                    else
                        _objController.Udt.UdtFilters["Match Part"] = filter;
                    _objController.Udt.Notify("Match Part");
                    listBox1.SetSelected(index, true);
                }

            }
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            _objController.selectedMatchPart = listBox1.Text;
            UdtFilter filter = new UdtFilter();
            filter.FilterColumn = "Name";
            filter.FilterValue = listBox1.Text;
            filter.TableIndex = 11;
            if (!_objController.Udt.UdtFilters.ContainsKey("Match Part"))
                _objController.Udt.UdtFilters.Add("Match Part", filter);
            else
                _objController.Udt.UdtFilters["Match Part"] = filter;

            foreach (DataRowView item in listBox1.Items)
            {
                System.Diagnostics.Trace.WriteLine(item["Name"].ToString());

                if (item["Name"].ToString() == listBox1.Text)
                    _objUDT.UpdateUDT(11, new string[] { "Active" }, new string[] { "true" }, "Name", listBox1.Text);
                else
                    _objUDT.UpdateUDT(11, new string[] { "Active" }, new string[] { "false" }, "Name", item["Name"].ToString());
            }

            this.Close();
        }
    }
}
