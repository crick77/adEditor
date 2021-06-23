
namespace adEditor
{
    partial class StringEditForm
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
            this.gbEdit = new System.Windows.Forms.GroupBox();
            this.lValue = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.gbEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbEdit
            // 
            this.gbEdit.Controls.Add(this.lValue);
            this.gbEdit.Controls.Add(this.tbValue);
            this.gbEdit.Location = new System.Drawing.Point(10, 21);
            this.gbEdit.Name = "gbEdit";
            this.gbEdit.Size = new System.Drawing.Size(711, 73);
            this.gbEdit.TabIndex = 0;
            this.gbEdit.TabStop = false;
            this.gbEdit.Text = "groupBox1";
            // 
            // lValue
            // 
            this.lValue.AutoSize = true;
            this.lValue.Location = new System.Drawing.Point(17, 35);
            this.lValue.Name = "lValue";
            this.lValue.Size = new System.Drawing.Size(40, 13);
            this.lValue.TabIndex = 1;
            this.lValue.Text = "&Valore:";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(63, 32);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(622, 20);
            this.tbValue.TabIndex = 0;
            this.tbValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbValue_KeyDown);
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(213, 111);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 1;
            this.bSave.Text = "&Salva";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bCancel
            // 
            this.bCancel.Location = new System.Drawing.Point(469, 111);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "&Annulla";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // StringEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 156);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.gbEdit);
            this.Name = "StringEditForm";
            this.Text = "Modifica dato";
            this.Shown += new System.EventHandler(this.StringEditForm_Shown);
            this.gbEdit.ResumeLayout(false);
            this.gbEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbEdit;
        private System.Windows.Forms.Label lValue;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bCancel;
    }
}