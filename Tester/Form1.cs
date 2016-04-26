using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Npgsql;
using Universal.Data;

namespace Tester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource =
                    SqlHelper.ExecuteDataSet(Properties.Settings.Default.Portavox,
                    CommandType.StoredProcedure,
                    "slcPeriodos",
                    new SqlParameter("sdf", 34)
                    ).Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource =
                    SqlHelper.ExecuteDataTable(Properties.Settings.Default.Portavox,
                    CommandType.Text,
                    "select * from Departamento where idgerencia = @idgerencia and activo = @activo",
                    new SqlParameter("@IdGerencia", Convert.ToInt16(4)),
                    new SqlParameter("@activo", true)
                    );

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Retorno de Funcion Escalar: " + SqlHelper.ExecuteScalar(Properties.Settings.Default.Portavox,
                    CommandType.StoredProcedure,
                    "slcPeriodos"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(Properties.Settings.Default.Portavox,
                    CommandType.StoredProcedure,
                    "pii");
                MessageBox.Show("Ejecutado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var outputs = SqlHelper.ExecuteOutputValues(Properties.Settings.Default.Portavox,
                    CommandType.StoredProcedure,
                    "testOutput",
                    new SqlParameter("@entrada", Convert.ToInt32(textBox1.Text)),
                    new SqlParameter("@salida", 0) { Direction = ParameterDirection.Output },
                    new SqlParameter("@salida2", 0) { Direction = ParameterDirection.Output }
                    );

                foreach (Object output in outputs)
                {
                    MessageBox.Show(output.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlTransaction sqlTransactionPortaVox = null;
            SqlConnection sqlConnectionPortaVox = null;

            try
            {
                sqlConnectionPortaVox = new SqlConnection(Properties.Settings.Default.Portavox);
                sqlConnectionPortaVox.Open();
                sqlTransactionPortaVox = sqlConnectionPortaVox.BeginTransaction();

                SqlHelper.ExecuteNonQuery(sqlTransactionPortaVox,
                    CommandType.StoredProcedure,
                    "insPeriodo",
                    new SqlParameter("Anho", 2018)
                    );

                SqlHelper.ExecuteNonQuery(sqlTransactionPortaVox,
                    CommandType.StoredProcedure,
                    "insPeriodo",
                    new SqlParameter("Anho", 2019)
                    );

                sqlTransactionPortaVox.Commit();
                MessageBox.Show("Ejecutado");

            }
            catch (Exception ex)
            {
                if (sqlTransactionPortaVox != null && sqlConnectionPortaVox.State == ConnectionState.Open)
                    sqlTransactionPortaVox.Rollback();

                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (sqlConnectionPortaVox != null)
                {
                    sqlConnectionPortaVox.Close();
                    sqlConnectionPortaVox.Dispose();
                }
                if (sqlTransactionPortaVox != null)
                    sqlTransactionPortaVox.Dispose();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("Id", typeof(int));
                dataTable.Columns.Add("val1", typeof(string));
                dataTable.Columns.Add("val2", typeof(string));

                dataTable.Rows.Add(1, "Jose", "Rodriguez");
                dataTable.Rows.Add(2, "María", "Benitez");
                dataTable.Rows.Add(3, "Marcela", "Troche");

                SqlHelper.ExecuteNonQuery(Properties.Settings.Default.Portavox,
                    CommandType.StoredProcedure, "test",
                    new SqlParameter("@param", dataTable));

                MessageBox.Show("ejecutado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dr = SqlHelper.ExecuteReader(Properties.Settings.Default.Portavox,
                                                          CommandType.StoredProcedure,
                                                          "slcPeriodos"))
                {
                    if (!dr.Read())
                        MessageBox.Show("sin datos");
                    else
                    {
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("colaño", "Año");
                        do
                        {
                            dataGridView1.Rows.Add(dr[0]);
                        } while (dr.Read());
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {

                //PostgresHelper.ExecuteNonQuery(Properties.Settings.Default.TestingPg,
                //    CommandType.Text,
                //    "insert into tablita values (3, :col2, :ahora)",
                //    new NpgsqlParameter(":col2", "juancito"),
                //    new NpgsqlParameter(":ahora", DateTime.Now)
                //    );

                //MessageBox.Show("ejecutado");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //dataGridView1.DataSource = PostgresHelper.ExecuteDataSet(Properties.Settings.Default.TestingPg,
                //    CommandType.Text,
                //    "select * from tablita",
                //    new NpgsqlParameter(":col2", "juancito")
                //    ).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                //dataGridView1.DataSource = PostgresHelper.ExecuteDataTable(Properties.Settings.Default.TestingPg,
                //        CommandType.StoredProcedure,
                //        "foo"
                //        );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
