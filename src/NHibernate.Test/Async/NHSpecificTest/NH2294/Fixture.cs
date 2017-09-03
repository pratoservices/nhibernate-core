﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Linq;
using NHibernate.Hql.Ast.ANTLR;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH2294
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		protected override System.Collections.IList Mappings
		{
			get
			{
				return Enumerable.Empty<object>().ToList();
			}
		}

		[Test, Ignore("External issue. The bug is inside RecognitionException of Antlr.")]
		public void WhenQueryHasJustAWhereThenThrowQuerySyntaxExceptionAsync()
		{
			using (ISession session = OpenSession())
			{
				Assert.That(() => session.CreateQuery("where").ListAsync(), Throws.TypeOf<QuerySyntaxException>());
			}
		}
	}
}
