using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace Domain
{
	public class Bookmark : DomainEntity
	{
		public virtual string Url { get; set; }

		public virtual string Notes { get; set; }
	}
}
