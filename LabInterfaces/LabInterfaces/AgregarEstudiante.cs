using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabInterfaces
{
    public partial class AgregarEstudiante : Form
    {
        public AgregarEstudiante()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ListaEstudiantes lista = new ListaEstudiantes();
            lista.Show();
            this.Hide();
        }
    }
}
