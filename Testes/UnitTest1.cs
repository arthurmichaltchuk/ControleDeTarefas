using ControleDeTarefas_2._0.ConsoleApp.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleDeTarefas_2._0.ConsoleApp.ConsoleApp;
using System;

namespace Testes
{
    [TestClass]
    public class UnitTest1
    {
        public Contatos contatos = new Contatos("","","","","");
        public TelaPrincipal telaPrincipal = new TelaPrincipal();

        [TestMethod]
        public void MetodoValidarDosContatos()
        {
            string email = "aaaa@.";
            string telefone = "999999999";
            Assert.AreEqual("VALIDO", contatos.Validar(email, telefone));
        }

        [TestMethod]
        public void MetodoValidarDosContatosErroTelefone()
        {
            string email = "aaaa@.";
            string telefone = "999999999999999";
            Assert.AreEqual("INVALIDO", contatos.Validar(email, telefone));
        }

        [TestMethod]
        public void MetodoValidarDosContatosErroEmail()
        {
            string email = "aaaa";
            string telefone = "99999999";
            Assert.AreEqual("INVALIDO", contatos.Validar(email, telefone));
        }

        [TestMethod]
        public void MetodoValidarDosContatosErroAmbos()
        {
            string email = "aaaa";
            string telefone = "999999";
            Assert.AreEqual("INVALIDO", contatos.Validar(email, telefone));
        }

        [TestMethod]
        public void TestandoInput()
        {
            string opcao = "1";
            Assert.AreEqual(false, telaPrincipal.OpcaoInvalida(opcao));
        }
    }
}
