using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LogicaNegocio.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        public string Valor { get; private set; }

        public Email(string valor)
        {
            Valor = valor;
            Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrWhiteSpace(Valor))
                throw new Exception("El email es obligatorio");

            if (!Valor.Contains("@"))
                throw new Exception("Formato de email inválido");
        }

        public bool Equals(Email? obj)
        {
            if (obj == null) return false;
            return this.Valor.Equals(obj.Valor);
        }

    }
}
