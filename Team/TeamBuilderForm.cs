using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Team
{
    public partial class TeamBuilderForm : Form
    {
        public TeamBuilderForm()
        {
            InitializeComponent();
            listBox1.AllowDrop = true;
            listBox2.AllowDrop = true;

            listBox1.MouseDown += listBox1_MouseDown;
            listBox2.MouseDown += listBox2_MouseDown;
            listBox1.DragOver += listBox1_DragOver;
            listBox2.DragOver += listBox2_DragOver;
            listBox1.DragDrop += listBox1_DragDrop;
            listBox2.DragDrop += listBox2_DragDrop;
        }

        void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string str = (string)e.Data.GetData(
                    DataFormats.StringFormat);
                listBox2.Items.Add(str);
            }
        }

        void listBox2_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox1.Items.Count == 0)
                return;
            int index = listBox1.IndexFromPoint(e.X, e.Y);
            string s = listBox1.Items[index].ToString();
            DragDropEffects dde1 = DoDragDrop(s,
                DragDropEffects.All);

            if (dde1 == DragDropEffects.All)
            {
                listBox1.Items.RemoveAt(listBox1.IndexFromPoint(e.X, e.Y));
            } 
        }

        void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string str = (string)e.Data.GetData(
                    DataFormats.StringFormat);

                listBox1.Items.Add(str);
            }
        }

        void listBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox2.Items.Count == 0)
                return;
            int index = listBox2.IndexFromPoint(e.X, e.Y);
            string s = listBox2.Items[index].ToString();
            DragDropEffects dde1 = DoDragDrop(s,
                DragDropEffects.All);

            if (dde1 == DragDropEffects.All)
            {
                listBox2.Items.RemoveAt(listBox2.IndexFromPoint(e.X, e.Y));
            } 
        }
        public TeamBuilderForm(UDTProvider.UDTProvider UDTProvider)
        {
            _objUDTProvider = UDTProvider;
            
        }
        public void FIllTeam()
        {
            DataRow[] dtTeams = _objUDTProvider.CurrentDataSet.Tables[7].Select("Team = '" + Team + "'");
            foreach (var item in dtTeams)
            {
                listBox1.Items.Add(item["Name"]);
            }

        }
        public UDTProvider.UDTProvider _objUDTProvider { get; set; }
        public string Team { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in listBox1.Items)
            {
                _objUDTProvider.UpdateUDT(7, new string[] { "Playing" }, new string[] { "false" }, "Name", item.ToString());
            }
            foreach (var item in listBox2.Items)
            {
                _objUDTProvider.UpdateUDT(7, new string[] { "Playing" }, new string[] { "true" }, "Name", item.ToString());
            }
        }
    }
}
