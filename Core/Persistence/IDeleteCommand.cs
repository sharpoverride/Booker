using System;
namespace Core.Persistence{
	public interface IDeleteCommand<ENTITY>
	 where ENTITY : Core.Domain.DomainEntity
	{
		void Execute( ENTITY entity );
	}
}
