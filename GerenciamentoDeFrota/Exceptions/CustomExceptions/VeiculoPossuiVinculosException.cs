using GerenciamentoDeFrota.Exceptions.ExceptionBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Exceptions.CustomExceptions
{
    public class VeiculoPossuiVinculosException : GerenciamentoDeFrotaExceptions
    {
        public int? TotalAgendamentos { get; }
        public VeiculoPossuiVinculosException(int? totalAgendamentos) : 
            base($"Este veículo tem um total de {totalAgendamentos} agendamentos relacionados!")
        => TotalAgendamentos = totalAgendamentos;
    }
}
