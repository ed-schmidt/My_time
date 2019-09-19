using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Logging;
using NotesManager;
using DataTypes;
using TaskManager;
using MDB_Access;   // until I can remove it
using IniParser;
using IniParser.Model;


namespace MyNotes2
{
	public partial class MyNotes : Form
	{
		NotesMgr _NotesMgr = new NotesMgr();
		TaskMgr _TaskMgr = new TaskMgr();
        //Int32 _IdleTimer = 300000; // 5 minutes default
        Int32 _IdleTimer = 30000;   // every 30 seconds
        private Int32  timerTicks = 0;
        DateTime dailyDate = DateTime.Today;	// hold the current date can't trust the calendar control
		private string orderBy = string.Empty;
        private int ordering = 0;
        private int taskOrdering = 0;
		private Boolean taskReady = false;
		private int _taskID = 0;
        string pswd = string.Empty; // don't know what scope this should have

		public MyNotes()
		{
			InitializeComponent();

            try
            {
                var parser = new FileIniDataParser();
                IniData data = parser.ReadFile("MyNotes2.ini");
                string textFont = data["MySettings"]["TextFont"];
                taskOrdering = Properties.Settings.Default.taskOrdering;
                var trash = _NotesMgr.hoursThisWeek(DateTime.Parse("Jan 1, 2009"), DateTime.Parse("Jan 7, 2009"));  // can I connect?
            }
            catch (Exception ex)
            {
                if (ex.Message == "Not a valid password.")
                {
                    // change to setStatus.text = 'Please go to options tab and enter a password.'
                    ShowInputDialog(ref pswd);
                    Security.Password = pswd;
                    Logging.TextLog.currentError = string.Empty; // CLEAR this error this one time
                }
                //else
                //    TextLog.LogErr(ex.ToString());    // already been logged
            }
			monthCalendar1.ShowTodayCircle = true;
			// what is the calendar's date? should be today
			try
			{
				DateTime buildDate = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).LastWriteTime;
				//TextLog.Logit(string.Format("Starting MyNotes, Version: {0}, Build date: {1}.", Application.ProductVersion.ToString(), buildDate.ToLongDateString()));
                dpSearchStops.Value = Convert.ToDateTime(Properties.Settings.Default.OldestSearchDate);
             
				string strNotesFont = Properties.Settings.Default.NotesFontSize;
                //strNotesFont = Functions.AppSettingGet("NotesFontSize", "12");
				string font = Properties.Settings.Default.NotesFontFamily;
				this.txtNotes.Font = new Font(font, float.Parse(strNotesFont));

                //string strTaskFontSize = Properties.Settings.Default.TaskFontSZ;
                //strTaskFontSize = Functions.AppSettingGet("NotesFontSize2", "10");
                //var X = float.Parse(Properties.Settings.Default.NotesFontSize2);
				this.txtTasks.Font = new Font(Properties.Settings.Default.TaskFontFamily,
                    float.Parse(Properties.Settings.Default.TaskFontSZ));
				_IdleTimer = Int32.Parse(Properties.Settings.Default.AutoSaveTime);
				idleTimer.Interval = _IdleTimer;
				notesPopulate(_NotesMgr.get(monthCalendar1.TodayDate));

                //cboTaskName.Font.Size = Properties.Settings.Default.TaskCboFontSZ;
                // font.size is read only at runtime.
			}
			catch (Exception ex)
			{
                // check for error being not valid password
                //prompt for a password
                // send that password to something that notesMgr and TaskMgr can query to get password
                // then try again
                setStatus(ex.Message);
                if (ex.Message == "Not a valid password.")
                {
                    // change to setStatus.text = 'Please go to options tab and enter a password.'
                    ShowInputDialog(ref pswd);
                    Security.Password = pswd;
                }
                else
				    TextLog.LogErr(ex.ToString());
			}
		}

        //private string theDate = string.Empty;
		private void MyNotes_FormClosing(object sender, FormClosingEventArgs e)
		{
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string theDate = dpSearchStops.Value.ToShortDateString();
                Properties.Settings.Default.OldestSearchDate = theDate;
                //Properties.Settings.Default.NotesFontSize2 = "16";
                Properties.Settings.Default.myColor = System.Drawing.Color.AliceBlue.ToString();
                Properties.Settings.Default.Save();
                TextLog.Logit("MyNotes closing");
                _NotesMgr.save(formToDaily());
                _NotesMgr.saveall();
                taskSave();  // will save the current task from form to memory which will then be saved
               // _TaskMgr.saveAll();     //TaskManagerSaveAll();
                //TextLog.Logit("MyNotes Form1 completed closing");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

		}

		private void tabControl1_Click(object sender, EventArgs e)
		{
			//taskReady = false;
			this.AcceptButton = null;
			switch (tabControl1.SelectedIndex)
			{
				case 0: //Notes
					resetIdle();
                    if (DateTime.Today.ToShortDateString() != dailyDate.ToShortDateString()) 
                        notesPopulate(_NotesMgr.get(monthCalendar1.SelectionStart.Date));   // not displaying today's notes then look it up
					StatusLabel.Text = string.Format("Notes for {0}", dailyDate.ToShortDateString());
					break;
				case 1:	// TASKS
					resetIdle();
					if (cboTaskName.Items.Count == 0)
					{
						taskReady = false;
                        if (taskOrdering != 0)
                        {
                            if ((taskOrdering & 1) != 0)
                                chkDev.Checked = true;
                            if ((taskOrdering & 2) != 0)
                                chkSysTest.Checked = true;
                            if ((taskOrdering & 4) != 0)
                                chkUAT.Checked = true;
                            if ((taskOrdering & 8) != 0)
                                chkPROD.Checked = true;
                        }
                        CboTasksPopulate();
                        if (cboTaskName.Items.Count > 0)
                        {
                            cboTaskName.SelectedIndex = 0;
                            taskToForm(_TaskMgr.get((int)cboTaskName.SelectedValue));
                        }
                        else
                            setStatus("No tasks returned.");
						taskReady = true;
					}
					//else
					//	if (td != null) // just checking
					//		setStatus(string.Format("Task: {0}", td.ToDoDesc + ""));
					break;
				case 2:	// Search	
					this.AcceptButton = btnSearch;
					setStatus("");
					break;
				case 3: // options
                    txtPassword.Text = Security.Password;
                    txtPassword.UseSystemPasswordChar = true;
                    chkShowPass.Checked = false;
                    break;
			}
		}
        private static DialogResult ShowInputDialog(ref string input)
        {
            System.Drawing.Size size = new System.Drawing.Size(400, 70);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Please Enter the database password";

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 5);
            textBox.Text = input;
            textBox.UseSystemPasswordChar = true;
            
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

        #region DailyFormEvents
        #region Calendar
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
			StatusLabel.Text = String.Empty;
			try
			{
				//copy form contents to a new daily
				daily d = formToDaily();

				//if it is different from a new daily
				// call managerSave
				//if (dailyHasData(d)) notes manager will check to see if it is in the collection & save or add to collection
					_NotesMgr.save(d);
				notesPopulate(_NotesMgr.get(monthCalendar1.SelectionStart.Date));
			}
			catch (Exception ex)
			{
				StatusLabel.Text = ex.Message.ToString();
				TextLog.LogErr(ex.ToString());
			}
		}
		private void btnYesterday_Click(object sender, EventArgs e)
		{
			DateTime startdate = monthCalendar1.SelectionStart.Date.AddDays(-1);
			monthCalendar1.SelectionRange = new SelectionRange(startdate, startdate);
		}
		private void btnTomorrow_Click(object sender, EventArgs e)
		{
			DateTime startdate = monthCalendar1.SelectionStart.Date.AddDays(1);
			monthCalendar1.SelectionRange = new SelectionRange(startdate, startdate);
		}
		private void btnSaveNotes_Click(object sender, EventArgs e)
		{
			//copy form contents to a new daily
			daily d = formToDaily();

			//if it is different from a new daily
			// call managerSave
			if (dailyHasData(d))				
				_NotesMgr.save(d);
		}
		private void btnLunch_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				string value = String.Empty;
				if (myInputBox.InputBox("Change Time", "Enter New Time", ref value) == DialogResult.OK)
				{
					if (value != string.Empty)
					{
						if (value.Contains(':'))
							btnLunch.Text = value;  // assume correct format
						else
							btnLunch.Text = Functions.Int32ToHoursMinutes(Convert.ToInt32(value));
					}
				}
			}
		}
		private void btnTaskTimer_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				string value = String.Empty;
				if (myInputBox.InputBox("Change Time", "Enter New Time", ref value) == DialogResult.OK)
				{
					if (value != string.Empty)
					{
						if (value.Contains(':'))
							btnTaskTimer.Text = value;  // assume correct format
						else
							btnTaskTimer.Text = Functions.Int32ToHoursMinutes(Convert.ToInt32(value));
					}
				}
			}
		}
		
		//private void btnCompleted_Click(object sender, EventArgs e)
		//{
		//	// Just toggle the text
		//	btnCompleted.Tag = "Changed";
		//	if (btnCompleted.Text == "Closed")
		//		btnCompleted.Text = "Open";
		//	else
		//		btnCompleted.Text = "Closed";
		//	btnCompleted.Tag = "Changed";   // note it for dirty check
		//}
		private void btnTaskTimer_Click(object sender, EventArgs e)
		{
			if (btnTaskTimer.Tag.ToString() == "stoped")
			{
				btnTaskTimer.BackColor = Color.LightGreen;
				btnTaskTimer.Tag = "started";
			}
			else
			{
				btnTaskTimer.BackColor = Color.Transparent;
				btnTaskTimer.Tag = "stoped";
			}
		}
		private void txtNotes_KeyUp(object sender, KeyEventArgs e)
		{
			resetIdle();            
		}
		private void idleTimer_Tick(object sender, EventArgs e)
		{
            checkForErrors();
            timerTicks += 1;

            if (timerTicks == 10)
                idleSave(sender, e);

            timerTicks %= 10;   // keep it 0 to 10   
		}

        private void idleSave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                StatusLabel.Text = "Saving notes while idle";
                btnSaveNotes_Click(sender, e);  // pretend save was clicked
                _NotesMgr.saveall();
                _TaskMgr.saveAll();
                //TextLog.Logit("idleTimer Saving");
                StatusLabel.Text = string.Format("Notes for {0}", dailyDate.ToShortDateString());
                checkForErrors();
            }
            catch (Exception ex)
            {
                StatusLabel.Text = ex.Message.ToString();
                TextLog.LogErr(ex.ToString());
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }
        #endregion

        #endregion
        #region TaskEvents
		private void txtTasks_KeyUp(object sender, KeyEventArgs e)
		{
			resetIdle();
		}
		private void CboTasksPopulate()
		{
			cboTaskName.DataSource = null;
			cboTaskName.Items.Clear();
			DataAccess DA = new DataAccess();
			//DataTable dt = DA.tasks_Get(chkShowAll.Checked, orderBy);
			DataTable dt = DA.tasks_Get(taskOrderingToString(taskOrdering), orderBy,  dpSearchStops.Value.ToShortDateString());
			cboTaskName.DataSource = dt;
			cboTaskName.ValueMember = dt.Columns["TaskID"].ToString();
			cboTaskName.DisplayMember = dt.Columns["ToDoDesc"].ToString();
			dt = null;
			DA = null;
			Application.DoEvents();
			cboTaskName.DropDownStyle = ComboBoxStyle.DropDown;
		}
		private string taskOrderingToString(int taskOrdering)
        {
            string task = "(";// always show tasks that haven't been 
            if (taskOrdering == 0)
            {
                task += "0";   // finish it
            }
            else
            {
                if ((taskOrdering & 1) == 1)
                {
                   task += "1"; // length is now two
                }
                if ((taskOrdering & 2) == 2)
                {
                    if (task.Length > 1) task += ",";
                    task += "2";
                }
                if ((taskOrdering & 4) == 4)
                {
                    if (task.Length > 1) task += ",";
                    task += "4";
                }
                if ((taskOrdering & 8) == 8)
                {
                    if (task.Length > 1) task += ",";
                    task += "8";
                }
            }
                task += ")";
            return task;
        }
		private void taskSave()
		{
            try
            {

			ToDo T = taskPopulateFromform();
			if (_TaskMgr.save(T) )
                {
                    string marker = string.Format("\n{0}: Worked on Task {1}\n", DateTime.Now.ToShortTimeString(), T.ToDoDesc.ToString());
                    //_NotesMgr.get(DateTime.Today);
                // Always note task
                    if ( monthCalendar1.SelectionStart.Date == DateTime.Today)
                    {
                        //if (txtNotes.Text.Contains(marker) == false)
                        //{
                           // var x = string.Format("\n{0}: Worked on Task {1}\n", DateTime.Now.ToShortTimeString(), marker);
                                //string.Format("{0}Worked on Task {1}{2}", "\n", marker, "\n");
                        //var x = string.Format("\n{0}: Worked on Task {1}\n", DateTime.Now.ToShortTimeString(), T.ToDoDesc.ToString());
                        //_NotesMgr.appendText(DateTime.Now, x);
                        txtNotes.AppendText(marker);
//                            _NotesMgr.appendText(DateTime.Today, "");   // set changed flag
                        //}
                    }
                    else
                        _NotesMgr.appendText(DateTime.Today,  marker);
                      // if (!_NotesMgr.containsText(DateTime.Today, marker))                 
                           // _NotesMgr.appendText(DateTime.Today, string.Format("\n{0}: Worked on Task {1}\n", DateTime.Now.ToShortTimeString(), marker));

                }
			_taskID = T.TaskID;
            }
            catch (Exception ex)
            {
                Logging.TextLog.LogErr(ex.ToString());
            }
		}

		private void taskToForm(ToDo td)
		{
			resetIdle();
			_taskID = td.TaskID;
			txtTasks.Text = td.Notes;
            bool X = taskReady;     //save state
            taskReady = false;      // keep events from firing on radio buttons
            clearTaskStatus();
           switch (td.TaskStatus)
            {
                case 1:
                    radBtnDEV.Checked = true;
                    break;
                case 2:
                    radBtnSYSTEST.Checked = true;
                    break;
                case 4:
                    radBtnUAT.Checked = true;
                    break;
                case 8:
                case 16:
                    radBtnPROD.Checked = true;
                    break;
            }
            taskReady = X;      // restore it back

            txtLOE.Text = td.LOE.ToString();    // zero is default
            //if (txtLOE.Text == "0")
            //    txtLOE.ReadOnly = false;
            //else
            //    txtLOE.ReadOnly = true;

            cboPriority.Text = td.Priority.ToString();
			setStatus(string.Format("Task {0}: {1}.", td.TaskID, td.ToDoDesc));

			btnTaskTimer.BackColor = Color.Transparent;
			btnTaskTimer.Tag = "stoped";
			lblTotalTime.Text = Functions.Int32ToHoursMinutes(td.totalTime);
			btnTaskTimer.Text = Functions.Int32ToHoursMinutes(td.taskTime);
		}

        private ToDo taskPopulateFromform()
		{
			ToDo td = new ToDo();
			try
			{
				td.ToDoDesc = cboTaskName.Text;
                td.Created = DateTime.Now;
                if (radBtnDEV.Checked == true)
                    td.TaskStatus = 1;
                else if (radBtnSYSTEST.Checked == true)
                    td.TaskStatus = 2;
                else if (radBtnUAT.Checked == true)
                    td.TaskStatus = 4;
                else if (radBtnPROD.Checked == true)
                    td.TaskStatus = 8;
                else
                    td.TaskStatus = 0;
                td.LOE = Convert.ToByte(txtLOE.Text);
                td.Notes = txtTasks.Text;
				td.Priority = Int32.Parse(cboPriority.Text);
				td.LastActivity = DateTime.MinValue;
				td.TaskID = _taskID;
				td.taskTime = Functions.hoursMinutesToMinutes(btnTaskTimer.Text.ToString());
				// total time won't change until updated in database
			}
			catch (Exception ex)
			{
				TextLog.LogErr(ex.ToString());
				setStatus(ex.Message);
			}
			return td;
		}
		private void btnQueryTask_Click(object sender, EventArgs e)
		{
			taskSave();
			string TaskName = cboTaskName.Text; // save your place
			int i = 0;
			try
			{
				_TaskMgr.saveAll();
				taskReady = false;
				CboTasksPopulate();
				for ( i = 0; i < cboTaskName.Items.Count; i++)
				{
					cboTaskName.SelectedIndex = i;
					if (cboTaskName.Text == TaskName)
						break;
				}
			}
			catch (Exception ex)
			{
				TextLog.LogErr(ex.ToString());
				setStatus(ex.Message);
			}
			finally
			{
				taskReady = true;
				if (i >= cboTaskName.Items.Count && cboTaskName.Items.Count > 0)	// if Found, task could have been closed
	//				i = 0;	// display the first
    				cboTaskName.SelectedIndex = 0;	// go find it
			}			
		}
        private void chkDev_CheckedChanged(object sender, EventArgs e)
        {
            if (taskReady == true)
            { 
            int x = taskOrdering;
            taskOrdering = x ^ 0x01;
        }
        }

        private void chkSysTest_CheckedChanged(object sender, EventArgs e)
        {
            if (taskReady == true)
            {
                int x = taskOrdering;
                taskOrdering = x ^ 0x02;
            }
        }

        private void chkUAT_CheckedChanged(object sender, EventArgs e)
        {
            if (taskReady == true)
            {
                int x = taskOrdering;
                taskOrdering = x ^ 0x04;
            }
        }

        private void chkPROD_CheckedChanged(object sender, EventArgs e)
        {
            if (taskReady == true)
            {
                int x = taskOrdering;
                taskOrdering = x ^ 0x08;
            }
        }

        #region TaskStatus
        private void radBtnDEV_CheckedChanged(object sender, EventArgs e)
        {
            if (taskReady && radBtnDEV.Checked == true)
            {
                //string theTime = DateTime.Now.ToShortTimeString();
                txtTasks.AppendText(string.Format("{0}{1}{2}{3}", '\n', "Status set to DEV ", DateTime.Now.ToString(), '\n'));
            }
        }

        private void radBtnSYSTEST_CheckedChanged(object sender, EventArgs e)
        {
            if (taskReady && radBtnSYSTEST.Checked == true)
            {
                ///string theTime = DateTime.Now.ToShortTimeString();
                txtTasks.AppendText(string.Format("{0}{1}{2}{3}", '\n', "Status set to SYSTEST ", DateTime.Now.ToString(), '\n'));
            }
        }

        private void radBtnUAT_CheckedChanged(object sender, EventArgs e)
        {
            if (taskReady && radBtnUAT.Checked == true)
            {
                //string theTime = DateTime.Now.ToShortTimeString();
                txtTasks.AppendText(string.Format("{0}{1}{2}{3}", '\n', "Status set to UAT ", DateTime.Now.ToString(), '\n'));
            }
        }

        private void radBtnPROD_CheckedChanged(object sender, EventArgs e)
        {
            if (taskReady && radBtnPROD.Checked == true)
            {
              //  string theTime = DateTime.Now.ToShortTimeString();
                txtTasks.AppendText(string.Format("{0}{1}{2}{3}", '\n', "Status set to PROD ", DateTime.Now.ToString(), '\n'));
            }
        }
        #endregion


        private void btnNewTask_Click(object sender, EventArgs e)
		{
			cboTaskName.DropDownStyle = ComboBoxStyle.Simple;
			cboTaskName.Text = "";
			txtTasks.Text = string.Empty;
            _taskID = 0;
            cboPriority.SelectedIndex = 0;
			setStatus("New Task");
			lblTotalTime.Text = "0:00";
			btnTaskTimer.Text = "0:00";
			btnTaskTimer_Click(sender, e);  // turn it on
            clearTaskStatus();
            txtLOE.Text = "0";
            txtLOE.Enabled = true;

        }

        private void clearTaskStatus()
        {
            radBtnDEV.Checked = false;
            radBtnSYSTEST.Checked = false;
            radBtnUAT.Checked = false;
            radBtnPROD.Checked = false;
        }
		private void btnSaveTask_Click(object sender, EventArgs e)
		{
			taskSave();
		}

		private void btnDeleteTask_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(string.Format("OK to delete task '{0}'", cboTaskName.Text), "Delete Task?", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				try
				{
				_TaskMgr.TaskDelete(taskPopulateFromform());
				System.Threading.Thread.Sleep(500); //give access time to delete record
				taskReady = false;
				CboTasksPopulate();
				cboTaskName.SelectedIndex = 0;
				taskToForm(_TaskMgr.get((int)cboTaskName.SelectedValue));
				taskReady = true;
				}
				catch (Exception ex)
				{
					TextLog.LogErr(ex.ToString());
					setStatus(ex.Message);
				}
			}

		}
		private void cboTaskName_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (taskReady)
					taskToForm(_TaskMgr.get((int)cboTaskName.SelectedValue));
			}
			catch (Exception ex)
			{
				TextLog.LogErr(ex.ToString());
			}
		}

		private void cboTaskName_SelectionChangeCommitted(object sender, EventArgs e)
		{
			if (taskReady)
				taskSave();
		}

		private void taskTimer_Tick(object sender, EventArgs e)
		{
			if (btnTaskTimer.Tag.ToString() == "started")
			{
				Int32 X = Functions.hoursMinutesToMinutes(btnTaskTimer.Text);
				btnTaskTimer.Text = Functions.Int32ToHoursMinutes(++X);
			}

			// check the lunch button
            if (btnLunch.Tag != null && btnLunch.Tag.ToString() == "on")
			{
				int I = Functions.hoursMinutesToMinutes(btnLunch.Text);
				btnLunch.Text = Functions.Int32ToHoursMinutes(++I);
			}
		}


        private void btnLunch_Click(object sender, EventArgs e)
		{
			DateTime startdate = monthCalendar1.SelectionStart.Date.AddDays(0);
			if (startdate == DateTime.Today) // you can only start timing lunch for the current day
			{
				if (btnLunch.Tag.ToString() == "off")
				{
					btnLunch.Tag = "on";
					btnLunch.BackColor = Color.LightGreen;
					if (btnLunch.Text.ToLower() == "lunch")
						btnLunch.Text = "0:00"; // else a continuation of lunch
				}
				else
				{
					btnLunch.Tag = "off";
					btnLunch.BackColor = Color.Transparent;
				}
			}
		}
#endregion
		#region SupportingFuncs
		private void resetIdle()
		{			
			idleTimer.Stop();	// RESTART
			idleTimer.Interval = _IdleTimer;
			idleTimer.Enabled = true;
		}

		private daily formToDaily()
		{
			daily d = new daily();
			d.Notes = txtNotes.Text;
            if (txtEndDay.Text != "")
                d.end_day =  Convert.ToDateTime(txtEndDay.Text);
            if (txtStartDay.Text != "")
                d.start_day = Convert.ToDateTime(txtStartDay.Text);
			d.date = dailyDate;
			d.end_day = addTimeToDate(dailyDate, d.end_day.Hour, d.end_day.Minute);
			d.start_day = addTimeToDate(dailyDate, d.start_day.Hour, d.start_day.Minute);
			d.lunchDuration = Functions.hoursMinutesToMinutes(btnLunch.Text);
			return d;
		}
		private DateTime addTimeToDate(DateTime date, double Hours, double Minutes)
		{
			date = date.AddHours(Hours);
			date = date.AddMinutes(Minutes);
			return date;
		}
		private Boolean dailyHasData(daily d)
		{
			Boolean result = true;
			// compair against new if different return true
            //PROBLEM: If there was a note and it is erased that change isn't saved.
            //NEED to compair d.notes with the daily in memory read from the dailys collection
/*			daily x = new daily();
			x.Notes = string.Empty;
			if (d.Notes == x.Notes &&
				d.end_day.ToShortTimeString() == x.end_day.ToShortTimeString() &&
				d.start_day.ToShortTimeString() == x.start_day.ToShortTimeString() &&
				d.lunchDuration == x.lunchDuration)
				return false;
			x = null; */
			return result;
		}
		private void notesPopulate(daily d)
		{
			try
			{
				//if (d.date.ToShortDateString() == "1/1/0001")
				dailyDate =	d.date;
			    txtNotes.Text = (d.Notes != null) ? d.Notes.ToString() : "";
			    txtEndDay.Text = d.end_day.ToShortTimeString();
			    txtStartDay.Text = d.start_day.ToShortTimeString();
                txtHoursToday.Text = (Functions.calcHoursToday(d)/60.0).ToString();
                if (d.lunchDuration.ToString() == "0")
                    {
                        btnLunch.Text = "Lunch";
                        btnLunch.Tag = "off";
                        btnLunch.BackColor = Color.Transparent;
                    }
                else
                    {
                        btnLunch.Text = Functions.Int32ToHoursMinutes( d.lunchDuration);
                        btnLunch.Tag = "off";
                        btnLunch.BackColor = Color.Transparent;
                    }
				setHoursThisWeek(d.date);
				StatusLabel.Text = string.Format("Notes for {0}", d.date.ToShortDateString());
			}
			catch (Exception ex)
			{
				TextLog.LogErr(ex.ToString());
			}
		}
		private void setHoursThisWeek(DateTime date)
		{
			List<DateTime> dts = Functions.weekStartEndDates(date);
			txtHoursThisWeek.Text = _NotesMgr.hoursThisWeek(dts[0], dts[1]).ToString();
		}
		private void setStatus(string str)
		{
			StatusLabel.Text = str;

		}
        #endregion

        #region "Task Orderby"
                private void chkStatus_CheckedChanged(object sender, EventArgs e)
                    {
                        if (chkStatus.CheckState == CheckState.Unchecked)
                            ordering &= 0xfffe;
                        else
                            ordering |= 1;
                        setOrder();// "Status");
                    }

                private void chkPriority_CheckedChanged(object sender, EventArgs e)
			            {
                            if (chkPriority.CheckState == CheckState.Unchecked)
                                ordering &= 0xfffd;
                            else
                                ordering |= 2;
                        setOrder();// "Priority");
			            }

			    private void chkTaskName_CheckedChanged(object sender, EventArgs e)
			    {
                    if (chkTaskName.CheckState == CheckState.Unchecked)
                        ordering &= 0xfffb;
                    else
                        ordering |= 4;
                    setOrder();// "ToDoDesc");
			    }

			    private void chkLastUpdated_CheckedChanged(object sender, EventArgs e)
			    {
                    if (chkLastUpdated.CheckState == CheckState.Unchecked)
                        ordering &= 0xff07;
                    else
                        ordering |= 8;
                    setOrder();// "LastActivity");
			    }

			    private void setOrder()//string order)
			    {
                    orderBy = "";
                    try
                    {
                        if ((ordering & 1) == 1)
                        {
                            switch (chkStatus.CheckState)
                            {
                                case CheckState.Checked:
                                    orderBy = " TaskStatus asc,";
                                    break;
                                case CheckState.Indeterminate:
                                    orderBy = " TaskStatus desc,";
                                    break;
                            }
                        }
                        if ((ordering & 2) == 2)
                        {
                            switch (chkPriority.CheckState)
                            {
                                case CheckState.Checked:
                                    orderBy += " Priority asc,";
                                    break;
                                case CheckState.Indeterminate:
                                    orderBy += " Priority desc,";
                                    break;
                            }
                        }
                        if ((ordering & 4) == 4)
                        {
                            switch (chkTaskName.CheckState)
                            {
                                case CheckState.Checked:
                                    orderBy += " ToDoDesc asc,";
                                    break;
                                case CheckState.Indeterminate:
                                    orderBy += " ToDoDesc desc,";
                                    break;
                            }
                        }
                        if ((ordering & 8) == 8)
                        {
                            switch (chkLastUpdated.CheckState)
                            {
                                case CheckState.Checked:
                                    orderBy += " LastActivity asc,";
                                    break;
                                case CheckState.Indeterminate:
                                    orderBy += " LastActivity desc,";
                                    break;
                            }
                        }
                    if (orderBy.Length > 0)
                        orderBy = orderBy.Substring(0, orderBy.Length - 1); // remove that last comma
                    }
                    catch (Exception ex)
                    {
                        TextLog.LogErr(ex.Message);
                    }
				    setStatus("Order by " + orderBy);
			    }
			    #endregion


		#region "Search tab"
		private void btnSearch_Click(object sender, EventArgs e)
		{
			// Todo: move Data Access to a manager
			//
			rtSearch.Clear();

			string searchFor = txtSearch.Text;
			Int32 padding = 100;
			if (Properties.Settings.Default.SearchPadding != "")
				padding = Convert.ToInt32(Properties.Settings.Default.SearchPadding);

			if (searchFor != string.Empty)
			{
				int finds = 0;
				setStatus(string.Format("Searching for {0}.", searchFor));
				DataAccess DA = new DataAccess();
				try
				{
					//rtSearch.Text = DA.SearchFor(txtSearch.Text, rbNotes.Checked);
                    // need to pass in current value for dpSearchStops to limit search
                    // 9/30/14 Added last Date parameter
					DataTable dt = DA.SearchFor(txtSearch.Text, rbNotes.Checked,dpSearchStops.Value.ToShortDateString());
					if (dt != null)
					{
						// populate rtSearch with the results
						int z = 0;
						using (DataTableReader dtr = new DataTableReader(dt))
						{
							for (z = 0; z < dt.Rows.Count; z++)
							{
								//TextLog.Logit(string.Format("rtSearch.textLength is {0}.", rtSearch.TextLength));
								dtr.Read();
                                if (rbNotes.Checked == false)
                                    rtSearch.AppendText(string.Format("<--------------{0}---------------->\r", priorityToString(dtr[2].ToString())));
                                else
								    rtSearch.AppendText("<----------------------------------------------->\r");
								rtSearch.AppendText(dtr[0].ToString() + "\r");
								//string xx = Functions.near(dtr[1].ToString(), searchFor, 100) + " \n\r";
								//TextLog.Logit(string.Format("Adding {0} characters to rtSearch",xx.Length));
								rtSearch.AppendText(Functions.near(dtr[1].ToString(), searchFor, padding) + "\r");
							}
						}
					}

				}
				catch (Exception ex)
				{
					TextLog.LogErr(ex.ToString());
					setStatus(ex.Message);
				}
				// high light the search text
				int i = rtSearch.Find(txtSearch.Text, 0, RichTextBoxFinds.NoHighlight);
				while (i >= 0)
				{
					finds++;
					rtSearch.SelectionStart = i;
					rtSearch.SelectionLength = txtSearch.TextLength;
					rtSearch.SelectionColor = Color.Red;
					i = rtSearch.Find(txtSearch.Text, ++i, RichTextBoxFinds.NoHighlight);
				}
				rtSearch.SelectionStart = 0;
				rtSearch.SelectionLength = 0;
				setStatus(string.Format("Found {0} occurances.", finds));
			}
		}
		#endregion // Search Tab

        private string priorityToString(string priority )
        {
            if (priority == "1")
                return "DEV";
            else if (priority == "2")
                return "SYSTEST";
            else if (priority == "4")
                return "UAT";
            else if (priority == "8")
                return "PROD";
            else
                return "";
        }
        private void txtNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
            {      
                string txtSearchFor = string.Empty;
                if (myInputBox.InputBox("Search for...", "Enter Search String", ref txtSearchFor) == DialogResult.OK)
                {
                    if (txtSearchFor != string.Empty)
                    {
                        // search for this string
                        // display in status F3 to find next

                        // or pop up message box Search for next?
                        {
                            string theNotes = txtNotes.Text.ToString();
                            int fstart = theNotes.IndexOf(txtSearchFor,0, StringComparison.CurrentCultureIgnoreCase);
                             //fstart = txtNotes.Find(txtSearchFor, fstart, txtNotes.TextLength,RichTextBoxFinds.None);
                            if (fstart > 0)
                            {
                                txtNotes.Select(fstart, txtSearchFor.Length);
                                //fstart = txtNotes.Find(txtSearchFor, fstart, txtNotes.TextLength, RichTextBoxFinds.None);
                                setStatus("Press F3 to search again.");
                            }
                             //DialogResult searchAgain = DialogResult.No;
                             //do
                             //{
                             //    txtNotes.Select(fstart, txtSearchFor.Length);
                             //    searchAgain = MessageBox.Show(string.Format("Find {0} again?",txtSearchFor),"", MessageBoxButtons.YesNo);
                             //    if (searchAgain == DialogResult.Yes)
                             //    {
                             //        fstart += txtSearchFor.Length; // start searching for the next string
                             //        fstart = txtNotes.Find(txtSearchFor, fstart, txtNotes.TextLength, RichTextBoxFinds.None);
                             //    }
                             //    txtNotes.Select(fstart, txtSearchFor.Length);
                             //} while (fstart > 0 && searchAgain == DialogResult.Yes);
    
                        }
                    }
                }
            }
            switch (e.KeyCode)
            {
                case Keys.F3:
                    {
                        if (txtNotes.SelectionLength > 0)   // if something is selected look for that again
                        {
                            //int fstart = txtNotes.Find(txtNotes.SelectedText, txtNotes.SelectionStart + txtNotes.SelectionLength, txtNotes.TextLength, RichTextBoxFinds.None);
                            string theNotes = txtNotes.Text.ToString();
                            int fstart = theNotes.IndexOf(txtNotes.SelectedText, txtNotes.SelectionStart + txtNotes.SelectionLength, StringComparison.CurrentCultureIgnoreCase);
                            if (fstart > 0)
                                txtNotes.Select(fstart, txtNotes.SelectedText.Length);
                        }
                    } break;
                case Keys.F5: // insert \n Time  \n into txtNotes
                    {
                        string theTime = DateTime.Now.ToShortTimeString();                        
                        txtNotes.AppendText(string.Format("{0}{1}{2}", '\n',theTime,'\n'));
                    }break;
            }            
        }

        private void txtTasks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        taskSave();
                        break;
                    case Keys.P:
                        break;
                    case Keys.F:
                        string txtSearchFor = string.Empty;
                        if (myInputBox.InputBox("Search for...", "Enter Search String", ref txtSearchFor) == DialogResult.OK)
                        {
                            if (txtSearchFor != string.Empty)
                            {
                                string theNotes = txtTasks.Text.ToString();
                                int fstart = theNotes.IndexOf(txtSearchFor, 0, StringComparison.CurrentCultureIgnoreCase);
                                if (fstart > 0)
                                {
                                    txtTasks.Select(fstart, txtSearchFor.Length);
                                    setStatus("Press F3 to search again.");
                                }
                            }
                        }
                        break;
                }
            }
            switch(e.KeyCode)               
            {
                case Keys.F3:
                    {
                        if (txtTasks.SelectionLength > 0)   // if something is selected look for that again
                        {
                            string theNotes = txtTasks.Text.ToString();
                            int fstart = theNotes.IndexOf(txtTasks.SelectedText, txtTasks.SelectionStart + txtTasks.SelectionLength, StringComparison.CurrentCultureIgnoreCase);
                            if (fstart > 0)
                                txtTasks.Select(fstart, txtTasks.SelectedText.Length);
                        }
                    } break;
                case Keys.F5:
                    {
                        txtTasks.AppendText(string.Format("{0}{1}{2}", '\n', DateTime.Now.ToString(), '\n'));
                    } break;
            }
        }

        private void rtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F && e.Modifiers == Keys.Control)
            {
                string txtSearchFor = string.Empty;
                if (myInputBox.InputBox("Search for...", "Enter Search String", ref txtSearchFor) == DialogResult.OK)
                {
                    if (txtSearchFor != string.Empty)
                    {
                        string theNotes = rtSearch.Text.ToString();
                        int fstart = theNotes.IndexOf(txtSearchFor, 0, StringComparison.CurrentCultureIgnoreCase);
                        if (fstart > 0)
                        {
                            rtSearch.Select(fstart, txtSearchFor.Length);
                            setStatus("Press F3 to search again.");
                        }
                    }
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                if (txtNotes.SelectionLength > 0)   // if something is selected look for that again
                {
                    string theNotes = rtSearch.Text.ToString();
                    int fstart = theNotes.IndexOf(rtSearch.SelectedText, rtSearch.SelectionStart + rtSearch.SelectionLength, StringComparison.CurrentCultureIgnoreCase);
                    if (fstart > 0)
                        rtSearch.Select(fstart, rtSearch.SelectedText.Length);
                }
            }
        }

        private void txtStartDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Subtract && txtStartDay.Text != "12:00 AM")
            {
                DateTime x = DateTime.Parse(txtStartDay.Text);
                x = x.AddMinutes(-15);
                Application.DoEvents();
                txtStartDay.Text = x.ToString("h:mm tt");
            }
            if (e.KeyCode == Keys.Add && txtStartDay.Text != "12:00 AM")
            {
                DateTime x = DateTime.Parse(txtStartDay.Text);
                x = x.AddMinutes(15);
                Application.DoEvents(); // this causes the keyboard buffer to insert the '+'
                txtStartDay.Text = x.ToString("h:mm tt");
            }
            if (e.KeyCode == Keys.F5)
            {
                string theTime = DateTime.Now.ToShortTimeString();
				theTime = Functions.Close15(theTime);	// roll up or down to closest 15 minute interval
                txtStartDay.Text = string.Format("{0}{1}{2}", '\n', theTime, '\n').Trim();
            }
        }

        private void txtEndDay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Subtract && txtEndDay.Text != "12:00 AM")
            {
                DateTime x = DateTime.Parse(txtEndDay.Text);
                x = x.AddMinutes(-15);
                Application.DoEvents();
                txtEndDay.Text = x.ToString("h:mm tt");
            }
            if (e.KeyCode == Keys.Add && txtEndDay.Text != "12:00 AM")
            {
                DateTime x = DateTime.Parse(txtEndDay.Text);
                x = x.AddMinutes(15);
                Application.DoEvents(); // this causes the keyboard buffer to insert the '+'
                txtEndDay.Text = x.ToString("h:mm tt");
            }
            if (e.KeyCode == Keys.F5)
            {
                string theTime = DateTime.Now.ToShortTimeString();
				theTime = Functions.Close15(theTime);
                txtEndDay.Text = string.Format("{0}{1}{2}", '\n', theTime, '\n').Trim();
            }
        }

        private void checkForErrors()
        {
            try
            {
                if (Logging.TextLog.currentError != string.Empty)
                {
                    if (!tabControl1.TabPages.Contains(ErrorTabPage))
                    {
                        tabControl1.TabPages.Add(ErrorTabPage);
                        this.ErrorTabPage.Controls.Add(this.txtErrors);
                        this.txtErrors.AcceptsTab = true;
                        this.txtErrors.Anchor = System.Windows.Forms.AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        this.txtErrors.Font = new System.Drawing.Font("New Times Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        this.txtErrors.Location = new System.Drawing.Point(3, 5);
                        this.txtErrors.Name = "txtErrors";
                        this.txtErrors.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
                        this.txtErrors.Size = new System.Drawing.Size(433, 358);
                         this.txtErrors.Dock = System.Windows.Forms.DockStyle.Fill;
                       this.txtErrors.TabIndex = 9;
                        this.txtErrors.Text = "";
                        this.txtErrors.Visible = true;
                    }
                    txtErrors.Text = Logging.TextLog.currentError;
                }
            }
            catch (Exception ex)
            {
                txtErrors.Text += string.Format("{0}: {1}\n", DateTime.Now.ToString(), ex.ToString());
            }
        }
        #region OptionTab
        private void chkOnTop_CheckedChanged(object sender, EventArgs e)
        {
        MyNotes.ActiveForm.TopMost = chkOnTop.Checked;
        }

        private void btnCompactMDB_Click(object sender, EventArgs e)
        {
            string results = string.Empty;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                DataAccess Da = new DataAccess();
                Da.closeConn();
                results = Functions.CompactDatabase(Da.ConnectionString(), Da.sourcePath());
                setStatus(results);
            }
            catch (Exception)
            {
                setStatus("Error Compacting MDB");
                //already logged
            }
            finally
            {
                TextLog.Logit(results + " Compacting MDB");
                Cursor.Current = Cursors.Default;
            }

        }
         private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPass.Checked == true)
                 txtPassword.UseSystemPasswordChar = false;   
             else
                txtPassword.UseSystemPasswordChar = true;
        }
         private void txtPassword_Leave(object sender, EventArgs e)
        {
            Security.Password = txtPassword.Text;
        }
        #endregion

    }
}
