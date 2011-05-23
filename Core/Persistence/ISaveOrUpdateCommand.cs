using System;
namespace Core.Persistence{
	public interface ISaveOrUpdateCommand<ENTITY>
	 where ENTITY : Core.Domain.DomainEntity
	{
		void Execute( ENTITY entity );
	}
}
