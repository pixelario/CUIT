using System;
using System.Text.RegularExpressions;

namespace Pixelario.CUIT
{
    public class CUIT : IEquatable<CUIT>
    {
        private const long MINCUIT = 20010000000;
        private const long MAXCUIT = 34999999999;
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
        private ComponentesStruct Componentes { get; set; }
        public CUIT(string cuit)
        {
            if (cuit.Length < 9 ||
                cuit.Length > 13)
            {
                this._isValid = false;
            }
            else
            {
                var regexSoloNumeros = new Regex(@"^\d+$");
                if (regexSoloNumeros.IsMatch(cuit))
                {
                    this.Componentes = this.CastString(cuit);
                }
                var regexConGuiones = new Regex(@"^\d{2}(-\d{7,8})-\d$");
                if (regexConGuiones.IsMatch(cuit))
                {
                    this.Componentes = this.CastString(cuit, '-');
                }
                var regexConPuntos = new Regex(@"^\d{2}(\.\d{7,8})\.\d$");
                if (regexConPuntos.IsMatch(cuit))
                {
                    this.Componentes = this.CastString(cuit, '.');
                }
                var regexConEspacio = new Regex(@"^\d{2}(\s\d{7,8})\s\d$");
                if (regexConEspacio.IsMatch(cuit))
                {
                    this.Componentes = this.CastString(cuit, ' ');
                }
                if (this._isValid)
                {
                    this._isValid = this.CalcularVerificador() == this.Componentes.Verificador;
                }
            }
        }

        public CUIT(TipoDeCUIT tipoDeCUIT, int numeroDeDocumento, int verificador)
        {
            this.Componentes = new ComponentesStruct(
                tipo: tipoDeCUIT,
                numeroDeDocumento: numeroDeDocumento,
                verificador: verificador);
            this._isValid = this.CalcularVerificador() == this.Componentes.Verificador;
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
                this._isValid = this.CalcularVerificador() == this.Componentes.Verificador;
            }
        }
        private ComponentesStruct CastString(string cuit)
        {
            if (cuit.Length < 10 ||
                cuit.Length > 11)
            {
                this._isValid = false;
            }
            int tipo = int.Parse(cuit.Substring(0, 2));
            if (!Enum.IsDefined(typeof(TipoDeCUIT), tipo))
            {
                this._isValid = false;
            }

            int numeroDeDocumento;
            int verificador;
            if (cuit.Length == 11)
            {
                numeroDeDocumento = int.Parse(cuit.Substring(2, 8));
                verificador = int.Parse(cuit.Substring(10));
            }
            else
            {
                numeroDeDocumento = int.Parse(cuit.Substring(2, 7));
                verificador = int.Parse(cuit.Substring(9));

            }
            return new ComponentesStruct(tipo: (TipoDeCUIT)tipo,
                numeroDeDocumento: numeroDeDocumento,
                verificador: verificador);
        }
        private ComponentesStruct CastString(string cuit, char separador)
        {
            var componentes = cuit.Split(separador);
            if (componentes.Length != 3)
            {
                this._isValid = false;
            }
            int tipo = int.Parse(componentes[0]);
            int numeroDeDocumento = int.Parse(componentes[1]);
            int verificador = int.Parse(componentes[2]);
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

        private int CalcularVerificador()
        {
            string codes = "6789456789";
            int x = 0;
            int suma = 0;
            while (x < 10)
            {
                int producto1 = int.Parse(codes.Substring((x), 1));
                int producto2 = int.Parse(this.ToString().Substring((x), 1));
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
