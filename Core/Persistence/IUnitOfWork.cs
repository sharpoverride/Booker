using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Persistence
{
	/// <summary>
	/// This represents the UnitOfWork 
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Initializes a unit of work
		/// </summary>
		void Initialize();
		/// <summary>
		/// Commits the results
		/// </summary>
		void Close();
		
	}
}
