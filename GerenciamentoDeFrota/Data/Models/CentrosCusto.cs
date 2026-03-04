using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Data.Models
{
    [Serializable]
    public class CentrosCusto  // ← não precisa herdar BaseModel
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Observacoes { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}
