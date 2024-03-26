﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentation
{
    public partial class Doktor : Form
    {
        Giris1 forkn = new Giris1();
        Business.Sırala sırala = new Business.Sırala();
        Business.Giris giris = new Business.Giris();
        Business.DataGrid datagrid = new Business.DataGrid();
        private Control[] eskiElemanlar; // Panel içindeki eski elemanları saklamak için bir dizi


        public Doktor(string kimlikno)
        {            

            InitializeComponent();
            label6.Text = kimlikno;

        }

        private void Doktor_Load(object sender, EventArgs e)
        {
            List<string> liste = new List<string>();
            sırala.DoktorBilgileriSırala(label6.Text, liste);
            label1.Text = liste[0];
            label2.Text = liste[1];
            label3.Text = liste[2];
            label4.Text = liste[3];
            
            eskiElemanlar = new Control[panel1.Controls.Count];
            panel1.Controls.CopyTo(eskiElemanlar, 0);

            button1.Visible = false;
            label7.Text = DateTime.Today.ToLongDateString();
            dateTimePicker1.MinDate = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today;
            dataGridView2.DataSource = datagrid.filldatagridhastarandevu(label6.Text,dateTimePicker1.Value);

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string kimlik = dataGridView2.SelectedRows[0].Cells[0].Value.ToString() ;
            Gorusme1 form2 = new Gorusme1(kimlik,label6.Text);
            eskiElemanlar = new Control[panel1.Controls.Count];
            
            panel1.Controls.CopyTo(eskiElemanlar, 0);
            panel1.Controls.Clear();

            if (form2 != null)
            {
                button1.Visible = true;
                dateTimePicker1.Visible = false;
                form2 = new Gorusme1(kimlik,label2.Text);
                form2.TopLevel = false; // Form2'nin ana form olmadığını belirtin
                form2.FormBorderStyle = FormBorderStyle.None; // Form2'nin kenarlık stilini belirleyin
                form2.Dock = DockStyle.Fill; // Form2'yi Panel içinde dolduracak şekilde ayarlayın
                panel1.Controls.Add(form2); // Form2'yi Panel'e ekle
                form2.Show(); // Form2'yi göster
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            // Eski elemanları geri ekle
            if (eskiElemanlar != null)
            {
                panel1.Controls.AddRange(eskiElemanlar);
            }
            button1.Visible = false;
            dateTimePicker1.Visible = true;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = datagrid.filldatagridhastarandevu(label6.Text, dateTimePicker1.Value);

        }
    }
}
