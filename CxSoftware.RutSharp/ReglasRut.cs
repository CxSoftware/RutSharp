using System;

namespace CxSoftware.RutSharp
{
	public enum ReglasRut
	{
		Ninguna = 0x00,
		ConSeparadorDeMiles = 0x01,
		SinSeparadorDeMiles = 0x02,
		ConGuion = 0x04,
		SinGuion = 0x08,
		Mayuscula = 0x10,
		Minuscula = 0x20
	}
}

