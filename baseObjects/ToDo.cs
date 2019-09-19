using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.Serialization;

namespace DataTypes
{
	public class ToDo
	{
		private bool _dirty;
		// table Todo
		[DataMember]
		public string ToDoDesc { get; set; }

        [DataMember]
        public DateTime Created { get; set; }

        [DataMember]
        public byte LOE { get; set; }   // Level Of Effort  -- once set it can not be changed

        // Don't care if it is completed or when
        // will note the date and time when it was moved to different lanes,
        // task status of 16 is complete
        //[DataMember]
        //public DateTime Completed { get; set; }

        [DataMember]
		public string Notes { get; set; }

		[DataMember]
		public int Priority { get; set; }

		[DataMember]
		public int TaskID { get; set; } //PK

		[DataMember]
		public DateTime LastActivity { get; set; }

        [DataMember]
        public byte TaskStatus { get; set; }

		public Boolean dirty
		{
			set
			{
				_dirty = value;
			}
			get
			{
				return _dirty;
			}
		}
		public Int32 totalTime { get; set; }
		public Int32 taskTime {get; set;}
	}

}
