using System;
using NUnit.Framework;

namespace CxSoftware.RutSharp.Tests
{
	[TestFixture]
	public class Test
	{
		[Test]
		[ExpectedException]
		public void TestParseNull ()
		{
			Rut.Parse (null);
		}

		[Test]
		[ExpectedException]
		public void TestParseEmpty ()
		{
			Rut.Parse (string.Empty);
		}

		[Test]
		public void TestParseReglasNingunaValid1 ()
		{
			var rut = Rut.Parse ("9.986.332-3");
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 9986332);
			Assert.AreEqual (rut.DV, '3');
		}

		[Test]
		public void TestParseReglasNingunaValid2 ()
		{
			var rut = Rut.Parse ("13392728-k");
			Assert.AreEqual (rut.Numero, 13392728);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasNingunaValid3 ()
		{
			var rut = Rut.Parse ("77244425");
			Assert.AreEqual (rut.Numero, 7724442);
			Assert.AreEqual (rut.DV, '5');
		}

		[Test]
		public void TestParseReglasNingunaValid4 ()
		{
			var rut = Rut.Parse ("12.380.6654");
			Assert.AreEqual (rut.Numero, 12380665);
			Assert.AreEqual (rut.DV, '4');
		}

		[Test]
		public void TestParseReglasNingunaValid5 ()
		{
			var rut = Rut.Parse ("12178968-k");
			Assert.AreEqual (rut.Numero, 12178968);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasNingunaValid6 ()
		{
			var rut = Rut.Parse ("12178968-K");
			Assert.AreEqual (rut.Numero, 12178968);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasNingunaValid7 ()
		{
			var rut = Rut.Parse ("12178968k");
			Assert.AreEqual (rut.Numero, 12178968);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasNingunaValid8 ()
		{
			var rut = Rut.Parse ("12178968K");
			Assert.AreEqual (rut.Numero, 12178968);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasNingunaValid9 ()
		{
			var rut = Rut.Parse ("12.178.968-k");
			Assert.AreEqual (rut.Numero, 12178968);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasNingunaValid10 ()
		{
			var rut = Rut.Parse ("12.178.968-K");
			Assert.AreEqual (rut.Numero, 12178968);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasConSeparadorDeMiles1 ()
		{
			var rut = Rut.Parse ("16.737.060-8");
			Assert.AreEqual (rut.Numero, 16737060);
			Assert.AreEqual (rut.DV, '8');
		}

		[Test]
		public void TestParseReglasConSeparadorDeMiles2 ()
		{
			var rut = Rut.Parse ("16.737.0608");
			Assert.AreEqual (rut.Numero, 16737060);
			Assert.AreEqual (rut.DV, '8');
		}

		[Test]
		public void TestParseReglasConSeparadorDeMiles3 ()
		{
			var rut = Rut.Parse ("7.442.676-K");
			Assert.AreEqual (rut.Numero, 7442676);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasConSeparadorDeMiles4 ()
		{
			var rut = Rut.Parse ("7.442.676K");
			Assert.AreEqual (rut.Numero, 7442676);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasConSeparadorDeMiles5 ()
		{
			var rut = Rut.Parse ("7.442.676-k");
			Assert.AreEqual (rut.Numero, 7442676);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseReglasConSeparadorDeMiles6 ()
		{
			var rut = Rut.Parse ("7.442.676k");
			Assert.AreEqual (rut.Numero, 7442676);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseConGuion1 ()
		{
			var rut = Rut.Parse ("18780209-1");
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 18780209);
			Assert.AreEqual (rut.DV, '1');
		}

		[Test]
		public void TestParseConGuion2 ()
		{
			var rut = Rut.Parse ("18.780.209-1");
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 18780209);
			Assert.AreEqual (rut.DV, '1');
		}

		[Test]
		public void TestParseConGuion3 ()
		{
			var rut = Rut.Parse ("22582584-k");
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 22582584);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		public void TestParseConGuion4 ()
		{
			var rut = Rut.Parse ("22582584-K");
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 22582584);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		[ExpectedException]
		public void TestParseConGuionInvalid1 ()
		{
			var rut = Rut.Parse ("22582584K", ReglasRut.ConGuion);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 22582584);
			Assert.AreEqual (rut.DV, 'K');
		}

		[Test]
		[ExpectedException]
		public void TestParseConGuionInvalid2 ()
		{
			Rut.Parse ("22582584--K", ReglasRut.ConGuion);
		}

		[Test]
		public void TestMinimumValid ()
		{
			var rut = new Rut (1);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 1, "rut.Numero");
			Assert.AreEqual (rut.DV, '9', "rut.DV");
		}

		[Test]
		[ExpectedException]
		public void TestMinimumInvalid ()
		{
			new Rut (0);
		}

		[Test]
		public void TestMaximumValid ()
		{
			var rut = new Rut (99999999);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 99999999, "rut.Numero");
			Assert.AreEqual (rut.DV, '9', "rut.DV");
		}

		[Test]
		[ExpectedException]
		public void TestMaximumInvalid ()
		{
			new Rut (100000000);
		}

		[Test]
		public void TestDV0 ()
		{
			var rut = new Rut (15568505);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 15568505, "rut.Numero");
			Assert.AreEqual (rut.DV, '0', "rut.DV");

			rut = new Rut (15568505, '0');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 15568505, "rut.Numero");
			Assert.AreEqual (rut.DV, '0', "rut.DV");
		}

		[Test]
		public void TestDV1 ()
		{
			var rut = new Rut (6323218);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 6323218, "rut.Numero");
			Assert.AreEqual (rut.DV, '1', "rut.DV");

			rut = new Rut (6323218, '1');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 6323218, "rut.Numero");
			Assert.AreEqual (rut.DV, '1', "rut.DV");
		}

		[Test]
		public void TestDV2 ()
		{
			var rut = new Rut (11840331);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 11840331, "rut.Numero");
			Assert.AreEqual (rut.DV, '2', "rut.DV");

			rut = new Rut (11840331, '2');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 11840331, "rut.Numero");
			Assert.AreEqual (rut.DV, '2', "rut.DV");
		}

		[Test]
		public void TestDV3 ()
		{
			var rut = new Rut (8838814);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 8838814, "rut.Numero");
			Assert.AreEqual (rut.DV, '3', "rut.DV");

			rut = new Rut (8838814, '3');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 8838814, "rut.Numero");
			Assert.AreEqual (rut.DV, '3', "rut.DV");
		}

		[Test]
		public void TestDV4 ()
		{
			var rut = new Rut (11119523);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 11119523, "rut.Numero");
			Assert.AreEqual (rut.DV, '4', "rut.DV");

			rut = new Rut (11119523, '4');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 11119523, "rut.Numero");
			Assert.AreEqual (rut.DV, '4', "rut.DV");
		}

		[Test]
		public void TestDV5 ()
		{
			var rut = new Rut (7645434);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 7645434, "rut.Numero");
			Assert.AreEqual (rut.DV, '5', "rut.DV");

			rut = new Rut (7645434, '5');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 7645434, "rut.Numero");
			Assert.AreEqual (rut.DV, '5', "rut.DV");
		}

		[Test]
		public void TestDV6 ()
		{
			var rut = new Rut (16806248);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 16806248, "rut.Numero");
			Assert.AreEqual (rut.DV, '6', "rut.DV");

			rut = new Rut (16806248, '6');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 16806248, "rut.Numero");
			Assert.AreEqual (rut.DV, '6', "rut.DV");
		}

		[Test]
		public void TestDV7 ()
		{
			var rut = new Rut (20412982);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 20412982, "rut.Numero");
			Assert.AreEqual (rut.DV, '7', "rut.DV");

			rut = new Rut (20412982, '7');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 20412982, "rut.Numero");
			Assert.AreEqual (rut.DV, '7', "rut.DV");
		}

		[Test]
		public void TestDV8 ()
		{
			var rut = new Rut (9762904);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 9762904, "rut.Numero");
			Assert.AreEqual (rut.DV, '8', "rut.DV");

			rut = new Rut (9762904, '8');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 9762904, "rut.Numero");
			Assert.AreEqual (rut.DV, '8', "rut.DV");
		}

		[Test]
		public void TestDV9 ()
		{
			var rut = new Rut (11796267);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 11796267, "rut.Numero");
			Assert.AreEqual (rut.DV, '9', "rut.DV");

			rut = new Rut (11796267, '9');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 11796267, "rut.Numero");
			Assert.AreEqual (rut.DV, '9', "rut.DV");
		}

		[Test]
		public void TestDVK ()
		{
			var rut = new Rut (5958457);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 5958457, "rut.Numero");
			Assert.AreEqual (rut.DV, 'K', "rut.DV");

			rut = new Rut (5958457, 'K');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 5958457, "rut.Numero");
			Assert.AreEqual (rut.DV, 'K', "rut.DV");

			rut = new Rut (5958457, 'k');
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 5958457, "rut.Numero");
			Assert.AreEqual (rut.DV, 'K', "rut.DV");
		}

		[Test]
		[ExpectedException]
		public void TestWrongDV ()
		{
			new Rut (15568505, '1');
		}

		[Test]
		public void TestToStringDefault1 ()
		{
			var rut = new Rut (15141590);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 15141590, "rut.Numero");
			Assert.AreEqual (rut.DV, '3');

			var rutString = rut.ToString ();
			Assert.IsNotNull (rutString, "rut.ToString()");
			Assert.AreEqual (rutString, "15.141.590-3");
		}

		[Test]
		public void TestToStringDefault2 ()
		{
			var rut = new Rut (21098755);
			Assert.IsNotNull (rut);
			Assert.AreEqual (rut.Numero, 21098755, "rut.Numero");
			Assert.AreEqual (rut.DV, 'K');

			var rutString = rut.ToString ();
			Assert.IsNotNull (rutString, "rut.ToString()");
			Assert.AreEqual (rutString, "21.098.755-K");
		}

		[Test]
		public void TestCompareEquals ()
		{
			var rut1 = new Rut (5264875);
			var rut2 = new Rut (5264875);
			Assert.AreEqual (rut1, rut2);
		}

		[Test]
		public void TestCompareEqualOperator1 ()
		{
			var rut1 = new Rut (5264875);
			var rut2 = new Rut (5264875);
			Assert.IsTrue (rut1 == rut2);
		}

		[Test]
		public void TestCompareEqualOperator2 ()
		{
			var rut1 = new Rut (5264875);
			var rut2 = new Rut (5264876);
			Assert.IsFalse (rut1 == rut2);
		}

		[Test]
		public void TestCompareNotEqualOperator1 ()
		{
			var rut1 = new Rut (5264875);
			var rut2 = new Rut (5264876);
			Assert.IsTrue (rut1 != rut2);
		}

		[Test]
		public void TestCompareNotEqualOperator2 ()
		{
			var rut1 = new Rut (5264875);
			var rut2 = new Rut (5264875);
			Assert.IsFalse (rut1 != rut2);
		}

		[Test]
		public void TestGetHashCode1 ()
		{
			var rut1 = new Rut (79584555);
			var rut2 = new Rut (79584555);
			Assert.AreEqual (rut1.GetHashCode (), rut2.GetHashCode ());
		}

		[Test]
		public void TestGetHashCode2 ()
		{
			var rut1 = new Rut (79584555);
			var rut2 = new Rut (79584557);
			Assert.AreNotEqual (rut1.GetHashCode (), rut2.GetHashCode ());
		}
	}
}

