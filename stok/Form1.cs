
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Zen.Barcode;

namespace stok
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        dtStokEntities baglantı = new dtStokEntities();
        tblStok stok = new tblStok();
        private void Form1_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = baglantı.tblSatıs.ToList();
            dataGridView2.DataSource = baglantı.tblStoks.ToList();


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        // stok tablosu ekleme
        private void button4_Click(object sender, EventArgs e)
        {



            tblStok satıs = new tblStok();

            satıs.Barkod = textBox4.Text;
            satıs.GelisFiyat = int.Parse(textBox5.Text);
            satıs.SatısFiyat = int.Parse(textBox6.Text);
            satıs.Adet = int.Parse(textBox7.Text);
            satıs.Cinsi = comboBox1.Text;
            satıs.Tarih = dateTimePicker1.Value;

            baglantı.tblStoks.Add(satıs);
            baglantı.SaveChanges();

            dataGridView2.DataSource = baglantı.tblStoks.ToList();


        }

        // STOK TABLOSU SİLME
        private void button5_Click(object sender, EventArgs e)
        {
            var sil = baglantı.tblStoks.Where(x => x.Barkod == textBox4.Text).FirstOrDefault();
            baglantı.tblStoks.Remove(sil);
            baglantı.SaveChanges();
            dataGridView2.DataSource = baglantı.tblStoks.ToList();
        }

        // stok tablosu güncelleme
        private void button6_Click(object sender, EventArgs e)
        {

            var güncelle = baglantı.tblStoks.Where(x => x.Barkod == textBox4.Text).FirstOrDefault();

            güncelle.Barkod = textBox4.Text;
            güncelle.GelisFiyat = int.Parse(textBox5.Text);
            güncelle.SatısFiyat = int.Parse(textBox6.Text);
            güncelle.Adet = int.Parse(textBox7.Text);
            güncelle.Cinsi = comboBox1.Text;
            güncelle.Tarih = dateTimePicker1.Value;
            baglantı.SaveChanges();

            dataGridView2.DataSource = baglantı.tblStoks.ToList();

        }

        // textboxa taşıma
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox4.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox7.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();

            comboBox1.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

        //arama yapma
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

            dataGridView2.DataSource = baglantı.tblStoks.Where(x => x.Barkod.Contains(textBox4.Text)).ToList();

        }

        // nakit işlemi
        private void button1_Click(object sender, EventArgs e)
        {

            tblSatıs satıs = new tblSatıs();

            var ara = baglantı.tblStoks.Where(x => x.Barkod == textBox1.Text).FirstOrDefault();

            satıs.Barkod = textBox1.Text;
            satıs.GelisFiyat = ara.GelisFiyat;
            satıs.SatısFiyat = int.Parse(textBox2.Text);
            satıs.Adet = int.Parse(textBox3.Text);
            satıs.Cinsi = ara.Cinsi;
            satıs.Tarih = dateTimePicker1.Value;
            satıs.Odeme = "NAKİT";
            ara.Adet -= int.Parse(textBox3.Text);
            baglantı.tblSatıs.Add(satıs);
            baglantı.SaveChanges();


            dataGridView1.DataSource = baglantı.tblSatıs.ToList();




        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var ara = baglantı.tblStoks.Where(x => x.Barkod.Contains(textBox1.Text)).FirstOrDefault();

            if (ara != null)
            {
                int satısfiyat = ara.SatısFiyat;
                textBox2.Text = satısfiyat.ToString();
            }




        }

        //kart işlemi
        private void button2_Click(object sender, EventArgs e)
        {

            tblSatıs satıs = new tblSatıs();

            var ara = baglantı.tblStoks.Where(x => x.Barkod == textBox1.Text).FirstOrDefault();

            satıs.Barkod = textBox1.Text;
            satıs.GelisFiyat = ara.GelisFiyat;
            satıs.SatısFiyat = int.Parse(textBox2.Text);
            satıs.Adet = int.Parse(textBox3.Text);
            satıs.Cinsi = ara.Cinsi;
            satıs.Tarih = dateTimePicker1.Value;
            satıs.Odeme = "KART";
            ara.Adet -= int.Parse(textBox3.Text);
            baglantı.tblSatıs.Add(satıs);
            baglantı.SaveChanges();


            dataGridView1.DataSource = baglantı.tblSatıs.ToList();

        }

        //satıs silme işlemi
        private void button3_Click(object sender, EventArgs e)
        {
            var ara = baglantı.tblStoks.Where(x => x.Barkod == textBox1.Text).FirstOrDefault();
            ara.Adet += int.Parse(textBox3.Text);
            int satısıd = int.Parse(textBox8.Text);
            var sil = baglantı.tblSatıs.Where(y => y.SatısId == satısıd).FirstOrDefault();
            baglantı.tblSatıs.Remove(sil);
            baglantı.SaveChanges();

            dataGridView1.DataSource = baglantı.tblSatıs.ToList();

        }

        // satıs gösterme
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox8.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();



        }

        //print yeri
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            BarcodeDraw bdraw = BarcodeDrawFactory.GetSymbology(BarcodeSymbology.Code128);
            Image barcodeImage = bdraw.Draw(textBox4.Text, 40);



            e.Graphics.DrawImage(barcodeImage, 10, 5);
            e.Graphics.DrawString(textBox4.Text, new Font("arial", 10, FontStyle.Bold), new SolidBrush(Color.Black), 10, 50);
            e.Graphics.DrawString(textBox6.Text, new Font("arial", 10, FontStyle.Bold), new SolidBrush(Color.Black), 95, 50);
            e.Graphics.DrawString("TL", new Font("arial", 10, FontStyle.Bold), new SolidBrush(Color.Black), 125, 50);

            //Font myFont = new Font("Calibri", 1);
            //Font myFont1 = new Font("Calibri", 1);
            //myFont = new Font("Calibri", 1, FontStyle.Bold);

            //SolidBrush sbrush = new SolidBrush(Color.Black);
            //Pen myPen = new Pen(Color.Black);

            // e.Graphics.DrawString("İşlem : " + textBox3.Text, myFont, sbrush, 0, 0);
        }

        private void button7_Click(object sender, EventArgs e)
        {

            //pageSetupDialog1.ShowDialog();

            printPreviewDialog1.ShowDialog();

            //printDocument1.Print();

        }
        // raporlama
        SqlConnection sqlbaglantı = new SqlConnection("Data Source =.; Initial Catalog = dtStok; Integrated Security = True");

        private void button8_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                string sql = "select * from tblSatıs where Tarih between @p1 and @p2";
                DataTable atl = new DataTable();
                SqlDataAdapter adl = new SqlDataAdapter(sql, sqlbaglantı);
                adl.SelectCommand.Parameters.Add("@p1", SqlDbType.Date).Value = dateTimePicker2.Value;
                adl.SelectCommand.Parameters.Add("@p2", SqlDbType.Date).Value = dateTimePicker3.Value;

                adl.Fill(atl);
                dataGridView3.DataSource = atl;
                label17.Text = dataGridView3.Rows[2].Cells.Count.ToString();
                label18.Text = dataGridView3.Rows[3].Cells.Count.ToString();
                label19.Text = dataGridView3.Rows[4].Cells.Count.ToString();

            }
            else
            {


                string sql = "select * from tblSatıs where Tarih between @p1 and @p2 and Cinsi =@p3 ";
                DataTable atl = new DataTable();
                SqlDataAdapter adl = new SqlDataAdapter(sql, sqlbaglantı);
                adl.SelectCommand.Parameters.Add("@p1", SqlDbType.Date).Value = dateTimePicker2.Value;
                adl.SelectCommand.Parameters.Add("@p2", SqlDbType.Date).Value = dateTimePicker3.Value;

                adl.SelectCommand.Parameters.Add("@p3", comboBox2.Text);


                adl.Fill(atl);
                dataGridView3.DataSource = atl;
                label17.Text = atl.Rows.Count.ToString();

            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
