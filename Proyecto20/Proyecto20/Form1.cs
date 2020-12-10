using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto20
{
    public partial class Form1 : Form
    {
        private SqlConnection conexion = new SqlConnection("Data Source=SAM; Initial Catalog=bd1; Integrated security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string sql = "delete from articulos where descripcion=@descripcion";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox1.Text;

            int cant = comando.ExecuteNonQuery();

            MessageBox.Show("Cantidad de registros borrados: " + cant.ToString());

            conexion.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string sql = "insert into articulos(descripcion,rubro,precio) values (@descripcion,@rubro,@precio)";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox1.Text;
            comando.Parameters.Add("@rubro", SqlDbType.VarChar).Value = textBox2.Text;
            comando.Parameters.Add("@precio", SqlDbType.Float).Value = textBox3.Text;

            comando.ExecuteNonQuery();

            conexion.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            MessageBox.Show("Los datos del articulo fueron cargados");

            CargarGrilla();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string sql = "select descripcion,precio,rubro from articulos where codigo=@codigo";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@codigo", SqlDbType.Int).Value = textBox4.Text;

            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                textBox1.Text = registro["descripcion"].ToString();
                textBox2.Text = registro["rubro"].ToString();
                textBox3.Text = registro["precio"].ToString();
            }
            else
                MessageBox.Show("No existe un artículo con el código " + textBox4.Text);

            registro.Close();
            conexion.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string sql = "update articulos set descripcion=@descripcion,rubro=@rubro,precio=@precio where codigo=@codigo";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = textBox1.Text;
            comando.Parameters.Add("@rubro", SqlDbType.VarChar).Value = textBox2.Text;
            comando.Parameters.Add("@precio", SqlDbType.Float).Value = textBox3.Text;
            comando.Parameters.Add("@codigo", SqlDbType.Int).Value = textBox4.Text;

            int cant = comando.ExecuteNonQuery();

            if (cant == 0)
                MessageBox.Show("No existe un articulo con el codigo ingresado");
            else
                MessageBox.Show("Los datos del articulo fueron modificados");

            conexion.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            conexion.Open();

            string sql = "select codigo,descripcion,rubro,precio from articulos";
            SqlCommand comando = new SqlCommand(sql, conexion);

            SqlDataReader registros = comando.ExecuteReader();

            dataGridView1.Rows.Clear();

            while (registros.Read())
            {
                dataGridView1.Rows.Add(registros["codigo"].ToString(),
                                       registros["descripcion"].ToString(),
                                       registros["rubro"].ToString(),
                                       registros["precio"].ToString());
            }

            registros.Close();
            conexion.Close();
        }

        private void consultaDeArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }
    }
}
