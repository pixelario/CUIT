using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pixelario.CUIT
{
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

            private int _tipo;
            public TipoDeCUIT Tipo => (TipoDeCUIT)_tipo;
            public int NumeroDeDocumento { get; set; }
            public int Verificador { get; set; }
            public ComponentesStruct(int tipo, int numeroDeDocumento,
                int verificador)
            {
                this._tipo = tipo;
                this.NumeroDeDocumento = numeroDeDocumento;
                this.Verificador = verificador;
            }
        }
        private bool _isValid = true;
        public ComponentesStruct Componentes { get; private set; }



        public CUIT(string cuit)
        {
            this.Componentes = CastCUIT(cuit);
            this._isValid = this.Componentes.Tipo != default(int) &&
                this.Componentes.NumeroDeDocumento != default(int) &&
                CUIT.CalcularVerificador(this.ToString()) == this.Componentes.Verificador;

        }

        public static CUIT Complete(TipoDeCUIT tipoDeCUIT, int numeroDeDocumento)
        {
            return new CUIT(tipoDeCUIT, numeroDeDocumento,
                CUIT.CalcularVerificador(
                    string.Format("{0}{1}0", (int)tipoDeCUIT,
                        numeroDeDocumento.ToString().Length > 7 ?
                            numeroDeDocumento.ToString() :
                            string.Format("0{0}", numeroDeDocumento.ToString()))));
        }
        public CUIT(TipoDeCUIT tipoDeCUIT, int numeroDeDocumento, int verificador)
        {
            this.Componentes = new ComponentesStruct(
                tipo: (int)tipoDeCUIT,
                numeroDeDocumento: numeroDeDocumento,
                verificador: verificador);
            this._isValid = CUIT.CalcularVerificador(this.ToString()) == this.Componentes.Verificador;
        }
        public CUIT(long cuit)
        {
            if (cuit < MINCUIT || cuit > MAXCUIT)
            {
                this._isValid = false;
            }
            else
            {
                this.Componentes = this.CastCUIT(cuit.ToString());
                this._isValid = CUIT.CalcularVerificador(cuit.ToString()) == this.Componentes.Verificador;
            }
        }

        private ComponentesStruct CastCUIT(string cuit) =>
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

        private ComponentesStruct CastString(string cuit, int indice1, int count1, int indice2)
        {
            ReadOnlySpan<char> _cuitSpan = cuit.AsSpan();
            return new ComponentesStruct(tipo: int.Parse(_cuitSpan.Slice(0, 2)),
                        numeroDeDocumento: int.Parse(_cuitSpan.Slice(indice1, count1)),
                        verificador: int.Parse(_cuitSpan.Slice(indice2)));
        }
        public override string ToString()
        {
            return string.Format("{0}{1}{2}",
                FastTipoDeCuitToString(this.Componentes.Tipo),
                this.Componentes.NumeroDeDocumento.ToString().Length > 7 ?
                    this.Componentes.NumeroDeDocumento.ToString() :
                    string.Format("0{0}", this.Componentes.NumeroDeDocumento.ToString()),
                this.Componentes.Verificador);
        }
        public string ToString(string separador)
        {
            switch (separador)
            {
                case "hyphen":
                case "guion":
                    return string.Format("{0}-{1}-{2}",
                FastTipoDeCuitToString(this.Componentes.Tipo),
                this.Componentes.NumeroDeDocumento.ToString().Length > 7 ?
                    this.Componentes.NumeroDeDocumento.ToString() :
                    string.Format("0{0}", this.Componentes.NumeroDeDocumento.ToString()),
                        this.Componentes.Verificador);
                case "dot":
                case "punto":
                    return string.Format("{0}.{1}.{2}",
                        FastTipoDeCuitToString(this.Componentes.Tipo),
                        this.Componentes.NumeroDeDocumento.ToString().Length > 7 ?
                            this.Componentes.NumeroDeDocumento.ToString() :
                            string.Format("0{0}", this.Componentes.NumeroDeDocumento.ToString()),
                        this.Componentes.Verificador);
                case "space":
                case "espacio":
                    return string.Format("{0} {1} {2}",
                        FastTipoDeCuitToString(this.Componentes.Tipo),
                        this.Componentes.NumeroDeDocumento.ToString().Length > 7 ?
                            this.Componentes.NumeroDeDocumento.ToString() :
                            string.Format("0{0}", this.Componentes.NumeroDeDocumento.ToString()),
                        this.Componentes.Verificador);

                default:
                    return this.ToString();
            }
        }

        public static string FastTipoDeCuitToString(TipoDeCUIT tipo)
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

        private static int CalcularVerificador(string cadena)
        {
            ReadOnlySpan<char> code = "6789456789".AsSpan();
            ReadOnlySpan<char> _cadenaSpan = cadena.AsSpan();
            int x = 0;
            int suma = 0;
            while (x < 10)
            {
                int producto1 = int.Parse(code.Slice((x), 1));
                int producto2 = int.Parse(_cadenaSpan.Slice((x), 1));
                int producto = producto1 * producto2;
                suma += producto;
                x++;
            }
            return suma % 11;
        }
        public bool IsValid()
        {
            return this._isValid;
        }

        public bool Equals(CUIT other)
        {
            return this.ToString() == other.ToString();
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
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
