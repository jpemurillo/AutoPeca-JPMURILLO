using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.BE
{
    public class PecasBE
    {
        private VO.Pecas pec;
        private DAO.PecasDAO dao;

        public PecasBE(VO.Pecas pec)
        {
            this.pec = pec;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.pec.Descricao))
            {
                throw new Exception("Descrição da Peça Obrigatoria");
            }

            dao = new DAO.PecasDAO(this.pec);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.PecasDAO(this.pec);
            dao.alterar();
        }
        public VO.Pecas carregar(int id)
        {
            dao = new DAO.PecasDAO(this.pec);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.PecasDAO(this.pec);
            dao.remover(id);
        }

        public List<VO.Pecas> listar()
        {
            dao = new DAO.PecasDAO(this.pec);
            return dao.listar();
        }
    }
}
