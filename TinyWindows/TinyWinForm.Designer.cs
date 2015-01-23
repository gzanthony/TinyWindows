namespace TinyWindows
{
    partial class TinyWinForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TinyWinForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuItemSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemClearup = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAboutMe = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemRegisterKEY = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BeginConvertButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStopBtn = new System.Windows.Forms.ToolStripButton();
            this.ImageCountLabel = new System.Windows.Forms.ToolStripLabel();
            this.ImageCount = new System.Windows.Forms.ToolStripLabel();
            this.ImageConvertLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ConvertFailCountLabel = new System.Windows.Forms.ToolStripLabel();
            this.ConvertFailCount = new System.Windows.Forms.ToolStripLabel();
            this.APIKeyCountLabel = new System.Windows.Forms.ToolStripLabel();
            this.APIKeyCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ProcessMessage = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemSetting,
            this.MenuItemClearup,
            this.MenuItemAboutMe,
            this.MenuItemRegisterKEY,
            this.MenuItemQuit,
            this.toolStripTextBox1});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // MenuItemSetting
            // 
            this.MenuItemSetting.Name = "MenuItemSetting";
            resources.ApplyResources(this.MenuItemSetting, "MenuItemSetting");
            // 
            // MenuItemClearup
            // 
            this.MenuItemClearup.Name = "MenuItemClearup";
            resources.ApplyResources(this.MenuItemClearup, "MenuItemClearup");
            this.MenuItemClearup.Click += new System.EventHandler(this.MenuItemClearup_Click);
            // 
            // MenuItemAboutMe
            // 
            this.MenuItemAboutMe.Name = "MenuItemAboutMe";
            resources.ApplyResources(this.MenuItemAboutMe, "MenuItemAboutMe");
            this.MenuItemAboutMe.Click += new System.EventHandler(this.MenuItemAboutMe_Click);
            // 
            // MenuItemRegisterKEY
            // 
            this.MenuItemRegisterKEY.Name = "MenuItemRegisterKEY";
            resources.ApplyResources(this.MenuItemRegisterKEY, "MenuItemRegisterKEY");
            this.MenuItemRegisterKEY.Click += new System.EventHandler(this.MenuItemRegisterKEY_Click);
            // 
            // MenuItemQuit
            // 
            this.MenuItemQuit.Name = "MenuItemQuit";
            resources.ApplyResources(this.MenuItemQuit, "MenuItemQuit");
            this.MenuItemQuit.Click += new System.EventHandler(this.MenuItemQuit_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.toolStripTextBox1, "toolStripTextBox1");
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.ShortcutsEnabled = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.toolStrip1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BeginConvertButton,
            this.ToolStopBtn,
            this.ImageCountLabel,
            this.ImageCount,
            this.ImageConvertLabel,
            this.toolStripLabel1,
            this.ConvertFailCountLabel,
            this.ConvertFailCount,
            this.APIKeyCountLabel,
            this.APIKeyCount,
            this.toolStripSeparator1,
            this.ProgressBar,
            this.toolStripLabel2});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Name = "toolStrip1";
            // 
            // BeginConvertButton
            // 
            this.BeginConvertButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.BeginConvertButton, "BeginConvertButton");
            this.BeginConvertButton.Name = "BeginConvertButton";
            this.BeginConvertButton.Click += new System.EventHandler(this.BeginConvertButton_Click);
            // 
            // ToolStopBtn
            // 
            this.ToolStopBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.ToolStopBtn, "ToolStopBtn");
            this.ToolStopBtn.Name = "ToolStopBtn";
            this.ToolStopBtn.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // ImageCountLabel
            // 
            this.ImageCountLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.ImageCountLabel, "ImageCountLabel");
            this.ImageCountLabel.Name = "ImageCountLabel";
            this.ImageCountLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // ImageCount
            // 
            resources.ApplyResources(this.ImageCount, "ImageCount");
            this.ImageCount.Name = "ImageCount";
            this.ImageCount.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // ImageConvertLabel
            // 
            resources.ApplyResources(this.ImageConvertLabel, "ImageConvertLabel");
            this.ImageConvertLabel.Name = "ImageConvertLabel";
            this.ImageConvertLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // ConvertFailCountLabel
            // 
            resources.ApplyResources(this.ConvertFailCountLabel, "ConvertFailCountLabel");
            this.ConvertFailCountLabel.Name = "ConvertFailCountLabel";
            this.ConvertFailCountLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // ConvertFailCount
            // 
            resources.ApplyResources(this.ConvertFailCount, "ConvertFailCount");
            this.ConvertFailCount.Name = "ConvertFailCount";
            this.ConvertFailCount.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // APIKeyCountLabel
            // 
            resources.ApplyResources(this.APIKeyCountLabel, "APIKeyCountLabel");
            this.APIKeyCountLabel.Name = "APIKeyCountLabel";
            this.APIKeyCountLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // APIKeyCount
            // 
            this.APIKeyCount.Name = "APIKeyCount";
            this.APIKeyCount.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            resources.ApplyResources(this.APIKeyCount, "APIKeyCount");
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // ProgressBar
            // 
            this.ProgressBar.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ProgressBar.Name = "ProgressBar";
            resources.ApplyResources(this.ProgressBar, "ProgressBar");
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            resources.ApplyResources(this.toolStripLabel2, "toolStripLabel2");
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(Panel1_DragDrop);
            this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(Panel1_DragEnter);
            // 
            // ProcessMessage
            // 
            this.ProcessMessage.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.ProcessMessage, "ProcessMessage");
            this.ProcessMessage.ForeColor = System.Drawing.Color.PaleGreen;
            this.ProcessMessage.FormattingEnabled = true;
            this.ProcessMessage.Name = "ProcessMessage";
            // 
            // TinyWinForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProcessMessage);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TinyWinForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemQuit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAboutMe;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BeginConvertButton;
        private System.Windows.Forms.ToolStripLabel ImageCountLabel;
        private System.Windows.Forms.ToolStripMenuItem MenuItemClearup;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSetting;
        private System.Windows.Forms.ToolStripMenuItem MenuItemRegisterKEY;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel ImageCount;
        private System.Windows.Forms.ToolStripLabel ImageConvertLabel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel ConvertFailCountLabel;
        private System.Windows.Forms.ToolStripLabel ConvertFailCount;
        private System.Windows.Forms.ToolStripLabel APIKeyCountLabel;
        private System.Windows.Forms.ToolStripLabel APIKeyCount;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton ToolStopBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ListBox ProcessMessage;
    }
}

