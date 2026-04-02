namespace NNDSA_SemB.GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelLeft;
        private System.Windows.Forms.GroupBox groupBoxOperations;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOperations;
        private System.Windows.Forms.Button buttonParkCar;
        private System.Windows.Forms.Button buttonRemoveCar;
        private System.Windows.Forms.Button buttonFindNearestFreeSpot;
        private System.Windows.Forms.Button buttonShowOccupiedSpots;
       
        private System.Windows.Forms.GroupBox groupBoxFileOperations;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFileOperations;
        private System.Windows.Forms.Button buttonLoadFromFile;
        private System.Windows.Forms.Button buttonSaveToFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private System.Windows.Forms.TabControl tabControlViews;
        private System.Windows.Forms.TabPage tabPageParkingView;
        private System.Windows.Forms.TabPage tabPageTreapView;
        private System.Windows.Forms.Panel panelParkingView;
        private System.Windows.Forms.Label labelParkingViewPlaceholder;
        private System.Windows.Forms.Panel panelTreapView;
        private System.Windows.Forms.Label labelTreapViewPlaceholder;
        private System.Windows.Forms.OpenFileDialog openFileDialogMain;
        private System.Windows.Forms.SaveFileDialog saveFileDialogMain;

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
        ///  Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanelMain = new TableLayoutPanel();
            panelHeader = new Panel();
            labelTitle = new Label();
            splitContainerMain = new SplitContainer();
            tableLayoutPanelLeft = new TableLayoutPanel();
            groupBoxOperations = new GroupBox();
            flowLayoutPanelOperations = new FlowLayoutPanel();
            buttonParkCar = new Button();
            buttonRemoveCar = new Button();
            buttonFindNearestFreeSpot = new Button();
            buttonShowOccupiedSpots = new Button();
            groupBoxFileOperations = new GroupBox();
            flowLayoutPanelFileOperations = new FlowLayoutPanel();
            buttonLoadFromFile = new Button();
            buttonSaveToFile = new Button();
            tableLayoutPanelRight = new TableLayoutPanel();
            tabControlViews = new TabControl();
            tabPageParkingView = new TabPage();
            panelParkingView = new Panel();
            labelParkingViewPlaceholder = new Label();
            tabPageTreapView = new TabPage();
            panelTreapView = new Panel();
            labelTreapViewPlaceholder = new Label();
            openFileDialogMain = new OpenFileDialog();
            saveFileDialogMain = new SaveFileDialog();
            tableLayoutPanelMain.SuspendLayout();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).BeginInit();
            splitContainerMain.Panel1.SuspendLayout();
            splitContainerMain.Panel2.SuspendLayout();
            splitContainerMain.SuspendLayout();
            tableLayoutPanelLeft.SuspendLayout();
            groupBoxOperations.SuspendLayout();
            flowLayoutPanelOperations.SuspendLayout();
            groupBoxFileOperations.SuspendLayout();
            flowLayoutPanelFileOperations.SuspendLayout();
            tableLayoutPanelRight.SuspendLayout();
            tabControlViews.SuspendLayout();
            tabPageParkingView.SuspendLayout();
            panelParkingView.SuspendLayout();
            tabPageTreapView.SuspendLayout();
            panelTreapView.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(panelHeader, 0, 0);
            tableLayoutPanelMain.Controls.Add(splitContainerMain, 0, 1);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Margin = new Padding(0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 2;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(1211, 646);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(235, 242, 250);
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Dock = DockStyle.Fill;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(14, 8, 14, 8);
            panelHeader.Size = new Size(1211, 50);
            panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelTitle.Location = new Point(14, 9);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(166, 30);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Parkovací dům";
            // 
            // splitContainerMain
            // 
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.FixedPanel = FixedPanel.Panel1;
            splitContainerMain.Location = new Point(10, 59);
            splitContainerMain.Margin = new Padding(10, 9, 10, 9);
            splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            splitContainerMain.Panel1.Controls.Add(tableLayoutPanelLeft);
            splitContainerMain.Panel1MinSize = 320;
            // 
            // splitContainerMain.Panel2
            // 
            splitContainerMain.Panel2.Controls.Add(tableLayoutPanelRight);
            splitContainerMain.Panel2MinSize = 420;
            splitContainerMain.Size = new Size(1191, 578);
            splitContainerMain.SplitterDistance = 320;
            splitContainerMain.TabIndex = 1;
            // 
            // tableLayoutPanelLeft
            // 
            tableLayoutPanelLeft.ColumnCount = 1;
            tableLayoutPanelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Controls.Add(groupBoxOperations, 0, 0);
            tableLayoutPanelLeft.Controls.Add(groupBoxFileOperations, 0, 1);
            tableLayoutPanelLeft.Dock = DockStyle.Fill;
            tableLayoutPanelLeft.Location = new Point(0, 0);
            tableLayoutPanelLeft.Margin = new Padding(0);
            tableLayoutPanelLeft.Name = "tableLayoutPanelLeft";
            tableLayoutPanelLeft.RowCount = 3;
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 280F));
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tableLayoutPanelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelLeft.Size = new Size(320, 578);
            tableLayoutPanelLeft.TabIndex = 0;
            // 
            // groupBoxOperations
            // 
            groupBoxOperations.Controls.Add(flowLayoutPanelOperations);
            groupBoxOperations.Dock = DockStyle.Fill;
            groupBoxOperations.Location = new Point(3, 3);
            groupBoxOperations.Name = "groupBoxOperations";
            groupBoxOperations.Padding = new Padding(9, 8, 9, 8);
            groupBoxOperations.Size = new Size(314, 274);
            groupBoxOperations.TabIndex = 0;
            groupBoxOperations.TabStop = false;
            groupBoxOperations.Text = "Operace";
            // 
            // flowLayoutPanelOperations
            // 
            flowLayoutPanelOperations.AutoScroll = true;
            flowLayoutPanelOperations.Controls.Add(buttonParkCar);
            flowLayoutPanelOperations.Controls.Add(buttonRemoveCar);
            flowLayoutPanelOperations.Controls.Add(buttonFindNearestFreeSpot);
            flowLayoutPanelOperations.Controls.Add(buttonShowOccupiedSpots);
            flowLayoutPanelOperations.Dock = DockStyle.Fill;
            flowLayoutPanelOperations.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelOperations.Location = new Point(9, 24);
            flowLayoutPanelOperations.Name = "flowLayoutPanelOperations";
            flowLayoutPanelOperations.Size = new Size(296, 242);
            flowLayoutPanelOperations.TabIndex = 0;
            flowLayoutPanelOperations.WrapContents = false;
            // 
            // buttonParkCar
            // 
            buttonParkCar.Location = new Point(3, 3);
            buttonParkCar.Margin = new Padding(3, 3, 3, 8);
            buttonParkCar.Name = "buttonParkCar";
            buttonParkCar.Size = new Size(267, 30);
            buttonParkCar.TabIndex = 0;
            buttonParkCar.Text = "Obsadit místo";
            buttonParkCar.UseVisualStyleBackColor = true;
            // 
            // buttonRemoveCar
            // 
            buttonRemoveCar.Location = new Point(3, 41);
            buttonRemoveCar.Margin = new Padding(3, 0, 3, 8);
            buttonRemoveCar.Name = "buttonRemoveCar";
            buttonRemoveCar.Size = new Size(267, 30);
            buttonRemoveCar.TabIndex = 1;
            buttonRemoveCar.Text = "Uvolnit místo";
            buttonRemoveCar.UseVisualStyleBackColor = true;
            // 
            // buttonFindNearestFreeSpot
            // 
            buttonFindNearestFreeSpot.Location = new Point(3, 79);
            buttonFindNearestFreeSpot.Margin = new Padding(3, 0, 3, 8);
            buttonFindNearestFreeSpot.Name = "buttonFindNearestFreeSpot";
            buttonFindNearestFreeSpot.Size = new Size(267, 30);
            buttonFindNearestFreeSpot.TabIndex = 3;
            buttonFindNearestFreeSpot.Text = "Najít nejbližší volné místo";
            buttonFindNearestFreeSpot.UseVisualStyleBackColor = true;
            // 
            // buttonShowOccupiedSpots
            // 
            buttonShowOccupiedSpots.Location = new Point(3, 117);
            buttonShowOccupiedSpots.Margin = new Padding(3, 0, 3, 8);
            buttonShowOccupiedSpots.Name = "buttonShowOccupiedSpots";
            buttonShowOccupiedSpots.Size = new Size(267, 30);
            buttonShowOccupiedSpots.TabIndex = 4;
            buttonShowOccupiedSpots.Text = "Zobrazit obsazená místa";
            buttonShowOccupiedSpots.UseVisualStyleBackColor = true;
            // 
            // groupBoxFileOperations
            // 
            groupBoxFileOperations.Controls.Add(flowLayoutPanelFileOperations);
            groupBoxFileOperations.Dock = DockStyle.Fill;
            groupBoxFileOperations.Location = new Point(3, 283);
            groupBoxFileOperations.Name = "groupBoxFileOperations";
            groupBoxFileOperations.Padding = new Padding(9, 8, 9, 8);
            groupBoxFileOperations.Size = new Size(314, 144);
            groupBoxFileOperations.TabIndex = 1;
            groupBoxFileOperations.TabStop = false;
            groupBoxFileOperations.Text = "Soubor a data";
            // 
            // flowLayoutPanelFileOperations
            // 
            flowLayoutPanelFileOperations.AutoScroll = true;
            flowLayoutPanelFileOperations.Controls.Add(buttonLoadFromFile);
            flowLayoutPanelFileOperations.Controls.Add(buttonSaveToFile);
            flowLayoutPanelFileOperations.Dock = DockStyle.Fill;
            flowLayoutPanelFileOperations.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelFileOperations.Location = new Point(9, 24);
            flowLayoutPanelFileOperations.Name = "flowLayoutPanelFileOperations";
            flowLayoutPanelFileOperations.Size = new Size(296, 112);
            flowLayoutPanelFileOperations.TabIndex = 0;
            flowLayoutPanelFileOperations.WrapContents = false;
            // 
            // buttonLoadFromFile
            // 
            buttonLoadFromFile.Location = new Point(3, 3);
            buttonLoadFromFile.Margin = new Padding(3, 3, 3, 8);
            buttonLoadFromFile.Name = "buttonLoadFromFile";
            buttonLoadFromFile.Size = new Size(267, 30);
            buttonLoadFromFile.TabIndex = 0;
            buttonLoadFromFile.Text = "Načíst ze souboru";
            buttonLoadFromFile.UseVisualStyleBackColor = true;
            buttonLoadFromFile.Click += buttonLoadFromFile_Click;
            // 
            // buttonSaveToFile
            // 
            buttonSaveToFile.Location = new Point(3, 41);
            buttonSaveToFile.Margin = new Padding(3, 0, 3, 8);
            buttonSaveToFile.Name = "buttonSaveToFile";
            buttonSaveToFile.Size = new Size(267, 30);
            buttonSaveToFile.TabIndex = 1;
            buttonSaveToFile.Text = "Uložit do souboru";
            buttonSaveToFile.UseVisualStyleBackColor = true;
            buttonSaveToFile.Click += buttonSaveToFile_Click;
            // 
            // tableLayoutPanelRight
            // 
            tableLayoutPanelRight.ColumnCount = 1;
            tableLayoutPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelRight.Controls.Add(tabControlViews, 0, 0);
            tableLayoutPanelRight.Dock = DockStyle.Fill;
            tableLayoutPanelRight.Location = new Point(0, 0);
            tableLayoutPanelRight.Margin = new Padding(0);
            tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            tableLayoutPanelRight.RowCount = 2;
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 98.44291F));
            tableLayoutPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 1.55709338F));
            tableLayoutPanelRight.Size = new Size(867, 578);
            tableLayoutPanelRight.TabIndex = 0;
            // 
            // tabControlViews
            // 
            tabControlViews.Controls.Add(tabPageParkingView);
            tabControlViews.Dock = DockStyle.Fill;
            tabControlViews.Location = new Point(3, 3);
            tabControlViews.Name = "tabControlViews";
            tabControlViews.SelectedIndex = 0;
            tabControlViews.Size = new Size(861, 563);
            tabControlViews.TabIndex = 0;
            // 
            // tabPageParkingView
            // 
            tabPageParkingView.Controls.Add(panelParkingView);
            tabPageParkingView.Location = new Point(4, 24);
            tabPageParkingView.Name = "tabPageParkingView";
            tabPageParkingView.Padding = new Padding(7, 6, 7, 6);
            tabPageParkingView.Size = new Size(853, 535);
            tabPageParkingView.TabIndex = 0;
            tabPageParkingView.Text = "Parkoviště";
            tabPageParkingView.UseVisualStyleBackColor = true;
            // 
            // panelParkingView
            // 
            panelParkingView.BackColor = Color.White;
            panelParkingView.BorderStyle = BorderStyle.FixedSingle;
            panelParkingView.Controls.Add(labelParkingViewPlaceholder);
            panelParkingView.Dock = DockStyle.Fill;
            panelParkingView.Location = new Point(7, 6);
            panelParkingView.Name = "panelParkingView";
            panelParkingView.Padding = new Padding(10, 9, 10, 9);
            panelParkingView.Size = new Size(839, 523);
            panelParkingView.TabIndex = 0;
            // 
            // labelParkingViewPlaceholder
            // 
            labelParkingViewPlaceholder.Dock = DockStyle.Fill;
            labelParkingViewPlaceholder.ForeColor = Color.DimGray;
            labelParkingViewPlaceholder.Location = new Point(10, 9);
            labelParkingViewPlaceholder.Name = "labelParkingViewPlaceholder";
            labelParkingViewPlaceholder.Size = new Size(817, 503);
            labelParkingViewPlaceholder.TabIndex = 0;
            labelParkingViewPlaceholder.Text = "Zde budeme vykreslovat zvolené podlaží parkovacího domu.";
            labelParkingViewPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tabPageTreapView
            // 
            tabPageTreapView.Controls.Add(panelTreapView);
            tabPageTreapView.Location = new Point(4, 24);
            tabPageTreapView.Name = "tabPageTreapView";
            tabPageTreapView.Padding = new Padding(7, 6, 7, 6);
            tabPageTreapView.Size = new Size(853, 359);
            tabPageTreapView.TabIndex = 1;
            tabPageTreapView.Text = "Treap";
            tabPageTreapView.UseVisualStyleBackColor = true;
            // 
            // panelTreapView
            // 
            panelTreapView.BackColor = Color.White;
            panelTreapView.BorderStyle = BorderStyle.FixedSingle;
            panelTreapView.Controls.Add(labelTreapViewPlaceholder);
            panelTreapView.Dock = DockStyle.Fill;
            panelTreapView.Location = new Point(7, 6);
            panelTreapView.Name = "panelTreapView";
            panelTreapView.Padding = new Padding(10, 9, 10, 9);
            panelTreapView.Size = new Size(839, 347);
            panelTreapView.TabIndex = 0;
            // 
            // labelTreapViewPlaceholder
            // 
            labelTreapViewPlaceholder.Dock = DockStyle.Fill;
            labelTreapViewPlaceholder.ForeColor = Color.DimGray;
            labelTreapViewPlaceholder.Location = new Point(10, 9);
            labelTreapViewPlaceholder.Name = "labelTreapViewPlaceholder";
            labelTreapViewPlaceholder.Size = new Size(817, 327);
            labelTreapViewPlaceholder.TabIndex = 0;
            labelTreapViewPlaceholder.Text = "Zde budeme zobrazovat strukturu Treapu vybraného podlaží.";
            labelTreapViewPlaceholder.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // openFileDialogMain
            // 
            openFileDialogMain.Filter = "Textové soubory|*.txt;*.csv|Všechny soubory|*.*";
            openFileDialogMain.Title = "Načíst data parkovacího domu";
            // 
            // saveFileDialogMain
            // 
            saveFileDialogMain.Filter = "Textové soubory|*.txt;*.csv|Všechny soubory|*.*";
            saveFileDialogMain.Title = "Uložit data parkovacího domu";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1211, 646);
            Controls.Add(tableLayoutPanelMain);
            MinimumSize = new Size(964, 580);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NNDSA_SemB.GUI";
            tableLayoutPanelMain.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            splitContainerMain.Panel1.ResumeLayout(false);
            splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerMain).EndInit();
            splitContainerMain.ResumeLayout(false);
            tableLayoutPanelLeft.ResumeLayout(false);
            groupBoxOperations.ResumeLayout(false);
            flowLayoutPanelOperations.ResumeLayout(false);
            groupBoxFileOperations.ResumeLayout(false);
            flowLayoutPanelFileOperations.ResumeLayout(false);
            tableLayoutPanelRight.ResumeLayout(false);
            tabControlViews.ResumeLayout(false);
            tabPageParkingView.ResumeLayout(false);
            panelParkingView.ResumeLayout(false);
            tabPageTreapView.ResumeLayout(false);
            panelTreapView.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}