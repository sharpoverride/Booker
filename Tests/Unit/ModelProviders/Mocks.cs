using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using Core.Persistence;
using Core.Domain;

namespace Tests.Unit.ModelProviders
{
	internal static class Mocks
	{
		static Mocks()
		{

			QueryRoles = MockRepository.GenerateMock<IQueryService<Role>>();
			QueryUser = MockRepository.GenerateMock<IQueryService<User>>();
			UpdateUser = MockRepository.GenerateMock<ISaveOrUpdateCommand<User>>();

			FakeRole = MockRepository.GenerateMock<Role>();
			FakeUser = MockRepository.GenerateMock<User>();

		}

		public static IQueryService<Role> QueryRoles { get; set; }

		public static IQueryService<User> QueryUser { get; set; }

		public static ISaveOrUpdateCommand<User> UpdateUser { get; set; }

		public static Role FakeRole { get; set; }

		public static User FakeUser { get; set; }
	}
}
