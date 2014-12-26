// <copyright file="ReglasRut.cs" company="CxSoftware">
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
		Minuscula = 0x20,

		/// <summary>
		/// Agregar cero a la izquierda (por ejemplo: 06.904.204-K)
		/// </summary>
		ConCeroALaIzquierda = 0x40,

		/// <summary>
		/// Sin cero a la izquierda (por ejemplo: 6.904.204-K)
		/// </summary>
		SinCeroALaIzquierda = 0x80
	}
}

