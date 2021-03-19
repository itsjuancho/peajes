using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Peaje
{
    public partial class frmPeaje : Form
    {
        int N, F, C;
        public string[] NombrePeaje;
        public int[,] MPeajes;
        public int[,] MResultados;
        public string[,] MMPPeaje;

        public frmPeaje()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                N = Convert.ToInt32(Interaction.InputBox("Ingrese el número de peajes"));
                dataGridView1.RowCount = (N + 4);
                dataGridView1.Rows[N].Cells[0].Value = "Venta total de categoría";
                dataGridView1.Rows[N + 1].Cells[0].Value = "Promedio";
                dataGridView1.Rows[N + 2].Cells[0].Value = "Mejor peaje por Categoría";
                dataGridView1.Rows[N + 3].Cells[0].Value = "Peor peaje por Categoría";
                dataGridView1.Rows[N].Cells[4].Style.BackColor = Color.Transparent;
                dataGridView1.Rows[N + 1].Cells[4].Style.BackColor = Color.Transparent;
                dataGridView1.Rows[N + 2].Cells[4].Style.BackColor = Color.Transparent;
                dataGridView1.Rows[N + 3].Cells[4].Style.BackColor = Color.Transparent;

                NombrePeaje = new String[N];
                MPeajes = new int[N, 4];
                for(F=0; F < N; F++)
                {
                    NombrePeaje[F] = Interaction.InputBox("Ingrese el nombre del peaje");
                    dataGridView1.Rows[F].Cells[0].Value = NombrePeaje[F];

                    MPeajes[F, 0] = Convert.ToInt32(Interaction.InputBox("Ingrese la cantidad de autos para la categoría 1"));
                    dataGridView1.Rows[F].Cells[1].Value = MPeajes[F, 0];

                    MPeajes[F, 1] = Convert.ToInt32(Interaction.InputBox("Ingrese la cantidad de autos para la categoría 2"));
                    dataGridView1.Rows[F].Cells[2].Value = MPeajes[F, 1];

                    MPeajes[F, 2] = Convert.ToInt32(Interaction.InputBox("Ingrese la cantidad de autos para la categoría 3"));
                    dataGridView1.Rows[F].Cells[3].Value = MPeajes[F, 2];

                    MPeajes[F, 3] = ((MPeajes[F, 0] * 8000) + (MPeajes[F, 1] * 10500) + (MPeajes[F, 2] * 15000));
                    dataGridView1.Rows[F].Cells[4].Value = string.Format("{0:c}", MPeajes[F, 3]);
                }
                btnIngresar.Enabled = false;
                btnCalcular.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show("Ocurrió un error al ejecutar o seguir con el programa, inténtalo de nuevo.");
                dataGridView1.RowCount = 0;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            MResultados = new int[N, 3];
            MMPPeaje = new String[N, 3];
            int Ac = 0;
            int V = 0;
            int Aux = 0;
            string MejorP = "", PeorP = "";
            for (C=0; C < 3; C++)
            {
                for (F=0; F < N; F++)
                {
                    Ac = Ac + MPeajes[F, C];
                    if (C == 0)
                    {
                        V = 8000;
                    }else if (C == 1)
                    {
                        V = 10500;
                    }else if (C == 2)
                    {
                        V = 15000;
                    }

                    if (F == 0)
                    {
                        MejorP = NombrePeaje[F];
                        PeorP = NombrePeaje[F];
                    }
                    else
                    {
                        if (MPeajes[F, C] > Aux)
                        {
                            Aux = MPeajes[F, C];
                            MejorP = NombrePeaje[F];
                        }
                        else
                        {
                            Aux = MPeajes[F, C];
                            PeorP = NombrePeaje[F];
                        }
                    }
                }
                MResultados[0, C] = Ac * V;
                dataGridView1.Rows[N].Cells[C+1].Value = string.Format("{0:c}", MResultados[0, C]);
                MResultados[1, C] = MResultados[0, C] / N;
                dataGridView1.Rows[N+1].Cells[C+1].Value = string.Format("{0:c}", MResultados[1, C]);
                MMPPeaje[0, C] = MejorP;
                dataGridView1.Rows[N + 2].Cells[C + 1].Value = MMPPeaje[0, C];
                MMPPeaje[1, C] = PeorP;
                dataGridView1.Rows[N + 3].Cells[C + 1].Value = MMPPeaje[1, C];
            }
            btnCalcular.Enabled = false;
            btnReiniciar.Enabled = true;
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            btnIngresar.Enabled = true;
            btnCalcular.Enabled = false;
            btnReiniciar.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult Result = MessageBox.Show("¿Está seguro que va a salir del aplicativo?", "Salir App", MessageBoxButtons.YesNo);
            if(Result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                MessageBox.Show("Continuará la ejecución...");
            }
        }
    }
}
