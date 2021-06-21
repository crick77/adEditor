
namespace adEditor
{
    partial class GuardsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbVars = new System.Windows.Forms.ComboBox();
            this.cbDecrease = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudCounter = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDates = new System.Windows.Forms.ComboBox();
            this.cbExpire = new System.Windows.Forms.CheckBox();
            this.bOk = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCounter)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbVars);
            this.groupBox1.Controls.Add(this.cbDecrease);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudCounter);
            this.groupBox1.Location = new System.Drawing.Point(6, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(488, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Counter guard";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "by";
            // 
            // cbVars
            // 
            this.cbVars.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVars.FormattingEnabled = true;
            this.cbVars.Location = new System.Drawing.Point(106, 26);
            this.cbVars.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbVars.Name = "cbVars";
            this.cbVars.Size = new System.Drawing.Size(209, 21);
            this.cbVars.TabIndex = 5;
            this.cbVars.SelectedIndexChanged += new System.EventHandler(this.cbVars_SelectedIndexChanged);
            // 
            // cbDecrease
            // 
            this.cbDecrease.AutoSize = true;
            this.cbDecrease.Location = new System.Drawing.Point(30, 28);
            this.cbDecrease.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDecrease.Name = "cbDecrease";
            this.cbDecrease.Size = new System.Drawing.Size(72, 17);
            this.cbDecrease.TabIndex = 3;
            this.cbDecrease.Text = "&Decrease";
            this.cbDecrease.UseVisualStyleBackColor = true;
            this.cbDecrease.CheckedChanged += new System.EventHandler(this.cbDecrease_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(418, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "each time";
            // 
            // nudCounter
            // 
            this.nudCounter.Enabled = false;
            this.nudCounter.Location = new System.Drawing.Point(346, 27);
            this.nudCounter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.nudCounter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCounter.Name = "nudCounter";
            this.nudCounter.Size = new System.Drawing.Size(60, 20);
            this.nudCounter.TabIndex = 1;
            this.nudCounter.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbDates);
            this.groupBox2.Controls.Add(this.cbExpire);
            this.groupBox2.Location = new System.Drawing.Point(6, 84);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(488, 79);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Date guard";
            // 
            // cbDates
            // 
            this.cbDates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDates.FormattingEnabled = true;
            this.cbDates.Location = new System.Drawing.Point(106, 37);
            this.cbDates.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbDates.Name = "cbDates";
            this.cbDates.Size = new System.Drawing.Size(209, 21);
            this.cbDates.TabIndex = 1;
            // 
            // cbExpire
            // 
            this.cbExpire.AutoSize = true;
            this.cbExpire.Location = new System.Drawing.Point(30, 38);
            this.cbExpire.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbExpire.Name = "cbExpire";
            this.cbExpire.Size = new System.Drawing.Size(75, 17);
            this.cbExpire.TabIndex = 0;
            this.cbExpire.Text = "&Expires on";
            this.cbExpire.UseVisualStyleBackColor = true;
            this.cbExpire.CheckedChanged += new System.EventHandler(this.cbExpire_CheckedChanged);
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(92, 184);
            this.bOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(66, 29);
            this.bOk.TabIndex = 3;
            this.bOk.Text = "Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(259, 187);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 26);
            this.bCancel.TabIndex = 4;
            this.bCancel.Text = "&Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // GuardsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 232);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "GuardsForm";
            this.Text = "GuardsForm";
            this.Load += new System.EventHandler(this.GuardsForm_Load);
            this.Shown += new System.EventHandler(this.GuardsForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCounter)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbDecrease;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudCounter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbExpire;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbVars;
        private System.Windows.Forms.ComboBox cbDates;
        private System.Windows.Forms.Button bCancel;
    }
}