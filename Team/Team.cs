using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UDTProvider;

namespace Team
{
    public partial class Team : UserControl
    {
        private UDTProvider.UDTProvider _objUDTProvider;
        
        public UDTProvider.UDTProvider UDTProvider
        {
            get
            {
                return _objUDTProvider;
            }
            set
            {
                _objUDTProvider = value;
                _objUDTProvider.FilterChanged += OnFilterChangeEvent;
            }
        }
        private string _TeamType;

        private void OnFilterChangeEvent(string filterparam)
        {
            if (filterparam == "Active Match")
            {
                var activeMatch = _objUDTProvider.UdtFilters["Active Match"];
                DataRow[] dr = _objUDTProvider.CurrentDataSet.Tables[10].Select("Name = '" + activeMatch.FilterValue + "'");
                var HomeTeam = dr[0]["HomeTeam"].ToString();
                var AwayTeam = dr[0]["AwayTeam"].ToString();
                DataRow[] drPlayers;
                if (TeamType == "home")
                {
                    drPlayers = _objUDTProvider.CurrentDataSet.Tables[7].Select("Team = '" + HomeTeam + "' AND Playing='true'");
                }
                else
                {
                    drPlayers = _objUDTProvider.CurrentDataSet.Tables[7].Select("Team = '" + AwayTeam + "' AND Playing='true'");
                }
                for (int i = 0; i < drPlayers.Count(); i++)
                {
                    DataGridViewRow dgv = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    dgv.Cells[0].Value = drPlayers[i]["JerseyNo"].ToString();
                    dgv.Cells[1].Value = drPlayers[i]["Name"].ToString();
                    dgv.Cells[2].Value = drPlayers[i]["Name"].ToString();
                    dataGridView1.Rows.Add(dgv);
                }
            }
        }
        public string TeamType
        {
            get
            {
                return _TeamType;
            }
            set
            {
                _TeamType = value;
            }
        }
      

        public Team()
        {
            InitializeComponent();
          
           
        }
        public Team(UDTProvider.UDTProvider UDTProvider)
        {
            _objUDTProvider = UDTProvider;

            var activeMatch = _objUDTProvider.UdtFilters["Active Match"];
            DataRow[] dr = _objUDTProvider.CurrentDataSet.Tables[10].Select("Name = '" + activeMatch.FilterValue + "'");
            var HomeTeam = dr[0]["HomeTeam"].ToString();
            var AwayTeam = dr[0]["AwayTeam"].ToString();
            DataRow[] drPlayers;
            if (TeamType == "home")
            {
                drPlayers = _objUDTProvider.CurrentDataSet.Tables[7].Select("Team = '" + HomeTeam + "'");
            }
            else
            {
                drPlayers = _objUDTProvider.CurrentDataSet.Tables[7].Select("Team = '" + AwayTeam + "'");
            }
            for (int i = 0; i < drPlayers.Count(); i++)
            {
                DataGridViewRow dgv = new DataGridViewRow();
                dgv.Cells[0].Value = drPlayers[0]["JerseyNo"].ToString();
                dgv.Cells[1].Value = drPlayers[0]["Name"].ToString();
                dgv.Cells[2].Value = drPlayers[0]["Name"].ToString();
                dataGridView1.Rows.Add(dgv);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var column = senderGrid.Columns[e.ColumnIndex] as DataGridViewButtonColumn;
                switch(e.ColumnIndex)
                {
                    case 2: //Goals
                        (senderGrid.Columns[e.ColumnIndex] as DataGridViewButtonColumn).Text = "GOAL";
                        column.Selected = false;
                        string ColumnName = "";
                       
                        var activeMatch = _objUDTProvider.UdtFilters["Active Match"];
                        var currentMatchPart = _objUDTProvider.UdtFilters["Match Part"];
                        DataRow[] dr = _objUDTProvider.CurrentDataSet.Tables[10].Select("Name = '" + activeMatch.FilterValue + "'");
                        if (TeamType == "home")
                        {
                            ColumnName = "HomeScore";
                            TeamName = dr[0]["HomeTeam"].ToString();
                        }
                        else
                        {
                            ColumnName = "AwayScore";
                            TeamName = dr[0]["AwayTeam"].ToString();
                        }
                      
                        var HomeScore = dr[0][ColumnName];
                        int score = Convert.ToInt32(HomeScore) + 1;
                        _objUDTProvider.UpdateUDT(10, new string[] { ColumnName }, new string[] { score.ToString() }, "ID", dr[0]["ID"].ToString());
                        // Insert Into Match Events
                        _objUDTProvider.InsertUDTData(12, new string[] { "MatchID", "MatchPart", "Time", "EventType", "Team", "Player" }, new string[] { activeMatch.FilterValue, currentMatchPart.FilterValue,DateTime.Now.ToString(),"Goal",TeamName,"" });

                        break;
                    case 4: //Shots on Goal
                        break;
                    case 5: //foul
                        break;
                    case 7: //yellow
                        break;
                    case 8: //red
                        break;
                }
            }
            else if((senderGrid.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && e.RowIndex >= 0))
            {

            }
        }

        private void btnTeamList_Click(object sender, EventArgs e)
        {
           
                var activeMatch = _objUDTProvider.UdtFilters["Active Match"];
                DataRow[] dr = _objUDTProvider.CurrentDataSet.Tables[10].Select("Name = '" + activeMatch.FilterValue + "'");
                if (TeamType == "home")
                {
                    TeamName = dr[0]["HomeTeam"].ToString();
                }
                else
                {
                    TeamName = dr[0]["AwayTeam"].ToString();
                }
        
            TeamBuilderForm objTb = new TeamBuilderForm();
            objTb._objUDTProvider = _objUDTProvider;
            objTb.Team = TeamName;
            objTb.FIllTeam();
            objTb.ShowDialog();
            objTb = null;
        }

        public string TeamName { get; set; }
    }
}
