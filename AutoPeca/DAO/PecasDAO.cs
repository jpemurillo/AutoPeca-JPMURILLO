using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AutoPeca.DAO.DataAccess;

namespace AutoPeca.DAO
{
    public class PecasDAO : BaseDAO
    {
      private VO.Pecas vo;
      public PecasDAO(VO.Pecas vo)
      {

            if (DAO.listpecas == null)
            {
                DAO.listpecas = new List<VO.Pecas>();
            }
            this.vo = vo;
      }

            public void incluir()
            {
                try
                {
                    string sql = "insert into tb_pecas (VL_CODIGO,NM_DESC,NM_CODBARRAS) " +
                        "values (@cod,@desc,@codbarras)";
                    db.AddParameter("@cod", vo.Codigo, ParameterDirection.Input);
                    db.AddParameter("@desc", vo.Descricao, ParameterDirection.Input);
                    db.AddParameter("@codbarras", vo.CodigoBarras, ParameterDirection.Input);

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
                    string sql = "update tb_pecas set " +
                        "VL_CODIGO = @cod," +
                        "NM_DESC = @desc " +
                        "NM_CODBARRAS = @codbarras" +
                        "where ID = @id";
                    db.AddParameter("@cod", vo.Codigo, ParameterDirection.Input);
                    db.AddParameter("@desc", vo.Descricao, ParameterDirection.Input);
                    db.AddParameter("@codbarras", vo.CodigoBarras, ParameterDirection.Input);

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
                    string sql = $"delete from tb_pecas where ID = @id";
                    db.AddParameter("@id", id, ParameterDirection.Input);
                    db.Execute(sql, CommandType.Text);

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro no Código : " + ex.Message);
                }
            }

            public VO.Pecas carregar(int id)
            {
                string sql = $"SELECT id,vl_codigo,nm_desc,nm_codbarras from tb_pecas where id=@id";
                db.AddParameter("@id", id, ParameterDirection.Input);
                try
                {
                    using (var dr = db.ExecuteReader(sql, CommandType.Text))
                    {
                        while (dr.Read())
                        {
                            vo = LoadPecas(dr);
                        }
                        return vo;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro no Código : " + ex.Message);
                }
            }

            private VO.Pecas LoadPecas(DbDataReader dr)
            {
                vo = new VO.Pecas();
                vo.Id = Convert.ToInt32(dr["ID"]);
                vo.Codigo = dr["vl_codigo"] != DBNull.Value ? int.Parse(dr["vl_codigo"].ToString()) : 0;
                vo.Descricao = dr["nm_desc"] != DBNull.Value ? dr["nm_desc"].ToString() : "";
                vo.CodigoBarras = dr["nm_codbarras"] != DBNull.Value ? dr["nm_codbarras"].ToString() : "";
                return vo;
            }

            public List<VO.Pecas> listar()
            {
                try
                {
                    string sql = "SELECT * FROM tb_pecas;";
                    using (var dr = db.ExecuteReader(sql, CommandType.Text))
                    {
                        var objResultado = new List<VO.Pecas>();

                        while (dr.Read())
                        {
                            vo = LoadPecas(dr);
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
