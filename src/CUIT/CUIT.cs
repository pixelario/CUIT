using System;
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
            public TipoDeCUIT Tipo { get; set; }
            public int NumeroDeDocumento { get; set; }
            public int Verificador { get; set; }
            public ComponentesStruct(TipoDeCUIT tipo, int numeroDeDocumento,
                int verificador)
            {
                this.Tipo = tipo;
                this.NumeroDeDocumento = numeroDeDocumento;
                this.Verificador = verificador;
            }
        }
        private bool _isValid = true;
        public ComponentesStruct Componentes { get; private set; }
        public CUIT(string cuit)
        {
            if (cuit.Length < 9 ||
                cuit.Length > 13)
            {
                this._isValid = false;
            }
            else
            {
                if (RegexSoloNumeros.IsMatch(cuit))
                {
                    this.Componentes = this.CastString(cuit);
                }                
                if (RegexConGuiones.IsMatch(cuit))
                {
                    this.Componentes = this.CastString(cuit, '-');
                }                
                if (RegexConPuntos.IsMatch(cuit))
                {
                    this.Componentes = this.CastString(cuit, '.');
                }                
                if (RegexConEspacio.IsMatch(cuit))
                {
                    this.Componentes = this.CastString(cuit, ' ');
                }
                if (this._isValid)
                {
                    this._isValid = CUIT.CalcularVerificador(this.ToString()) == this.Componentes.Verificador;
                }
            }
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
                tipo: tipoDeCUIT,
                numeroDeDocumento: numeroDeDocumento,
                verificador: verificador);
            this._isValid = CUIT.CalcularVerificador(this.ToString()) == this.Componentes.Verificador;
        }
        public CUIT(long cuit)
        {
            if(cuit < MINCUIT || cuit > MAXCUIT)
            {
                this._isValid = false;
            }
            else
            {
                this.Componentes = this.CastString(cuit.ToString());
                this._isValid = CUIT.CalcularVerificador(cuit.ToString()) == this.Componentes.Verificador;
            }
        }
        private ComponentesStruct CastString(string cuit)
        {
            ReadOnlySpan<char> _cuitSpan = cuit.AsSpan();
            if (_cuitSpan.Length < 10 ||
                _cuitSpan.Length > 11)
            {
                this._isValid = false;
            }
            int tipo = int.Parse(_cuitSpan.Slice(0, 2));
            if (!Enum.IsDefined(typeof(TipoDeCUIT), tipo))
            {
                this._isValid = false;
            }

            int numeroDeDocumento;
            int verificador;
            if (cuit.Length == 11)
            {
                numeroDeDocumento = int.Parse(_cuitSpan.Slice(2, 8));
                verificador = int.Parse(_cuitSpan.Slice(10));
            }
            else
            {
                numeroDeDocumento = int.Parse(_cuitSpan.Slice(2, 7));
                verificador = int.Parse(_cuitSpan.Slice(9));

            }
            return new ComponentesStruct(tipo: (TipoDeCUIT)tipo,
                numeroDeDocumento: numeroDeDocumento,
                verificador: verificador);
        }
        private ComponentesStruct CastString(string cuit, char separador)
        {
            int tipo = 0, numeroDeDocumento = 0, verificador = 0;
            ReadOnlySpan<char> _cuitSpan = cuit.AsSpan();
            for (int i = 0; i<3; i++)
            {
                if (_cuitSpan.Length == 0)
                {
                    this._isValid = false;
                }
                else
                {
                    var index = _cuitSpan.IndexOf(separador);
                    if(index == -1 && i < 2)
                    {
                        this._isValid = false;
                    }
                    else
                    {
                        switch(i)
                        {
                            case 0:
                                tipo = int.Parse(_cuitSpan.Slice(0, index));
                                break;
                            case 1:
                                numeroDeDocumento = int.Parse(_cuitSpan.Slice(0, index));
                                break;
                            case 2:
                                verificador = int.Parse(_cuitSpan);
                                break;
                        }                        
                        _cuitSpan = _cuitSpan.Slice(index + 1);
                    }
                }
            }
            return new ComponentesStruct(tipo: (TipoDeCUIT)tipo,
                numeroDeDocumento: numeroDeDocumento,
                verificador: verificador);
        }
        public override string ToString()
        {
            return string.Format("{0}{1}{2}",
                (int)this.Componentes.Tipo,
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
                (int)this.Componentes.Tipo,
                this.Componentes.NumeroDeDocumento.ToString().Length > 7 ?
                    this.Componentes.NumeroDeDocumento.ToString() :
                    string.Format("0{0}", this.Componentes.NumeroDeDocumento.ToString()),
                        this.Componentes.Verificador);
                case "dot":
                case "punto":
                    return string.Format("{0}.{1}.{2}",
                (int)this.Componentes.Tipo,
                this.Componentes.NumeroDeDocumento.ToString().Length > 7 ?
                    this.Componentes.NumeroDeDocumento.ToString() :
                    string.Format("0{0}", this.Componentes.NumeroDeDocumento.ToString()),
                        this.Componentes.Verificador);
                case "space":
                case "espacio":
                    return string.Format("{0} {1} {2}",
                (int)this.Componentes.Tipo,
                this.Componentes.NumeroDeDocumento.ToString().Length > 7 ?
                    this.Componentes.NumeroDeDocumento.ToString() :
                    string.Format("0{0}", this.Componentes.NumeroDeDocumento.ToString()),
                        this.Componentes.Verificador);

                default:
                    return this.ToString();
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
