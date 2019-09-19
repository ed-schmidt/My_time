using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using System.Data;
using System.Data.OleDb;
using Logging;
using System.Diagnostics;



namespace MDB_Access
{
    public class DataAccess
    {
		public CommandType StatementType { get; set; }
		public string Statement { get; set; }
		private OleDbConnection conn = null;
		private OleDbDataReader dr = null;
        private string _myConnString = string.Empty;

		public DataAccess()
		{
            //Provider=Microsoft.Jet.OLEDB.4.0; Data Source=d:\Northwind.mdb;User ID=Admin;Password=;
            //Cannot start your application. The workgroup information file is missing or opened exclusively by another user.
            if (Security.Password == null)
                conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\mytime.mdb");
            else
                conn = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\mytime.mdb;Jet OLEDB:Database Password={0}", Security.Password));
        }

        public string ConnectionString()
        {            
            return conn.ConnectionString.ToString();
        }
        public string sourcePath()
        {
            return System.IO.Directory.GetCurrentDirectory().ToString() + "\\mytime.mdb";
        }
		public void closeConn()  
		{
			if ( conn.State != ConnectionState.Closed)
				conn.Close();
		}
        public DataTable SearchFor(string match, Boolean inNotes)
        {
			string str = string.Empty;
            string orderby = string.Empty;

                if (inNotes)
                {
                    str = "Select date, Notes from 2002 where Notes like ";
                    //str = "Select date, Notes from 2002 where [date] > #" + stop + "# AND Notes like ";
                    orderby = " order by [date] desc";
                }
                else
                {
                    str = "Select ToDoDesc, Notes from ToDo where Notes like ";
                    orderby = " order by Created desc";
                }
				str += '"' + "%" +  match + "%" + '"'  + orderby;

            return getDataTable(str);   // let getDataTable catch the errors
        }

		public bool IsValidDateTime(string dateTimeToCheck)
		{
			if (dateTimeToCheck == null || dateTimeToCheck == "")
				return false;
			DateTime myDateTimeResult;
			return DateTime.TryParse(dateTimeToCheck, out myDateTimeResult);
		}

		#region "Notes"
        public double dailys_GetHoursForWeek(DateTime starting, DateTime ending)
        {
            double total = 0.0D;
            //Int32 time = 0;
            try
            {
                DataTable Dt = getDataTable(string.Format("SELECT Sum((DateDiff('n',start_day,end_day)-lunchDuration) / 60.00) AS totalHours FROM 2002 WHERE [date] between #{0}# and #{1}#;", starting, ending));
                using (DataTableReader R = new DataTableReader(Dt))
                {
                    R.Read();
                    if (R[0] != null)
                    {
                        string strTotal = R["totalHours"].ToString();
                        if (strTotal != string.Empty)
                            total = Convert.ToDouble(strTotal);
                        else total = 0.0D;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                closeConn();
            }
            return total;
        }

		public daily dailys_Get(DateTime date)
		{
			DataTable dt = getDataTable(string.Format("select * from 2002 where [date] = #{0}#", date.ToShortDateString()));
			return dailyFromDT(dt);
		}
		private daily dailyFromDT(DataTable dt)
		{
			daily d = null;
			if (dt.Rows.Count > 0)
			//1/19/14 changed to return null if no data rather than new daily();	
			//if (dt.Rows.Count > 0)
			{
			 d = new daily();
				using (DataTableReader R = new DataTableReader(dt))
				{
					initDaily(d);
						R.Read();
						if (R["date"] != null)
							d.date = Convert.ToDateTime(R["date"].ToString());
						if (IsValidDateTime(R["start_day"].ToString()))
							d.start_day = Convert.ToDateTime(R["start_day"].ToString());
                        d.lunchDuration = Convert.ToInt16(R["lunchDuration"]);
						if (IsValidDateTime(R["end_day"].ToString()))
							d.end_day = Convert.ToDateTime(R["end_day"].ToString());			
						if (R["Notes"]!= null)
							d.Notes = R["Notes"].ToString();
				}
			}
			closeConn();
			return d;
		}
		private void initDaily(daily d)
		{
			d.Notes = string.Empty;
		}
		// move this after testing
		private ToDo toDoFromDT(DataTable dt)
		{
			ToDo td = new ToDo();
			if (dt.Rows.Count > 0)
			{
				using (DataTableReader R = new DataTableReader(dt))
				{
					R.Read();
                    //if (R["Completed"] != null && R["Completed"].ToString() != string.Empty)
                    //	td.Completed = Convert.ToDateTime(R["Completed"].ToString());
                    if (R["Created"] != null && R["Created"].ToString() != string.Empty)
                        td.Created = Convert.ToDateTime(R["Created"].ToString());
                    if (R["LOE"] != null && R["LOE"].ToString() != string.Empty)
                        td.LOE = Convert.ToByte(R["LOE"].ToString());
                    if (R["LastActivity"] != null && R["LastActivity"].ToString() != string.Empty)
						td.LastActivity = Convert.ToDateTime(R["LastActivity"].ToString());
					if (R["Notes"] != null)
						td.Notes = R["Notes"].ToString();
					if (R["Priority"] != null)
						td.Priority = Convert.ToInt16(R["Priority"].ToString());
					if (R["ToDoDesc"] != null)
						td.ToDoDesc = R["ToDoDesc"].ToString();
                    if (Convert.DBNull.Equals(R["TaskStatus"]) == false)
                        td.TaskStatus = Convert.ToByte(R["TaskStatus"]); 
                    else
                        td.TaskStatus = 0;
					td.TaskID = Convert.ToInt16(R["TaskID"].ToString());
				}
			}
			return td;
		}

		private Boolean noteExists(daily d)
		{
			OleDbCommand cmd = new OleDbCommand("select 1 from 2002 where [date] = ?",conn);
			cmd.Parameters.AddWithValue("date", d.date);
			if (conn.State != ConnectionState.Open)
				conn.Open();
			var Res = cmd.ExecuteScalar();
			closeConn();
			return Res != null;
		}
		public string dailys_Save( daily d)
		{
			string result = string.Empty;
			string cmdStr = string.Empty;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
           
			if (noteExists(d))
				cmdStr = "update 2002 set start_day = ?, lunchDuration = ?, end_day = ?, Notes = ? where [date] = ?";
			else
				cmdStr = "Insert into 2002 (start_day, lunchDuration, end_day, Notes, [date]) values (?,?,?,?,?)";
			try
			{
				using (OleDbCommand cmd = new OleDbCommand(cmdStr,conn))
				{
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.AddWithValue("start_day", d.start_day.ToString());
					cmd.Parameters.AddWithValue("lunchDuration", d.lunchDuration.ToString());
					cmd.Parameters.AddWithValue("end_day", d.end_day.ToString());
					cmd.Parameters.AddWithValue("Notes", d.Notes);
					cmd.Parameters.AddWithValue("[date]", d.date.ToShortDateString());
					if (conn.State != ConnectionState.Open)
						conn.Open();
					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				TextLog.LogErr(ex.ToString());
				throw;
			}
			finally
			{
			closeConn();
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.            
            //TextLog.Logit(string.Format("Saved {0} in {1} Ms.", d.date.ToString(), ts.Milliseconds / 10));
			}
		return result;
		}
		public int getTaskID(ToDo d)
		{
			OleDbCommand cmd = new OleDbCommand("select TaskID from todo where ToDoDesc = ? and Created = ?", conn);
			cmd.Parameters.AddWithValue("ToDoDesc", d.ToDoDesc.ToString());
            cmd.Parameters.AddWithValue("Created", d.Created.ToString());
            if (conn.State != ConnectionState.Open)
				conn.Open();
			var Res = cmd.ExecuteScalar();
			conn.Close();
			return Convert.ToInt16(Res.ToString());
		}

		//public async void dailys_SaveAsync(daily d)
		//{
		//	string cmdStr = string.Empty;
		//	if (noteExists(d))
		//		cmdStr = "update 2002 set start_day = ?, lunchDuration = ?, end_day = ?, Notes = ? where [date] = ?";
		//	else
		//		cmdStr = "Insert into 2002 (start_day, lunchDuration, end_day, Notes, [date]) values (?,?,?,?,?)";

		//	try
		//	{
		//		using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
		//		{
		//			cmd.CommandType = CommandType.Text;
		//			cmd.Parameters.AddWithValue("start_day", d.start_day.ToString());
		//			cmd.Parameters.AddWithValue("lunchDuration", d.lunchDuration.ToString());
		//			cmd.Parameters.AddWithValue("end_day", d.end_day.ToString());
		//			cmd.Parameters.AddWithValue("Notes", d.Notes);
		//			cmd.Parameters.AddWithValue("[date]", d.date.ToShortDateString());
		//			if (conn.State != ConnectionState.Open)
		//				conn.Open();
		//			await cmd.ExecuteNonQueryAsync();
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		TextLog.Logit(ex.ToString());
		//		throw;
		//	}
		//	finally
		//	{
		//		closeConn();
		//	}
		//}

#endregion
		#region "Tasks section"
		private Boolean taskIDExists(ToDo td)
		{
			OleDbCommand cmd = new OleDbCommand("select 1 from todo where taskID = ?", conn);
			cmd.Parameters.AddWithValue("taskID", td.TaskID);
			if (conn.State != ConnectionState.Open)
				conn.Open();
			var Res = cmd.ExecuteScalar();
			conn.Close();
			return Res != null;
		}


		public string mostRecentTask()
		{
			string str = string.Empty;
			try
			{
				dr = getDataReader("SELECT TOP 1 ToDo.ToDoDesc, ToDo.Created FROM ToDo ORDER BY ToDo.Created DESC;");
				if (dr.HasRows)
				{
					dr.Read();
					str = dr[0].ToString();
				}

			}
			catch (Exception ex)
			{
				TextLog.LogErr(ex.ToString());
				str = ex.ToString();
			}
			finally
			{
				if (dr != null) dr.Close();
				if (conn != null) conn.Close();
			}
			return str;
			//return "This is a test";
			//SELECT TOP 1 ToDo.ToDoDesc, ToDo.Created FROM ToDo ORDER BY ToDo.Created DESC;
		}

		/// <summary>
		/// get name and ID of all tasks
		/// </summary>
		/// GetAll
		/// <returns></returns>
		/// 
		public void task_Delete(string TaskID)
		{
			using (OleDbCommand cmd = new OleDbCommand("Delete * from Todo where [TaskID] = ?", conn))
			{
				cmd.CommandType = CommandType.Text;				
				cmd.Parameters.AddWithValue("TaskID", TaskID);
				if (conn.State != ConnectionState.Open)
					conn.Open();
				cmd.ExecuteNonQuery();
			}
		}
		public DataTable tasks_Get(string taskOrdering, string Orderby, string start)
		{
			string str = "Select ToDoDesc, TaskID from todo";
            if (start != string.Empty)
                str += string.Format(" Where LastActivity >= #{0}#", start);
            str += string.Format(" AND TaskStatus IN {0}", taskOrdering);
			if (Orderby != string.Empty)
				str += " Order by " + Orderby;

			DataTable dt = getDataTable(str);
			return dt;
		}

		public ToDo task_Get(int TaskID)
		{
			//DataTable dt = getDataTable(string.Format("Select * from ToDo where TaskID = ",TaskID.ToString()));
			DataSet DS = new DataSet();
			DataTable DT = new DataTable();
			try
			{
				OleDbCommand cmd = new OleDbCommand();
				cmd.Connection = conn;
				if (conn.State != ConnectionState.Open)
					conn.Open();
				cmd.Connection = conn;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "Select * from ToDo where TaskID = ?";
				cmd.Parameters.AddWithValue("TaskID", TaskID);
				using (OleDbDataAdapter DA = new OleDbDataAdapter(cmd))
				{
					DA.Fill(DS);
					if (DS.Tables.Count > 0)
					{
						DT = DS.Tables[0];
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				throw;
			}
			finally
			{
				if (DS != null)
					DS = null;
				closeConn();
			}
			return toDoFromDT(DT);
		}
        public void task_TimeGet(int TaskID, DateTime dtDate, ref Int32 totalTime, ref Int32 minutes )
        {
            /// return total time and time for given Date
            totalTime = 0;
            minutes = 0;
            string SQLStr = string.Format("SELECT SUM(minutes) FROM TaskTime WHERE Task_ID = {0};",TaskID);
            using (OleDbCommand cmd = new OleDbCommand("", conn))
            {
                OleDbDataReader dr = getDataReader(SQLStr);
                if (dr.HasRows)
                {
                    dr.Read();
                    try
                    {
                        totalTime = Convert.ToInt32(dr[0]); // lame
                    }
                    catch 
                    {
                    }
                }
                ///////////////////////// get minutes 
                SQLStr = string.Format("SELECT [minutes] FROM TaskTime WHERE Task_ID = {0} and dtDate = #{1}#;", TaskID, dtDate);
                dr = getDataReader(SQLStr);
                if (dr.HasRows)
                {
                    dr.Read();
                    try
                    {
                        minutes = Convert.ToInt32(dr[0]);
                    }
                    catch 
                    {
                    } 
                }
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
        public void task_TimeSave(int TaskID, string minutes, DateTime dtDate)
        {
			// Task times are saved in a separate table 
			// So I can report on what was worked on
            using (OleDbCommand cmd = new OleDbCommand("", conn))
            {
                string SQLStr = string.Empty;
                cmd.CommandType = CommandType.Text;
                SQLStr = string.Format("SELECT 1 FROM TaskTime WHERE Task_ID = {0} and  dtDate = #{1}# ;", TaskID, dtDate);
               OleDbDataReader db = getDataReader(SQLStr);
               if (db.HasRows)
               {    // update
                   SQLStr = string.Format("UPDATE TaskTime SET minutes = {0} WHERE Task_ID = {1} and dtDate = #{2}#;", minutes, TaskID, dtDate);

               }
               else
               { // insert
                   SQLStr = string.Format("INSERT INTO TaskTime(Task_ID, dtDate, minutes) VALUES({0},#{1}#,{2});", TaskID, dtDate, minutes);                   
               }
               cmd.CommandText = SQLStr;
               cmd.ExecuteNonQuery();
            }
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
		public void task_Save( ToDo d)
		{
			string cmdStr = string.Empty;
			using (OleDbCommand cmd = new OleDbCommand(cmdStr, conn))
			{
				cmd.CommandType = CommandType.Text;
				StringBuilder sb = new StringBuilder();
				StringBuilder val = new StringBuilder(") values (");
				Boolean bAnInsert = false;
				if (taskIDExists(d))
					sb.Append("UPDATE ToDo SET ");
				else
				{
					sb.Append("INSERT INTO ToDo (");
					bAnInsert = true;
				}
				cmd.Parameters.AddWithValue("ToDoDesc", d.ToDoDesc.ToString());
				if (bAnInsert) // have to have a description
				{
					sb.Append("TodoDesc ");
					val.Append("? ");
				}
				else
					sb.Append("ToDoDesc = ? ");

				cmd.Parameters.AddWithValue("Notes", d.Notes.ToString());
				if (bAnInsert)
				{
					sb.Append (", Notes ");
					val.Append(", ?");
				}
				else
					sb.Append (",Notes = ? ");

                // end of the gotta haves
                if (d.Created.ToString() != string.Empty)
                {
                    cmd.Parameters.AddWithValue("Creted", d.Created.ToString());
                    if (bAnInsert)
                    {
                        sb.Append(", Created ");
                        val.Append(", ? ");
                    }
                    else
                        sb.Append(", Created = ? ");
                }

                if (d.TaskStatus >= 0)
                {
                    cmd.Parameters.AddWithValue("TaskStatus", d.TaskStatus.ToString());
                    if (bAnInsert)
                    {
                        sb.Append(", TaskStatus ");
                        val.Append(", ? ");
                    }
                    else
                        sb.Append(", TaskStatus = ?");
                }
                if (d.Priority.ToString() != string.Empty)
				{
					cmd.Parameters.AddWithValue("Priority", d.Priority.ToString());
					if (bAnInsert)
					{
						sb.Append(", Priority");
						val.Append(", ?");
					}
					else
						sb.Append(", Priority = ?");
				}
                if (d.LOE.ToString() != string.Empty)
                {
                    cmd.Parameters.AddWithValue("LOE", d.LOE.ToString());
                    if (bAnInsert)
                    {
                        sb.Append(", LOE");
                        val.Append(", ?");
                    }
                    else
                        sb.Append(", LOE = ?");
                }

                if (d.LastActivity.ToString() != string.Empty)
                {
                    cmd.Parameters.AddWithValue("LastActivity", DateTime.Now.ToShortDateString());   //
                    //cmd.Parameters.AddWithValue("LastActivity", d.LastActivity.ToString());
                if (bAnInsert)
					{
						sb.Append(",LastActivity");
						val.Append(", ?");
					}
					else
						sb.Append(", LastActivity = ?");
                }


                if (bAnInsert)
				sb.Append(val + ")");
			else
			{
				sb.Append(" where [TaskID] = ?");
				cmd.Parameters.AddWithValue("TaskID", d.TaskID.ToString());
			}

                cmd.CommandText = sb.ToString();
				if (conn.State != ConnectionState.Open)
					conn.Open();
                try
                {

                    //            Stopwatch stopWatch = new Stopwatch();
                    //            stopWatch.Start();
                    cmd.ExecuteNonQuery();
                    //            stopWatch.Stop();
                    //            // Get the elapsed time as a TimeSpan value.
                    //            TimeSpan ts = stopWatch.Elapsed;
                    // Format and display the TimeSpan value.            
                    //TextLog.Logit(string.Format("Saved Task {0} in {1} Ms.", d.ToDoDesc.ToString(), ts.Milliseconds / 10));
                }
                catch (Exception ex)
                {
                    TextLog.LogErr(ex.ToString());                
                    throw;
                }
				if (bAnInsert)
					d.TaskID = getTaskID(d);	// find out what ID was assigned to this task
			}
			if (conn.State == ConnectionState.Open)
				conn.Close();

		}
#endregion
		#region "Holidays table"
		/// <summary>
		/// returns all Holidays
		/// </summary>
		/// <returns></returns>
		public DataTable specialDays_Get()
		{
			throw new NotImplementedException();
		}

		public string specialDays_Save(string name, string month, string day, string year, Boolean PaidHoliday)
		{
			throw new NotImplementedException();
		}
#endregion
		/// <summary>
		/// returns event if today is a special day
		/// </summary>
		/// <param name="today"></param>
		/// <returns></returns>
		public string specialDay(DateTime today)
		{
			throw new NotImplementedException();
		}

#region "Low level DataAccess"
		private OleDbDataReader getDataReader(string cmdString)
		{
            if (conn.State != ConnectionState.Open)
			    conn.Open();

			OleDbCommand cmd = new OleDbCommand(cmdString);
			cmd.Connection = conn;
			return cmd.ExecuteReader();
		}

/*************************
 * [C#]
string ConnString = Utils.GetConnString();
string SqlString = "Select * From Contacts Where FirstName = ? And LastName = ?";
using (OleDbConnection conn = new OleDbConnection(ConnString))
{
  using (OleDbCommand cmd = new OleDbCommand(SqlString, conn))
  {
    cmd.CommandType = CommandType.Text;
    cmd.Parameters.AddWithValue("FirstName", txtFirstName.Text);
    cmd.Parameters.AddWithValue("LastName", txtLastName.Text);
 
    conn.Open();
    using (OleDbDataReader reader = cmd.ExecuteReader())
    {
      while (reader.Read())
      {
        Response.Write(reader["FirstName"].ToString() + " " + reader["LastName"].ToString());
      }
    }
  }
}
 * *****************************************/
        public DataTable SearchFor( string match, Boolean inNotes, string  stop)
        {
            string str = string.Empty;
            string orderby = string.Empty;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();

            match = "%" + match + "%";
            stop = "#" + stop + "#";
            if (inNotes)
            {
                str = string.Format("Select date, Notes from 2002 where [date] > {0} AND Notes like ?",stop);
                orderby = " order by [date] desc";
            }
            else
            {
                str = string.Format("Select ToDoDesc, Notes,Priority from ToDo where LastActivity > {0} AND Notes like ?", stop);
                orderby = " order by Created desc";
            }
            //str += '"' + "%" + match + "%" + '"' + orderby;
            str += orderby;
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(str, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //if (inNotes)
                    //    cmd.Parameters.AddWithValue("[date]", stop);
                    // HACK Can't get the [date] parameter to not error 
                    cmd.Parameters.AddWithValue("Notes", match);
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    using (OleDbDataAdapter DA = new OleDbDataAdapter(cmd))
                    {
                        DA.Fill(DS);
                        if (DS.Tables.Count > 0)
                        {
                            DT = DS.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TextLog.LogErr(ex.ToString());
                throw;
            }
            finally
            {
                if (DS != null)
                    DS = null;
                closeConn();
            }
            return DT;

        }
		public DataTable getDataTable(string cmdString)
		{
            //TextLog.Logit("getDataTable: " + cmdString);
			DataSet DS = new DataSet();
			DataTable DT = new DataTable();
			try
			{
				OleDbCommand cmd = new OleDbCommand(cmdString,conn);
				if (conn.State != ConnectionState.Open)
					conn.Open();
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = cmdString;
				cmd.Connection = conn;
				using (OleDbDataAdapter DA = new OleDbDataAdapter(cmd))
				{
					DA.Fill(DS);
					if (DS.Tables.Count > 0)
					{
						DT = DS.Tables[0];
					}
				}
			}
			catch (Exception ex)
			{
                TextLog.LogErr(ex.ToString());
				throw;
			}
			finally
			{
				if (DS != null)
					DS = null;
				closeConn();
			}
			return DT;
		}
		public int executeScaler(string cmdString)
		{
			throw new NotImplementedException();
		}
  
#endregion
	}

    }

