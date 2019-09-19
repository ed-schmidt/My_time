using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;
using MDB_Access;

namespace TaskManager
{
    public  class TaskMgr
    {
		private List<ToDo> Tasks = new List<ToDo>();

		public  void saveAll()
		{
			foreach (ToDo t in Tasks)
			{
				//if (t.dirty)
				//{
					save(t);
					t.dirty = false;
				//}
			}
		}
		public Int32 getIdOfTaskName(string desc)
		{
			// newly created task the UI will not know it
			int tskId = 0;
			foreach (ToDo t in Tasks)
			{
				if (t.ToDoDesc == desc)
				{
					tskId = t.TaskID;
					break;
				}
			}
			return tskId;
		}
		public  Boolean save(ToDo T)
		{
			if (T.ToDoDesc == string.Empty)
				return false;
			DataAccess Da = new DataAccess();
			if (T.TaskID > 0)
			{
				try
				{
					// ToDo: Replace with Lambda statement
					foreach (ToDo t in Tasks) // loop through the tasks in memory to match the one passed.
					{
						if (t.TaskID == T.TaskID)
						{
                            if (t.ToDoDesc != T.ToDoDesc)
                            {
                                t.ToDoDesc = T.ToDoDesc;
                                t.dirty = true;
                            }
                            if (t.LOE != T.LOE)
                            {
                                t.LOE = T.LOE;
                                t.dirty = true;
                            }
                            //if (t.Completed != T.Completed)
                            //{
                            //    t.Completed = T.Completed;
                            //    t.dirty = true;
                            //}
                            if (t.TaskStatus != T.TaskStatus)
                            {
                                t.TaskStatus = T.TaskStatus;
                                t.dirty = true;
                            }
                            if (t.Notes != T.Notes)
                            {
                                t.Notes = T.Notes;
                                t.dirty = true;
                            }
                            if (t.Priority != T.Priority)
                            {
                                t.Priority = T.Priority;
                                t.dirty = true;
                            }
                            if (t.taskTime != T.taskTime)
                            {
                                t.taskTime = T.taskTime;
                                t.dirty = true;
                            }
                            if (t.dirty)
                            {
                                //Logging.TextLog.Logit(string.Format("Saving task {0}", t.ToDoDesc));
							    T.LastActivity = DateTime.Now;
							    Da.task_Save(T);
                                t.dirty = false;
                                Da.task_TimeSave(t.TaskID, t.taskTime.ToString(), DateTime.Today);
                                Logging.TextLog.Logit(string.Format("Saving task {0} complete", t.ToDoDesc));
                                return true;
                            }
						}
					}
				}
				catch (Exception)
				{
					throw;
				}
			}
			else
			{   // new task
				Da.task_Save(T);
				T.TaskID = Da.getTaskID(T);
				Da.task_TimeSave(T.TaskID, T.taskTime.ToString(), DateTime.Today);
			    T.dirty = false;
				Tasks.Add(T);
                return true;    // saved
			}
			Da = null;
            return false;   // didn't save anything to keep the compiler happy
		}

		public void TaskDelete(ToDo T)
		{
			try
			{
				Tasks.Remove(T);
				DataAccess Da = new DataAccess();
				Da.task_Delete(T.TaskID.ToString());
				Da = null;
			}
			catch (Exception)
			{
				
				throw;
			}

		}
		public  ToDo get(Int32 TaskID)
		{   // is the task in memory?
			foreach (ToDo T in Tasks)
			{
				if (T.TaskID == TaskID)
					return T;
			}

			// Task not in memory, go get it
			DataAccess Da = new DataAccess();
			ToDo t = Da.task_Get(TaskID);
			// get the time in minutes spent on the task
			Int32 totalTime = 0;
			Int32 taskTime = 0;
			Da.task_TimeGet(t.TaskID, DateTime.Today, ref totalTime, ref taskTime);
			t.totalTime = totalTime;
			t.taskTime = taskTime;

			Tasks.Add(t);
			Da = null;
			return t;
		}

    }
}
