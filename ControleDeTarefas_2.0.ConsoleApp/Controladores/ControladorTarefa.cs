using ControleDeTarefas_2._0.ConsoleApp.ConsoleApp;
using ControleDeTarefas_2._0.ConsoleApp.Telas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.Controladores
{
    public class ControladorTarefa : Controlador<Tarefa>
    {
        public string enderecoDBTarefas = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";
        public List<Tarefa> ObterTodasTarefas()
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                [ID],
                [TITULO],
                [PRIORIDADE],
                [DATACRIACAO],
                [DATACONCLUSAO],
                [PERCENTUALCONCLUIDO]
                    FROM 
                        TBTAREFAS";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                string nome = Convert.ToString(leitorTarefas["TITULO"]);
                int prioridade = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                int percentualConcluido = Convert.ToInt32(leitorTarefas["PERCENTUALCONCLUIDO"]);

                Tarefa Ta = new Tarefa(nome, prioridade, dataCriacao, dataConclusao, percentualConcluido);
                Ta.Id = id;

                tarefas.Add(Ta);
            }

            conexaoComBanco.Close();

            return tarefas;
        }

        public override string InserirNovo(Tarefa tarefa)
        {
            string sqlInsercao =
            @"INSERT INTO TBTAREFAS
                (                
                [TITULO],
                [PRIORIDADE],
                [DATACRIACAO],
                [DATACONCLUSAO],
                [PERCENTUALCONCLUIDO]
                )
            VALUES
                (
                @TITULO,
                @PRIORIDADE,
                @DATACRIACAO,
                @DATACONCLUSAO,
                @PERCENTUALCONCLUIDO
                );";

            try
            {
                ExecutarCodigo(sqlInsercao, tarefa, TipoExecucao.Inserindo, 0);
                return "Registrado com sucesso!";
            }
            catch
            {
                return "Erro ao registrar!";
            }
        }

        public override Tarefa SelecionarRegistroPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
                        [TITULO],
                        [PRIORIDADE],
                        [DATACRIACAO],
                        [DATACONCLUSAO],
                        [PERCENTUALCONCLUIDO]
                    FROM 
                        TBTAREFAS
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            if (leitorTarefas.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorTarefas["ID"]);
            string nome = Convert.ToString(leitorTarefas["TITULO"]);
            int prioridade = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);
            DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);
            DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
            int percentualConcluido = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);

            Tarefa Ta = new Tarefa(nome, prioridade, dataCriacao, dataConclusao, percentualConcluido);
            Ta.Id = id;

            conexaoComBanco.Close();

            return Ta;
        }

        public override string EditarRegistro(int idSelecionado, Tarefa registro)
        {
            string sqlAtualizacao =
                @"UPDATE TBTAREFAS 
	                SET	
                        [TITULO] = @TITULO,
                        [PRIORIDADE] = @PRIORIDADE,
                        [DATACRIACAO] = @DATACRIACAO,
                        [PERCENTUALCONCLUIDO] = @PERCENTUALCONCLUIDO
	                WHERE 
		                [ID] = @ID";
            try
            {
                ExecutarCodigo(sqlAtualizacao, registro, TipoExecucao.Editando, idSelecionado);
                return "Atualizado com sucesso!";
            }
            catch
            {
                return "Erro ao atualizar!";
            }
        }

        public override string ExcluirRegistro(Tarefa registro)
        {
            string sqlExclusao =
                @"DELETE FROM TBTAREFAS 	                
	                WHERE 
		                [ID] = @ID";
            try
            {
                ExecutarCodigo(sqlExclusao, registro, TipoExecucao.Excluindo, 0);
                return "Excluído com sucesso!";
            }
            catch
            {
                return "Erro ao Excluir";
            }
        }

        private void ExecutarCodigo(string comando, Tarefa registro, TipoExecucao tipoExecucao, int idSelecionado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBTarefas;
            conexaoComBanco.Open();

            SqlCommand comandoExecucao = new SqlCommand();
            comandoExecucao.Connection = conexaoComBanco;

            if (tipoExecucao == TipoExecucao.Excluindo)
            {
                comandoExecucao.CommandText = comando;

                comandoExecucao.Parameters.AddWithValue("ID", registro.Id);

                comandoExecucao.ExecuteNonQuery();
            }
            else if (tipoExecucao == TipoExecucao.Editando)
            {
                comandoExecucao.CommandText = comando;

                comandoExecucao.Parameters.AddWithValue("ID", idSelecionado);
                comandoExecucao.Parameters.AddWithValue("TITULO", registro.Titulo);
                comandoExecucao.Parameters.AddWithValue("PRIORIDADE", registro.Prioridade);
                comandoExecucao.Parameters.AddWithValue("DATACRIACAO", registro.DataCriacao);
                comandoExecucao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", registro.PercentualConcluido);

                comandoExecucao.ExecuteNonQuery();
            }
            else if (tipoExecucao == TipoExecucao.Inserindo)
            {
                comando +=
                @"SELECT SCOPE_IDENTITY();";

                comandoExecucao.CommandText = comando;
                comandoExecucao.Parameters.AddWithValue("TITULO", registro.Titulo);
                comandoExecucao.Parameters.AddWithValue("PRIORIDADE", registro.Prioridade);
                comandoExecucao.Parameters.AddWithValue("DATACRIACAO", registro.DataCriacao);
                comandoExecucao.Parameters.AddWithValue("DATACONCLUSAO", registro.DataConclusao);
                comandoExecucao.Parameters.AddWithValue("PERCENTUALCONCLUIDO", registro.PercentualConcluido);

                object id = comandoExecucao.ExecuteScalar();

                registro.Id = Convert.ToInt32(id);
            }
            conexaoComBanco.Close();
        }
    }
}
