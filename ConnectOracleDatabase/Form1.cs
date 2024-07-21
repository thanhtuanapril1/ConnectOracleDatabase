using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using Oracle.ManagedDataAccess.Client;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //Initial variable
        static string connStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))" +
            "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=OTMVN)));User Id=PRODVIEW;Password=Prod-View-OTM-2024;"; 
        OracleConnection conn = new OracleConnection(connStr);
        public Form1()
        {
            InitializeComponent();
        }


        /*      SUB PROGRAM     */
        void viewGridData()
        {
            try
            {
                conn.Open();
                //OracleCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "select * from otm.TCO_COMPANY";
                //cmd.CommandType = CommandType.Text;


                OracleCommand cmd = new OracleCommand("select * from otm.TCO_COMPANY",conn);

                OracleDataAdapter da = new OracleDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds);
                //dataGridView1.DataSource = ds.Tables[0];
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
            finally
            {
                conn.Close();
            }
        }
        void ClearTextbox()
        {
            tbID.Clear();
            tbRegNo.Clear();
            tbStudentName.Clear();
            tbAddress.Clear();
        }
        void Sql_Command(String SqlCmd)
        {
            try
            {
                conn.Open();

                OracleDataAdapter da = new OracleDataAdapter(SqlCmd, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                MessageBox.Show("Command Successfully!");
            }
            catch (Exception x)
            {
                MessageBox.Show(x + "");
            }
            finally
            {
                conn.Close();
            }
        }
        /*    END SUB PROGRAM   */
        private void FormMySQL_Load(object sender, EventArgs e)
        {
            //viewGridData();
        }

        private void btView_Click(object sender, EventArgs e)
        {
            viewGridData();
        }

        private void btInsert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbID.Text))
            {
                String insert = "insert into student (Id,RegNo,student,address) value('"
                    + tbID.Text + "','" + tbRegNo.Text + "','" + tbStudentName.Text + "','" + tbAddress.Text + "' )";
                Sql_Command(insert);
                //For datagrid
                viewGridData();
                ClearTextbox();
            }

        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            String update = "update student set Regno = '" + tbRegNo.Text + "',student='" + tbStudentName.Text + "',address= '"
                    + tbAddress.Text + "' where id =" + tbID.Text + " ";
            Sql_Command(update);
            //For datagrid
            viewGridData();
            ClearTextbox();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {

            String delete = "delete from student where id ='" + tbID.Text + "';";
            Sql_Command(delete);
            //For datagrid
            viewGridData();
            ClearTextbox();
        }

    }
}
