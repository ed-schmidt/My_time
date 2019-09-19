using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.Serialization;

namespace DataTypes
{
	public class daily
	{
		// Table 2002
		[DataMember]
		public DateTime date { get; set; }  // PK

		[DataMember]
		public DateTime start_day { get; set; }

		[DataMember]
		public Int32 lunchDuration { get; set; }

		[DataMember]
		public DateTime end_day { get; set; }

		[DataMember]
		public string Notes { get; set; }

		// internal use
		public bool _modified { get; set; }
		public bool _saved { get; set; }
	}
}
