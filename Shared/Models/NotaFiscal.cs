using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace ProjetoIntegrador.Shared
{

    public class NotaFiscal
    {
        [Required]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime data { get; set; }
        public string cpf_comprador { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string nome { get; set; }
        [Required]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = "Informe o CVV do Cartão")]
        public int cvv { get; set; }
        [Required(ErrorMessage = "O campo data vencimento é obrigatório")]
        public string dataVencimento { get; set; }

        [Required]
        [RegularExpression("(.*[1-9].*)|(.*[.].*[1-9].*)", ErrorMessage = "Informe o Número do Cartão")]
        public int numeroCartao { get; set; }

        public Pedido Pedido { get; set; }
    }
}