
namespace adEditor
{
    partial class CounterForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.nudValue = new System.Windows.Forms.NumericUpDown();
            this.bOk = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Value:";
            // 
            // nudValue
            // 
            this.nudValue.Location = new System.Drawing.Point(279, 36);
            this.nudValue.Maximum = new decimal(new int[] {
            -1981284353,
            -1966660860,
            0,
            0});
            this.nudValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudValue.Name = "nudValue";
            this.nudValue.Size = new System.Drawing.Size(366, 31);
            this.nudValue.TabIndex = 3;
            this.nudValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bOk
            // 
            this.bOk.Location = new System.Drawing.Point(219, 112);
            this.bOk.Name = "bOk";
            this.bOk.Size = new System.Drawing.Size(162, 62);
            this.bOk.TabIndex = 4;
            this.bOk.Text = "Ok";
            this.bOk.UseVisualStyleBackColor = true;
            this.bOk.Click += new System.EventHandler(this.bOk_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(567, 112);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(162, 62);
            this.bCancel.TabIndex = 5;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // CounterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 218);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOk);
            this.Controls.Add(this.nudValue);
            this.Controls.Add(this.label2);
            this.Name = "CounterForm";
            this.Text = "Add/edit counter";
            this.Load += new System.EventHandler(this.CounterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudValue;
        private System.Windows.Forms.Button bOk;
        private System.Windows.Forms.Button bCancel;
    }
}