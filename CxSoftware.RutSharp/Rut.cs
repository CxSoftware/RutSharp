// <copyright file="Rut.cs" company="CxSoftware">
// The MIT License (MIT)
//
// Copyright (c) 2014 Cx Software Ltda.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// </copyright>
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CxSoftware.RutSharp
{
	/// <summary>
	/// Representa un RUT o RUN chileno
	/// </summary>
	public class Rut
	{
		// Static fields

		/// <summary>
		/// Cultura chilena. Se utiliza para imprimir el número de Rut con separador de miles.
		/// </summary>
		private static readonly CultureInfo ChileanCulture = new CultureInfo ("es-CL");



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
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="CxSoftware.RutSharp.Rut"/>.
		/// </summary>
		/// <param name="o">The <see cref="System.Object"/> to compare with the current <see cref="CxSoftware.RutSharp.Rut"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="CxSoftware.RutSharp.Rut"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals (object o)
		{
			var rut2 = o as Rut;
			return this == rut2;
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="CxSoftware.RutSharp.Rut"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode ()
		{
			return this.Numero.GetHashCode ();
		}

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
				ReglasRut.SinCeroALaIzquierda |
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

			// Part 0
			if ((reglas & ReglasRut.ConCeroALaIzquierda) > 0)
				sb.Append ("0");

			// Part 1
			if ((reglas & ReglasRut.SinSeparadorDeMiles) > 0)
				sb.Append (this.Numero.ToString (CultureInfo.InvariantCulture));
			else
				sb.Append (this.Numero.ToString ("#,#", ChileanCulture));

			// Part 2
			if ((reglas & ReglasRut.SinGuion) == 0)
				sb.Append ("-");

			// Part 3
			if ((reglas & ReglasRut.Minuscula) > 0)
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
		/// <returns>Rut</returns>
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
		/// <returns>Rut</returns>
		public static Rut Parse (string texto, ReglasRut reglas)
		{
			return Parse (
				texto,
				BuildRegex (reglas),
				numero => numero.Replace (".", string.Empty));
		}

		/// <summary>
		/// Crea un nuevo RUT tomando en cuenta el texto ingresado
		/// y utilizando una expresión regular definida. Esta expresión
		/// regular debe contener dos grupo: "numero" para el número del RUT
		/// y "dv" para el dígito verificador.
		/// </summary>
		/// <param name="texto">El texto del RUT</param>
		/// <param name="regex">Expresión regular a evaluar</param>
		/// <returns>Rut</returns>
		public static Rut Parse (string texto, string regex)
		{
			return Parse (texto, regex, null);
		}

		/// <summary>
		/// Crea un nuevo RUT tomando en cuenta el texto ingresado,
		/// utilizando una expresión regular definida y modificando el
		/// texto a través de una función dada. La expresión
		/// regular debe contener dos grupo: "numero" para el número del RUT
		/// y "dv" para el dígito verificador.
		/// </summary>
		/// <param name="texto">El texto del RUT</param>
		/// <param name="regex">Expresión regular a evaluar</param>
		/// <param name="funcNumero">Función que modifica el texto del RUT una vez obtenido desde la expresión regular</param>
		/// <returns>Rut</returns>
		public static Rut Parse (string texto, string regex, Func<string, string> funcNumero)
		{
			// Build regex
			var match = Regex.Match (texto, regex);
			if (!match.Success)
				throw new ArgumentException (
					string.Format (
						"El texto no tiene formato esperado. " +
						"Texto: \"{0}\". " +
						"Expresión regular: \"{1}\".",
						texto,
						regex));

			// Check regex groups
			if (match.Groups ["numero"] == null)
				throw new Exception ("La expresión regular no contiene un grupo llamado \"número\": " + regex);
			if (match.Groups ["dv"] == null)
				throw new Exception ("La expresión regular no contiene un grupo llamado \"dv\": " + regex);

			// Get groups
			var numeroString = match.Groups ["numero"].Value;
			if (funcNumero != null)
				numeroString = funcNumero (numeroString);
			var digitoString = match.Groups ["dv"].Value;

			// Check parts
			if (numeroString.Length == 0)
				throw new Exception (
					string.Format (
						"El número del Rut debe tener al menos un dígito. " +
						"Texto: \"{0}\". " +
						"Expresión regular: \"{1}\". " +
						"Número: \"{2}\".",
						texto,
						regex,
						numeroString));
			if (digitoString.Length != 1)
				throw new Exception (
					string.Format (
						"El dígito verificador del Rut debe tener un dígito. " +
						"Texto: \"{0}\". " +
						"Expresión regular: \"{1}\". " +
						"Dígito verificador: \"{2}\".",
						texto,
						regex,
						digitoString));

			// Parse parts
			var numero = int.Parse (numeroString);
			var dv = digitoString [0];

			// Done
			return new Rut (numero, dv);
		}

		// Operators

		/// <summary>Operador de sigualdad entre dos Ruts</summary>
		/// <param name="rut1">Rut1</param>
		/// <param name="rut2">Rut2</param>
		/// <returns>Retorna verdadero si los dos Ruts son iguales</returns>
		public static bool operator == (Rut rut1, Rut rut2)
		{
			var o1 = (object) rut1;
			var o2 = (object) rut2;

			if (o1 == null && o2 == null)
				return true;
			if (o1 == null || o2 == null)
				return false;

			return rut1.Numero == rut2.Numero;
		}

		/// <summary>Operador de desigualdad entre dos Ruts</summary>
		/// <param name="rut1">Rut1</param>
		/// <param name="rut2">Rut2</param>
		/// <returns>Retorna verdadero si los dos Ruts son diferentes</returns>
		public static bool operator != (Rut rut1, Rut rut2)
		{
			return !(rut1 == rut2);
		}



		// Private static methods

		/// <summary>
		/// Crea la expresión regular para parsear el Rut según las reglas indicadas
		/// </summary>
		/// <returns>La expresión regular</returns>
		/// <param name="reglas">Reglas para el parseo del Rut</param>
		private static string BuildRegex (ReglasRut reglas)
		{
			// Init
			var sb = new StringBuilder ();

			// Cero a la izquierda
			if ((reglas & ReglasRut.ConCeroALaIzquierda) > 0)
				sb.Append ("^0");
			else if ((reglas & ReglasRut.SinCeroALaIzquierda) > 0)
				sb.Append ("^");
			else
				sb.Append ("^0?");

			// Número
			if ((reglas & ReglasRut.ConSeparadorDeMiles) > 0)
				sb.Append ("(?<numero>[1-9]\\d{0,2}(\\.\\d{3}){0,2})");
			else if ((reglas & ReglasRut.SinSeparadorDeMiles) > 0)
				sb.Append ("(?<numero>[1-9]\\d{0,8})");
			else
				sb.Append ("(?<numero>[1-9](\\d{0,2}(\\.\\d{3}){0,2}|\\d{0,8}))");

			// Guión
			if ((reglas & ReglasRut.ConGuion) > 0)
				sb.Append ("\\-");
			else if ((reglas & ReglasRut.SinGuion) == 0)
				sb.Append ("\\-?");

			// DV
			if ((reglas & ReglasRut.Mayuscula) > 0)
				sb.Append ("(?<dv>[0-9K])$");
			else if ((reglas & ReglasRut.Minuscula) > 0)
				sb.Append ("(?<dv>[0-9k])$");
			else
				sb.Append ("(?<dv>[0-9Kk])$");

			// Done
			return sb.ToString ();
		}

		/// <summary>
		/// Verifica que el número de Rut sea válido
		/// </summary>
		/// <param name="numero">Numero de Rut</param>
		private static void Check (int numero)
		{
			if (numero < 1 || numero >= 100000000)
				throw new ArgumentOutOfRangeException (
					"numero",
					numero,
					"El número de RUT no puede ser menor a 1 o mayor a 99.999.999");
		}

		/// <summary>
		/// Devuelve los dígitos de un número entero de derecha a izquierda
		/// </summary>
		/// <returns>Los digitos</returns>
		/// <param name="numero">Numero</param>
		private static IEnumerable <int> GetDigits (int numero)
		{
			do yield return numero % 10;
			while ((numero /= 10) > 0);
		}

		/// <summary>
		/// Genera el dígito verificador del Rut utilizando el algoritmo "módulo 11"
		/// </summary>
		/// <returns>El dígito verificador</returns>
		/// <param name="numero">Número de Rut</param>
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

