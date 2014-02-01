using System;

namespace CxSoftware.RutSharp
{
	/// <summary>
	/// Reglas utilizadas para el parseo y formateo del Rut
	/// </summary>
	public enum ReglasRut
	{
		/// <summary>
		/// No se aplica ninguna regla especial
		/// </summary>
		Ninguna = 0x00,

		/// <summary>
		/// Utilizar separador de miles (por ejemplo: 6.904.204-K)
		/// </summary>
		ConSeparadorDeMiles = 0x01,

		/// <summary>
		/// No utilizar separador de miles (por ejemplo: 6904204-K)
		/// </summary>
		SinSeparadorDeMiles = 0x02,

		/// <summary>
		/// Utilizar guión (por ejemplo: 6.904.204-K)
		/// </summary>
		ConGuion = 0x04,

		/// <summary>
		/// No utilizar guión (por ejemplo: 6904204K)
		/// </summary>
		SinGuion = 0x08,

		/// <summary>
		/// Usar mayúscula para el dígito verificador (por ejemplo: 6.904.204-K)
		/// </summary>
		Mayuscula = 0x10,

		/// <summary>
		/// Usar minúscula para el dígito verificador (por ejemplo: 6.904.204-k)
		/// </summary>
		Minuscula = 0x20
	}
}

