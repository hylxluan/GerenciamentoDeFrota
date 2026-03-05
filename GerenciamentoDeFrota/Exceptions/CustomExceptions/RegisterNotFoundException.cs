using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Exceptions.CustomExceptions
{
    public class RegisterNotFoundException : GerenciamentoDeFrotaExceptions
    {
        public RegisterNotFoundException(string? message) 
            : base(string.IsNullOrWhiteSpace(message) ? "O registro não foi encontrado!" 
                  : message) { }
    }
}
