using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoPeca.Formularios
{
    public partial class Pedido : Form
    {
        private VO.Pedidos ped;
        private List<VO.Pedidos> lista;
        private BE.PedidosBE be;
        public Pedido()
        {
            InitializeComponent();
            ped = new VO.Pedidos();
            lista = new List<VO.Pedidos>();
            liberarEdicao(false);
            carregar();
            carregarpecas();
            carregarcliente();
        }
        private void carregarcliente()
        {
            BE.ClientesBE cli = new BE.ClientesBE(new VO.Clientes());
            cmbcliente.DataSource = null;
            cmbcliente.DataSource = cli.listar();
            cmbcliente.ValueMember = "codigo";
            cmbcliente.DisplayMember = "codigo";
            cmbcliente.Refresh();
        }
        private void carregarpecas()
        {
            BE.PecasBE pec = new BE.PecasBE (new VO.Pecas());
            cmbpecas.DataSource = null;
            cmbpecas.DataSource = pec.listar();
            cmbpecas.ValueMember = "Id";
            cmbpecas.DisplayMember = "Id";
            cmbpecas.Refresh();
        }

        private void carregar()
        {
            be = new BE.PedidosBE(this.ped);
            listpedido.DataSource = null;
            listpedido.DataSource = be.listar();
            listpedido.ValueMember = "codigo";
            listpedido.DisplayMember = "codigo";
            listpedido.Refresh();
        }

        private void limpar()
        {
            txtcod.Text = "";
            cmbcliente.SelectedIndex = -1;
            cmbpecas.SelectedIndex = -1;
            txtdatetime.Text = "";
        }

        private void InteractToObject()
        {

            ped.codigo = int.Parse(txtcod.Text);
            ped.codigodapeca = (VO.Pecas)cmbpecas.SelectedItem;
            ped.codigodocliente = (VO.Clientes)cmbcliente.SelectedItem;
            ped.datetime = txtdatetime.Text;
        }
        private void liberarEdicao(bool habilita)
        {
            btnsalvar.Enabled = !habilita;
            btneditar.Enabled = habilita;
        }

        private void Pedido_Load(object sender, EventArgs e)
        {

        }

        private void btnsalvar_Click(object sender, EventArgs e)
        {
            try
            {
                ped = new VO.Pedidos();
                InteractToObject();
                be = new BE.PedidosBE(this.ped);
                be.incluir();
                limpar();
                carregar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro No Aplicativo");

            }
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            InteractToObject();
            be = new BE.PedidosBE(this.ped);
            be.alterar();
            carregar();
        }

        private void btnselect_Click(object sender, EventArgs e)
        {
            be = new BE.PedidosBE(this.ped);
            ped = be.carregar(int.Parse(listpedido.SelectedValue.ToString()));
            objecttoInterface();
            liberarEdicao(true);
        }
        private void objecttoInterface()
        {
            txtcod.Text = ped.codigo.ToString();
            txtdatetime.Text = ped.datetime.ToString();
            int index = 0;
            foreach (VO.Pecas item in cmbpecas.Items)
            {
                if (item.Id.Equals(ped.codigodapeca.Id))
                {
                    cmbpecas.SelectedIndex = index;
                    break;
                }
                index++;
            }
            int index1 = 0;
            foreach (VO.Clientes item in cmbcliente.Items)
            {
                if (item.codigo.Equals(ped.codigodocliente.codigo))
                {
                    cmbcliente.SelectedIndex = index1;
                    break;
                }
                index1++;
            }

        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            be = new BE.PedidosBE(this.ped);
            be.remover(int.Parse(listpedido.SelectedValue.ToString()));
            carregar();
        }

        private void btnlimpar_Click(object sender, EventArgs e)
        {
            limpar();
            liberarEdicao(false);
        }

        private void cmbpecas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
