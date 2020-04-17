using FI.AtividadeEntrevista.DAL.Clientes;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Interfaces.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoCliente : IBoCliente
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cliente.CPF = cliente.CPF.Replace(".", "").Replace("-", "");

            long id = cli.Incluir(cliente);

            if (cliente.Beneficiarios.Count() > 0)
            {
                cliente.Beneficiarios.ForEach(i => i.IdCliente = id);
                cliente.Beneficiarios.ForEach(i => i.CPF = i.CPF.Replace(".", "").Replace("-", ""));
            }
            IncluirBeneficiarios(cliente.Beneficiarios);
            return id;
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(DML.Cliente cliente)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cliente.CPF = cliente.CPF.Replace(".", "").Replace("-", "");


            cli.Alterar(cliente);

            if (cliente.Beneficiarios.Count() > 0)
            {
                cliente.Beneficiarios.ForEach(i => i.IdCliente = cliente.Id);
                cliente.Beneficiarios.ForEach(i => i.CPF = i.CPF.Replace(".", "").Replace("-", ""));
                IncluirBeneficiarios(cliente.Beneficiarios);
            }
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public DML.Cliente Consultar(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            DaoBeneficiario daoBeneficiario = new DaoBeneficiario();
            Cliente c = cli.Consultar(id);

            c.Beneficiarios = daoBeneficiario.Consultar(c.Id);

            c.CPF = Convert.ToUInt64(c.CPF).ToString(@"000\.000\.000\-00");
            c.Beneficiarios.ForEach(i => i.CPF = Convert.ToUInt64(i.CPF).ToString(@"000\.000\.000\-00"));
            return c;
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            cli.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Listar()
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoCliente cli = new DAL.DaoCliente();
            return cli.VerificarExistencia(CPF);
        }

        public Erro RegrasIncluir(DML.Cliente cliente)
        {
            Erro erro = new Erro()
            {
                Numero = 0,
                Msg = ""
            };

            if (!CPFvalido(cliente.CPF))
            {
                erro = new Erro() { Msg = "CPF Invalido", Numero = 1 };
                return erro;
            }

            if (VerificarExistencia(cliente.CPF))
            {
                erro = new Erro() { Msg = "CPF Ja Existe", Numero = 2 };
                return erro;
            }

            foreach(Beneficiario be in cliente.Beneficiarios)
            {
                if (!CPFvalido(be.CPF))
                {
                    erro = new Erro() { Msg = "CPF Invalido em Beneficiario " + be.Nome, Numero = 3 };
                    return erro;
                }

                if (cliente.Beneficiarios.Where(b => b.CPF == be.CPF).Count() > 1)
                {
                    erro = new Erro() { Msg = "CPF de beneficiario " + be.CPF + " cadastrado duas vezes", Numero = 4 };
                    return erro;
                }
            }

            return erro;
        }

        private bool CPFvalido(string cpf)
        {

            cpf = cpf.Replace(".", "").Replace("-", "");

            double n;
            bool isNumeric = double.TryParse(cpf, out n);

            if (string.IsNullOrEmpty(cpf) || string.IsNullOrWhiteSpace(cpf) || !isNumeric)
            {
                return false;
            }

            int num1, num2, num3, num4, num5, num6, num7, num8, num9, num10, num11, soma1, soma2;
            float resto1, resto2;



            num1 = Convert.ToInt32(cpf.Substring(0, 1));
            num2 = Convert.ToInt32(cpf.Substring(1, 1));
            num3 = Convert.ToInt32(cpf.Substring(2, 1));
            num4 = Convert.ToInt32(cpf.Substring(3, 1));
            num5 = Convert.ToInt32(cpf.Substring(4, 1));
            num6 = Convert.ToInt32(cpf.Substring(5, 1));
            num7 = Convert.ToInt32(cpf.Substring(6, 1));
            num8 = Convert.ToInt32(cpf.Substring(7, 1));
            num9 = Convert.ToInt32(cpf.Substring(8, 1));
            num10 = Convert.ToInt32(cpf.Substring(9, 1));
            num11 = Convert.ToInt32(cpf.Substring(10, 1));


            if ((num1 == num2) && (num2 == num3) && (num3 == num4) && (num4 == num5) && (num5 == num6) && (num6 == num7) && (num7 == num8) && (num8 == num9) && (num9 == num10) && (num10 == num11))
            { 
                return false;
            }

            soma1 = num1 * 10 + num2 * 9 + num3 * 8 + num4 * 7 + num5 * 6 + num6 * 5 + num7 * 4 + num8 * 3 + num9 * 2;
            resto1 = (soma1 * 10) % 11;

            if (resto1 == 10)
                resto1 = 0;


            soma2 = num1 * 11 + num2 * 10 + num3 * 9 + num4 * 8 + num5 * 7 + num6 * 6 + num7 * 5 + num8 * 4 + num9 * 3 + num10 * 2;

            resto2 = (soma2 * 10) % 11;

            if (resto2 == 10)
                resto2 = 0;

            if ((resto1 == num10) && (resto2 == num11))
                return true;
            return false;
        }

        private void IncluirBeneficiarios(List<Beneficiario> beneficiarios)
        {
            DaoBeneficiario daoBe = new DaoBeneficiario();

            daoBe.Excluir(beneficiarios.FirstOrDefault().IdCliente);

            foreach (Beneficiario be in beneficiarios)
            {
                daoBe.Incluir(be);
            }
        }
    }
}
