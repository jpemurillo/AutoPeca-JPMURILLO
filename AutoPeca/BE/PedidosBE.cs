using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.BE
{
    public class PedidosBE
    {
        private VO.Pedidos ped;
        private DAO.PedidosDAO dao;

        public PedidosBE(VO.Pedidos ped)
        {
            this.ped = ped;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.ped.datetime))
            {
                throw new Exception("Data do veículo Obrigatoria");
            }

            dao = new DAO.PedidosDAO(this.ped);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.PedidosDAO(this.ped);
            dao.alterar();
        }
        public VO.Pedidos carregar(int id)
        {
            dao = new DAO.PedidosDAO(this.ped);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.PedidosDAO(this.ped);
            dao.remover(id);
        }

        public List<VO.Pedidos> listar()
        {
            dao = new DAO.PedidosDAO(this.ped);
            return dao.listar();
        }
    }
}

