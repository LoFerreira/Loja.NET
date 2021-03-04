using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Loja.DTO;
using Loja.BLL;

namespace Loja.UI
{
    public partial class Cadastro_usuario : Form
    {
        string modo = "";
        int codUsuSelecionado = -1;
        public Cadastro_usuario()
        {
            InitializeComponent();
        }

        private void Cadastro_usuario_Load(object sender, EventArgs e)

        {
            carregaGrid();
        }
        private void carregaGrid()
        
        {
            try
            {
                IList<usuario_DTO> listUsuario_DTO = new List<usuario_DTO>();
                listUsuario_DTO = new UsuarioBLL().cargaUsuario();
                dataGridView1.DataSource = listUsuario_DTO;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int sel = dataGridView1.CurrentRow.Index;

            txtNome.Text = Convert.ToString(dataGridView1["nome", sel].Value);
            txtLogin.Text = Convert.ToString(dataGridView1["login", sel].Value);
            txtEmail.Text = Convert.ToString(dataGridView1["email", sel].Value);
            txtSenha.Text = Convert.ToString(dataGridView1["senha", sel].Value);
            txtCadastro.Text = Convert.ToString(dataGridView1["cadastro", sel].Value);

            if(Convert.ToString(dataGridView1["situacao", sel].Value) == "A")
            {
                cboSituacao.Text = "Ativo";
            }
            else
            {
                cboSituacao.Text = "Inativo";
            }

            switch (Convert.ToString(dataGridView1["perfil", sel].Value))
            {
                case "1":
                    cboPerfil.Text = "Administrador";
                    break;
                case "2":
                    cboPerfil.Text = "Operador";
                    break;
                case "3":
                    cboPerfil.Text = "Gerencial";
                    break;
            }


        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpar_campos();

            txtCadastro.Text = Convert.ToString(System.DateTime.Now);

            modo = "novo";
        }

        private void limpar_campos()
        {
            txtNome.Text = "";
            txtLogin.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";
            txtCadastro.Text = "";
            cboPerfil.Text = "";
            cboSituacao.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
                modo = "excluir";
                MessageBox.Show("Para excluir o usuario, clique em Confirmar");
            
        }


        private void button4_Click(object sender, EventArgs e)
        {
            modo = "";
            limpar_campos();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (modo == "novo")
            {
                try
                {
                    usuario_DTO USU = new usuario_DTO();
                    USU.nome = txtNome.Text;
                    USU.login = txtLogin.Text;
                    USU.email = txtEmail.Text;
                    USU.cadastro = System.DateTime.Now;
                    USU.senha = txtSenha.Text;
                    if (cboSituacao.Text == "Ativo")
                    {
                        USU.situacao = "A";
                    }
                    else
                    {
                        USU.situacao = "I";
                    }
                    switch (cboPerfil.Text)
                    {
                        case "Administrador":
                            USU.perfil = 1;
                            break;
                        case "Operador":
                            USU.perfil = 2;
                            break;
                        case "Gerencial":
                            USU.perfil = 3;
                            break;
                    }


                    int x = new UsuarioBLL().insereUsuario(USU);
                    if (x>0)
                    {
                        MessageBox.Show("Gravado com sucesso!");
                    }

                    carregaGrid();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado" + ex.Message);
                }
            }
            if (modo == "altera")
            {
                /*Tratamento de Erros, exibe msg*/
                try
                {
                    usuario_DTO USU = new usuario_DTO();
                    USU.cod_usuario = codUsuSelecionado;
                    USU.nome = txtNome.Text;
                    USU.login = txtLogin.Text;
                    USU.email = txtEmail.Text;
                    USU.cadastro = System.DateTime.Now;
                    USU.senha = txtSenha.Text;
                    if (cboSituacao.Text == "Ativo")
                    {
                        USU.situacao = "A";
                    }
                    else
                    {
                        USU.situacao = "I";
                    }
                    switch (cboPerfil.Text)
                    {
                        case "Administrador":
                            USU.perfil = 1;
                            break;
                        case "Operador":
                            USU.perfil = 2;
                            break;
                        case "Gerencial":
                            USU.perfil = 3;
                            break;
                    }
                    int x = new UsuarioBLL().editaUsuario(USU);
                    /*Verifica se houve alguma gravação*/
                    if (x > 0)
                    {
                        MessageBox.Show("Alterado com Sucesso!");
                    }
                    carregaGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado" + ex.Message);
                }
            }
            if (modo == "excluir")
            {
                try
                {
                    usuario_DTO USU = new usuario_DTO();
                    USU.cod_usuario = codUsuSelecionado;
                    int x = new UsuarioBLL().deletaUsuario(USU);
                    if (x > 0)
                    {
                        MessageBox.Show("Excluido com Sucesso!");
                    }
                    carregaGrid();
                    limpar_campos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro inesperado" + ex.Message);
                }
            }
            modo = "";
        }

    

    private void btnEditar_Click(object sender, EventArgs e)
        {
            modo = "altera";
        }
    }
}
