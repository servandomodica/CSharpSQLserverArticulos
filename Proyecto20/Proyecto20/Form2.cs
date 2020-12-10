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
    public partial class Form2 : Form
    {
        private SqlConnection conexion = new SqlConnection("Data Source=SAM; Initial Catalog=bd1; Integrated security=True");

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string sql = "select codigo,descripcion,rubro,precio from articulos where precio>=@precio1 and precio<=@precio2";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@precio1", SqlDbType.Float).Value = textBox1.Text;
            comando.Parameters.Add("@precio2", SqlDbType.Float).Value = textBox2.Text;

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

            ConsultarCantidad();
        }

        private void ConsultarCantidad()
        {
            conexion.Open();

            string sql = "select count(*) as cantidad from articulos where precio>=@precio1 and precio<=@precio2";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@precio1", SqlDbType.Float).Value = textBox1.Text;
            comando.Parameters.Add("@precio2", SqlDbType.Float).Value = textBox2.Text;

            SqlDataReader registro = comando.ExecuteReader();
            if (registro.Read())
            {
                labelCantidad.Text = registro["cantidad"].ToString();
            }

            registro.Close();
            conexion.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string sql = "select * From articulos where descripcion Like @buscar";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@buscar", SqlDbType.VarChar).Value = textBox3.Text+"%";

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

        private void Form2_Load(object sender, EventArgs e)
        {
            conexion.Open();

            string sql = "select rubro from articulos group by rubro";
            SqlCommand comando = new SqlCommand(sql, conexion);

            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                comboBox1.Items.Add(registros["rubro"].ToString());
            }

            registros.Close();
            conexion.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string sql = "select * from articulos where rubro=@rubro";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@rubro", SqlDbType.VarChar).Value = comboBox1.SelectedItem.ToString();

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

        private void button5_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string sql = "update articulos set precio=precio+precio*@aumento";
            SqlCommand comando = new SqlCommand(sql, conexion);

            comando.Parameters.Add("@aumento", SqlDbType.Float).Value = textBox4.Text;

            comando.ExecuteNonQuery();

            conexion.Close();

            MessageBox.Show("datos actualizados");
        }
    }
}
