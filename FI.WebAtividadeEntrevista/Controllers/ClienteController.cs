using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.Interfaces.BLL;
using AutoMapper;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IBoCliente _clienteBo;
        public ClienteController(IBoCliente boCliente)
        {
            _clienteBo = boCliente;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {

            
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }



            Cliente cliente = Mapper.Map<Cliente>(model);
            var erro = _clienteBo.RegrasIncluir(cliente);
            if (erro.Numero > 0)
            {
                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erro.Msg));
            }
            else
            {

                model.Id = _clienteBo.Incluir(cliente);
                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {

       
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }

            Cliente cliente = Mapper.Map<Cliente>(model);
            
            var erro = _clienteBo.RegrasIncluir(cliente);
            
            if (erro.Numero > 0)
            {
                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erro.Msg));
            }
            else
            {
                _clienteBo.Alterar(cliente);
                               
                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {

            Cliente cliente = _clienteBo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = Mapper.Map<ClienteModel>(cliente);
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = _clienteBo.Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Beneficiarios()
        {
            return PartialView();
        }

    }
}