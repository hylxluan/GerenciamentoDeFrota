using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Exceptions.CustomExceptions
{
    public class RegisterAlreadyExistsException : GerenciamentoDeFrotaExceptions
    {
        public RegisterAlreadyExistsException(string? message = "O registro já existe!") : base(message)
        {
        }
    }
}
