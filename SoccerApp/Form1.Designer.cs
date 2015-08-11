namespace SoccerApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UDTProvider.UDTProvider udtProvider1 = new UDTProvider.UDTProvider();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
          //  BeeSys.Wasp.KernelController.CUdtArgs cUdtArgs1 = new BeeSys.Wasp.KernelController.CUdtArgs();
            this.match1 = new Controller.Match();
            this.team1 = new Team.Team();
            this.team2 = new Team.Team();
            this.SuspendLayout();
            // 
            // match1
            // 
            this.match1.AwayTeamGoal = 0;
            this.match1.HomeTeamGoal = 0;
            this.match1.Location = new System.Drawing.Point(1, 12);
            this.match1.Name = "match1";
            this.match1.selectedMatch = null;
            this.match1.selectedMatchPart = null;
            this.match1.Size = new System.Drawing.Size(1279, 92);
            this.match1.TabIndex = 0;
            /*cUdtArgs1.ACTIONNAME = null;
            cUdtArgs1.DATA = resources.GetString("cUdtArgs1.DATA");
            cUdtArgs1.DataXmlVersion = "0";
            cUdtArgs1.DATE = null;
            cUdtArgs1.FORMAT = resources.GetString("cUdtArgs1.FORMAT");
            cUdtArgs1.ID = "06ee4e8b-f20f-4b61-8c60-2a4478d19f01";
            cUdtArgs1.IsInsert = false;
            cUdtArgs1.Lock = false;
            cUdtArgs1.NAME = "Soccer";
            cUdtArgs1.OldUdtId = null;
            cUdtArgs1.Published = false;
            cUdtArgs1.SessionId = null;
            cUdtArgs1.TableFormat = resources.GetString("cUdtArgs1.TableFormat");
            cUdtArgs1.UDTTABLE = null;
            cUdtArgs1.USER = null;
            cUdtArgs1.XsdVersion = "0";
            udtProvider1.CurrentUDT = cUdtArgs1;*/
            this.match1.Udt = udtProvider1;
            // 
            // team1
            // 
            this.team1.Location = new System.Drawing.Point(12, 127);
            this.team1.Name = "team1";
            this.team1.Size = new System.Drawing.Size(654, 358);
            this.team1.TabIndex = 1;
            this.team1.TeamType = null;
            // 
            // team2
            // 
            this.team2.Location = new System.Drawing.Point(689, 145);
            this.team2.Name = "team2";
            this.team2.Size = new System.Drawing.Size(654, 358);
            this.team2.TabIndex = 2;
            this.team2.TeamType = null;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1426, 539);
            this.Controls.Add(this.team2);
            this.Controls.Add(this.team1);
            this.Controls.Add(this.match1);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controller.Match controller1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controller.Match match1;
        private Team.Team team1;
        private Team.Team team2;
       


    }
}

