using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.Interfaces.BLL
{
    public interface IBoCliente
    {
        long Incluir(DML.Cliente cliente);
        void Alterar(DML.Cliente cliente);
        DML.Cliente Consultar(long id);
        void Excluir(long id);
        List<DML.Cliente> Listar();
        List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd);
        bool VerificarExistencia(string CPF);
        Erro RegrasIncluir(DML.Cliente cliente);
    }
}
