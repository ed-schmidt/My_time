using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using Logging;
using System.Configuration;
using System.IO;
using System.Windows.Forms;


namespace MyNotes2
{
    public static class Functions
    {

		public static String Int32ToHoursMinutes(Int32 X)
		{
			string minutes = "00" + (X % 60).ToString();
			var y = minutes.Substring(minutes.Length - 2, 2);
			return string.Format("{0}:{1}", (X / 60).ToString(), y);
		}

		public static Int32 calcHoursToday(daily d)
		{
			// if start of day is midnight return
			// if start of day is valid and start lunch is entered 
			//morning can be calculated
			//if end of lunch is entered and end of day is entered
			//  afternoon can be calculated
			// if all 4 times or begin and end of day are entered then you can calculate the day
			Int32 fullDay = 0;
			Int32 lunch = 0;

			try
			{
				if ((d.start_day != null) && (d.end_day != null))
					fullDay = timeElapsed(d.start_day, d.end_day);
				lunch = d.lunchDuration;
				if (fullDay != 0.0 && lunch != 0.0)
					fullDay -= lunch;
			}
			catch (Exception)
			{
				TextLog.LogErr(string.Format("Error in calcHoursToday, start {0}, lunchDuration{1}, DayEnd{2}", d.start_day, d.lunchDuration, d.end_day));
			}

			return fullDay;
		}
		public static Int32 timeElapsed(DateTime time1, DateTime time2)
		{
			Int32 result = 0;
			try
			{
				if (time1 < time2)
				{
					TimeSpan diffResult = time2.Subtract(time1);
					return diffResult.Hours * 60 + diffResult.Minutes;
				}
			}
			catch (Exception ex)
			{
				TextLog.LogErr(string.Format("Error in timeElapsed, time1 = {0}, time1={1}.", time1.ToShortTimeString(), time2.ToShortTimeString()));
				TextLog.LogErr(ex.ToString());
			}
			return result;
		}
		public static Int32 hoursMinutesToMinutes(string hoursMinutes)
		{
			try
			{
				if (hoursMinutes.ToLower() == "lunch")
					return (0);
				TimeSpan ts = TimeSpan.Parse(hoursMinutes);
				return (ts.Hours * 60) + ts.Minutes;
			}
			catch
			{
				TextLog.LogErr(string.Format("Attempted to convert {0} to minutes", hoursMinutes));
				return (0);
			}
		}

		public static List<DateTime> weekStartEndDates(DateTime theDay)
		{
			/// returns Key value pair
			/// firstOfWeek: date
			/// endOfWeek: date
			/// 
			List<DateTime> dates = new List<DateTime>(); ;
			try
			{
				double dow = Convert.ToDouble(theDay.DayOfWeek);
				theDay = theDay.AddDays(-dow);  // should be the start of the week
				DateTime eow = theDay.AddDays(7D);
				dates.Add(theDay);
				dates.Add(eow);
			}
			catch (Exception ex)
			{
				TextLog.LogErr(String.Format("Error in weekStartEndDates func: {0}", ex.ToString()));
				throw;
			}
			return dates;
		}
        public static string near(string str, string keyWord, Int32 padding)
        {
            /// Will search the string for a key word and return padding characters on each side 
            /// 
            Int32 location = 0;
            StringBuilder sb = new StringBuilder();
               try
                {
                   //  get the first location of keyword
                location = str.IndexOf(keyWord,StringComparison.CurrentCultureIgnoreCase);
                while (location >= 0)
                {                   
                    // adjust what will be displayed to whole words
                    Int32 starting = nextWhiteSpace(ref str, location - 10, false);
                    Int32 ending = nextWhiteSpace(ref str, location + padding, true);
                    
                    sb.AppendLine(str.Substring(starting, (ending - starting)) + "..."); // get full words now
                   
                    location = str.IndexOf(keyWord, (++ending > str.Length) ? str.Length : ending, StringComparison.CurrentCultureIgnoreCase);
                }
                if (sb.Length > 0) 
                    str = sb.ToString();
                }
                catch (Exception ex)
                {
                    TextLog.LogErr(String.Format("Error in Function near string:{0}, keyword: {1}, \n padding:{2}. \n {3}", str, keyWord, padding, ex.ToString()));
                }
               //TextLog.Logit(string.Format("Function near returning string of {0} length.", str.Length));
            return str;
        }
        public static Int32 nextWhiteSpace(ref string str, Int32 startLoc, Boolean moveForward)
        {
            // move start loc forward or backward based on moveForward to identify the whole word
            // take care of error conditions
            if (startLoc < 0)
                startLoc = 0;
            if (startLoc > str.Length)
                startLoc = str.Length;
            if (startLoc == str.Length && moveForward)
                return str.Length; // can't go beyond string length
            if (startLoc == 0 && !moveForward)
                return 0;   // can't backup past beginning of string

            while ((string.IsNullOrWhiteSpace(str.Substring(startLoc, 1)) == false) && startLoc > 0)
            {
                if (moveForward)
                    startLoc++;
                else
                    startLoc--;
            }

            return startLoc;
        }
        public static string AppSettingGet(string key, string Default)
        {
            try
            {
                var Settings = ConfigurationManager.AppSettings;
                string result = Settings[key] ?? Default;
                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                Logging.TextLog.LogErr(ex.ToString());
                return Default;
            }
        }

		public static string Close15(string input)
		{
			DateTime dt = DateTime.Now;
			try
			{
				string temp = dt.ToString("MM/dd/yyyy ") + input;
				TimeSpan ts = DateTime.Parse(temp).TimeOfDay;
				int min = (int)ts.Minutes;

				min += min % 15 < 15 - min % 15 ? -min % 15 : 15 - min % 15;
				ts = new TimeSpan(ts.Hours, min, 0) - new TimeSpan((int)ts.Days, 0, 0, 0);

				dt = new DateTime(dt.Year, dt.Month, dt.Day, (int)ts.Hours, (int)ts.Minutes, 0);

			}
			catch (Exception)
			{
				return (input);
			}
			return dt.ToString("h:mm tt");
		}
        /// <summary>
        /// MBD compact method (c) 2004 Alexander Youmashev
        /// !!IMPORTANT!!
        /// !make sure there's no open connections
        ///    to your db before calling this method!
        /// !!IMPORTANT!!
        /// </summary>
        /// <param name="connectionString">connection string to your db</param>
        /// <param name="mdwfilename">FULL name
        ///     of an MDB file you want to compress.</param>
        //public static void CompactAccessDB(string connectionString, string mdwfilename)
        //{
        //    object[] oParams;

        //    //create an inctance of a Jet Replication Object
        //    object objJRO =
        //      Activator.CreateInstance(Type.GetTypeFromProgID("JRO.JetEngine"));

        //    //filling Parameters array
        //    //cnahge "Jet OLEDB:Engine Type=5" to an appropriate value
        //    // or leave it as is if you db is JET4X format (access 2000,2002)
        //    //(yes, jetengine5 is for JET4X, no misprint here)
        //    oParams = new object[] { connectionString,  "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\tempdb.mdb;Jet OLEDB:Engine Type=5"};

        //    //invoke a CompactDatabase method of a JRO object
        //    //pass Parameters array
        //    objJRO.GetType().InvokeMember("CompactDatabase",
        //        System.Reflection.BindingFlags.InvokeMethod,
        //        null,
        //        objJRO,
        //        oParams);

        //    //database is compacted now
        //    //to a new file C:\\tempdb.mdw
        //    //let's copy it over an old one and delete it

        //    System.IO.File.Delete(mdwfilename);
        //    System.IO.File.Move("C:\\tempdb.mdb", mdwfilename);

        //    //clean up (just in case)
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(objJRO);
        //    objJRO = null;
        //}
        //public static void CompactMyTime()
        //{

        //    //Add a reference to "Microsoft Jet and Replication Objects 2.6 Library" (COM component)

        //    string FileSource = "MyAccessFile.mdb";
        //    string SourceConnection = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + FileSource;
        //    string DestConnection = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + "FileTmp.mdb";

        //    if (System.IO.File.Exists("FileTmp.mdb"))
        //    System.IO.File.Delete("FileTmp.mdb");
            
        //    JRO.JetEngineClass objJet = new JRO.JetEngineClass();
        //    objJet.CompactDatabase(SourceConnection, DestConnection);

        //    System.IO.File.Delete(FileSource);
        //    System.IO.File.Move("FileTmp.mdb", FileSource);
        //    }

        public static string CompactDatabase(string dbConnectionStr, string sourcePath)
        {

            JRO.JetEngine objJetEngine = new JRO.JetEngine();

            if (dbConnectionStr.Contains("|DataDirectory|"))
               dbConnectionStr = dbConnectionStr.Replace("|DataDirectory|", Path.GetDirectoryName(Application.ExecutablePath));
            string TargetConnection = dbConnectionStr.Replace("mytime.mdb","myTimeRpr.mdb");
            string targetPath = sourcePath.Replace("mytime.mdb","myTimeRpr.mdb");
            try 
                {	        
                if ( System.IO.File.Exists(targetPath))
                    System.IO.File.Delete(targetPath);
                System.IO.File.Move(sourcePath,targetPath); // take existing mdb rename it to old
                objJetEngine.CompactDatabase( TargetConnection,dbConnectionStr);
                // If all is good your done		
                return "Success";
                }
	        catch (Exception ex)
	        {
                // log exception
                Logging.TextLog.LogErr(ex.ToString());
                // copy mdb from target to source to keep running without a C&R Database
                System.IO.File.Move(targetPath,sourcePath); // take existing mdb rename it to old
                return ex.Message;
	        }
            finally
            {
                objJetEngine = null;
            }
        }

    }
	}

