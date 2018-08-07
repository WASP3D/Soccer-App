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
    public partial class Substitution : Form
    {
        public UDTProvider.UdtProvider ObjUdtprovider { get; set; }
        public string Team { get; set; }
        public string Hometeam { get; set; }
        public string Awayteam { get; set; }
        public string SelectedInPlayer { get; set; }
        public string SelectedOutPlayer { get; set; }
        public bool IsTeamSelected = false;

        public object SelectedTeamId { get; set; }

        public object SelectedInPlayerId { get; set; }

        public object SelectedOutPlayerId { get; set; }

        public Substitution()
        {
            InitializeComponent();
        }
        
        public void Initialize()
        {
            AutoCompleteStringCollection cmbstrName = new AutoCompleteStringCollection();
            AutoCompleteStringCollection cmbstrJer = new AutoCompleteStringCollection();
            DataRow drTeam = ObjUdtprovider.CurrentDataSet.Tables[2].Select("Name = '" + Team + "'").FirstOrDefault();
            if (drTeam != null)
            {
                SelectedTeamId = drTeam["T24_ID"];
                DataRow[] drPlayers = ObjUdtprovider.CurrentDataSet.Tables[3].Select("T24_ID = '" + drTeam["T24_ID"] + "' AND Playing=true");
                cmbIn.Items.Clear();
                cmbOut.Items.Clear();
                cmbJerIN.Items.Clear();
                cmbJerOUT.Items.Clear();
                foreach (DataRow item in drPlayers)
                {
                    cmbIn.Items.Add(item["First Name"]);
                    cmbstrName.Add(item["First Name"].ToString());
                    cmbOut.Items.Add(item["First Name"]);
                    cmbJerIN.Items.Add(item["Jersey No"]);
                    cmbJerOUT.Items.Add(item["Jersey No"]);
                    cmbstrJer.Add(item["Jersey No"].ToString());
                }
                cmbIn.SelectedIndex = 1;
                cmbOut.SelectedIndex = 1;
                cmbJerIN.SelectedIndex = 1;
                cmbJerOUT.SelectedIndex = 1;

                cmbIn.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbIn.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbOut.AutoCompleteCustomSource = cmbstrName;
                cmbOut.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbOut.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbOut.AutoCompleteCustomSource = cmbstrName;
                cmbJerIN.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbJerIN.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbJerIN.AutoCompleteCustomSource = cmbstrJer;
                cmbJerOUT.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbJerOUT.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbJerOUT.AutoCompleteCustomSource = cmbstrJer;
                if (cmbTeam.Items.Count <= 0)
                {
                    cmbTeam.Items.Add(Hometeam);
                    cmbTeam.Items.Add(Awayteam);
                }
            }

        }

        private void cmbJerOUT_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbOut.SelectedIndex = cmbJerOUT.SelectedIndex;
        }
        private void cmbOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedOutPlayer = cmbOut.Text;
            DataRow drPlayer = ObjUdtprovider.CurrentDataSet.Tables[3].Select("T24_ID = '" + SelectedTeamId + "' AND Playing=true AND [First Name]='" + SelectedOutPlayer + "'").FirstOrDefault();
            if (drPlayer != null)
            {
                SelectedOutPlayerId = drPlayer["T25_ID"];
            }
        }

        private void cmbJerIN_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbIn.SelectedIndex = cmbJerIN.SelectedIndex;
        }

        private void cmbIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbJerIN.SelectedIndex = cmbIn.SelectedIndex;
            SelectedInPlayer = cmbIn.Text;
            DataRow drPlayer = ObjUdtprovider.CurrentDataSet.Tables[3].Select("T24_ID = '" + SelectedTeamId + "' AND Playing=true AND [First Name]='" + SelectedInPlayer + "'").FirstOrDefault();
            if (drPlayer != null)
            {
                SelectedInPlayerId = drPlayer["T25_ID"];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Team = cmbTeam.Text;
            Initialize();
            if (!string.IsNullOrEmpty(cmbTeam.Text))
            {
                IsTeamSelected = true;
            }
            else
            {
                IsTeamSelected = false;
            }
        }
    }
}
