using System;
using System.Collections.Generic;
using System.Text;
namespace AutoPeca.BE
{
    class FabricanteBE : BaseBE
    {
        private VO.Fabricante fab;
        private DAO.FabricanteDAO dao;

        public FabricanteBE(VO.Fabricante fab)
        {
            this.fab = fab;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.fab.nome))
            {
                throw new Exception("Nome do Fabricante Obrigatorio");
            }

            dao = new DAO.FabricanteDAO(this.fab);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.FabricanteDAO(this.fab);
            dao.alterar();
        }
        public VO.Fabricante carregar(int id)
        {
            dao = new DAO.FabricanteDAO(this.fab);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.FabricanteDAO(this.fab);
            dao.remover(id);
        }

        public List<VO.Fabricante> listar()
        {
            dao = new DAO.FabricanteDAO(this.fab);
            return dao.listar();
        }
    }
}
