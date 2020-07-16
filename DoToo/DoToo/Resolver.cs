using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Autofac;

namespace DoToo
{
	public static class Resolver
	{
		private static IContainer _container;

		public static void Initialize(IContainer container)
		{
			_container = container;
		}

		public static T Resolve<T>()
		{
			return _container.Resolve<T>();
		}
	}
}
