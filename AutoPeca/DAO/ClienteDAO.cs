using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class ClienteDAO : BaseDAO
    {
        private VO.Clientes vo;
        public ClienteDAO(VO.Clientes vo)
        {

            if (DAO.listcliente == null)
            {
                DAO.listcliente = new List<VO.Clientes>();
            }
            this.vo = vo;
        }

        public void incluir()
        {
            try
            {
                string sql = "insert into tb_clientes (NM_NOME,NM_CPF,NM_ENDERECO,VL_NUM,NM_CIDADE,NM_ESTADO,NM_PAIS) " +
                    "values (@nome,@cpf,@endereco,@num,@cidade,@estado,@pais)";
                db.AddParameter("@nome", vo.nome, ParameterDirection.Input);
                db.AddParameter("@cpf", vo.cpf, ParameterDirection.Input);
                db.AddParameter("@endereco", vo.end, ParameterDirection.Input);
                db.AddParameter("@num", vo.num, ParameterDirection.Input);
                db.AddParameter("@cidade", vo.cidade, ParameterDirection.Input);
                db.AddParameter("@estado", vo.estado, ParameterDirection.Input);
                db.AddParameter("@pais", vo.pais, ParameterDirection.Input);

                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
        public void alterar()
        {
            try
            {
                string sql = "update tb_clientes set " +
                        "NM_NOME = @nome " +
                        "NM_CPF = @cpf " +
                        "NM_ENDERECO = @endereco " +
                        "NM_NUMERO = @num " +
                        "NM_CIDADE = @cidade " +
                        "NM_ESTADO = @estado " +
                        "NM_PAIS @pais " +
                        "where ID = @cod";
                db.AddParameter("@nome", vo.nome, ParameterDirection.Input);
                db.AddParameter("@cpf", vo.cpf, ParameterDirection.Input);
                db.AddParameter("@endereco", vo.end, ParameterDirection.Input);
                db.AddParameter("@num", vo.num, ParameterDirection.Input);
                db.AddParameter("@cidade", vo.cidade, ParameterDirection.Input);
                db.AddParameter("@estado", vo.estado, ParameterDirection.Input);
                db.AddParameter("@pais", vo.pais, ParameterDirection.Input);

                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
        public void remover(int id)
        {
            try
            {
                string sql = $"delete from tb_clientes where ID = @id";
                db.AddParameter("@id", id, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        public VO.Clientes carregar(int id)
        {
            string sql = $"SELECT id,NM_NOME,NM_CPF,NM_ENDERECO,VL_NUM,NM_CIDADE,NM_ESTADO,NM_PAIS from tb_clientes where id=@id";
            db.AddParameter("@id", id, ParameterDirection.Input);
            try
            {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                        vo = LoadClientes(dr);
                    }
                    return vo;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Clientes LoadClientes(DbDataReader dr)
        {
            vo = new VO.Clientes();
            vo.codigo = Convert.ToInt32(dr["ID"]);
            vo.nome = dr["NM_NOME"] != DBNull.Value ? dr["NM_NOME"].ToString() : "";
            vo.cpf = dr["NM_CPF"] != DBNull.Value ? dr["NM_CPF"].ToString() : "";
            vo.end = dr["NM_ENDERECO"] != DBNull.Value ? dr["NM_ENDERECO"].ToString() : "";
            vo.num = dr["VL_NUM"] != DBNull.Value ? dr["VL_NUM"].ToString() : "";
            vo.cidade = dr["NM_CIDADE"] != DBNull.Value ? dr["NM_CIDADE"].ToString() : "";
            vo.estado = dr["NM_ESTADO"] != DBNull.Value ? dr["NM_ESTADO"].ToString() : "";
            vo.pais = dr["NM_PAIS"] != DBNull.Value ? dr["NM_PAIS"].ToString() : "";
            return vo;
        }

        public List<VO.Clientes> listar()
        {
            try
            {
                string sql = "SELECT * FROM tb_clientes;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Clientes>();

                    while (dr.Read())
                    {
                        vo = LoadClientes(dr);
                        objResultado.Add(vo);
                    }
                    return objResultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }
    }
}
