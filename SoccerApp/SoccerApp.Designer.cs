namespace SoccerApp
{
    partial class SoccerApp
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlbuttons = new System.Windows.Forms.Panel();
            this.BtnLoadBG = new System.Windows.Forms.Button();
            this.cmbMatchPart = new System.Windows.Forms.ComboBox();
            this.btnYellow = new System.Windows.Forms.Button();
            this.cmbMatch = new System.Windows.Forms.ComboBox();
            this.btnRed = new System.Windows.Forms.Button();
            this.btnCorner = new System.Windows.Forms.Button();
            this.btnSubstitute = new System.Windows.Forms.Button();
            this.btnShots = new System.Windows.Forms.Button();
            this.btnFoul = new System.Windows.Forms.Button();
            this.btnShotsOff = new System.Windows.Forms.Button();
            this.pnlteamcntrs = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlAwayTeam = new System.Windows.Forms.Panel();
            this.btnawayminus = new System.Windows.Forms.Button();
            this.btnawayplus = new System.Windows.Forms.Button();
            this.lblAwayScore = new System.Windows.Forms.Label();
            this.lblAwayTeam = new System.Windows.Forms.Label();
            this.pnlAwayFlag = new System.Windows.Forms.Panel();
            this.pnlHmTeam = new System.Windows.Forms.Panel();
            this.btnhomeplus = new System.Windows.Forms.Button();
            this.lblHomeTeam = new System.Windows.Forms.Label();
            this.pnlHomeFlag = new System.Windows.Forms.Panel();
            this.lblHomeScore = new System.Windows.Forms.Label();
            this.btnHomeminus = new System.Windows.Forms.Button();
            this.pnlcntr = new System.Windows.Forms.Panel();
            this.btnstartstop = new System.Windows.Forms.Button();
            this.lblCounter = new System.Windows.Forms.TextBox();
            this.pnlMatch = new System.Windows.Forms.Panel();
            this.lblMatchname = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbxEngines = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMatchevents = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dgvMatchevents = new System.Windows.Forms.DataGridView();
            this.SNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchPart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventMem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLabels = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelPlayers = new System.Windows.Forms.TableLayoutPanel();
            this.dgvSelectPlayer = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlbuttons.SuspendLayout();
            this.pnlteamcntrs.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.pnlAwayTeam.SuspendLayout();
            this.pnlHmTeam.SuspendLayout();
            this.pnlcntr.SuspendLayout();
            this.pnlMatch.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatchevents)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.pnlLabels.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlbuttons, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.pnlteamcntrs, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlMatch, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblMatchevents, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.349282F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.30622F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.54545F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.167464F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.200957F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.8134F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1506, 1045);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlbuttons
            // 
            this.pnlbuttons.Controls.Add(this.BtnLoadBG);
            this.pnlbuttons.Controls.Add(this.cmbMatchPart);
            this.pnlbuttons.Controls.Add(this.btnYellow);
            this.pnlbuttons.Controls.Add(this.cmbMatch);
            this.pnlbuttons.Controls.Add(this.btnRed);
            this.pnlbuttons.Controls.Add(this.btnCorner);
            this.pnlbuttons.Controls.Add(this.btnSubstitute);
            this.pnlbuttons.Controls.Add(this.btnShots);
            this.pnlbuttons.Controls.Add(this.btnFoul);
            this.pnlbuttons.Controls.Add(this.btnShotsOff);
            this.pnlbuttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlbuttons.Location = new System.Drawing.Point(3, 232);
            this.pnlbuttons.Name = "pnlbuttons";
            this.pnlbuttons.Padding = new System.Windows.Forms.Padding(5);
            this.pnlbuttons.Size = new System.Drawing.Size(1500, 47);
            this.pnlbuttons.TabIndex = 33;
            // 
            // BtnLoadBG
            // 
            this.BtnLoadBG.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BtnLoadBG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLoadBG.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLoadBG.Location = new System.Drawing.Point(1387, 2);
            this.BtnLoadBG.Name = "BtnLoadBG";
            this.BtnLoadBG.Size = new System.Drawing.Size(96, 33);
            this.BtnLoadBG.TabIndex = 25;
            this.BtnLoadBG.Text = "Load BG";
            this.BtnLoadBG.UseVisualStyleBackColor = false;
            this.BtnLoadBG.Click += new System.EventHandler(this.BtnLoadBG_Click);
            // 
            // cmbMatchPart
            // 
            this.cmbMatchPart.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbMatchPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMatchPart.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cmbMatchPart.ForeColor = System.Drawing.Color.Black;
            this.cmbMatchPart.FormattingEnabled = true;
            this.cmbMatchPart.Items.AddRange(new object[] {
            "First Half",
            "Second Half",
            "Extra Time"});
            this.cmbMatchPart.Location = new System.Drawing.Point(215, 5);
            this.cmbMatchPart.Name = "cmbMatchPart";
            this.cmbMatchPart.Size = new System.Drawing.Size(212, 27);
            this.cmbMatchPart.TabIndex = 16;
            this.cmbMatchPart.Text = "Select Part";
            this.cmbMatchPart.SelectedIndexChanged += new System.EventHandler(this.cmbMatchPart_SelectedIndexChanged);
            // 
            // btnYellow
            // 
            this.btnYellow.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnYellow.Enabled = false;
            this.btnYellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYellow.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYellow.Location = new System.Drawing.Point(617, 1);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(96, 33);
            this.btnYellow.TabIndex = 23;
            this.btnYellow.Text = "Yellow";
            this.btnYellow.UseVisualStyleBackColor = false;
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            // 
            // cmbMatch
            // 
            this.cmbMatch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbMatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMatch.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cmbMatch.ForeColor = System.Drawing.Color.Black;
            this.cmbMatch.FormattingEnabled = true;
            this.cmbMatch.Location = new System.Drawing.Point(5, 5);
            this.cmbMatch.Name = "cmbMatch";
            this.cmbMatch.Size = new System.Drawing.Size(204, 27);
            this.cmbMatch.TabIndex = 15;
            this.cmbMatch.Text = "Select Match";
            this.cmbMatch.SelectedIndexChanged += new System.EventHandler(this.cmbMatch_SelectedIndexChanged);
            // 
            // btnRed
            // 
            this.btnRed.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRed.Enabled = false;
            this.btnRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRed.Location = new System.Drawing.Point(719, 1);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(96, 33);
            this.btnRed.TabIndex = 24;
            this.btnRed.Text = "Red";
            this.btnRed.UseVisualStyleBackColor = false;
            this.btnRed.Click += new System.EventHandler(this.btnRed_Click);
            // 
            // btnCorner
            // 
            this.btnCorner.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnCorner.Enabled = false;
            this.btnCorner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCorner.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorner.Location = new System.Drawing.Point(821, 0);
            this.btnCorner.Name = "btnCorner";
            this.btnCorner.Size = new System.Drawing.Size(96, 33);
            this.btnCorner.TabIndex = 20;
            this.btnCorner.Text = "Corner";
            this.btnCorner.UseVisualStyleBackColor = false;
            this.btnCorner.Click += new System.EventHandler(this.cmbCorner_Click);
            // 
            // btnSubstitute
            // 
            this.btnSubstitute.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnSubstitute.Enabled = false;
            this.btnSubstitute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubstitute.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubstitute.Location = new System.Drawing.Point(1127, 0);
            this.btnSubstitute.Name = "btnSubstitute";
            this.btnSubstitute.Size = new System.Drawing.Size(96, 33);
            this.btnSubstitute.TabIndex = 18;
            this.btnSubstitute.Text = "Substitute";
            this.btnSubstitute.UseVisualStyleBackColor = false;
            this.btnSubstitute.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnShots
            // 
            this.btnShots.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnShots.Enabled = false;
            this.btnShots.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShots.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShots.Location = new System.Drawing.Point(923, 0);
            this.btnShots.Name = "btnShots";
            this.btnShots.Size = new System.Drawing.Size(96, 33);
            this.btnShots.TabIndex = 21;
            this.btnShots.Text = "Shots ON";
            this.btnShots.UseVisualStyleBackColor = false;
            this.btnShots.Click += new System.EventHandler(this.btnShots_Click);
            // 
            // btnFoul
            // 
            this.btnFoul.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnFoul.Enabled = false;
            this.btnFoul.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFoul.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFoul.Location = new System.Drawing.Point(515, 0);
            this.btnFoul.Name = "btnFoul";
            this.btnFoul.Size = new System.Drawing.Size(96, 35);
            this.btnFoul.TabIndex = 19;
            this.btnFoul.Text = "Foul";
            this.btnFoul.UseVisualStyleBackColor = false;
            this.btnFoul.Click += new System.EventHandler(this.btnFoul_Click);
            // 
            // btnShotsOff
            // 
            this.btnShotsOff.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnShotsOff.Enabled = false;
            this.btnShotsOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShotsOff.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShotsOff.Location = new System.Drawing.Point(1025, 0);
            this.btnShotsOff.Name = "btnShotsOff";
            this.btnShotsOff.Size = new System.Drawing.Size(96, 33);
            this.btnShotsOff.TabIndex = 22;
            this.btnShotsOff.Text = "Shots OFF";
            this.btnShotsOff.UseVisualStyleBackColor = false;
            this.btnShotsOff.Click += new System.EventHandler(this.btnShotsOff_Click);
            // 
            // pnlteamcntrs
            // 
            this.pnlteamcntrs.Controls.Add(this.tableLayoutPanel3);
            this.pnlteamcntrs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlteamcntrs.Location = new System.Drawing.Point(3, 81);
            this.pnlteamcntrs.Name = "pnlteamcntrs";
            this.pnlteamcntrs.Padding = new System.Windows.Forms.Padding(5);
            this.pnlteamcntrs.Size = new System.Drawing.Size(1500, 145);
            this.pnlteamcntrs.TabIndex = 29;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33407F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33408F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33186F));
            this.tableLayoutPanel3.Controls.Add(this.pnlAwayTeam, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.pnlHmTeam, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.pnlcntr, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 139F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1490, 135);
            this.tableLayoutPanel3.TabIndex = 16;
            // 
            // pnlAwayTeam
            // 
            this.pnlAwayTeam.BackColor = System.Drawing.Color.Transparent;
            this.pnlAwayTeam.Controls.Add(this.btnawayminus);
            this.pnlAwayTeam.Controls.Add(this.btnawayplus);
            this.pnlAwayTeam.Controls.Add(this.lblAwayScore);
            this.pnlAwayTeam.Controls.Add(this.lblAwayTeam);
            this.pnlAwayTeam.Controls.Add(this.pnlAwayFlag);
            this.pnlAwayTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAwayTeam.Location = new System.Drawing.Point(995, 3);
            this.pnlAwayTeam.Name = "pnlAwayTeam";
            this.pnlAwayTeam.Padding = new System.Windows.Forms.Padding(5);
            this.pnlAwayTeam.Size = new System.Drawing.Size(492, 133);
            this.pnlAwayTeam.TabIndex = 29;
            // 
            // btnawayminus
            // 
            this.btnawayminus.Enabled = false;
            this.btnawayminus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnawayminus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnawayminus.Location = new System.Drawing.Point(3, 35);
            this.btnawayminus.Name = "btnawayminus";
            this.btnawayminus.Size = new System.Drawing.Size(27, 30);
            this.btnawayminus.TabIndex = 9;
            this.btnawayminus.Text = "-";
            this.btnawayminus.UseCompatibleTextRendering = true;
            this.btnawayminus.UseVisualStyleBackColor = true;
            this.btnawayminus.Click += new System.EventHandler(this.btnawayminus_Click);
            // 
            // btnawayplus
            // 
            this.btnawayplus.Enabled = false;
            this.btnawayplus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnawayplus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnawayplus.Location = new System.Drawing.Point(107, 35);
            this.btnawayplus.Name = "btnawayplus";
            this.btnawayplus.Size = new System.Drawing.Size(27, 30);
            this.btnawayplus.TabIndex = 7;
            this.btnawayplus.Text = "+";
            this.btnawayplus.UseCompatibleTextRendering = true;
            this.btnawayplus.UseVisualStyleBackColor = true;
            this.btnawayplus.Click += new System.EventHandler(this.btnawayplus_Click);
            // 
            // lblAwayScore
            // 
            this.lblAwayScore.BackColor = System.Drawing.Color.Transparent;
            this.lblAwayScore.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblAwayScore.Location = new System.Drawing.Point(20, 19);
            this.lblAwayScore.Name = "lblAwayScore";
            this.lblAwayScore.Size = new System.Drawing.Size(96, 62);
            this.lblAwayScore.TabIndex = 11;
            this.lblAwayScore.Text = "12";
            this.lblAwayScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAwayTeam
            // 
            this.lblAwayTeam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAwayTeam.BackColor = System.Drawing.Color.Gray;
            this.lblAwayTeam.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblAwayTeam.ForeColor = System.Drawing.Color.Black;
            this.lblAwayTeam.Location = new System.Drawing.Point(140, 34);
            this.lblAwayTeam.Name = "lblAwayTeam";
            this.lblAwayTeam.Size = new System.Drawing.Size(272, 35);
            this.lblAwayTeam.TabIndex = 3;
            this.lblAwayTeam.Text = "lblAwayTeam";
            this.lblAwayTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAwayTeam.DoubleClick += new System.EventHandler(this.lblAwayTeam_DoubleClick);
            // 
            // pnlAwayFlag
            // 
            this.pnlAwayFlag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAwayFlag.Location = new System.Drawing.Point(418, 35);
            this.pnlAwayFlag.Name = "pnlAwayFlag";
            this.pnlAwayFlag.Size = new System.Drawing.Size(51, 28);
            this.pnlAwayFlag.TabIndex = 4;
            // 
            // pnlHmTeam
            // 
            this.pnlHmTeam.BackColor = System.Drawing.Color.Transparent;
            this.pnlHmTeam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHmTeam.Controls.Add(this.btnhomeplus);
            this.pnlHmTeam.Controls.Add(this.lblHomeTeam);
            this.pnlHmTeam.Controls.Add(this.pnlHomeFlag);
            this.pnlHmTeam.Controls.Add(this.lblHomeScore);
            this.pnlHmTeam.Controls.Add(this.btnHomeminus);
            this.pnlHmTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHmTeam.Location = new System.Drawing.Point(3, 3);
            this.pnlHmTeam.Name = "pnlHmTeam";
            this.pnlHmTeam.Padding = new System.Windows.Forms.Padding(5);
            this.pnlHmTeam.Size = new System.Drawing.Size(490, 133);
            this.pnlHmTeam.TabIndex = 28;
            // 
            // btnhomeplus
            // 
            this.btnhomeplus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnhomeplus.Enabled = false;
            this.btnhomeplus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhomeplus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhomeplus.Location = new System.Drawing.Point(450, 38);
            this.btnhomeplus.Name = "btnhomeplus";
            this.btnhomeplus.Size = new System.Drawing.Size(27, 33);
            this.btnhomeplus.TabIndex = 6;
            this.btnhomeplus.Text = "+";
            this.btnhomeplus.UseCompatibleTextRendering = true;
            this.btnhomeplus.UseVisualStyleBackColor = true;
            this.btnhomeplus.Click += new System.EventHandler(this.btnhomeplus_Click);
            // 
            // lblHomeTeam
            // 
            this.lblHomeTeam.BackColor = System.Drawing.Color.Gray;
            this.lblHomeTeam.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblHomeTeam.ForeColor = System.Drawing.Color.Black;
            this.lblHomeTeam.Location = new System.Drawing.Point(57, 37);
            this.lblHomeTeam.Name = "lblHomeTeam";
            this.lblHomeTeam.Size = new System.Drawing.Size(260, 32);
            this.lblHomeTeam.TabIndex = 2;
            this.lblHomeTeam.Text = "lblHome";
            this.lblHomeTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHomeTeam.DoubleClick += new System.EventHandler(this.lblHomeTeam_DoubleClick);
            // 
            // pnlHomeFlag
            // 
            this.pnlHomeFlag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHomeFlag.Location = new System.Drawing.Point(3, 38);
            this.pnlHomeFlag.Name = "pnlHomeFlag";
            this.pnlHomeFlag.Size = new System.Drawing.Size(51, 28);
            this.pnlHomeFlag.TabIndex = 4;
            // 
            // lblHomeScore
            // 
            this.lblHomeScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHomeScore.BackColor = System.Drawing.Color.Transparent;
            this.lblHomeScore.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblHomeScore.Location = new System.Drawing.Point(360, 18);
            this.lblHomeScore.Name = "lblHomeScore";
            this.lblHomeScore.Size = new System.Drawing.Size(97, 65);
            this.lblHomeScore.TabIndex = 10;
            this.lblHomeScore.Text = "12";
            this.lblHomeScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnHomeminus
            // 
            this.btnHomeminus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHomeminus.Enabled = false;
            this.btnHomeminus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHomeminus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHomeminus.Location = new System.Drawing.Point(329, 38);
            this.btnHomeminus.Name = "btnHomeminus";
            this.btnHomeminus.Size = new System.Drawing.Size(27, 33);
            this.btnHomeminus.TabIndex = 8;
            this.btnHomeminus.Text = "-";
            this.btnHomeminus.UseCompatibleTextRendering = true;
            this.btnHomeminus.UseVisualStyleBackColor = true;
            this.btnHomeminus.Click += new System.EventHandler(this.btnHomeminus_Click);
            // 
            // pnlcntr
            // 
            this.pnlcntr.Controls.Add(this.btnstartstop);
            this.pnlcntr.Controls.Add(this.lblCounter);
            this.pnlcntr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlcntr.Location = new System.Drawing.Point(499, 3);
            this.pnlcntr.Name = "pnlcntr";
            this.pnlcntr.Padding = new System.Windows.Forms.Padding(5);
            this.pnlcntr.Size = new System.Drawing.Size(490, 133);
            this.pnlcntr.TabIndex = 31;
            // 
            // btnstartstop
            // 
            this.btnstartstop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnstartstop.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnstartstop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstartstop.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstartstop.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnstartstop.Location = new System.Drawing.Point(137, 65);
            this.btnstartstop.Name = "btnstartstop";
            this.btnstartstop.Size = new System.Drawing.Size(211, 29);
            this.btnstartstop.TabIndex = 13;
            this.btnstartstop.Text = "START/STOP";
            this.btnstartstop.UseVisualStyleBackColor = false;
            this.btnstartstop.Click += new System.EventHandler(this.btnstartstop_Click);
            // 
            // lblCounter
            // 
            this.lblCounter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCounter.BackColor = System.Drawing.Color.White;
            this.lblCounter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblCounter.Font = new System.Drawing.Font("Tahoma", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblCounter.ForeColor = System.Drawing.Color.Transparent;
            this.lblCounter.Location = new System.Drawing.Point(121, 10);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(238, 58);
            this.lblCounter.TabIndex = 25;
            this.lblCounter.Text = "00:00";
            this.lblCounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlMatch
            // 
            this.pnlMatch.Controls.Add(this.lblMatchname);
            this.pnlMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMatch.Location = new System.Drawing.Point(3, 37);
            this.pnlMatch.Name = "pnlMatch";
            this.pnlMatch.Size = new System.Drawing.Size(1500, 38);
            this.pnlMatch.TabIndex = 28;
            // 
            // lblMatchname
            // 
            this.lblMatchname.AutoSize = true;
            this.lblMatchname.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblMatchname.Location = new System.Drawing.Point(720, 3);
            this.lblMatchname.Name = "lblMatchname";
            this.lblMatchname.Size = new System.Drawing.Size(61, 24);
            this.lblMatchname.TabIndex = 0;
            this.lblMatchname.Text = "NAME";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.133333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.86667F));
            this.tableLayoutPanel2.Controls.Add(this.cmbxEngines, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1500, 28);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cmbxEngines
            // 
            this.cmbxEngines.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbxEngines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbxEngines.FormattingEnabled = true;
            this.cmbxEngines.Location = new System.Drawing.Point(139, 3);
            this.cmbxEngines.Name = "cmbxEngines";
            this.cmbxEngines.Size = new System.Drawing.Size(354, 21);
            this.cmbxEngines.TabIndex = 2;
            this.cmbxEngines.SelectedIndexChanged += new System.EventHandler(this.cmbxEngines_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 28);
            this.label4.TabIndex = 1;
            this.label4.Text = "Engine :- ";
            // 
            // lblMatchevents
            // 
            this.lblMatchevents.AutoSize = true;
            this.lblMatchevents.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMatchevents.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblMatchevents.Location = new System.Drawing.Point(3, 282);
            this.lblMatchevents.Name = "lblMatchevents";
            this.lblMatchevents.Size = new System.Drawing.Size(101, 22);
            this.lblMatchevents.TabIndex = 34;
            this.lblMatchevents.Text = "Match Events";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 307);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1500, 735);
            this.panel1.TabIndex = 35;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dgvMatchevents);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel4);
            this.splitContainer3.Size = new System.Drawing.Size(1500, 735);
            this.splitContainer3.SplitterDistance = 271;
            this.splitContainer3.TabIndex = 0;
            // 
            // dgvMatchevents
            // 
            this.dgvMatchevents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMatchevents.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvMatchevents.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMatchevents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvMatchevents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatchevents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNO,
            this.Time,
            this.MatchPart,
            this.EventMem,
            this.TeamName,
            this.PlayerName});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatchevents.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvMatchevents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMatchevents.Location = new System.Drawing.Point(0, 0);
            this.dgvMatchevents.Name = "dgvMatchevents";
            this.dgvMatchevents.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMatchevents.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvMatchevents.RowHeadersVisible = false;
            this.dgvMatchevents.Size = new System.Drawing.Size(1500, 271);
            this.dgvMatchevents.TabIndex = 15;
            this.dgvMatchevents.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMatchevents_CellDoubleClick);
            // 
            // SNO
            // 
            this.SNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SNO.HeaderText = "S.No.";
            this.SNO.Name = "SNO";
            this.SNO.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // MatchPart
            // 
            this.MatchPart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MatchPart.HeaderText = "Match Part";
            this.MatchPart.Name = "MatchPart";
            this.MatchPart.ReadOnly = true;
            // 
            // EventMem
            // 
            this.EventMem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventMem.HeaderText = "Event Memo";
            this.EventMem.Name = "EventMem";
            this.EventMem.ReadOnly = true;
            // 
            // TeamName
            // 
            this.TeamName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TeamName.HeaderText = "Team Name";
            this.TeamName.Name = "TeamName";
            this.TeamName.ReadOnly = true;
            // 
            // PlayerName
            // 
            this.PlayerName.HeaderText = "Player Name";
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.ReadOnly = true;
            this.PlayerName.Width = 92;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.pnlLabels, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.296137F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.70387F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1500, 460);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // pnlLabels
            // 
            this.pnlLabels.BackColor = System.Drawing.Color.Transparent;
            this.pnlLabels.Controls.Add(this.label3);
            this.pnlLabels.Controls.Add(this.label2);
            this.pnlLabels.Controls.Add(this.label1);
            this.pnlLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLabels.Location = new System.Drawing.Point(0, 0);
            this.pnlLabels.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLabels.Name = "pnlLabels";
            this.pnlLabels.Padding = new System.Windows.Forms.Padding(5);
            this.pnlLabels.Size = new System.Drawing.Size(1500, 33);
            this.pnlLabels.TabIndex = 31;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "P2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "P1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 21);
            this.label1.TabIndex = 27;
            this.label1.Text = "Select Graphic";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.98527F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.01472F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanelPlayers, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.dgvSelectPlayer, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 36);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1494, 421);
            this.tableLayoutPanel5.TabIndex = 32;
            // 
            // tableLayoutPanelPlayers
            // 
            this.tableLayoutPanelPlayers.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanelPlayers.ColumnCount = 2;
            this.tableLayoutPanelPlayers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPlayers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelPlayers.Location = new System.Drawing.Point(196, 3);
            this.tableLayoutPanelPlayers.Name = "tableLayoutPanelPlayers";
            this.tableLayoutPanelPlayers.RowCount = 2;
            this.tableLayoutPanelPlayers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPlayers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanelPlayers.Size = new System.Drawing.Size(1295, 415);
            this.tableLayoutPanelPlayers.TabIndex = 28;
            // 
            // dgvSelectPlayer
            // 
            this.dgvSelectPlayer.AllowUserToAddRows = false;
            this.dgvSelectPlayer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSelectPlayer.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvSelectPlayer.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSelectPlayer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSelectPlayer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelectPlayer.ColumnHeadersVisible = false;
            this.dgvSelectPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelectPlayer.Location = new System.Drawing.Point(3, 3);
            this.dgvSelectPlayer.MinimumSize = new System.Drawing.Size(0, 434);
            this.dgvSelectPlayer.Name = "dgvSelectPlayer";
            this.dgvSelectPlayer.RowHeadersVisible = false;
            this.dgvSelectPlayer.Size = new System.Drawing.Size(187, 434);
            this.dgvSelectPlayer.TabIndex = 27;
            // 
            // SoccerApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1506, 1045);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "SoccerApp";
            this.Text = "Soccer Application";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlbuttons.ResumeLayout(false);
            this.pnlteamcntrs.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.pnlAwayTeam.ResumeLayout(false);
            this.pnlHmTeam.ResumeLayout(false);
            this.pnlcntr.ResumeLayout(false);
            this.pnlcntr.PerformLayout();
            this.pnlMatch.ResumeLayout(false);
            this.pnlMatch.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatchevents)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.pnlLabels.ResumeLayout(false);
            this.pnlLabels.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cmbxEngines;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlMatch;
        private System.Windows.Forms.Label lblMatchname;
        private System.Windows.Forms.Panel pnlteamcntrs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel pnlAwayTeam;
        private System.Windows.Forms.Button btnawayminus;
        private System.Windows.Forms.Button btnawayplus;
        private System.Windows.Forms.Label lblAwayScore;
        private System.Windows.Forms.Label lblAwayTeam;
        private System.Windows.Forms.Panel pnlAwayFlag;
        private System.Windows.Forms.Panel pnlHmTeam;
        private System.Windows.Forms.Button btnhomeplus;
        private System.Windows.Forms.Label lblHomeTeam;
        private System.Windows.Forms.Panel pnlHomeFlag;
        private System.Windows.Forms.Label lblHomeScore;
        private System.Windows.Forms.Button btnHomeminus;
        private System.Windows.Forms.Panel pnlcntr;
        private System.Windows.Forms.Button btnstartstop;
        private System.Windows.Forms.TextBox lblCounter;
        private System.Windows.Forms.Panel pnlbuttons;
        private System.Windows.Forms.Button BtnLoadBG;
        private System.Windows.Forms.ComboBox cmbMatchPart;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.ComboBox cmbMatch;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnCorner;
        private System.Windows.Forms.Button btnSubstitute;
        private System.Windows.Forms.Button btnShots;
        private System.Windows.Forms.Button btnFoul;
        private System.Windows.Forms.Button btnShotsOff;
        private System.Windows.Forms.Label lblMatchevents;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dgvMatchevents;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchPart;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventMem;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayerName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel pnlLabels;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.DataGridView dgvSelectPlayer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPlayers;
    }
}