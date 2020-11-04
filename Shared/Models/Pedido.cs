using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace ProjetoIntegrador.Shared
{

    public class Pedido
    {
        [Required]
        public int Id { get; set; }
        public int UsuarioId { get; set; }

        public int NotaFiscalId {get; set;}
        public decimal total { get; set; }
        [DataType(DataType.Date)]
        public DateTime data { get; set; }
        public NotaFiscal NotaFiscal { get; set; }
        public Usuario Usuario { get; set; }
        [JsonIgnore]
        public ICollection<ProdutoPedido> ProdutoPedido { get; set; }

    }
}