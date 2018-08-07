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
    public partial class Player : Form
    {
        public UDTProvider.UdtProvider ObjUdtprovider { get; set; }
        public string Team { get; set; }
        public string SelectedPlayer { get; set; }

        public object SelectedTeamId { get; set; }

        public object SelectedPlayerId { get; set; }

        public bool IsPlayerSelected = false;

        public Player()
        {
            InitializeComponent();
        }

        public void FillTeam()
        {
            DataRow drTeam = ObjUdtprovider.CurrentDataSet.Tables[2].Select("Name = '" + Team+"'").FirstOrDefault();
            if (drTeam != null)
            {
                SelectedTeamId = drTeam["T24_ID"];
                DataRow[] drPlayers = ObjUdtprovider.CurrentDataSet.Tables[3].Select("T24_ID = '" + drTeam["T24_ID"] + "' AND Playing=true");
                foreach (var item in drPlayers)
                {
                    lstPlayers.Items.Add(item["First Name"]);
                }
            }
        }

        private void lstPlayers_DoubleClick(object sender, EventArgs e)
        {
            SelectedPlayer = lstPlayers.SelectedItem.ToString();
            DataRow drPlayer = ObjUdtprovider.CurrentDataSet.Tables[3].Select("T24_ID = '" +SelectedTeamId + "' AND Playing=true AND [First Name]='" + SelectedPlayer + "'").FirstOrDefault();
            if(drPlayer!=null)
            {
                SelectedPlayerId = drPlayer["T25_ID"];
            }
            if (!string.IsNullOrEmpty(lstPlayers.SelectedItem.ToString()))
            {
                IsPlayerSelected = true;
            }
            else
            {
                IsPlayerSelected = false;
            }
            this.Close();
        }
    }
}
