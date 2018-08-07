using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoccerApp
{
    public partial class TeamSelection : Form
    {
        public string SelectedTeam { get; set; }
        public bool IsTeamSelected = false;
        public object SelectedTeamId { get; set; }
        public UDTProvider.UdtProvider ObjUdtprovider { get; set; }

        public TeamSelection()
        {
            InitializeComponent();
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTeam = cmbTeam.Text;
            DataRow drTeam = ObjUdtprovider.CurrentDataSet.Tables[2].Select("Name = '" + SelectedTeam + "'").FirstOrDefault();
            if (drTeam != null)
            {
                SelectedTeamId = drTeam["T24_ID"];
            }
            if (!string.IsNullOrEmpty(cmbTeam.Text))
            {
                IsTeamSelected = true;
            }
            else
            {
                IsTeamSelected = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
