namespace WinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Настольные ПК");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Консоли");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Портативные консоли");
            this.treeView = new System.Windows.Forms.TreeView();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.l_desc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Location = new System.Drawing.Point(12, 41);
            this.treeView.Name = "treeView";
            treeNode4.Name = "T_PC";
            treeNode4.Text = "Настольные ПК";
            treeNode5.Name = "T_Consoles";
            treeNode5.Text = "Консоли";
            treeNode6.Name = "T_Portables";
            treeNode6.Text = "Портативные консоли";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode6});
            this.treeView.Size = new System.Drawing.Size(194, 541);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            // 
            // comboBox
            // 
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "Из списка",
            "Портативная консоль с самым большим разрешением экрана",
            "Устройство с самым большим ОЗУ",
            "Самое ранее устройство Commodore",
            "Консоль с самой большой видеопамятью",
            "ПК с наибольшим количеством доступных ОС"});
            this.comboBox.Location = new System.Drawing.Point(12, 12);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(194, 23);
            this.comboBox.TabIndex = 1;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.ImageLocation = "";
            this.pictureBox.Location = new System.Drawing.Point(212, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(365, 277);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // l_desc
            // 
            this.l_desc.AutoSize = true;
            this.l_desc.Location = new System.Drawing.Point(212, 292);
            this.l_desc.Name = "l_desc";
            this.l_desc.Size = new System.Drawing.Size(38, 15);
            this.l_desc.TabIndex = 3;
            this.l_desc.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 594);
            this.Controls.Add(this.l_desc);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.treeView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView treeView;
        private ComboBox comboBox;
        private PictureBox pictureBox;
        private Label l_desc;
    }
}