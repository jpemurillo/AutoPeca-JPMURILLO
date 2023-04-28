using System;
using System.Collections.Generic;
using System.Text;

namespace AutoPeca.BE
{
    public class ClientesBE
    {
        private VO.Clientes cli;
        private DAO.ClienteDAO dao;

        public ClientesBE(VO.Clientes cli)
        {
            this.cli = cli;
        }

        public void incluir()
        {
            if (string.IsNullOrEmpty(this.cli.cpf))
            {
                throw new Exception("CPF do Usuário Obrigatorio");
            }

            dao = new DAO.ClienteDAO(this.cli);
            dao.incluir();
        }
        public void alterar()
        {
            dao = new DAO.ClienteDAO(this.cli);
            dao.alterar();
        }
        public VO.Clientes carregar(int id)
        {
            dao = new DAO.ClienteDAO(this.cli);
            return dao.carregar(id);
        }
        public void remover(int id)
        {
            dao = new DAO.ClienteDAO(this.cli);
            dao.remover(id);
        }

        public List<VO.Clientes> listar()
        {
            dao = new DAO.ClienteDAO(this.cli);
            return dao.listar();
        }
    }
}
