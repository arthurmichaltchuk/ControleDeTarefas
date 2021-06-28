using ControleDeTarefas_2._0.ConsoleApp.Dominio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ControleDeTarefas_2._0.ConsoleApp.ConsoleApp;
using System;
using ControleDeTarefas_2._0.ConsoleApp.Telas;
using ControleDeTarefas_2._0.ConsoleApp.Controladores;
using System.Collections.Generic;

namespace Testes
{
    [TestClass]
    public class UnitTest1
    {
        public TelaPrincipal telaPrincipal = new TelaPrincipal();

        public static ControladorContatos controladorContatos = new ControladorContatos();
        public TelaContato telaContato = new TelaContato(controladorContatos);

        public static ControladorCompromisso controladorCompromisso = new ControladorCompromisso();
        public TelaCompromisso telaCompromisso = new TelaCompromisso(controladorCompromisso);

        [TestMethod]
        public void MetodoValidarDosContatos()
        {
            string email = "aaaa@.";
            string telefone = "999999999";
            Assert.AreEqual("VALIDO", telaContato.Validar(email, telefone));
        }

        [TestMethod]
        public void MetodoValidarDosContatosErroTelefone()
        {
            string email = "aaaa@.";
            string telefone = "999999999999999";
            Assert.AreEqual("INVALIDO", telaContato.Validar(email, telefone));
        }

        [TestMethod]
        public void MetodoValidarDosContatosErroEmail()
        {
            string email = "aaaa";
            string telefone = "99999999";
            Assert.AreEqual("INVALIDO", telaContato.Validar(email, telefone));
        }

        [TestMethod]
        public void MetodoValidarDosContatosErroAmbos()
        {
            string email = "aaaa";
            string telefone = "999999";
            Assert.AreEqual("INVALIDO", telaContato.Validar(email, telefone));
        }

        [TestMethod]
        public void TestandoInput()
        {
            string opcao = "1";
            Assert.AreEqual(false, telaPrincipal.OpcaoInvalida(opcao));
        }

        [TestMethod]
        public void MetodoValidarHoraInseridaEmConflito()
        {
            DateTime data = new DateTime(2000, 1, 1);
            List<Compromisso> compromisso = new List<Compromisso>();
            Compromisso Comp = new Compromisso("","", data, "14:00", "15:00", 0, "");

            compromisso.Add(Comp);

            string horaInicio = "14:15";
            string horaTermino = "15:15";

            Assert.AreEqual("INVALIDO1", telaCompromisso.Validar(data, horaInicio, horaTermino, compromisso));
        }

        [TestMethod]
        public void MetodoValidarHoraValida()
        {
            DateTime data = new DateTime(2000, 1, 1);
            List<Compromisso> compromisso = new List<Compromisso>();
            Compromisso Comp = new Compromisso("", "", data, "14:00", "15:00", 0, "");

            compromisso.Add(Comp);

            string horaInicio = "12:00";
            string horaTermino = "14:00";

            Assert.AreEqual("VALIDO", telaCompromisso.Validar(data, horaInicio, horaTermino, compromisso));
        }

        [TestMethod]
        public void MetodoValidarHoraExistenteEmConflito()
        {
            DateTime data = new DateTime(2000, 1, 1);
            List<Compromisso> compromisso = new List<Compromisso>();
            Compromisso Comp = new Compromisso("", "", data, "14:00", "15:00", 0, "");

            compromisso.Add(Comp);

            string horaInicio = "12:00";
            string horaTermino = "14:01";

            Assert.AreEqual("INVALIDO2", telaCompromisso.Validar(data, horaInicio, horaTermino, compromisso));
        }

        [TestMethod]
        public void MetodoValidarHoraFormatoIncorreto()
        {
            DateTime data = new DateTime(2000, 1, 1);
            List<Compromisso> compromisso = new List<Compromisso>();
            Compromisso Comp = new Compromisso("", "", data, "14:00", "15:00", 0, "");

            compromisso.Add(Comp);

            string horaInicio = "1200";
            string horaTermino = "1401";

            Assert.AreEqual("INVALIDO0", telaCompromisso.Validar(data, horaInicio, horaTermino, compromisso));
        }
    }
}
