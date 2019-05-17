using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForSeparateDocuments
{
	/// <summary>
	/// Custom message.
	/// </summary>
	[Serializable]
	public class MessageInfo
	{
		/// <summary>
		/// File name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Body.
		/// </summary>
		public byte[] Body { get; set; }

		/// <summary>
		/// The constructor.
		/// </summary>
		public MessageInfo()
		{
		}
	}
}
