using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CxSoftware.RutSharp
{
	public class Rut
	{
		// Static fields

		private static readonly CultureInfo chileanCulture = new CultureInfo ("es-CL");



		// Properties

		/// <summary>
		/// Dígito verificador del RUT en mayúscula
		/// </summary>
		public char DV { get; private set; }

		/// <summary>
		/// Número del RUT
		/// </summary>
		public int Numero { get; private set; }



		// Constructor

		/// <summary>
		/// Inicializa una instancia del RUT según su número
		/// </summary>
		/// <param name="numero">Número del rut</param>
		public Rut (int numero)
		{
			Check (numero);
			this.Numero = numero;
			this.DV = GenerateDV (numero);
		}

		/// <summary>
		/// Inicializa una instancia del RUT según su número y su dígito verificador
		/// </summary>
		/// <param name="numero">Número del RUT</param>
		/// <param name="dv">Dígito verificador del RUT en mayúscula o minúscula</param>
		public Rut (int numero, char dv)
		{
			Check (numero);
			var dv2 = GenerateDV (numero);
			if (dv.ToString ().ToUpper () != dv2.ToString ().ToUpper ())
				throw new Exception (
					string.Format (
						"Dígito verificador inválido. Se esperaba {0} para el número {1}.",
						dv2,
						numero));

			this.Numero = numero;
			this.DV = dv2;
		}



		// Methods

		/// <summary>
		/// Retorna un <see cref="System.String"/>
		/// que representa al <see cref="CxSoftware.RutSharp.Rut"/>.
		/// </summary>
		/// <returns>Un <see cref="System.String"/>
		/// que representa al <see cref="CxSoftware.RutSharp.Rut"/>
		/// en el formato con separador de miles, guión y dígito
		/// verificador en mayúscula.</returns>
		public override string ToString ()
		{
			return this.ToString (
				ReglasRut.ConSeparadorDeMiles |
				ReglasRut.ConGuion |
				ReglasRut.Mayuscula);
		}

		/// <summary>
		/// Retorna un <see cref="System.String"/>
		/// que representa al <see cref="CxSoftware.RutSharp.Rut"/>.
		/// </summary>
		/// <returns>Un <see cref="System.String"/>
		/// que representa al <see cref="CxSoftware.RutSharp.Rut"/>
		/// según el formato definido por las reglas.
		/// </returns>
		/// <param name="reglas">Reglas para la generación del RUT</param>
		public string ToString (ReglasRut reglas)
		{
			var sb = new StringBuilder ();

			// Part 1
			if ((reglas & ReglasRut.SinSeparadorDeMiles) == ReglasRut.SinSeparadorDeMiles)
				sb.Append (this.Numero.ToString (CultureInfo.InvariantCulture));
			else
				sb.Append (this.Numero.ToString ("#,#", chileanCulture));

			// Part 2
			if ((reglas & ReglasRut.SinGuion) == 0)
				sb.Append ("-");

			// Part 3
			if ((reglas & ReglasRut.Minuscula) == ReglasRut.Minuscula)
				sb.Append (this.DV.ToString ().ToLower ());
			else
				sb.Append (this.DV);

			// Done
			return sb.ToString ();
		}



		// Static methods

		/// <summary>
		/// Valida si el RUT es válido
		/// </summary>
		/// <returns><c>true</c> si el número y el dígito verificador son válidos; en otro caso, retorna <c>false</c>.</returns>
		/// <param name="numero">Número del RUT</param>
		/// <param name="dv">Dígito verificador del RUT</param>
		public static bool IsValid (int numero, char dv)
		{
			var rut = new Rut (numero);
			return rut.DV.ToString ().ToUpper () == rut.DV.ToString ().ToUpper ();
		}

		/// <summary>
		/// Crea un nuevo RUT tomando en cuenta el texto ingresado.
		/// </summary>
		/// <remarks>
		/// No se asume ninguna regla en particular, por lo cual el RUT puede
		/// estar escrito con o sin puntos, con o sin guión
		/// y con el dígito verificador en mayúscula o minúscula.
		/// </remarks>
		/// <param name="texto">El texto que representa al RUT</param>
		public static Rut Parse (string texto)
		{
			return Parse (texto, ReglasRut.Ninguna);
		}

		/// <summary>
		/// Crea un nuevo RUT tomando en cuenta el texto ingresado
		/// y las reglas definidas
		/// </summary>
		/// <param name="texto">El texto del RUT</param>
		/// <param name="reglas">Reglas a aplicar</param>
		public static Rut Parse (string texto, ReglasRut reglas)
		{
			// Build regex
			var regex = new Regex (BuildRegex (reglas));
			var match = regex.Match (texto);
			if (!match.Success)
				throw new ArgumentException ("El texto no tiene formato esperado: " + texto);

			// Get parts
			var numero = int.Parse (match.Groups ["numero"].Value.Replace (".", string.Empty));
			var dv = match.Groups ["dv"].Value [0];

			// Done
			return new Rut (numero, dv);
		}



		// Private static methods

		private static string BuildRegex (ReglasRut reglas)
		{
			// Init
			var sb = new StringBuilder ();

			// Número
			if ((reglas & ReglasRut.ConSeparadorDeMiles) == ReglasRut.ConSeparadorDeMiles)
				sb.Append ("^(?<numero>[1-9]\\d{0,2}(\\.\\d{3}){0,2})");
			else if ((reglas & ReglasRut.SinSeparadorDeMiles) == ReglasRut.SinSeparadorDeMiles)
				sb.Append ("^(?<numero>[1-9]\\d{0,8})");
			else
				sb.Append ("^(?<numero>[1-9](\\d{0,2}(\\.\\d{3}){0,2}|\\d{0,8}))");

			// Guión
			if ((reglas & ReglasRut.ConGuion) == ReglasRut.ConGuion)
				sb.Append ("\\-");
			else if ((reglas & ReglasRut.SinGuion) != ReglasRut.SinGuion)
				sb.Append ("\\-?");

			// DV
			if ((reglas & ReglasRut.Mayuscula) == ReglasRut.Mayuscula)
				sb.Append ("(?<dv>[0-9K])$");
			else if ((reglas & ReglasRut.Minuscula) == ReglasRut.Minuscula)
				sb.Append ("(?<dv>[0-9k])$");
			else
				sb.Append ("(?<dv>[0-9Kk])$");

			// Done
			return sb.ToString ();
		}

		private static void Check (int numero)
		{
			if (numero < 1 || numero >= 100000000)
				throw new ArgumentOutOfRangeException (
					"numero", numero,
					"El número de RUT no puede ser menor a 1 o mayor a 99.999.999");
		}

		private static IEnumerable <int> GetDigits (int numero)
		{
			do yield return numero % 10;
			while ((numero /= 10) > 0);
		}

		private static char GenerateDV (int numero)
		{
			return "0K987654321" [
				GetDigits (numero)
					.Select ((d, i) =>  
						((i % 6) + 2) * d)
					.Sum () % 11];
		}
	}
}

