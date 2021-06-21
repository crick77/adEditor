
namespace adEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewItem = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pDFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.varToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geographicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListNodes = new System.Windows.Forms.ImageList(this.components);
            this.mainMenu.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1672, 40);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openToolStripMenuItem1,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(72, 36);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(208, 44);
            this.openToolStripMenuItem.Text = "&New";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(208, 44);
            this.openToolStripMenuItem1.Text = "&Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(208, 44);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(205, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(208, 44);
            this.exitToolStripMenuItem.Text = "&Exit";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(46, 36);
            this.toolStripMenuItem1.Text = "&?";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(191, 44);
            this.infoToolStripMenuItem.Text = "&Info";
            // 
            // treeViewItem
            // 
            this.treeViewItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewItem.ContextMenuStrip = this.contextMenuStrip;
            this.treeViewItem.ImageIndex = 0;
            this.treeViewItem.ImageList = this.imageListNodes;
            this.treeViewItem.Location = new System.Drawing.Point(0, 52);
            this.treeViewItem.Margin = new System.Windows.Forms.Padding(6);
            this.treeViewItem.Name = "treeViewItem";
            this.treeViewItem.SelectedImageIndex = 0;
            this.treeViewItem.Size = new System.Drawing.Size(1668, 973);
            this.treeViewItem.TabIndex = 1;
            this.treeViewItem.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewItem_NodeMouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.editToolStripMenuItem,
            this.varToolStripMenuItem,
            this.eventsToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(200, 156);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textToolStripMenuItem,
            this.imageToolStripMenuItem,
            this.pDFToolStripMenuItem,
            this.videoToolStripMenuItem,
            this.anyToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(199, 38);
            this.addToolStripMenuItem.Text = "&Add item";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // textToolStripMenuItem
            // 
            this.textToolStripMenuItem.Name = "textToolStripMenuItem";
            this.textToolStripMenuItem.Size = new System.Drawing.Size(230, 44);
            this.textToolStripMenuItem.Text = "Text...";
            this.textToolStripMenuItem.Click += new System.EventHandler(this.textToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(230, 44);
            this.imageToolStripMenuItem.Text = "Image...";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.imageToolStripMenuItem_Click);
            // 
            // pDFToolStripMenuItem
            // 
            this.pDFToolStripMenuItem.Name = "pDFToolStripMenuItem";
            this.pDFToolStripMenuItem.Size = new System.Drawing.Size(230, 44);
            this.pDFToolStripMenuItem.Text = "PDF...";
            this.pDFToolStripMenuItem.Click += new System.EventHandler(this.pDFToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(230, 44);
            this.videoToolStripMenuItem.Text = "Video...";
            this.videoToolStripMenuItem.Click += new System.EventHandler(this.videoToolStripMenuItem_Click);
            // 
            // anyToolStripMenuItem
            // 
            this.anyToolStripMenuItem.Name = "anyToolStripMenuItem";
            this.anyToolStripMenuItem.Size = new System.Drawing.Size(230, 44);
            this.anyToolStripMenuItem.Text = "Any...";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(199, 38);
            this.editToolStripMenuItem.Text = "&Edit item";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // varToolStripMenuItem
            // 
            this.varToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCounterToolStripMenuItem,
            this.addDateToolStripMenuItem});
            this.varToolStripMenuItem.Name = "varToolStripMenuItem";
            this.varToolStripMenuItem.Size = new System.Drawing.Size(199, 38);
            this.varToolStripMenuItem.Text = "&Variables...";
            // 
            // addCounterToolStripMenuItem
            // 
            this.addCounterToolStripMenuItem.Name = "addCounterToolStripMenuItem";
            this.addCounterToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.addCounterToolStripMenuItem.Text = "&Add/edit counter";
            this.addCounterToolStripMenuItem.Click += new System.EventHandler(this.addCounterToolStripMenuItem_Click);
            // 
            // addDateToolStripMenuItem
            // 
            this.addDateToolStripMenuItem.Name = "addDateToolStripMenuItem";
            this.addDateToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.addDateToolStripMenuItem.Text = "A&dd/edit date";
            // 
            // eventsToolStripMenuItem
            // 
            this.eventsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.onreadToolStripMenuItem,
            this.expireToolStripMenuItem,
            this.geographicalToolStripMenuItem});
            this.eventsToolStripMenuItem.Name = "eventsToolStripMenuItem";
            this.eventsToolStripMenuItem.Size = new System.Drawing.Size(199, 38);
            this.eventsToolStripMenuItem.Text = "&Events...";
            // 
            // onreadToolStripMenuItem
            // 
            this.onreadToolStripMenuItem.Name = "onreadToolStripMenuItem";
            this.onreadToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.onreadToolStripMenuItem.Text = "on&Read";
            this.onreadToolStripMenuItem.Click += new System.EventHandler(this.onreadToolStripMenuItem_Click);
            // 
            // expireToolStripMenuItem
            // 
            this.expireToolStripMenuItem.Name = "expireToolStripMenuItem";
            this.expireToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.expireToolStripMenuItem.Text = "on&Copy";
            // 
            // geographicalToolStripMenuItem
            // 
            this.geographicalToolStripMenuItem.Name = "geographicalToolStripMenuItem";
            this.geographicalToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.geographicalToolStripMenuItem.Text = "on&Share";
            // 
            // imageListNodes
            // 
            this.imageListNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListNodes.ImageStream")));
            this.imageListNodes.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListNodes.Images.SetKeyName(0, "code.png");
            this.imageListNodes.Images.SetKeyName(1, "personal-information.png");
            this.imageListNodes.Images.SetKeyName(2, "font.png");
            this.imageListNodes.Images.SetKeyName(3, "steps.png");
            this.imageListNodes.Images.SetKeyName(4, "image.png");
            this.imageListNodes.Images.SetKeyName(5, "video.png");
            this.imageListNodes.Images.SetKeyName(6, "pdf.png");
            this.imageListNodes.Images.SetKeyName(7, "policeman.png");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1672, 1031);
            this.Controls.Add(this.treeViewItem);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "MainForm";
            this.Text = "adEditor";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.TreeView treeViewItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pDFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anyToolStripMenuItem;
        private System.Windows.Forms.ImageList imageListNodes;
        private System.Windows.Forms.ToolStripMenuItem varToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCounterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onreadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geographicalToolStripMenuItem;
    }
}

