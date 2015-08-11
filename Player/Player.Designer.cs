namespace Player
{
    partial class Player
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.JerseyNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Goal = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ShotsOnGoal = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Foul = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Substitution = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(325, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "PLAYERS";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JerseyNumber,
            this.Name,
            this.Position,
            this.Goal,
            this.ShotsOnGoal,
            this.Foul,
            this.Substitution});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(0, 39);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(803, 323);
            this.dataGridView1.TabIndex = 2;
            // 
            // JerseyNumber
            // 
            this.JerseyNumber.HeaderText = "Jersey Number";
            this.JerseyNumber.Name = "JerseyNumber";
            // 
            // Name
            // 
            this.Name.HeaderText = "Player Name";
            this.Name.Name = "Name";
            // 
            // Position
            // 
            this.Position.HeaderText = "Postion";
            this.Position.Name = "Position";
            // 
            // Goal
            // 
            this.Goal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Goal.HeaderText = "Goal";
            this.Goal.Name = "Goal";
            this.Goal.Text = "Goal(0)";
            // 
            // ShotsOnGoal
            // 
            this.ShotsOnGoal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ShotsOnGoal.HeaderText = "Shots On Goal";
            this.ShotsOnGoal.Name = "ShotsOnGoal";
            this.ShotsOnGoal.Text = "Shots On Goal(3)";
            // 
            // Foul
            // 
            this.Foul.HeaderText = "Foul";
            this.Foul.Name = "Foul";
            this.Foul.Text = "Foul(1)";
            // 
            // Substitution
            // 
            this.Substitution.HeaderText = "Substitution";
            this.Substitution.MinimumWidth = 200;
            this.Substitution.Name = "Substitution";
            this.Substitution.Width = 200;
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
          
            this.Size = new System.Drawing.Size(803, 362);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn JerseyNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewButtonColumn Goal;
        private System.Windows.Forms.DataGridViewButtonColumn ShotsOnGoal;
        private System.Windows.Forms.DataGridViewButtonColumn Foul;
        private System.Windows.Forms.DataGridViewComboBoxColumn Substitution;

    }
}
