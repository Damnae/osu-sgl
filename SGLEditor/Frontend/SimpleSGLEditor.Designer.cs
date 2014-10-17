namespace SGLEditor
{
    partial class SimpleSGLEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleSGLEditor));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabSGL = new System.Windows.Forms.TabPage();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.SGLBox = new Alsing.Windows.Forms.SyntaxBoxControl();
			this.syntaxSGL = new Alsing.SourceCode.SyntaxDocument(this.components);
			this.errorBox = new System.Windows.Forms.TextBox();
			this.tabSB = new System.Windows.Forms.TabPage();
			this.storyboardBox = new Alsing.Windows.Forms.SyntaxBoxControl();
			this.syntaxStoryboard = new Alsing.SourceCode.SyntaxDocument(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.compilerPBar = new System.Windows.Forms.ToolStripProgressBar();
			this.statusLabel = new System.Windows.Forms.ToolStripLabel();
			this.compileSBButton = new System.Windows.Forms.ToolStripButton();
			this.CopyrightLabel = new System.Windows.Forms.ToolStripLabel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miNew = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miSaveSgl = new System.Windows.Forms.ToolStripMenuItem();
			this.miSaveOsb = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.osuThreadBugsFeatureRequestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bugListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.sGLUserManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl.SuspendLayout();
			this.tabSGL.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabSB.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabSGL);
			this.tabControl.Controls.Add(this.tabSB);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 24);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(784, 513);
			this.tabControl.TabIndex = 0;
			// 
			// tabSGL
			// 
			this.tabSGL.Controls.Add(this.splitContainer1);
			this.tabSGL.Location = new System.Drawing.Point(4, 22);
			this.tabSGL.Name = "tabSGL";
			this.tabSGL.Padding = new System.Windows.Forms.Padding(3);
			this.tabSGL.Size = new System.Drawing.Size(776, 487);
			this.tabSGL.TabIndex = 0;
			this.tabSGL.Text = "SGL Code";
			this.tabSGL.UseVisualStyleBackColor = true;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(3, 3);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.SGLBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.errorBox);
			this.splitContainer1.Size = new System.Drawing.Size(770, 481);
			this.splitContainer1.SplitterDistance = 380;
			this.splitContainer1.TabIndex = 0;
			// 
			// SGLBox
			// 
			this.SGLBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
			this.SGLBox.AllowBreakPoints = false;
			this.SGLBox.AutoListPosition = null;
			this.SGLBox.AutoListSelectedText = "a123";
			this.SGLBox.AutoListVisible = false;
			this.SGLBox.BackColor = System.Drawing.Color.White;
			this.SGLBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
			this.SGLBox.CopyAsRTF = false;
			this.SGLBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SGLBox.Document = this.syntaxSGL;
			this.SGLBox.FontName = "Courier new";
			this.SGLBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.SGLBox.InfoTipCount = 1;
			this.SGLBox.InfoTipPosition = null;
			this.SGLBox.InfoTipSelectedIndex = 1;
			this.SGLBox.InfoTipVisible = false;
			this.SGLBox.Location = new System.Drawing.Point(0, 0);
			this.SGLBox.LockCursorUpdate = false;
			this.SGLBox.Name = "SGLBox";
			this.SGLBox.ShowScopeIndicator = false;
			this.SGLBox.Size = new System.Drawing.Size(770, 380);
			this.SGLBox.SmoothScroll = false;
			this.SGLBox.SplitviewH = -4;
			this.SGLBox.SplitviewV = -4;
			this.SGLBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
			this.SGLBox.TabIndex = 0;
			this.SGLBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			this.SGLBox.Click += new System.EventHandler(this.SGLBox_Click);
			this.SGLBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SGLBox_KeyUp);
			// 
			// syntaxSGL
			// 
			this.syntaxSGL.Lines = new string[] {
        ""};
			this.syntaxSGL.MaxUndoBufferSize = 1000;
			this.syntaxSGL.Modified = false;
			this.syntaxSGL.UndoStep = 0;
			// 
			// errorBox
			// 
			this.errorBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.errorBox.Location = new System.Drawing.Point(0, 0);
			this.errorBox.Multiline = true;
			this.errorBox.Name = "errorBox";
			this.errorBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.errorBox.Size = new System.Drawing.Size(770, 97);
			this.errorBox.TabIndex = 0;
			// 
			// tabSB
			// 
			this.tabSB.Controls.Add(this.storyboardBox);
			this.tabSB.Location = new System.Drawing.Point(4, 22);
			this.tabSB.Name = "tabSB";
			this.tabSB.Padding = new System.Windows.Forms.Padding(3);
			this.tabSB.Size = new System.Drawing.Size(776, 487);
			this.tabSB.TabIndex = 1;
			this.tabSB.Text = "Storyboard Code";
			this.tabSB.UseVisualStyleBackColor = true;
			// 
			// storyboardBox
			// 
			this.storyboardBox.ActiveView = Alsing.Windows.Forms.ActiveView.BottomRight;
			this.storyboardBox.AllowBreakPoints = false;
			this.storyboardBox.AutoListPosition = null;
			this.storyboardBox.AutoListSelectedText = "a123";
			this.storyboardBox.AutoListVisible = false;
			this.storyboardBox.BackColor = System.Drawing.Color.White;
			this.storyboardBox.BorderStyle = Alsing.Windows.Forms.BorderStyle.None;
			this.storyboardBox.CopyAsRTF = false;
			this.storyboardBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.storyboardBox.Document = this.syntaxStoryboard;
			this.storyboardBox.FontName = "Courier new";
			this.storyboardBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.storyboardBox.InfoTipCount = 1;
			this.storyboardBox.InfoTipPosition = null;
			this.storyboardBox.InfoTipSelectedIndex = 1;
			this.storyboardBox.InfoTipVisible = false;
			this.storyboardBox.Location = new System.Drawing.Point(3, 3);
			this.storyboardBox.LockCursorUpdate = false;
			this.storyboardBox.Name = "storyboardBox";
			this.storyboardBox.ShowScopeIndicator = false;
			this.storyboardBox.Size = new System.Drawing.Size(770, 481);
			this.storyboardBox.SmoothScroll = false;
			this.storyboardBox.SplitviewH = -4;
			this.storyboardBox.SplitviewV = -4;
			this.storyboardBox.TabGuideColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(233)))), ((int)(((byte)(233)))));
			this.storyboardBox.TabIndex = 0;
			this.storyboardBox.Text = "storyboardBox";
			this.storyboardBox.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			// 
			// syntaxStoryboard
			// 
			this.syntaxStoryboard.Lines = new string[] {
        ""};
			this.syntaxStoryboard.MaxUndoBufferSize = 1000;
			this.syntaxStoryboard.Modified = false;
			this.syntaxStoryboard.UndoStep = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compilerPBar,
            this.statusLabel,
            this.compileSBButton,
            this.CopyrightLabel});
			this.toolStrip1.Location = new System.Drawing.Point(0, 537);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(784, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// compilerPBar
			// 
			this.compilerPBar.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
			this.compilerPBar.Name = "compilerPBar";
			this.compilerPBar.Size = new System.Drawing.Size(100, 22);
			this.compilerPBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// statusLabel
			// 
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new System.Drawing.Size(0, 22);
			// 
			// compileSBButton
			// 
			this.compileSBButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.compileSBButton.Image = ((System.Drawing.Image)(resources.GetObject("compileSBButton.Image")));
			this.compileSBButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.compileSBButton.Name = "compileSBButton";
			this.compileSBButton.Size = new System.Drawing.Size(133, 22);
			this.compileSBButton.Text = "Compile Storyboard";
			this.compileSBButton.ToolTipText = "Compiles SGL code into storybard code";
			this.compileSBButton.Click += new System.EventHandler(this.compileSBButton_Click);
			// 
			// CopyrightLabel
			// 
			this.CopyrightLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.CopyrightLabel.Name = "CopyrightLabel";
			this.CopyrightLabel.Size = new System.Drawing.Size(292, 22);
			this.CopyrightLabel.Text = "SGL Editor (Beta 1.1.002 d3) - Dominik Halfkann (2013)";
			this.CopyrightLabel.Click += new System.EventHandler(this.CopyrightLabel_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(784, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miOpen,
            this.toolStripSeparator1,
            this.miSaveSgl,
            this.miSaveOsb});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// miNew
			// 
			this.miNew.Name = "miNew";
			this.miNew.Size = new System.Drawing.Size(123, 22);
			this.miNew.Text = "New";
			this.miNew.Click += new System.EventHandler(this.miNew_Click);
			// 
			// miOpen
			// 
			this.miOpen.Name = "miOpen";
			this.miOpen.Size = new System.Drawing.Size(123, 22);
			this.miOpen.Text = "Open...";
			this.miOpen.Click += new System.EventHandler(this.miOpen_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(120, 6);
			// 
			// miSaveSgl
			// 
			this.miSaveSgl.Name = "miSaveSgl";
			this.miSaveSgl.Size = new System.Drawing.Size(123, 22);
			this.miSaveSgl.Text = "Save .sgf";
			this.miSaveSgl.Click += new System.EventHandler(this.miSaveSgf_Click);
			// 
			// miSaveOsb
			// 
			this.miSaveOsb.Name = "miSaveOsb";
			this.miSaveOsb.Size = new System.Drawing.Size(123, 22);
			this.miSaveOsb.Text = "Save .osb";
			this.miSaveOsb.Click += new System.EventHandler(this.miSaveOsb_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.osuThreadBugsFeatureRequestsToolStripMenuItem,
            this.bugListToolStripMenuItem,
            this.toolStripSeparator2,
            this.sGLUserManualToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// osuThreadBugsFeatureRequestsToolStripMenuItem
			// 
			this.osuThreadBugsFeatureRequestsToolStripMenuItem.Name = "osuThreadBugsFeatureRequestsToolStripMenuItem";
			this.osuThreadBugsFeatureRequestsToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
			this.osuThreadBugsFeatureRequestsToolStripMenuItem.Text = "osu! Thread (Help/Bugs/Feature Requests)";
			this.osuThreadBugsFeatureRequestsToolStripMenuItem.Click += new System.EventHandler(this.osuThreadBugsFeatureRequestsToolStripMenuItem_Click);
			// 
			// bugListToolStripMenuItem
			// 
			this.bugListToolStripMenuItem.Name = "bugListToolStripMenuItem";
			this.bugListToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
			this.bugListToolStripMenuItem.Text = "Bug/Improvement List";
			this.bugListToolStripMenuItem.Click += new System.EventHandler(this.bugListToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(294, 6);
			// 
			// sGLUserManualToolStripMenuItem
			// 
			this.sGLUserManualToolStripMenuItem.Name = "sGLUserManualToolStripMenuItem";
			this.sGLUserManualToolStripMenuItem.Size = new System.Drawing.Size(297, 22);
			this.sGLUserManualToolStripMenuItem.Text = "SGL User Manual";
			this.sGLUserManualToolStripMenuItem.Click += new System.EventHandler(this.sGLUserManualToolStripMenuItem_Click);
			// 
			// SimpleSGLEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 562);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "SimpleSGLEditor";
			this.Text = "SGL Editor";
			this.Load += new System.EventHandler(this.SimpleSGLEditor_Load);
			this.tabControl.ResumeLayout(false);
			this.tabSGL.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.tabSB.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSGL;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Alsing.Windows.Forms.SyntaxBoxControl SGLBox;
        private Alsing.SourceCode.SyntaxDocument syntaxSGL;
        private System.Windows.Forms.TextBox errorBox;
        private System.Windows.Forms.TabPage tabSB;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripProgressBar compilerPBar;
        private System.Windows.Forms.ToolStripLabel statusLabel;
        private System.Windows.Forms.ToolStripButton compileSBButton;
        private Alsing.Windows.Forms.SyntaxBoxControl storyboardBox;
        private Alsing.SourceCode.SyntaxDocument syntaxStoryboard;
        private System.Windows.Forms.ToolStripLabel CopyrightLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miNew;
        private System.Windows.Forms.ToolStripMenuItem miOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miSaveSgl;
        private System.Windows.Forms.ToolStripMenuItem miSaveOsb;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sGLUserManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem osuThreadBugsFeatureRequestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bugListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}