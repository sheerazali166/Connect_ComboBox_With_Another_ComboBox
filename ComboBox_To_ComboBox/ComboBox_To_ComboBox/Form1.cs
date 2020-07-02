using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComboBox_To_ComboBox
{
    public partial class Form1 : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["csdb"].ConnectionString;
        DataRow dr;
        public Form1()
        {
            InitializeComponent();
            BindWithComboBoxCountries();
        }


        public void BindWithComboBoxCountries() {

            SqlConnection conn = new SqlConnection(cs);
            string query = "select * from Countries";
            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            DataTable data = new DataTable();
            adapter.Fill(data);
            dr = data.NewRow();
            dr.ItemArray = new object[] {0, "-- Select Country --" };
            data.Rows.InsertAt(dr, 0);
            comboBoxCountries.DisplayMember = "Name";
            comboBoxCountries.ValueMember = "Id";
            comboBoxCountries.DataSource = data;


        
        }

        private void comboBoxCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCountries.SelectedValue.ToString() != null) {

                int id = Convert.ToInt32(comboBoxCountries.SelectedValue.ToString());
                BindCitiesWithComboBox(id);
            }
        }

        public void BindCitiesWithComboBox(int id) {

            SqlConnection conn = new SqlConnection(cs);
            string query = "select * from Cities where Id = @id";
            SqlDataAdapter sda = new SqlDataAdapter(query, conn);
            sda.SelectCommand.Parameters.AddWithValue("@id", id);
            DataTable data = new DataTable();
            sda.Fill(data);
            dr = data.NewRow();
            dr.ItemArray = new object[] {0, "-- Select City --" };
            data.Rows.InsertAt(dr, 0);
            comboBoxCities.DisplayMember = "Name";
            comboBoxCities.ValueMember = "C_Id";
            comboBoxCities.DataSource = data;



        }
    }
}
