using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciamentoDeFrota.Data.Models
{
    [Serializable]
    public class Users
    {
        private int Id { get; set; }
        private string? Name { get; set; }
        private string? Email { get; set; }
        private string? login { get; set; }
        private string? password { get; set; }
        private bool? Ativo { get; set; }
        private int? IDOficina { get; set; }
        private int? IDGerenciadora { get; set; }
        private int? IDCondutor { get; set; }
        private DateTime? CreatedAt { get; set; }
        private DateTime? UpdatedAt { get; set; }
        

    }
}
