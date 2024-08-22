using System.Reflection;
using Common;
using Core.Cli;

namespace Core;

class LibraryLoader
{
	// https://stackoverflow.com/a/949285/22146374
	private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
	{
		return
			assembly.GetTypes()
			        .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
			        .ToArray();
	}

	public static void LoadItems()
	{
		Type[] types = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Content.Items");

		Console.WriteLine("Loading items...");

		foreach (var type in types)
		{
			Item instance = Activator.CreateInstance(type) as Item;

			if (instance == null)
			{
				continue;
			}

			instance.Load();

			ItemLibrary.Register(instance.Id, instance);
		}
	}

	public static void LoadEntities()
	{
		Type[] types = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Content.Entities");

		Console.WriteLine("Loading entities...");

		foreach (var type in types)
		{
			Entity instance = Activator.CreateInstance(type) as Entity;

			if (instance == null)
			{
				continue;
			}

			// instance.Load();

			EntityLibrary.Register(instance.Id, instance);
		}
	}

	public static void LoadCommands()
	{
		Type[] types = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Content.Commands");
		
		Console.WriteLine("Loading commands...");

		foreach (var type in types)
		{
			// if (type != typeof(Command))
			// {
			// 	continue;
			// }

			Command instance = Activator.CreateInstance(type) as Command;

			if (instance == null)
			{
				continue;
			}

			instance.Load();

			CommandLibrary.Register(instance.Name, instance);
		}
	}
}