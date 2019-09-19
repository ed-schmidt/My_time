using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using DataTypes;
using MDB_Access;

namespace NotesManager
{
    public  class NotesMgr
    {
		private List<daily> dailys = new List<daily>();

		public bool loggingOn { get; set;} // want ability to turn logging on and off at runtime
		// daily.date is the PK
		public void save(daily d)
		{
			Boolean addIt = true;
			d._modified = true;
			foreach (daily x in dailys)
			{
				// replace if you have it else add it
               if (x.date == d.date)
                {   //2019-01-05
                    addIt = false;
                   if (x.end_day != d.end_day ||
                        x.start_day != d.start_day ||
                        x.lunchDuration != d.lunchDuration ||
                        x.Notes != d.Notes)
                    {   // update everything
                        x._modified = true;
                        x.end_day = d.end_day;
                        x.start_day = d.start_day;
                        x.lunchDuration = d.lunchDuration;
                        x.Notes = d.Notes;
                    }
                    break;
                }
			}
			if (addIt)
				dailys.Add(d);
		}
        public bool containsText(DateTime dt, string text)
        {
            foreach (daily d in dailys)
            {
                if (d.date == dt)
                {
                    if (d.Notes.Contains(text))
                        return true;
                }
            }
            return false;
       }
		public void appendText(DateTime dt, string text)
		{
			foreach (daily d in dailys)
			{
				if (d.date.ToShortDateString() == dt.ToShortDateString())
				{
                    //d.Notes += '\n' + '\r' + text;
                    d.Notes += text;
                    d._modified = true;
                    //string x = d.Notes.ToString();
                    //x += "\n\r" + text;
                    //d.Notes = x;
				}
			}
		}
		public void saveall()
		{
			// save all called from ui when inactive timer fires and on form closing
			if (dailys.Count > 0)
			{
				try
				{
					DataAccess Da = new DataAccess();
					foreach (daily d in dailys)
					{
                        
						if (d._modified)
						{
							Da.dailys_Save(d);
							d._modified = false;
						}
					}
				}
				catch (Exception )
				{
					throw;
				}
			}
		}
		public daily get(DateTime dt)
		{
			foreach (daily d in dailys)
			{
				if (d.date == dt)
					return d;
			}
			// you are here if the date isn't in the collection of dailys
			// so go get it
			DataAccess Da = new DataAccess();
			daily newDaily = Da.dailys_Get(dt);
			Da = null;
			if (newDaily != null)
				dailys.Add(newDaily);
			else
			{
				newDaily = new daily();
				newDaily.date = newDaily.end_day = newDaily.start_day = dt;
                dailys.Add(newDaily);   // clean this up after debugging
			}
			return newDaily;
		}

		public double hoursThisWeek(DateTime date1, DateTime date2)
		{
			DataAccess Da = new DataAccess();
			return Da.dailys_GetHoursForWeek(date1, date2);

		}
    }
}
