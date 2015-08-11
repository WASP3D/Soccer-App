using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controller
{
    public partial class Match : UserControl
    {
        public UDTProvider.UDTProvider Udt { get; set; }
        public string selectedMatch { get; set; }
        public string selectedMatchPart { get; set; }
        public int HomeTeamGoal { get;set;}
        public int AwayTeamGoal { get; set; }

        public Match()
        {
            InitializeComponent();
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MatchSelection mc = new MatchSelection(this);
            mc.ShowDialog();
            mc = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MatchPart mp = new MatchPart(this);
            mp.ShowDialog();
            mp = null;
        }
        private void UpdateScoreinUDT(int HomeScore,int AwayScore)
        {
            
        }
       
    }
}
