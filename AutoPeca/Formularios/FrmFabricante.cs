using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoPeca.Formularios
{
    public partial class FrmFabricante : Form
    {

        private VO.Fabricante fab;
        private List<VO.Fabricante> lista;
        private BE.FabricanteBE be;

        public FrmFabricante()
        {
            InitializeComponent();
            fab = new VO.Fabricante();
            lista = new List<VO.Fabricante>();
            liberarEdicao(false);
            InicializarVeiculos();
            carregar();
        }
        private void InicializarVeiculos()
        {
            fab = new VO.Fabricante();
            if (DAO.DAO.listaFabricante == null)
            {
                DAO.DAO.listaFabricante = new List<VO.Fabricante>();
            }
            lista = DAO.DAO.listaFabricante;
        }

        private void txtnome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcod_TextChanged(object sender, EventArgs e)
        {

        }

        private void InteractToObject()
        {

            fab.codigo = int.Parse(txtcod.Text);
            fab.nome = txtnome.Text;
            fab.descricao = txtdesc.Text;


        }
        private void limpar1()
        {
            txtcod.Text = "";
            txtnome.Text = "";
            txtdesc.Text = "";
        }
        private void carregar()
        {
            be = new BE.FabricanteBE(this.fab);
            lstfabricante.DataSource = null;
            lstfabricante.DataSource = be.listar();
            lstfabricante.ValueMember = "codigo";
            lstfabricante.DisplayMember = "nome";
            lstfabricante.Refresh();
        }

        private void btnlimpar2_Click(object sender, EventArgs e)
        {
            limpar1();
            liberarEdicao(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                fab = new VO.Fabricante();
                InteractToObject();
                be = new BE.FabricanteBE(this.fab);
                be.incluir();
                limpar1();
                carregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro No Aplicativo");

            }
        }

        private void btnselecionar_Click(object sender, EventArgs e)
        {
            be = new BE.FabricanteBE(this.fab);
            fab = be.carregar(int.Parse(lstfabricante.SelectedValue.ToString()));
            txtcod.Text = fab.codigo.ToString();
            txtnome.Text = fab.nome.ToString();
            txtdesc.Text = fab.descricao.ToString();
            liberarEdicao(true);
        } 
        private void liberarEdicao(bool habilita)
        {
            button1.Enabled = !habilita;
            btneditar.Enabled = habilita;
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            InteractToObject();
            be = new BE.FabricanteBE(this.fab);
            be.alterar();
            carregar();
        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            be = new BE.FabricanteBE(this.fab);
            be.remover(int.Parse(lstfabricante.SelectedValue.ToString()));
            carregar();
        }
    }

}

