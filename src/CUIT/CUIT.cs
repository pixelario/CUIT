using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pixelario.CUIT
{
    /// <summary>
    /// Representa una clave unica de identificación tributaria de Argentina (CUIT)
    /// </summary>
    public class CUIT : IEquatable<CUIT>
    {
        private const long MINCUIT = 20010000000;
        private const long MAXCUIT = 34999999999;
        private static readonly Regex RegexSoloNumeros = new Regex(@"^\d+$", // Cadena regex
            RegexOptions.Compiled | //El regex debe estar precompilado
            RegexOptions.CultureInvariant, // No necesitamos chequear cultura para digitos, -,. y espacios
            TimeSpan.FromMilliseconds(100) // Importante no puede tardar mas de 100 milisegundos en detectar (evitar consumo de micro)
            );

        private static readonly Regex RegexConGuiones = new Regex(@"^\d{2}(-\d{7,8})-\d$", RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100));

        private static readonly Regex RegexConPuntos = new Regex(@"^\d{2}(\.\d{7,8})\.\d$", RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100));
        private static readonly Regex RegexConEspacio = new Regex(@"^\d{2}(\s\d{7,8})\s\d$", RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100));
        public struct ComponentesStruct
        {
            public string Tipo { get; set; }
            public string NumeroDeDocumento { get; set; }
            public string Verificador { get; set; }
            public ComponentesStruct(TipoDeCUIT tipo, int numeroDeDocumento,
                int verificador)
            {
                this.Tipo = CUIT.FastTipoDeCuitToString(tipo);

                this.NumeroDeDocumento = numeroDeDocumento >= 10_000_000 ?
                    numeroDeDocumento.ToString() :
                    string.Create(8, numeroDeDocumento.ToString(),
                        (span, value) =>
                        {
                            var numeroDeDocumento = value.AsSpan();
                            span[0] = '0';
                            for (int i = 1; i <= 7; i++)
                            {
                                span[i] = numeroDeDocumento[i - 1];
                            }
                        });
                this.Verificador = verificador.ToString();
            }
        }


        private bool _isValid = false;
        public ComponentesStruct Componentes { get; private set; }
        /// <summary>
        /// Inicializa una nueva instancia de un <see cref="CUIT">CUIT</see> ingresando los componentes 
        /// de forma individual
        /// </summary>
        /// <param name="tipoDeCUIT">Valor del enum que representa el tipo en un CUIT</param>
        /// <param name="numeroDeDocumento">Valor del número de documento del CUIT</param>
        /// <param name="verificador">Valor que representa el verificador del CUIT</param>
        /// <exception cref="ArgumentOutOfRangeException">Cuando <paramref name="numeroDeDocumento"/> es menor a 1.000.000 
        /// o cuando <paramref name="verificador"/> es mayor a 9</exception>
        public CUIT(TipoDeCUIT tipoDeCUIT, 
            int numeroDeDocumento, 
            byte verificador)
        {
            if (numeroDeDocumento < 1_000_000)
            {
                throw new ArgumentOutOfRangeException(nameof(numeroDeDocumento));
            }
            if (verificador > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(numeroDeDocumento));
            }
            this.Componentes = new ComponentesStruct(
                tipo: tipoDeCUIT,
                numeroDeDocumento: numeroDeDocumento,
                verificador: verificador);
            this._isValid = CUIT.CalcularVerificador(this.ToString()).ToString() == this.Componentes.Verificador;
        }
        private CUIT(ComponentesStruct componentes)
        {
            this.Componentes = componentes;
            this._isValid = CUIT.CalcularVerificador(this.ToString()).ToString() == this.Componentes.Verificador;
        }
        /// <summary>
        /// Convierte una cadena de texto en un <see cref="CUIT">CUIT</see>
        /// </summary>
        /// <param name="cuit">Valor de la cadena de texto a convertir</param>
        /// <returns>Un nuevo <see cref="CUIT">CUIT</see> instanciado</returns>
        /// <exception cref="ArgumentNullException">Cuando <paramref name="cuit"/> es null</exception>
        /// <exception cref="FormatException">Cuando <paramref name="cuit"/> está vacio o cuando no se puede convertir <paramref name="cuit"/> en <see cref="CUIT">CUIT</see></exception>
        public static CUIT Parse(string cuit)
        {
            if(cuit is null)
            {
                throw new ArgumentNullException(nameof(cuit));
            }
            if(string.IsNullOrWhiteSpace(cuit))
            {
                throw new FormatException("La cadena ingresada no tiene un formato valido.");
            }
            var componentes = CastCUIT(cuit);
            if(string.IsNullOrEmpty(componentes.Tipo) ||
                string.IsNullOrEmpty(componentes.NumeroDeDocumento) ||
                string.IsNullOrEmpty(componentes.Verificador))
            {
                throw new FormatException("La cadena ingresada no tiene un formato valido.");
            }
            return new CUIT(componentes: componentes);
        }
        /// <summary>
        /// Intenta convertir una cadena de texto en un <see cref="CUIT">CUIT</see>
        /// </summary>
        /// <param name="input">Valor de la cadena de texto a convertir</param>
        /// <param name="cuit">Variable al que se le instanciará un nuevo <see cref="CUIT">CUIT</see> si resulta la converción</param>
        /// <returns>True si puede convertir <paramref name="input"/> en un<see cref="CUIT">CUIT</see>; False de lo contrario</returns>
        public static bool TryParse(string input, out CUIT cuit)
        {
            cuit = null;
            if (input is null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }
            var componentes = CastCUIT(input);
            if (string.IsNullOrEmpty(componentes.Tipo) ||
                string.IsNullOrEmpty(componentes.NumeroDeDocumento) ||
                string.IsNullOrEmpty(componentes.Verificador))
            {
                return false;
            }
            cuit = new CUIT(componentes: componentes);
            return true;
        }
        /// <summary>
        /// Convierte un número long (64-bit con signo) en un <see cref="CUIT">CUIT</see>
        /// </summary>
        /// <param name="cuit">Valor del número long (64-bit con signo) a convertir</param>
        /// <returns>Un nuevo <see cref="CUIT">CUIT</see> instanciado</returns>
        /// <exception cref="número long (64-bit con signo)">Cuando <paramref name="cuit"/> es menor que 20010000000 o cuando <paramref name="cuit"/> es mayor que 34999999999</exception>
        public static CUIT Parse(long cuit)
        {
            if (cuit < MINCUIT || cuit > MAXCUIT)
            {
                throw new ArgumentOutOfRangeException(nameof(cuit));
            }
            return new CUIT(componentes: CastCUIT(cuit.ToString()));
        }
        /// <summary>
        /// Intenta convertir un número long (64-bit con signo) en un <see cref="CUIT">CUIT</see>
        /// </summary>
        /// <param name="input">Valor del número long (64-bit con signo) a convertir</param>
        /// <param name="cuit">Variable al que se le instanciará un nuevo <see cref="CUIT">CUIT</see> si resulta la converción</param>
        /// <returns>True si puede convertir <paramref name="input"/> en un<see cref="CUIT">CUIT</see>; False de lo contrario</returns>
        public static bool TryParse(long input, out CUIT cuit)
        {
            if (input < MINCUIT || input > MAXCUIT)
            {
                cuit = null;
                return false;
            }
            cuit = new CUIT(componentes: CastCUIT(input.ToString()));
            return true;

        }
        /// <summary>
        /// Completa un <see cref="CUIT">CUIT</see> con solos los primeros dos componentes
        /// </summary>
        /// <param name="tipoDeCUIT">Valor del enum que representa el tipo en un CUIT</param>
        /// <param name="numeroDeDocumento">Valor del número de documento del CUIT</param>
        /// <returns>Un nuevo <see cref="CUIT">CUIT</see> instanciado</returns>
        public static CUIT Complete(TipoDeCUIT tipoDeCUIT, int numeroDeDocumento)
        {
            return new CUIT(tipoDeCUIT, numeroDeDocumento,
                CUIT.CalcularVerificador(
                    string.Format("{0}{1}0", (int)tipoDeCUIT,
                        numeroDeDocumento.ToString().Length > 7 ?
                            numeroDeDocumento.ToString() :
                            string.Format("0{0}", numeroDeDocumento.ToString()))));
        }

        private static ComponentesStruct CastCUIT(string cuit) =>
            new (Func<string, bool> condition, Func<string, ComponentesStruct> Cast)[]
            {
                (cuit => RegexSoloNumeros.IsMatch(cuit) && cuit.Length == 11,
                    cuit => CastString(cuit, 2, 8, 10)),
                (cuit => RegexSoloNumeros.IsMatch(cuit) && cuit.Length == 10,
                    cuit => CastString(cuit, 2,7,9)),

                (cuit => (  RegexConGuiones.IsMatch(cuit) ||
                            RegexConPuntos.IsMatch(cuit) ||
                            RegexConEspacio.IsMatch(cuit)) &&
                        cuit.Length == 13,
                    cuit => CastString(cuit,3,8, 12)),

                (cuit => (  RegexConGuiones.IsMatch(cuit) ||
                            RegexConPuntos.IsMatch(cuit) ||
                            RegexConEspacio.IsMatch(cuit)) && cuit.Length == 12,
                    cuit => CastString(cuit,3,7,11)),
                (cuit => true, (cuit) => { return new ComponentesStruct(); })
            }.First(x => x.condition(cuit)).Cast(cuit);

        private static ComponentesStruct CastString(string cuit, int indice1, int count1, int indice2)
        {
            ReadOnlySpan<char> _cuitSpan = cuit.AsSpan();
            return new ComponentesStruct(tipo: (TipoDeCUIT)int.Parse(_cuitSpan.Slice(0, 2)),
                        numeroDeDocumento: int.Parse(_cuitSpan.Slice(indice1, count1)),
                        verificador: int.Parse(_cuitSpan.Slice(indice2)));
        }

        /// <summary>
        /// Retorna la representación en cadena de texto del actual <see cref="CUIT">CUIT</see>
        /// </summary>
        /// <returns>Representación en cadena de texto sin separador</returns>
        public override string ToString() =>             
            string.Create(11, this.Componentes,
                (span, value) =>
                    {
                        var x = value.Tipo.AsSpan();
                        var y = value.NumeroDeDocumento.AsSpan();
                        var z = value.Verificador.AsSpan();
                        span[0] = x[0];
                        span[1] = x[1];
                        span[2] = y[0];
                        span[3] = y[1];
                        span[4] = y[2];
                        span[5] = y[3];
                        span[6] = y[4];
                        span[7] = y[5];
                        span[8] = y[6];
                        span[9] = y[7];
                        span[10] = z[0];
                    });
        /// <summary>
        /// Retorna la representación en cadena de texto del actual <see cref="CUIT">CUIT</see>
        /// </summary>
        /// <param name="separador">Valor de la cadena de texto que representa el separador</param>
        /// <returns>Representación en cadena de texto con separador</returns>
        public string ToString(string separador)
        {
            switch (separador)
            {
                case "hyphen":
                case "guion":
                    return string.Create(13, this.Componentes, (span, value) => {
                        var x = value.Tipo.AsSpan();
                        var y = value.NumeroDeDocumento.AsSpan();
                        var z = value.Verificador.AsSpan();
                        span[0] = x[0];
                        span[1] = x[1];
                        span[2] = '-';
                        span[3] = y[0];
                        span[4] = y[1];
                        span[5] = y[2];
                        span[6] = y[3];
                        span[7] = y[4];
                        span[8] = y[5];
                        span[9] = y[6];
                        span[10] = y[7];
                        span[11] = '-';
                        span[12] = z[0];
                    });
                case "dot":
                case "punto":
                    return string.Create(13, this.Componentes, (span, value) => {
                        var x = value.Tipo.AsSpan();
                        var y = value.NumeroDeDocumento.AsSpan();
                        var z = value.Verificador.AsSpan();
                        span[0] = x[0];
                        span[1] = x[1];
                        span[2] = '.';
                        span[3] = y[0];
                        span[4] = y[1];
                        span[5] = y[2];
                        span[6] = y[3];
                        span[7] = y[4];
                        span[8] = y[5];
                        span[9] = y[6];
                        span[10] = y[7];
                        span[11] = '.';
                        span[12] = z[0];
                    });
                case "space":
                case "espacio":
                    return string.Create(13, this.Componentes, (span, value) => {
                        var x = value.Tipo.AsSpan();
                        var y = value.NumeroDeDocumento.AsSpan();
                        var z = value.Verificador.AsSpan();
                        span[0] = x[0];
                        span[1] = x[1];
                        span[2] = (char)32;
                        span[3] = y[0];
                        span[4] = y[1];
                        span[5] = y[2];
                        span[6] = y[3];
                        span[7] = y[4];
                        span[8] = y[5];
                        span[9] = y[6];
                        span[10] = y[7];
                        span[11] = (char)32;
                        span[12] = z[0];
                    });
                default:
                    return this.ToString();
            }
        }
        /// <summary>
        /// Retorna la representación en cadena de texto del actual <see cref="CUIT">CUIT</see>
        /// </summary>
        /// <param name="separador">Valor char que representa el separador</param>
        /// <returns>Representación en cadena de texto con separador</returns>
        public string ToString(char separador)
        {
            return string.Create(13, this.Componentes, (span, value) => {
                var x = value.Tipo.AsSpan();
                var y = value.NumeroDeDocumento.AsSpan();
                var z = value.Verificador.AsSpan();
                span[0] = x[0];
                span[1] = x[1];
                span[2] = separador;
                span[3] = y[0];
                span[4] = y[1];
                span[5] = y[2];
                span[6] = y[3];
                span[7] = y[4];
                span[8] = y[5];
                span[9] = y[6];
                span[10] = y[7];
                span[11] = separador;
                span[12] = z[0];
            });
        }
        private static string FastTipoDeCuitToString(TipoDeCUIT tipo)
        {
            switch (tipo)
            {
                case TipoDeCUIT._20:
                    return "20";
                case TipoDeCUIT._23:
                    return "23";
                case TipoDeCUIT._24:
                    return "24";
                case TipoDeCUIT._27:
                    return "27";
                case TipoDeCUIT._30:
                    return "30";
                case TipoDeCUIT._33:
                    return "33";
                case TipoDeCUIT._34:
                    return "34";
                default:
                    return "0";
            }
        }

        private static byte CalcularVerificador(string cadena)
        {

            ReadOnlySpan<char> _cadenaSpan = cadena.AsSpan();
            return Convert.ToByte((
                (6 * byte.Parse(_cadenaSpan.Slice((0), 1))) +
                (7 * byte.Parse(_cadenaSpan.Slice((1), 1))) +
                (8 * byte.Parse(_cadenaSpan.Slice((2), 1))) +
                (9 * byte.Parse(_cadenaSpan.Slice((3), 1))) +
                (4 * byte.Parse(_cadenaSpan.Slice((4), 1))) +
                (5 * byte.Parse(_cadenaSpan.Slice((5), 1))) +
                (6 * byte.Parse(_cadenaSpan.Slice((6), 1))) +
                (7 * byte.Parse(_cadenaSpan.Slice((7), 1))) +
                (8 * byte.Parse(_cadenaSpan.Slice((8), 1))) +
                (9 * byte.Parse(_cadenaSpan.Slice((9), 1)))
                ) % 11);
        }
        /// <summary>
        /// Determina cuando el <see cref="CUIT">CUIT</see> epecífico es válido
        /// </summary>
        /// <returns>True si es válido; False de lo contrario</returns>
        public bool IsValid()
        {
            return this._isValid;
        }
        /// <summary>
        /// Determina cuando el <see cref="CUIT">CUIT</see> epecífico es igual a otro.
        /// </summary>
        /// <param name="other">Valor del <see cref="CUIT">CUIT</see> con el que se va a comparar</param>
        /// <returns>True si son iguales; False de lo contrario</returns>
        public bool Equals(CUIT other)
        {
            return this.ToString() == other.ToString();
        }
        /// <summary>
        /// Retorna el código hash del <see cref="CUIT">CUIT</see> epecífico
        /// </summary>
        /// <returns>Un código hash(32-bit signed integer)</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        /// Determina cuando el <see cref="CUIT">CUIT</see> epecífico es igual a un objeto.
        /// </summary>
        /// <param name="obj">Valor del objeto con el que se va a comparar</param>
        /// <returns>True si son iguales; False de lo contrario</returns>
        public override bool Equals(object obj)
        {
            CUIT cuit = obj as CUIT;
            if (cuit != null)
                return Equals(cuit);
            else
                return false;
        }

        public static bool operator ==(CUIT cuit1, CUIT cuit2)
        {
            if (object.ReferenceEquals(cuit1, cuit2)) return true;
            if (object.ReferenceEquals(cuit1, null)) return false;
            if (object.ReferenceEquals(cuit2, null)) return false;
            return cuit1.Equals(cuit2);

        }
        public static bool operator !=(CUIT cuit1, CUIT cuit2)
        {
            if (object.ReferenceEquals(cuit1, cuit2)) return false;
            if (object.ReferenceEquals(cuit1, null)) return true;
            if (object.ReferenceEquals(cuit2, null)) return true;
            return !cuit1.Equals(cuit2);
        }
    }
}
