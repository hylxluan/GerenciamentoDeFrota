using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Exceptions.ExceptionBase
{
    public class GerenciamentoDeFrotaExceptions : SystemException
    {
        public GerenciamentoDeFrotaExceptions(string? message)
            : base(string.IsNullOrWhiteSpace(message) ? "Houve um erro inesperado na aplicação!" 
                  : message) { }
    }
}
