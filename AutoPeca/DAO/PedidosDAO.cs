using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class PedidosDAO : BaseDAO
    {
        private VO.Pedidos vo;
        public PedidosDAO(VO.Pedidos vo)
        {

            if (DAO.listpedido == null)
            {
                DAO.listpedido = new List<VO.Pedidos>();
            }
            this.vo = vo;
        }

        public void incluir()
        {
            try
            {
                string sql = "insert into tb_pedidos (ID_CLIENTE,ID_PECAS,NM_DATA) " +
                    "values (@cliente,@pecas,@data)";
                db.AddParameter("@data", vo.datetime, ParameterDirection.Input);
                db.AddParameter("@cliente", vo.codigodocliente.codigo, ParameterDirection.Input);
                db.AddParameter("@pecas", vo.codigodapeca.Id, ParameterDirection.Input);

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
                string sql = "update tb_pedidos set " +
                    "ID_CLIENTE = @cliente," +
                    "ID_PECAS = @pecas " +
                    "NM_DATA = @data" +
                    "where ID = @id";
                db.AddParameter("@cod", vo.codigo, ParameterDirection.Input);
                db.AddParameter("@cliente", vo.codigodocliente.codigo, ParameterDirection.Input);
                db.AddParameter("@pecas", vo.codigodapeca.Id, ParameterDirection.Input);
                db.AddParameter("@nm_data", vo.datetime, ParameterDirection.Input);

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
                string sql = $"delete from tb_pedidos where ID = @id";
                db.AddParameter("@id", id, ParameterDirection.Input);
                db.Execute(sql, CommandType.Text);

            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        public VO.Pedidos carregar(int id)
        {
            string sql = $"SELECT id,ID_CLIENTE,ID_PECAS,NM_DATA from tb_pedidos where id=@id";
            db.AddParameter("@id", id, ParameterDirection.Input);
            try
            {
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    while (dr.Read())
                    {
                      vo = LoadPedidos(dr);
                    }
                    return vo;
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro no Código : " + ex.Message);
            }
        }

        private VO.Pedidos LoadPedidos(DbDataReader dr)
        {

            vo = new VO.Pedidos();
            vo.codigo = Convert.ToInt32(dr["ID"]);
            vo.codigodocliente = new VO.Clientes();
            vo.codigodocliente.codigo = Convert.ToInt32(dr["ID_CLIENTE"]); 
            vo.codigodapeca = new VO.Pecas();
            vo.codigodapeca.Id = Convert.ToInt32(dr["ID_PECAS"]);
            vo.datetime = dr["nm_data"] != DBNull.Value ? dr["nm_data"].ToString() : "";
            return vo;
        }

        public List<VO.Pedidos> listar()
        {
            try
            {
                string sql = "SELECT * FROM tb_pedidos;";
                using (var dr = db.ExecuteReader(sql, CommandType.Text))
                {
                    var objResultado = new List<VO.Pedidos>();

                    while (dr.Read())
                    {
                        vo = LoadPedidos(dr);
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
