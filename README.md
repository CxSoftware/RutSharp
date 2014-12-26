rutsharp
========

Implementación del RUT chileno para Mono / .Net

# Licencia

El código es distribuido bajo la licencia MIT. Puede leer la licencia en el archivo LICENSE

# Uso

Puede utilizar esta biblioteca para parsear, validar y generar RUTs chilenos.

## Parsear

Puede llamar al método estático Rut.Parse para parsear el RUT sin importar el formato:

`var rut = Rut.Parse ("9788461-7");`

También puedes aplicar ciertas reglas al parseo. Por ejemplo, exigir puntos separadores de miles:

`var rut = Rut.Parse ("7.063.151-2", ReglasRut.ConSeparadorDeMiles);`

Vea más detalles en la sección de reglas.

## Validar

Puede verificar si un RUT es válido llamando al método estático Rut.IsValid:

`bool esValido = Rut.IsValid (14707574, 'K');`

## Generar

Puede inicializar un RUT simplemente con su número (sin dígito verificador) utilizando el constructor:

`var rut = new Rut (24262413);`

Puede imprimir luego el RUT en esta forma:

`var texto = rut.ToString ();`

El método ToString, en este caso, debería retornar:

`24.262.413-0`

El método ToString soporta reglas. Puede leer más detalles sobre las reglas más abajo.

# Reglas

Las reglas están definidas en la enumeración ReglasRut y pueden ser utilizadas en el segundo
parámetro del método Parse y como parámetro en el método ToString. En ambos casos,
el uso de reglas es opcional. Por defecto, al parsear un RUT, se soportan todos los formatos
disponibles. En el caso de ToString, si se llama sin formato, se utiliza el formato sin
cero a la izquierda, con separadores de miles, con guión y el dígito verificador en mayúsculas.

Las reglas son máscaras de bits y, por lo tanto, pueden combinarse. Por ejemplo:

`rut.ToString (ReglasRut.ConSeparadorDeMiles | ReglasRut.ConGuion | ReglasRut.Minuscula);`

## Listado

Las reglas disponibles son:

* **Ninguna:** No se aplica ninguna regla especial. En el caso de Parse, esto permite soportar todos los formatos.
* **ConCeroALaIzquierda:** Cero a la izquierda. En el caso de Parse, se exige un cero a la izquierda. En el caso de ToString, se genera con 
un cero al inicio.
* **SinCeroALaIzquierda:** Sin cero a la izquierda. En el caso de Parse, no se permite un ero a la izquierda. En el caso de ToString, se 
genera sin un cero al inicio.
* **ConSeparadorDeMiles:** Separador de miles. En el caso de Parse, se exige el separador (punto). En el caso de ToString, se utiliza el separador.
* **SinSeparadorDeMiles:** Sin separador de miles. En el caso de Parse, se prohibe el uso de separador. En el caso de ToString, no se utiliza el 
separador.
* **ConGuion:** Con guión. En el caso de Parse, se exige el guión. En el caso de ToString, se utiliza el guión.
* **SinGuion:** Sin guión. En el caso de Parse, se prohibe el uso del guión. En el caso de ToString, no se utiliza un guión.
* **Mayuscula:** Mayúscula. En el caso de Parse, se exige que el dígito verificador, si es K, esté escrito en mayúscula. En el caso de ToString, 
se utiliza mayúscula si el dígito verificador es K.
* **Minuscula:** Minúscula. En el caso de Parse, se exige que el dígito verificador, si es K, esté escrito en minúscula. En el caso de ToString, 
se utiliza minúscula si el dígito verificador es K.

## Ejemplos

Algunos ejemplos son:

```
var r = new Rut (5023293);
r.ToString (); // "5.023.293-K"
r.ToString (ReglasRut.ConCeroALaIzquierda); // "05.023.293-K"
r.ToString (ReglasRut.SinCeroALaIzquierda); // "5.023.293-K"
r.ToString (ReglasRut.ConSeparadorDeMiles); // "5.023.293-K"
r.ToString (ReglasRut.SinSeparadorDeMiles); // "5023293-K"
r.ToString (ReglasRut.ConGuion); // "5.023.293-K"
r.ToString (ReglasRut.SinGuion); // "5.023.293K"
r.ToString (ReglasRut.Mayuscula); // "5.023.293-K"
r.ToString (ReglasRut.Minuscula); // "5.023.293-k"
r.ToString (ReglasRut.SinSeparadorDeMiles | ReglasRut.SinGuion); // "5023293K"
r.ToString (ReglasRut.SinSeparadorDeMiles | ReglasRut.Minuscula); // "5023293-k"
r.ToString (ReglasRut.SinSeparadorDeMiles | ReglasRut.SinGuion | ReglasRut.Minuscula); // "5023293k"
r.ToString (ReglasRut.ConCeroALaIzquierda | ReglasRut.SinSeparadorDeMiles | ReglasRut.SinGuion | ReglasRut.Minuscula); // "05023293k"

```


