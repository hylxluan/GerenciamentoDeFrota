using FluentValidation;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Helpers
{
    public static class ValidatorHelper
    {
        /// <summary>
        /// Valida o objeto usando o validator informado.
        /// Acumula TODOS os erros antes de lançar, em vez de parar no primeiro.
        /// Lança <see cref="ErrorOnValidationException"/> com as mensagens concatenadas.
        /// </summary>
        public static void ValidarOuLancar<T>(AbstractValidator<T> validator, T objeto)
        {
            var resultado = validator.Validate(objeto);

            if (resultado.IsValid) return;

            var erros = string.Join(Environment.NewLine,
                resultado.Errors.Select(e => e.ErrorMessage));

            throw new ErrorOnValidationException(erros);
        }
    }
}
