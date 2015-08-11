namespace Team
{
    partial class Team
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnTeamList = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Card = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Substitution = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Foul = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ShotsOnGoal = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Goal = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JerseyNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(211, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Team";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(260, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(135, 20);
            this.textBox1.TabIndex = 8;
            // 
            // btnTeamList
            // 
            this.btnTeamList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTeamList.Location = new System.Drawing.Point(397, 14);
            this.btnTeamList.Name = "btnTeamList";
            this.btnTeamList.Size = new System.Drawing.Size(67, 24);
            this.btnTeamList.TabIndex = 21;
            this.btnTeamList.Text = "Team List";
            this.btnTeamList.UseVisualStyleBackColor = true;
            this.btnTeamList.Click += new System.EventHandler(this.btnTeamList_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JerseyNumber,
            this.Name,
            this.Goal,
            this.ShotsOnGoal,
            this.Foul,
            this.Substitution,
            this.Card});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(654, 299);
            this.dataGridView1.TabIndex = 22;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnTeamList);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(654, 358);
            this.splitContainer1.SplitterDistance = 55;
            this.splitContainer1.TabIndex = 23;
            // 
            // Card
            // 
            this.Card.HeaderText = "Card";
            this.Card.Name = "Card";
            this.Card.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Card.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Substitution
            // 
            this.Substitution.HeaderText = "Substitution";
            this.Substitution.MinimumWidth = 200;
            this.Substitution.Name = "Substitution";
            this.Substitution.Width = 200;
            // 
            // Foul
            // 
            this.Foul.HeaderText = "Foul";
            this.Foul.Name = "Foul";
            this.Foul.Text = "Foul(1)";
            this.Foul.Width = 50;
            // 
            // ShotsOnGoal
            // 
            this.ShotsOnGoal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShotsOnGoal.HeaderText = "Shots On Goal";
            this.ShotsOnGoal.Name = "ShotsOnGoal";
            this.ShotsOnGoal.Text = "Shots On Goal(3)";
            this.ShotsOnGoal.Width = 50;
            // 
            // Goal
            // 
            this.Goal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Goal.HeaderText = "Goal";
            this.Goal.Name = "Goal";
            this.Goal.Text = "Goal(0)";
            this.Goal.Width = 50;
            // 
            // Name
            // 
            this.Name.HeaderText = "Player Name";
            this.Name.Name = "Name";
            // 
            // JerseyNumber
            // 
            this.JerseyNumber.HeaderText = "Jersey Number";
            this.JerseyNumber.Name = "JerseyNumber";
            // 
            // Team
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
          
            this.Size = new System.Drawing.Size(654, 358);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnTeamList;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn JerseyNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewButtonColumn Goal;
        private System.Windows.Forms.DataGridViewButtonColumn ShotsOnGoal;
        private System.Windows.Forms.DataGridViewButtonColumn Foul;
        private System.Windows.Forms.DataGridViewComboBoxColumn Substitution;
        private System.Windows.Forms.DataGridViewComboBoxColumn Card;
    }
}
