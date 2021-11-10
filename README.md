## Dale una estrella :star:
Si le gusta este proyecto puede apoyarlo dandole una estrella, seguirlo o contribuir!

# CUIT
Una estructura para trabajar de forma rápida y segura el valor CUIT (Argentino) en aplicaciones C#.

Plataforma | Estado
--- | ---
**NuGet** | [![nuget](https://img.shields.io/nuget/v/Pixelario.CUIT.svg)](https://www.nuget.org/packages/Pixelario.CUIT/)

## Instalación
```
Install-Package Pixelario.CUIT -Version 0.10.3
```

## Modo de uso
Puede instanciar un CUIT de tres formas. Por sus componentes, por un valor long o una cadena con y sin separadores.

#### Por sus componentes
```c#
var tipo = TipoDeCUIT._20;
var numeroDeDocumento = 27001001;
var verificador = (byte)7;
var cuit = new CUIT(
    tipoDeCUIT: tipo, 
    numeroDeDocumento: numeroDeDocumento, 
    verificador: verificador);
```

#### Por un número long
```c#
var long1 = 20270010017;
var cuit1 = CUIT.Parse(
    cuit: long1);
```

#### Por una cadena con o sin separador
```c#
var cadena1 = "20270010017";
var cuit1 = CUIT.Parse(
    cuit: cadena1);

var cadena2 = "20-27001001-7";
var cuit2 = CUIT.Parse(
    cuit: cadena2);

var cadena3 = "20.7001001.7";
var cuit3 = CUIT.Parse(
    cuit: cadena3);

var cadena4 = "20 27001001 7";
var cuit4 = CUIT.Parse(
    cuit: cadena4);
```

El componente tipo es un enum con las siguientes opciones
```c#
public enum TipoDeCUIT {
    _20 = 20,
    _23 = 23,
    _24 = 24,
    _27 = 27,
    _30 = 30,
    _33 = 33,
    _34 = 34
}
```

## Métodos

### IsValid
.IsValid calcula y controla que los componentes del CUIT verifican que es valido.
```c#
bool cuitIsValid = cuit.IsValid();
```

### ToString
Puede expresar un CUIT de distintas formas usuando como parametro un separador.
```c#
var tipo = TipoDeCUIT._20;
var numeroDeDocumento = 27001001;
var verificador = (byte)7;
var cuit = new CUIT(
    tipoDeCUIT: tipo, 
    numeroDeDocumento: numeroDeDocumento, 
    verificador: verificador);
Console.WriteLine(cuit.ToString()); // 20270010017
Console.WriteLine(cuit.ToString("guion")); // 20-27001001-7
Console.WriteLine(cuit.ToString("hyphen")); // 20-27001001-7
Console.WriteLine(cuit.ToString("punto")); // 20.27001001.7
Console.WriteLine(cuit.ToString("dot")); // 20.27001001.7
Console.WriteLine(cuit.ToString("espacio")); // 20 27001001 7
Console.WriteLine(cuit.ToString("space")); // 20 27001001 7
Console.WriteLine(cuit.ToString('%')); // 20%27001001%7
```

### Complete
Puede completar un CUIT con solo los dos primeros componentes: Tipo y Número de Documento.
```c#
var cuit = CUIT.Complete(
    tipoDeCUIT: TipoDeCUIT._20,
    numeroDeDocumento: 27001001);

```
## Extensiones

### OnlyValid
Puede filtrar CUITs validos de un IEnumerable 
```c#
var cuitsValidos = listaDeCUITs.OnlyValid().ToList();

```

### RangeDocumento
Puede filtrar CUITs de un IEnumerable por un rango de números de documento.
```c#
var cuits = listaDeCUITs.RangeDocumento(27001001, 10).ToList();

```

## Configurar Entity Framework Core para SQL:
Puede configurar el Context de EFCore con el siguiente código:

```c#
 public class PersonaEntityTypeConfigurationVarcharSQL : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.Property(p => p.CUIT)
                .HasConversion(
                c =>c.ToString(),
                c => CUIT.Parse(c));

        }
    }

```

Crear una migración y actualizar la base de datos.

```c#
dotnet ef migrations add Initial
dotnet ef database update
```

Se construirá una columna en la tabla de la entidad para el valor CUIT en string

_Ver Web API demo_

## Referencias:

[Clave Única de Identificación Tributaria](https://es.wikipedia.org/wiki/Clave_%C3%9Anica_de_Identificaci%C3%B3n_Tributaria)

## ¿Quiere contribuir?
Puede agregar un Issue, una discución, ser sponsor o enviar un PR.
