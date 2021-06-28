using ControleDeTarefas_2._0.ConsoleApp.Dominio;
using ControleDeTarefas_2._0.ConsoleApp.Telas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeTarefas_2._0.ConsoleApp.Controladores
{
    public class ControladorCompromisso : Controlador<Compromisso>
    {
        public string enderecoDBCompromisso = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=DBTarefas;Integrated Security=True;Pooling=False";
        public List<Compromisso> ObterTodosCompromissos()
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBCompromisso;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
		                [ASSUNTO],
		                [LOCAL],
		                [DATA],
		                [HORAINICIO],
		                [HORATERMINO],
                        [IDCONTATO],
                        [LINK]
                    FROM 
                        TBCOMPROMISSO";

            comandoSelecao.CommandText = sqlSelecao;

            SqlDataReader leitorCompromisso = comandoSelecao.ExecuteReader();

            List<Compromisso> compromisso = new List<Compromisso>();

            while (leitorCompromisso.Read())
            {
                int id = Convert.ToInt32(leitorCompromisso["ID"]);
                string assunto = Convert.ToString(leitorCompromisso["ASSUNTO"]);
                string local = Convert.ToString(leitorCompromisso["LOCAL"]);
                DateTime data = Convert.ToDateTime(leitorCompromisso["DATA"]);
                string horaInicio = Convert.ToString(leitorCompromisso["HORAINICIO"]);
                string horaTermino = Convert.ToString(leitorCompromisso["HORATERMINO"]);
                int idContato = Convert.ToInt32(leitorCompromisso["IDCONTATO"]);
                string link = Convert.ToString(leitorCompromisso["LINK"]);

                Compromisso Comp = new Compromisso(assunto, local, data, horaInicio, horaTermino, idContato, link);
                Comp.Id = id;

                compromisso.Add(Comp);
            }

            conexaoComBanco.Close();

            return compromisso;
        }
        public override string EditarRegistro(int idSelecionado, Compromisso registro)
        {
            string sqlAtualizacao =
                @"UPDATE TBCOMPROMISSO 
	                SET	
		                [ASSUNTO] = @ASSUNTO,
		                [LOCAL] = @LOCAL,
		                [DATA] = @DATA,
		                [HORAINICIO] = @HORAINICIO,
		                [HORATERMINO] = @HORATERMINO,
                        [LINK] = @LINK
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

        public override string ExcluirRegistro(Compromisso registro)
        {
            string sqlExclusao =
                @"DELETE FROM TBCOMPROMISSO 	                
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

        public override string InserirNovo(Compromisso registro)
        {
            string sqlInsercao =
            @"INSERT INTO TBCOMPROMISSO
                (                
		        [ASSUNTO],
		        [LOCAL],
		        [DATA],
		        [HORAINICIO],
		        [HORATERMINO],
                [IDCONTATO],
                [LINK]
                )
            VALUES
                (
                @ASSUNTO,
                @LOCAL,
                @DATA,
                @HORAINICIO,
                @HORATERMINO,
                @IDCONTATO,
                @LINK
                );";

            try
            {
                ExecutarCodigo(sqlInsercao, registro, TipoExecucao.Inserindo, 0);
                return "Registrado com sucesso!";
            }
            catch
            {
                return "Erro ao registrar!";
            }
        }

        public override Compromisso SelecionarRegistroPorId(int idPesquisado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBCompromisso;
            conexaoComBanco.Open();

            SqlCommand comandoSelecao = new SqlCommand();
            comandoSelecao.Connection = conexaoComBanco;

            string sqlSelecao =
                @"SELECT 
                        [ID],
		                [ASSUNTO],
		                [LOCAL],
		                [DATA],
		                [HORAINICIO],
		                [HORATERMINO],
                        [IDCONTATO],
                        [LINK]
                    FROM 
                        TBCOMPROMISSO
                    WHERE 
                        ID = @ID";

            comandoSelecao.CommandText = sqlSelecao;
            comandoSelecao.Parameters.AddWithValue("ID", idPesquisado);

            SqlDataReader leitorCompromisso = comandoSelecao.ExecuteReader();

            if (leitorCompromisso.Read() == false)
                return null;

            int id = Convert.ToInt32(leitorCompromisso["ID"]);
            string assunto = Convert.ToString(leitorCompromisso["ASSUNTO"]);
            string local = Convert.ToString(leitorCompromisso["LOCAL"]);
            DateTime data = Convert.ToDateTime(leitorCompromisso["DATA"]);
            string horaInicio = Convert.ToString(leitorCompromisso["HORAINICIO"]);
            string horaTermino = Convert.ToString(leitorCompromisso["HORATERMINO"]);
            int idContato = Convert.ToInt32(leitorCompromisso["IDCONTATO"]);
            string link = Convert.ToString(leitorCompromisso["LINK"]);

            Compromisso Comp = new Compromisso(assunto, local, data, horaInicio, horaTermino, idContato, link);
            Comp.Id = id;

            conexaoComBanco.Close();

            return Comp;
        }

        private void ExecutarCodigo(string comando, Compromisso registro, TipoExecucao tipoExecucao, int idSelecionado)
        {
            SqlConnection conexaoComBanco = new SqlConnection();
            conexaoComBanco.ConnectionString = enderecoDBCompromisso;
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
                comandoExecucao.Parameters.AddWithValue("ASSUNTO", registro.Assunto);
                comandoExecucao.Parameters.AddWithValue("LOCAL", registro.Local);
                comandoExecucao.Parameters.AddWithValue("DATA", registro.Data);
                comandoExecucao.Parameters.AddWithValue("HORAINICIO", registro.HoraInicio);
                comandoExecucao.Parameters.AddWithValue("HORATERMINO", registro.HoraTermino);
                comandoExecucao.Parameters.AddWithValue("LINK", registro.Link);

                comandoExecucao.ExecuteNonQuery();
            }
            else if (tipoExecucao == TipoExecucao.Inserindo)
            {
                comando +=
                @"SELECT SCOPE_IDENTITY();";

                comandoExecucao.CommandText = comando;
                comandoExecucao.Parameters.AddWithValue("ASSUNTO", registro.Assunto);
                comandoExecucao.Parameters.AddWithValue("LOCAL", registro.Local);
                comandoExecucao.Parameters.AddWithValue("DATA", registro.Data);
                comandoExecucao.Parameters.AddWithValue("HORAINICIO", registro.HoraInicio);
                comandoExecucao.Parameters.AddWithValue("HORATERMINO", registro.HoraTermino);
                comandoExecucao.Parameters.AddWithValue("IDCONTATO", registro.IdContato);
                comandoExecucao.Parameters.AddWithValue("LINK", registro.Link);

                object id = comandoExecucao.ExecuteScalar();

                registro.Id = Convert.ToInt32(id);
            }
            conexaoComBanco.Close();
        }
    }
}
