using System.Collections.Generic;
using System.Linq;
using NHibernate.Type;

namespace NHibernate.Tuple.Entity
{
	/// <summary>
	/// WHY is this thing needed?
	/// We want to do something else besides rehydrating a many-to-one to allow lazy loading.
	/// Thus we need to implement a many-to-one type. Because that's not something that's supported in NH,
	/// we "register" this statically after loading our configuration.
	/// </summary>
	public class CustomEntityTypeMapper
	{
		private static readonly Dictionary<string, IType> TypeDict = new Dictionary<string, IType>();

		public static IType Map(IType type)
		{
			var key = GenerateTypeKey(type);
			if (TypeDict.ContainsKey(key))
			{
				return TypeDict[key];
			}
			return type;
		}

		public static void Register(string forName, EntityType type)
		{
			TypeDict[forName + "#" + type.RHSUniqueKeyPropertyName] = type;
		}

		public static void Register(string forName, IType type)
		{
			TypeDict[forName] = type;
		}

		// WHY should we need two different keys?
		// Possibility to use other mapped columns as foreign keys while using the same entity type.
		private static string GenerateTypeKey(IType type)
		{
			var key = type.Name;
			var entityType = type as EntityType;
			if (entityType != null)
			{
				key = entityType.Name + "#" + entityType.RHSUniqueKeyPropertyName;
			}
			return key;
		}

		public static bool IsKnownType(System.Type type)
		{
			return TypeDict.Keys.Any(key => key.StartsWith(type.FullName));
		}
	}
}
