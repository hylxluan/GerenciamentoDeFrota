using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Exceptions.CustomExceptions
{
    public class ErrorOnValidationException : GerenciamentoDeFrotaExceptions
    {
        public ErrorOnValidationException(string? message)
            : base(string.IsNullOrWhiteSpace(message) ? "Erro na validação!" : message) { }
    }
}
