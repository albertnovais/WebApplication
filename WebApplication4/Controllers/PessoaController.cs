using WebApplication4.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Collections.Generic;
using System;

namespace DesafioIATec.Controllers
{
    public class PessoaController : Controller
    {

        /// <summary>
        /// Códigos de erro 
        /// 
        /// 1: Sucesso
        /// 2: Erro
        /// 3: Outros
        /// 
        /// Mensagens tratadas nas Views
        /// 
        /// </summary>

        
        DESAFIO_IATECEntities1 bd = new DESAFIO_IATECEntities1();

       
        public ActionResult ListarPessoa(int? erro)
        {
            ViewBag.erro = erro;
            var lista = bd.Pessoa.ToList();
            return View(lista);
        }

        
        public ActionResult CriarPessoa(int? erro)
        {
            ViewBag.erro = erro;
            return View();
        }


        [HttpPost]
        // Para aceitar o código html vindo do campo Descrição
        [ValidateInput(false)]
        public ActionResult CriarPessoa(Pessoa pessoa)
        {   
            if (bd.Pessoa.FirstOrDefault(x => x.CPF == pessoa.CPF && x.Email == pessoa.Email) != null) return RedirectToAction("CriarPessoa", "Pessoa", 2);
            pessoa.AcessoId = 2;
            bd.Pessoa.Add(pessoa);
            bd.SaveChanges();
            return RedirectToAction("ListarPessoa", "Pessoa", 1);
        }
        
        public ActionResult DetalhePessoa(string cpf, int? erro)
        {
            ViewBag.erro = erro;
            var pessoa = bd.Pessoa.FirstOrDefault(x=> x.CPF == cpf);
            ViewBag.Descricao = pessoa.Descricao;
            return View(pessoa);
        }


        [HttpPost]
        public ActionResult AdicionarNumero(Telefone telefone)
        {
            if(bd.Telefone.FirstOrDefault(x=> x.Numero == telefone.Numero && x.CPF == telefone.CPF) != null ) return RedirectToAction("ListarPessoa", "Pessoa", 2);
            bd.Telefone.Add(telefone);
            bd.SaveChanges();
            return RedirectToAction("ListarPessoa", "Pessoa",1);
        }

        public ActionResult EditarRemoverNumero(Telefone telefone, bool remove)
        {
           return RedirectToAction ("DetalhePessoa", "Pessoa",1);
        }

        [HttpPost]
        public ActionResult AdicionarEndereco(Endereco endereco)
        {
            bd.Endereco.Add(endereco);
            bd.SaveChanges();
            return RedirectToAction("ListarPessoa", "Pessoa",1);
        }

        public ActionResult EditarRemoverEndereco(Telefone telefone, bool remove)
        {
            return RedirectToAction("DetalhePessoa", "Pessoa", 1);
        }

    }
}