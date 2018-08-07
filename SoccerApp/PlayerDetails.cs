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
    public partial class PlayerDetails : Form
    {
        public UDTProvider.UdtProvider ObjUdtprovider { get; set; }
        public string Team { get; set; }
        public string SelectedPlayer { get; set; }
        public string ParentName { get; set; }
        public bool IsTeamSelected = false;

        public object SelectedTeamId { get; set; }

        public object SelectedPlayerId { get; set; }

        public PlayerDetails()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            SetFormHeader();
            AutoCompleteStringCollection cmbstrName = new AutoCompleteStringCollection();
            AutoCompleteStringCollection cmbstrJer = new AutoCompleteStringCollection();
            DataRow drTeam = ObjUdtprovider.CurrentDataSet.Tables[2].Select("Name = '" + Team + "'").FirstOrDefault();
            if (drTeam != null)
            {
                SelectedTeamId = drTeam["T24_ID"];
                DataRow[] drPlayers = ObjUdtprovider.CurrentDataSet.Tables[3].Select("T24_ID = '" + drTeam["T24_ID"] + "' AND Playing=true");
                cmbPlayerJer.Items.Clear();
                cmbPlayerName.Items.Clear();
                foreach (DataRow item in drPlayers)
                {
                    cmbPlayerName.Items.Add(item["First Name"]);
                    cmbstrName.Add(item["First Name"].ToString());
                    cmbPlayerJer.Items.Add(item["Jersey No"]);
                    cmbstrJer.Add(item["Jersey No"].ToString());
                }
                cmbPlayerName.SelectedIndex = 1;
                cmbPlayerJer.SelectedIndex = 1;
                cmbPlayerName.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbPlayerName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbPlayerName.AutoCompleteCustomSource = cmbstrName;
                cmbPlayerJer.AutoCompleteMode = AutoCompleteMode.Suggest;
                cmbPlayerJer.AutoCompleteSource = AutoCompleteSource.CustomSource;
                cmbPlayerJer.AutoCompleteCustomSource = cmbstrJer;
            }
        }

        private void SetFormHeader()
        {
            switch (ParentName)
            {
                case "Foul":
                    this.Text = "Foul: Player Selection";
                    break;
                default:
                    break;
            }
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

        private void cmbPlayerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedPlayer = cmbPlayerName.Text;
            DataRow drPlayer = ObjUdtprovider.CurrentDataSet.Tables[3].Select("T24_ID = '" + SelectedTeamId + "' AND Playing=true AND [First Name]='" + SelectedPlayer + "'").FirstOrDefault();
            if (drPlayer != null)
            {
                SelectedPlayerId = drPlayer["T25_ID"];
            }
            cmbPlayerJer.SelectedIndex = cmbPlayerName.SelectedIndex;
        }

        private void cmbPlayerJer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPlayerName.SelectedIndex = cmbPlayerJer.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
