namespace MyNotes2
{
	partial class MyNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyNotes));
            this.idleTimer = new System.Windows.Forms.Timer(this.components);
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbTasks = new System.Windows.Forms.RadioButton();
            this.rbNotes = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.rtSearch = new System.Windows.Forms.RichTextBox();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkShowPass = new System.Windows.Forms.CheckBox();
            this.btnCompactMDB = new System.Windows.Forms.Button();
            this.chkOnTop = new System.Windows.Forms.CheckBox();
            this.dpSearchStops = new System.Windows.Forms.DateTimePicker();
            this.lblSearch = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnLunch = new System.Windows.Forms.Button();
            this.btnTaskTimer = new System.Windows.Forms.Button();
            this.txtLOE = new System.Windows.Forms.TextBox();
            this.chkPROD = new System.Windows.Forms.CheckBox();
            this.chkDev = new System.Windows.Forms.CheckBox();
            this.chkSysTest = new System.Windows.Forms.CheckBox();
            this.chkUAT = new System.Windows.Forms.CheckBox();
            this.btnYesterday = new System.Windows.Forms.Button();
            this.btnSaveNotes = new System.Windows.Forms.Button();
            this.txtHoursThisWeek = new System.Windows.Forms.TextBox();
            this.txtHoursToday = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabDaily = new System.Windows.Forms.TabPage();
            this.btnTomorrow = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Day = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEndDay = new System.Windows.Forms.TextBox();
            this.txtStartDay = new System.Windows.Forms.TextBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.txtErrors = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTasks = new System.Windows.Forms.TabPage();
            this.gBoxStatus = new System.Windows.Forms.GroupBox();
            this.radBtnPROD = new System.Windows.Forms.RadioButton();
            this.radBtnUAT = new System.Windows.Forms.RadioButton();
            this.radBtnSYSTEST = new System.Windows.Forms.RadioButton();
            this.radBtnDEV = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTasks = new System.Windows.Forms.RichTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.lblTotalTime = new System.Windows.Forms.Label();
            this.lbl12 = new System.Windows.Forms.Label();
            this.btnNewTask = new System.Windows.Forms.Button();
            this.cboPriority = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboTaskName = new System.Windows.Forms.ComboBox();
            this.btnDeleteTask = new System.Windows.Forms.Button();
            this.btnQueryTask = new System.Windows.Forms.Button();
            this.btnSaveTask = new System.Windows.Forms.Button();
            this.gboxOrder = new System.Windows.Forms.GroupBox();
            this.chkLastUpdated = new System.Windows.Forms.CheckBox();
            this.chkTaskName = new System.Windows.Forms.CheckBox();
            this.chkPriority = new System.Windows.Forms.CheckBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.taskTimer = new System.Windows.Forms.Timer(this.components);
            this.ErrorTabPage = new System.Windows.Forms.TabPage();
            this.tabSearch.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabDaily.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Day.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabTasks.SuspendLayout();
            this.gBoxStatus.SuspendLayout();
            this.gboxOrder.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // idleTimer
            // 
            this.idleTimer.Enabled = true;
            this.idleTimer.Interval = 50000;
            this.idleTimer.Tick += new System.EventHandler(this.idleTimer_Tick);
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.btnSearch);
            this.tabSearch.Controls.Add(this.groupBox2);
            this.tabSearch.Controls.Add(this.label11);
            this.tabSearch.Controls.Add(this.txtSearch);
            this.tabSearch.Controls.Add(this.rtSearch);
            this.tabSearch.Location = new System.Drawing.Point(4, 25);
            this.tabSearch.Margin = new System.Windows.Forms.Padding(4);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(621, 454);
            this.tabSearch.TabIndex = 2;
            this.tabSearch.Text = "Search";
            this.tabSearch.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(341, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 28);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbTasks);
            this.groupBox2.Controls.Add(this.rbNotes);
            this.groupBox2.Location = new System.Drawing.Point(480, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(99, 75);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Search:";
            // 
            // rbTasks
            // 
            this.rbTasks.AutoSize = true;
            this.rbTasks.Location = new System.Drawing.Point(9, 47);
            this.rbTasks.Margin = new System.Windows.Forms.Padding(4);
            this.rbTasks.Name = "rbTasks";
            this.rbTasks.Size = new System.Drawing.Size(67, 21);
            this.rbTasks.TabIndex = 1;
            this.rbTasks.Text = "Tasks";
            this.rbTasks.UseVisualStyleBackColor = true;
            // 
            // rbNotes
            // 
            this.rbNotes.AutoSize = true;
            this.rbNotes.Checked = true;
            this.rbNotes.Location = new System.Drawing.Point(9, 23);
            this.rbNotes.Margin = new System.Windows.Forms.Padding(4);
            this.rbNotes.Name = "rbNotes";
            this.rbNotes.Size = new System.Drawing.Size(66, 21);
            this.rbNotes.TabIndex = 0;
            this.rbNotes.TabStop = true;
            this.rbNotes.Text = "Notes";
            this.rbNotes.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 5);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "Search phrase";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 41);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(459, 22);
            this.txtSearch.TabIndex = 1;
            // 
            // rtSearch
            // 
            this.rtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtSearch.Location = new System.Drawing.Point(0, 89);
            this.rtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.rtSearch.Name = "rtSearch";
            this.rtSearch.Size = new System.Drawing.Size(617, 360);
            this.rtSearch.TabIndex = 0;
            this.rtSearch.Text = "";
            this.rtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtSearch_KeyDown);
            // 
            // tabOptions
            // 
            this.tabOptions.Controls.Add(this.txtPassword);
            this.tabOptions.Controls.Add(this.chkShowPass);
            this.tabOptions.Controls.Add(this.btnCompactMDB);
            this.tabOptions.Controls.Add(this.chkOnTop);
            this.tabOptions.Controls.Add(this.dpSearchStops);
            this.tabOptions.Controls.Add(this.lblSearch);
            this.tabOptions.Location = new System.Drawing.Point(4, 25);
            this.tabOptions.Margin = new System.Windows.Forms.Padding(4);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Size = new System.Drawing.Size(621, 454);
            this.tabOptions.TabIndex = 4;
            this.tabOptions.Text = "Options";
            this.tabOptions.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(89, 230);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(415, 22);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // chkShowPass
            // 
            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Location = new System.Drawing.Point(89, 278);
            this.chkShowPass.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Size = new System.Drawing.Size(129, 21);
            this.chkShowPass.TabIndex = 6;
            this.chkShowPass.Text = "Show Password";
            this.chkShowPass.UseVisualStyleBackColor = true;
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.chkShowPass_CheckedChanged);
            // 
            // btnCompactMDB
            // 
            this.btnCompactMDB.Location = new System.Drawing.Point(89, 124);
            this.btnCompactMDB.Margin = new System.Windows.Forms.Padding(4);
            this.btnCompactMDB.Name = "btnCompactMDB";
            this.btnCompactMDB.Size = new System.Drawing.Size(184, 28);
            this.btnCompactMDB.TabIndex = 5;
            this.btnCompactMDB.Text = "Compact & Repair MDB";
            this.btnCompactMDB.UseVisualStyleBackColor = true;
            this.btnCompactMDB.Click += new System.EventHandler(this.btnCompactMDB_Click);
            // 
            // chkOnTop
            // 
            this.chkOnTop.AutoSize = true;
            this.chkOnTop.Location = new System.Drawing.Point(89, 96);
            this.chkOnTop.Margin = new System.Windows.Forms.Padding(4);
            this.chkOnTop.Name = "chkOnTop";
            this.chkOnTop.Size = new System.Drawing.Size(125, 21);
            this.chkOnTop.TabIndex = 4;
            this.chkOnTop.Text = "Always On Top";
            this.chkOnTop.UseVisualStyleBackColor = true;
            this.chkOnTop.CheckedChanged += new System.EventHandler(this.chkOnTop_CheckedChanged);
            // 
            // dpSearchStops
            // 
            this.dpSearchStops.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpSearchStops.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpSearchStops.Location = new System.Drawing.Point(312, 382);
            this.dpSearchStops.Margin = new System.Windows.Forms.Padding(4);
            this.dpSearchStops.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
            this.dpSearchStops.Name = "dpSearchStops";
            this.dpSearchStops.Size = new System.Drawing.Size(171, 30);
            this.dpSearchStops.TabIndex = 1;
            this.dpSearchStops.TabStop = false;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(37, 383);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(245, 25);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Search back until this date.";
            // 
            // btnLunch
            // 
            this.btnLunch.Location = new System.Drawing.Point(9, 25);
            this.btnLunch.Margin = new System.Windows.Forms.Padding(4);
            this.btnLunch.Name = "btnLunch";
            this.btnLunch.Size = new System.Drawing.Size(100, 28);
            this.btnLunch.TabIndex = 0;
            this.btnLunch.Text = "Start";
            this.toolTip1.SetToolTip(this.btnLunch, "Start/Stop lunch timer; Right Click to Change");
            this.btnLunch.UseVisualStyleBackColor = true;
            this.btnLunch.Click += new System.EventHandler(this.btnLunch_Click);
            this.btnLunch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLunch_MouseUp);
            // 
            // btnTaskTimer
            // 
            this.btnTaskTimer.Location = new System.Drawing.Point(321, 78);
            this.btnTaskTimer.Margin = new System.Windows.Forms.Padding(4);
            this.btnTaskTimer.Name = "btnTaskTimer";
            this.btnTaskTimer.Size = new System.Drawing.Size(56, 28);
            this.btnTaskTimer.TabIndex = 20;
            this.btnTaskTimer.Tag = "stoped";
            this.btnTaskTimer.Text = "00:00";
            this.toolTip1.SetToolTip(this.btnTaskTimer, "Right Click to change");
            this.btnTaskTimer.UseVisualStyleBackColor = true;
            this.btnTaskTimer.Click += new System.EventHandler(this.btnTaskTimer_Click);
            this.btnTaskTimer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTaskTimer_MouseUp);
            // 
            // txtLOE
            // 
            this.txtLOE.Location = new System.Drawing.Point(47, 44);
            this.txtLOE.Margin = new System.Windows.Forms.Padding(4);
            this.txtLOE.MaxLength = 3;
            this.txtLOE.Name = "txtLOE";
            this.txtLOE.Size = new System.Drawing.Size(43, 22);
            this.txtLOE.TabIndex = 26;
            this.toolTip1.SetToolTip(this.txtLOE, "Once set it can not be changed");
            // 
            // chkPROD
            // 
            this.chkPROD.AutoSize = true;
            this.chkPROD.Location = new System.Drawing.Point(385, 122);
            this.chkPROD.Margin = new System.Windows.Forms.Padding(4);
            this.chkPROD.Name = "chkPROD";
            this.chkPROD.Size = new System.Drawing.Size(70, 21);
            this.chkPROD.TabIndex = 10;
            this.chkPROD.Text = "PROD";
            this.toolTip1.SetToolTip(this.chkPROD, "Include Tasks in PROD");
            this.chkPROD.UseVisualStyleBackColor = true;
            this.chkPROD.CheckedChanged += new System.EventHandler(this.chkPROD_CheckedChanged);
            // 
            // chkDev
            // 
            this.chkDev.AutoSize = true;
            this.chkDev.Location = new System.Drawing.Point(385, 53);
            this.chkDev.Margin = new System.Windows.Forms.Padding(4);
            this.chkDev.Name = "chkDev";
            this.chkDev.Size = new System.Drawing.Size(58, 21);
            this.chkDev.TabIndex = 28;
            this.chkDev.Text = "DEV";
            this.toolTip1.SetToolTip(this.chkDev, "Include Tasks in PROD");
            this.chkDev.UseVisualStyleBackColor = true;
            this.chkDev.CheckedChanged += new System.EventHandler(this.chkDev_CheckedChanged);
            // 
            // chkSysTest
            // 
            this.chkSysTest.AutoSize = true;
            this.chkSysTest.Location = new System.Drawing.Point(385, 76);
            this.chkSysTest.Margin = new System.Windows.Forms.Padding(4);
            this.chkSysTest.Name = "chkSysTest";
            this.chkSysTest.Size = new System.Drawing.Size(93, 21);
            this.chkSysTest.TabIndex = 29;
            this.chkSysTest.Text = "SYSTEST";
            this.toolTip1.SetToolTip(this.chkSysTest, "Include Tasks in PROD");
            this.chkSysTest.UseVisualStyleBackColor = true;
            this.chkSysTest.CheckedChanged += new System.EventHandler(this.chkSysTest_CheckedChanged);
            // 
            // chkUAT
            // 
            this.chkUAT.AutoSize = true;
            this.chkUAT.Location = new System.Drawing.Point(385, 99);
            this.chkUAT.Margin = new System.Windows.Forms.Padding(4);
            this.chkUAT.Name = "chkUAT";
            this.chkUAT.Size = new System.Drawing.Size(58, 21);
            this.chkUAT.TabIndex = 30;
            this.chkUAT.Text = "UAT";
            this.toolTip1.SetToolTip(this.chkUAT, "Include Tasks in PROD");
            this.chkUAT.UseVisualStyleBackColor = true;
            this.chkUAT.CheckedChanged += new System.EventHandler(this.chkUAT_CheckedChanged);
            // 
            // btnYesterday
            // 
            this.btnYesterday.Font = new System.Drawing.Font("Wingdings 2", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYesterday.Image = ((System.Drawing.Image)(resources.GetObject("btnYesterday.Image")));
            this.btnYesterday.Location = new System.Drawing.Point(429, 69);
            this.btnYesterday.Margin = new System.Windows.Forms.Padding(4);
            this.btnYesterday.Name = "btnYesterday";
            this.btnYesterday.Size = new System.Drawing.Size(57, 53);
            this.btnYesterday.TabIndex = 10;
            this.btnYesterday.UseVisualStyleBackColor = true;
            this.btnYesterday.Click += new System.EventHandler(this.btnYesterday_Click);
            // 
            // btnSaveNotes
            // 
            this.btnSaveNotes.Location = new System.Drawing.Point(477, 142);
            this.btnSaveNotes.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveNotes.Name = "btnSaveNotes";
            this.btnSaveNotes.Size = new System.Drawing.Size(93, 28);
            this.btnSaveNotes.TabIndex = 8;
            this.btnSaveNotes.Text = "Save";
            this.btnSaveNotes.UseVisualStyleBackColor = true;
            this.btnSaveNotes.Click += new System.EventHandler(this.btnSaveNotes_Click);
            // 
            // txtHoursThisWeek
            // 
            this.txtHoursThisWeek.Location = new System.Drawing.Point(396, 145);
            this.txtHoursThisWeek.Margin = new System.Windows.Forms.Padding(4);
            this.txtHoursThisWeek.MaxLength = 5;
            this.txtHoursThisWeek.Name = "txtHoursThisWeek";
            this.txtHoursThisWeek.ReadOnly = true;
            this.txtHoursThisWeek.Size = new System.Drawing.Size(72, 22);
            this.txtHoursThisWeek.TabIndex = 7;
            this.txtHoursThisWeek.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtHoursToday
            // 
            this.txtHoursToday.Location = new System.Drawing.Point(307, 146);
            this.txtHoursToday.Margin = new System.Windows.Forms.Padding(4);
            this.txtHoursToday.MaxLength = 5;
            this.txtHoursToday.Name = "txtHoursToday";
            this.txtHoursToday.ReadOnly = true;
            this.txtHoursToday.Size = new System.Drawing.Size(72, 22);
            this.txtHoursToday.TabIndex = 6;
            this.txtHoursToday.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(392, 126);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "This week";
            // 
            // tabDaily
            // 
            this.tabDaily.Controls.Add(this.btnTomorrow);
            this.tabDaily.Controls.Add(this.btnYesterday);
            this.tabDaily.Controls.Add(this.txtNotes);
            this.tabDaily.Controls.Add(this.btnSaveNotes);
            this.tabDaily.Controls.Add(this.txtHoursThisWeek);
            this.tabDaily.Controls.Add(this.txtHoursToday);
            this.tabDaily.Controls.Add(this.label6);
            this.tabDaily.Controls.Add(this.label5);
            this.tabDaily.Controls.Add(this.groupBox1);
            this.tabDaily.Controls.Add(this.Day);
            this.tabDaily.Controls.Add(this.monthCalendar1);
            this.tabDaily.Location = new System.Drawing.Point(4, 25);
            this.tabDaily.Margin = new System.Windows.Forms.Padding(4);
            this.tabDaily.Name = "tabDaily";
            this.tabDaily.Padding = new System.Windows.Forms.Padding(4);
            this.tabDaily.Size = new System.Drawing.Size(621, 454);
            this.tabDaily.TabIndex = 0;
            this.tabDaily.Text = "Daily Notes";
            this.tabDaily.UseVisualStyleBackColor = true;
            // 
            // btnTomorrow
            // 
            this.btnTomorrow.Image = ((System.Drawing.Image)(resources.GetObject("btnTomorrow.Image")));
            this.btnTomorrow.Location = new System.Drawing.Point(501, 69);
            this.btnTomorrow.Margin = new System.Windows.Forms.Padding(4);
            this.btnTomorrow.Name = "btnTomorrow";
            this.btnTomorrow.Size = new System.Drawing.Size(57, 53);
            this.btnTomorrow.TabIndex = 11;
            this.btnTomorrow.UseVisualStyleBackColor = true;
            this.btnTomorrow.Click += new System.EventHandler(this.btnTomorrow_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.AcceptsTab = true;
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotes.Font = new System.Drawing.Font("Poor Richard", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(4, 207);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(4);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(609, 237);
            this.txtNotes.TabIndex = 9;
            this.txtNotes.Text = "";
            this.txtNotes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNotes_KeyDown);
            this.txtNotes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNotes_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(299, 126);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Hours today";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLunch);
            this.groupBox1.Location = new System.Drawing.Point(303, 64);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(119, 58);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lunch";
            // 
            // Day
            // 
            this.Day.Controls.Add(this.label2);
            this.Day.Controls.Add(this.label1);
            this.Day.Controls.Add(this.txtEndDay);
            this.Day.Controls.Add(this.txtStartDay);
            this.Day.Location = new System.Drawing.Point(303, 9);
            this.Day.Margin = new System.Windows.Forms.Padding(4);
            this.Day.Name = "Day";
            this.Day.Padding = new System.Windows.Forms.Padding(4);
            this.Day.Size = new System.Drawing.Size(267, 48);
            this.Day.TabIndex = 2;
            this.Day.TabStop = false;
            this.Day.Text = "Day Start/End";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "End";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start";
            // 
            // txtEndDay
            // 
            this.txtEndDay.Location = new System.Drawing.Point(173, 16);
            this.txtEndDay.Margin = new System.Windows.Forms.Padding(4);
            this.txtEndDay.MaxLength = 8;
            this.txtEndDay.Name = "txtEndDay";
            this.txtEndDay.Size = new System.Drawing.Size(72, 22);
            this.txtEndDay.TabIndex = 1;
            this.txtEndDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEndDay_KeyDown);
            // 
            // txtStartDay
            // 
            this.txtStartDay.Location = new System.Drawing.Point(45, 18);
            this.txtStartDay.Margin = new System.Windows.Forms.Padding(4);
            this.txtStartDay.MaxLength = 8;
            this.txtStartDay.Name = "txtStartDay";
            this.txtStartDay.Size = new System.Drawing.Size(72, 22);
            this.txtStartDay.TabIndex = 0;
            this.txtStartDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStartDay_KeyDown);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.AnnuallyBoldedDates = new System.DateTime[] {
        new System.DateTime(2012, 1, 1, 0, 0, 0, 0),
        new System.DateTime(2012, 12, 25, 0, 0, 0, 0),
        new System.DateTime(2012, 7, 4, 0, 0, 0, 0)};
            this.monthCalendar1.Location = new System.Drawing.Point(-5, 0);
            this.monthCalendar1.Margin = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.TabStop = false;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // txtErrors
            // 
            this.txtErrors.Location = new System.Drawing.Point(0, 0);
            this.txtErrors.Name = "txtErrors";
            this.txtErrors.Size = new System.Drawing.Size(100, 96);
            this.txtErrors.TabIndex = 0;
            this.txtErrors.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDaily);
            this.tabControl1.Controls.Add(this.tabTasks);
            this.tabControl1.Controls.Add(this.tabSearch);
            this.tabControl1.Controls.Add(this.tabOptions);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(629, 483);
            this.tabControl1.TabIndex = 9;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabTasks
            // 
            this.tabTasks.Controls.Add(this.chkUAT);
            this.tabTasks.Controls.Add(this.chkSysTest);
            this.tabTasks.Controls.Add(this.chkDev);
            this.tabTasks.Controls.Add(this.gBoxStatus);
            this.tabTasks.Controls.Add(this.txtLOE);
            this.tabTasks.Controls.Add(this.label3);
            this.tabTasks.Controls.Add(this.txtTasks);
            this.tabTasks.Controls.Add(this.btnTaskTimer);
            this.tabTasks.Controls.Add(this.label12);
            this.tabTasks.Controls.Add(this.lblTotalTime);
            this.tabTasks.Controls.Add(this.lbl12);
            this.tabTasks.Controls.Add(this.btnNewTask);
            this.tabTasks.Controls.Add(this.cboPriority);
            this.tabTasks.Controls.Add(this.chkPROD);
            this.tabTasks.Controls.Add(this.label10);
            this.tabTasks.Controls.Add(this.cboTaskName);
            this.tabTasks.Controls.Add(this.btnDeleteTask);
            this.tabTasks.Controls.Add(this.btnQueryTask);
            this.tabTasks.Controls.Add(this.btnSaveTask);
            this.tabTasks.Controls.Add(this.gboxOrder);
            this.tabTasks.Location = new System.Drawing.Point(4, 25);
            this.tabTasks.Margin = new System.Windows.Forms.Padding(4);
            this.tabTasks.Name = "tabTasks";
            this.tabTasks.Padding = new System.Windows.Forms.Padding(4);
            this.tabTasks.Size = new System.Drawing.Size(621, 454);
            this.tabTasks.TabIndex = 1;
            this.tabTasks.Text = "Tasks";
            this.tabTasks.UseVisualStyleBackColor = true;
            // 
            // gBoxStatus
            // 
            this.gBoxStatus.Controls.Add(this.radBtnPROD);
            this.gBoxStatus.Controls.Add(this.radBtnUAT);
            this.gBoxStatus.Controls.Add(this.radBtnSYSTEST);
            this.gBoxStatus.Controls.Add(this.radBtnDEV);
            this.gBoxStatus.Location = new System.Drawing.Point(8, 74);
            this.gBoxStatus.Margin = new System.Windows.Forms.Padding(4);
            this.gBoxStatus.Name = "gBoxStatus";
            this.gBoxStatus.Padding = new System.Windows.Forms.Padding(4);
            this.gBoxStatus.Size = new System.Drawing.Size(307, 36);
            this.gBoxStatus.TabIndex = 27;
            this.gBoxStatus.TabStop = false;
            // 
            // radBtnPROD
            // 
            this.radBtnPROD.AutoSize = true;
            this.radBtnPROD.Location = new System.Drawing.Point(224, 7);
            this.radBtnPROD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radBtnPROD.Name = "radBtnPROD";
            this.radBtnPROD.Size = new System.Drawing.Size(69, 21);
            this.radBtnPROD.TabIndex = 3;
            this.radBtnPROD.TabStop = true;
            this.radBtnPROD.Text = "PROD";
            this.radBtnPROD.UseVisualStyleBackColor = true;
            this.radBtnPROD.CheckedChanged += new System.EventHandler(this.radBtnPROD_CheckedChanged);
            // 
            // radBtnUAT
            // 
            this.radBtnUAT.AutoSize = true;
            this.radBtnUAT.Location = new System.Drawing.Point(160, 7);
            this.radBtnUAT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radBtnUAT.Name = "radBtnUAT";
            this.radBtnUAT.Size = new System.Drawing.Size(57, 21);
            this.radBtnUAT.TabIndex = 2;
            this.radBtnUAT.TabStop = true;
            this.radBtnUAT.Text = "UAT";
            this.radBtnUAT.UseVisualStyleBackColor = true;
            this.radBtnUAT.CheckedChanged += new System.EventHandler(this.radBtnUAT_CheckedChanged);
            // 
            // radBtnSYSTEST
            // 
            this.radBtnSYSTEST.AutoSize = true;
            this.radBtnSYSTEST.Location = new System.Drawing.Point(67, 7);
            this.radBtnSYSTEST.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radBtnSYSTEST.Name = "radBtnSYSTEST";
            this.radBtnSYSTEST.Size = new System.Drawing.Size(92, 21);
            this.radBtnSYSTEST.TabIndex = 1;
            this.radBtnSYSTEST.TabStop = true;
            this.radBtnSYSTEST.Text = "SYSTEST";
            this.radBtnSYSTEST.UseVisualStyleBackColor = true;
            this.radBtnSYSTEST.CheckedChanged += new System.EventHandler(this.radBtnSYSTEST_CheckedChanged);
            // 
            // radBtnDEV
            // 
            this.radBtnDEV.AutoSize = true;
            this.radBtnDEV.Location = new System.Drawing.Point(3, 7);
            this.radBtnDEV.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.radBtnDEV.Name = "radBtnDEV";
            this.radBtnDEV.Size = new System.Drawing.Size(57, 21);
            this.radBtnDEV.TabIndex = 0;
            this.radBtnDEV.TabStop = true;
            this.radBtnDEV.Text = "DEV";
            this.radBtnDEV.UseVisualStyleBackColor = true;
            this.radBtnDEV.CheckedChanged += new System.EventHandler(this.radBtnDEV_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 48);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "LOE";
            // 
            // txtTasks
            // 
            this.txtTasks.AcceptsTab = true;
            this.txtTasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTasks.Location = new System.Drawing.Point(5, 153);
            this.txtTasks.Margin = new System.Windows.Forms.Padding(4);
            this.txtTasks.Name = "txtTasks";
            this.txtTasks.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtTasks.Size = new System.Drawing.Size(604, 300);
            this.txtTasks.TabIndex = 24;
            this.txtTasks.Text = "";
            this.txtTasks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTasks_KeyDown);
            this.txtTasks.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTasks_KeyUp);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(317, 53);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 17);
            this.label12.TabIndex = 19;
            this.label12.Text = "Today:";
            // 
            // lblTotalTime
            // 
            this.lblTotalTime.AutoSize = true;
            this.lblTotalTime.BackColor = System.Drawing.Color.White;
            this.lblTotalTime.Location = new System.Drawing.Point(275, 52);
            this.lblTotalTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalTime.Name = "lblTotalTime";
            this.lblTotalTime.Size = new System.Drawing.Size(44, 17);
            this.lblTotalTime.TabIndex = 18;
            this.lblTotalTime.Text = "00:00";
            // 
            // lbl12
            // 
            this.lbl12.AutoSize = true;
            this.lbl12.Location = new System.Drawing.Point(200, 52);
            this.lbl12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl12.Name = "lbl12";
            this.lbl12.Size = new System.Drawing.Size(79, 17);
            this.lbl12.TabIndex = 17;
            this.lbl12.Text = "Total Time:";
            // 
            // btnNewTask
            // 
            this.btnNewTask.Location = new System.Drawing.Point(95, 117);
            this.btnNewTask.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewTask.Name = "btnNewTask";
            this.btnNewTask.Size = new System.Drawing.Size(79, 28);
            this.btnNewTask.TabIndex = 15;
            this.btnNewTask.Text = "New";
            this.btnNewTask.UseVisualStyleBackColor = true;
            this.btnNewTask.Click += new System.EventHandler(this.btnNewTask_Click);
            // 
            // cboPriority
            // 
            this.cboPriority.FormattingEnabled = true;
            this.cboPriority.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cboPriority.Location = new System.Drawing.Point(153, 42);
            this.cboPriority.Margin = new System.Windows.Forms.Padding(4);
            this.cboPriority.MaxLength = 1;
            this.cboPriority.Name = "cboPriority";
            this.cboPriority.Size = new System.Drawing.Size(44, 24);
            this.cboPriority.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(99, 52);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "Priority";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTaskName
            // 
            this.cboTaskName.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboTaskName.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTaskName.FormattingEnabled = true;
            this.cboTaskName.Location = new System.Drawing.Point(4, 4);
            this.cboTaskName.Margin = new System.Windows.Forms.Padding(4);
            this.cboTaskName.MaxLength = 200;
            this.cboTaskName.Name = "cboTaskName";
            this.cboTaskName.Size = new System.Drawing.Size(613, 27);
            this.cboTaskName.TabIndex = 5;
            this.cboTaskName.SelectedIndexChanged += new System.EventHandler(this.cboTaskName_SelectedIndexChanged);
            this.cboTaskName.SelectionChangeCommitted += new System.EventHandler(this.cboTaskName_SelectionChangeCommitted);
            // 
            // btnDeleteTask
            // 
            this.btnDeleteTask.Location = new System.Drawing.Point(260, 117);
            this.btnDeleteTask.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteTask.Name = "btnDeleteTask";
            this.btnDeleteTask.Size = new System.Drawing.Size(79, 28);
            this.btnDeleteTask.TabIndex = 4;
            this.btnDeleteTask.Text = "Delete";
            this.btnDeleteTask.UseVisualStyleBackColor = true;
            this.btnDeleteTask.Click += new System.EventHandler(this.btnDeleteTask_Click);
            // 
            // btnQueryTask
            // 
            this.btnQueryTask.Location = new System.Drawing.Point(8, 117);
            this.btnQueryTask.Margin = new System.Windows.Forms.Padding(4);
            this.btnQueryTask.Name = "btnQueryTask";
            this.btnQueryTask.Size = new System.Drawing.Size(79, 28);
            this.btnQueryTask.TabIndex = 3;
            this.btnQueryTask.Text = "ReQuery";
            this.btnQueryTask.UseVisualStyleBackColor = true;
            this.btnQueryTask.Click += new System.EventHandler(this.btnQueryTask_Click);
            // 
            // btnSaveTask
            // 
            this.btnSaveTask.Location = new System.Drawing.Point(177, 117);
            this.btnSaveTask.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveTask.Name = "btnSaveTask";
            this.btnSaveTask.Size = new System.Drawing.Size(79, 28);
            this.btnSaveTask.TabIndex = 2;
            this.btnSaveTask.Text = "Save";
            this.btnSaveTask.UseVisualStyleBackColor = true;
            this.btnSaveTask.Click += new System.EventHandler(this.btnSaveTask_Click);
            // 
            // gboxOrder
            // 
            this.gboxOrder.Controls.Add(this.chkLastUpdated);
            this.gboxOrder.Controls.Add(this.chkTaskName);
            this.gboxOrder.Controls.Add(this.chkPriority);
            this.gboxOrder.Controls.Add(this.chkStatus);
            this.gboxOrder.Location = new System.Drawing.Point(480, 43);
            this.gboxOrder.Margin = new System.Windows.Forms.Padding(4);
            this.gboxOrder.Name = "gboxOrder";
            this.gboxOrder.Padding = new System.Windows.Forms.Padding(4);
            this.gboxOrder.Size = new System.Drawing.Size(131, 112);
            this.gboxOrder.TabIndex = 1;
            this.gboxOrder.TabStop = false;
            this.gboxOrder.Text = "Order By";
            // 
            // chkLastUpdated
            // 
            this.chkLastUpdated.AutoSize = true;
            this.chkLastUpdated.Location = new System.Drawing.Point(11, 87);
            this.chkLastUpdated.Margin = new System.Windows.Forms.Padding(4);
            this.chkLastUpdated.Name = "chkLastUpdated";
            this.chkLastUpdated.Size = new System.Drawing.Size(115, 21);
            this.chkLastUpdated.TabIndex = 3;
            this.chkLastUpdated.Text = "Last Updated";
            this.chkLastUpdated.ThreeState = true;
            this.chkLastUpdated.UseVisualStyleBackColor = true;
            this.chkLastUpdated.Click += new System.EventHandler(this.chkLastUpdated_CheckedChanged);
            // 
            // chkTaskName
            // 
            this.chkTaskName.AutoSize = true;
            this.chkTaskName.Location = new System.Drawing.Point(11, 66);
            this.chkTaskName.Margin = new System.Windows.Forms.Padding(4);
            this.chkTaskName.Name = "chkTaskName";
            this.chkTaskName.Size = new System.Drawing.Size(102, 21);
            this.chkTaskName.TabIndex = 2;
            this.chkTaskName.Text = "Task Name";
            this.chkTaskName.ThreeState = true;
            this.chkTaskName.UseVisualStyleBackColor = true;
            this.chkTaskName.Click += new System.EventHandler(this.chkTaskName_CheckedChanged);
            // 
            // chkPriority
            // 
            this.chkPriority.AutoSize = true;
            this.chkPriority.Location = new System.Drawing.Point(11, 44);
            this.chkPriority.Margin = new System.Windows.Forms.Padding(4);
            this.chkPriority.Name = "chkPriority";
            this.chkPriority.Size = new System.Drawing.Size(74, 21);
            this.chkPriority.TabIndex = 1;
            this.chkPriority.Text = "Priority";
            this.chkPriority.ThreeState = true;
            this.chkPriority.UseVisualStyleBackColor = true;
            this.chkPriority.Click += new System.EventHandler(this.chkPriority_CheckedChanged);
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Location = new System.Drawing.Point(11, 23);
            this.chkStatus.Margin = new System.Windows.Forms.Padding(4);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(70, 21);
            this.chkStatus.TabIndex = 0;
            this.chkStatus.Text = "Status";
            this.chkStatus.ThreeState = true;
            this.chkStatus.UseVisualStyleBackColor = true;
            this.chkStatus.Click += new System.EventHandler(this.chkStatus_CheckedChanged);
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(609, 20);
            this.StatusLabel.Spring = true;
            this.StatusLabel.Text = "toolStripStatusLabel1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 483);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(629, 25);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // taskTimer
            // 
            this.taskTimer.Enabled = true;
            this.taskTimer.Interval = 60000;
            this.taskTimer.Tick += new System.EventHandler(this.taskTimer_Tick);
            // 
            // ErrorTabPage
            // 
            this.ErrorTabPage.Location = new System.Drawing.Point(0, 0);
            this.ErrorTabPage.Name = "ErrorTabPage";
            this.ErrorTabPage.Size = new System.Drawing.Size(200, 100);
            this.ErrorTabPage.TabIndex = 0;
            this.ErrorTabPage.Text = "Errors!";
            // 
            // MyNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 508);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MyNotes";
            this.Text = "MyNotes 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MyNotes_FormClosing);
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.tabDaily.ResumeLayout(false);
            this.tabDaily.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.Day.ResumeLayout(false);
            this.Day.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabTasks.ResumeLayout(false);
            this.tabTasks.PerformLayout();
            this.gBoxStatus.ResumeLayout(false);
            this.gBoxStatus.PerformLayout();
            this.gboxOrder.ResumeLayout(false);
            this.gboxOrder.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Timer idleTimer;
		private System.Windows.Forms.TabPage tabSearch;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton rbTasks;
		private System.Windows.Forms.RadioButton rbNotes;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.RichTextBox rtSearch;
		private System.Windows.Forms.TabPage tabOptions;
		private System.Windows.Forms.Button btnLunch;
		private System.Windows.Forms.Button btnYesterday;
		private System.Windows.Forms.Button btnSaveNotes;
		private System.Windows.Forms.TextBox txtHoursThisWeek;
		private System.Windows.Forms.TextBox txtHoursToday;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TabPage tabDaily;
		private System.Windows.Forms.Button btnTomorrow;
		private System.Windows.Forms.RichTextBox txtNotes;
        private System.Windows.Forms.RichTextBox txtErrors;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox Day;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtEndDay;
		private System.Windows.Forms.TextBox txtStartDay;
		private System.Windows.Forms.MonthCalendar monthCalendar1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.Timer taskTimer;
        private System.Windows.Forms.DateTimePicker dpSearchStops;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TabPage ErrorTabPage;
        private System.Windows.Forms.CheckBox chkOnTop;
        private System.Windows.Forms.Button btnCompactMDB;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TabPage tabTasks;
        private System.Windows.Forms.TextBox txtLOE;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtTasks;
        private System.Windows.Forms.Button btnTaskTimer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.Label lbl12;
        private System.Windows.Forms.Button btnNewTask;
        private System.Windows.Forms.ComboBox cboPriority;
        private System.Windows.Forms.CheckBox chkPROD;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboTaskName;
        private System.Windows.Forms.Button btnDeleteTask;
        private System.Windows.Forms.Button btnQueryTask;
        private System.Windows.Forms.Button btnSaveTask;
        private System.Windows.Forms.GroupBox gboxOrder;
        private System.Windows.Forms.CheckBox chkLastUpdated;
        private System.Windows.Forms.CheckBox chkTaskName;
        private System.Windows.Forms.CheckBox chkPriority;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.GroupBox gBoxStatus;
        private System.Windows.Forms.RadioButton radBtnPROD;
        private System.Windows.Forms.RadioButton radBtnUAT;
        private System.Windows.Forms.RadioButton radBtnSYSTEST;
        private System.Windows.Forms.RadioButton radBtnDEV;
        private System.Windows.Forms.CheckBox chkUAT;
        private System.Windows.Forms.CheckBox chkSysTest;
        private System.Windows.Forms.CheckBox chkDev;
    }
}

