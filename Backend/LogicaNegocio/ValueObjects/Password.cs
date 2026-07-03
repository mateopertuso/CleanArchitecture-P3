using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LogicaNegocio.ValueObjects
{
    public class Password : IEquatable<Password>
    {
        public string Valor { get; private set; }

        public Password(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Valor))
                throw new Exception("La contraseña es obligatoria");

            if (Valor.Length < 8)
                throw new Exception("La contraseña debe tener al menos 8 caracteres");

            if (!Valor.Any(char.IsUpper))
                throw new Exception("Debe tener al menos una mayúscula");

            if (!Valor.Any(char.IsLower))
                throw new Exception("Debe tener al menos una minúscula");

            if (!Valor.Any(char.IsDigit))
                throw new Exception("Debe tener al menos un número");

            if (!Valor.Any(c => !char.IsLetterOrDigit(c)))
                throw new Exception("Debe tener al menos un carácter especial");
        }

        public bool Equals(Password? obj)
        {
            if (obj == null) return false;
            return this.Valor.Equals(obj.Valor);
        }
    }
}
