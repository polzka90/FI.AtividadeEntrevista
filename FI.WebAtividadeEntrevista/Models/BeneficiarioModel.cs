using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public long IdCliente { get; set; }
    }
}