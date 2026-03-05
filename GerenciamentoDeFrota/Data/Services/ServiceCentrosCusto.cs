using GerenciamentoDeFrota.Data.Models;
using GerenciamentoDeFrota.Interfaces.Gerenciadores;
using GerenciamentoDeFrota.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using GerenciamentoDeFrota.Exceptions.CustomExceptions;

namespace GerenciamentoDeFrota.Data.Services
{
    public class ServiceCentrosCusto : IServiceCentrosCusto
    {
        private readonly ICentrosCustoRepository _repository;

        #region Construtor
        public ServiceCentrosCusto(ICentrosCustoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        #endregion

        #region Transações DB
        public void DeletarCentroCusto(long id)
        {
            _repository.DeleteCentroCusto(id);
        }

        public List<CentrosCusto> ListarCentrosCustos() => _repository.GetCentrosCustos();

        public CentrosCusto? RecuperarCentrosCustoById(long id) => 
            _repository.GetCentroCustoById(id) ?? throw new RegisterNotFoundException(string.Empty);

        public void SalvarCentroCusto(CentrosCusto centroCusto)
        {
            if (centroCusto == null) 
                throw new ArgumentNullException(nameof(centroCusto), "O centro de custo não pode ser nulo!");

            if (string.IsNullOrWhiteSpace(centroCusto.Nome)) 
                throw new ErrorOnValidationException("O nome do centro de custo é obrigatório!");

            if (centroCusto.Id == 0)
            {
                _repository.AddCentroCusto(centroCusto);
            }
            else
            {
                _repository.UpdateCentroCusto(centroCusto);
            }
        }
        #endregion
    }
}
