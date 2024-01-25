using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeeManagementApp
{
    public partial class MainForm : Form
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        private SqlCommandBuilder commandBuilder;

        public MainForm()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadData();
        }

        private void InitializeDatabase()
        {
            string connectionString = "YourConnectionStringHere";
            connection = new SqlConnection(connectionString);

            // Create the Employee table
            string createTableQuery = "CREATE TABLE Employee (Id INT PRIMARY KEY, Name NVARCHAR(100), Age INT, Salary DECIMAL(18,2))";
            ExecuteNonQuery(createTableQuery);
        }

        private void LoadData()
        {
            // Load data into DataTable and bind to DataGridView
            adapter = new SqlDataAdapter("SELECT * FROM Employee", connection);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView.DataSource = dataTable;
        }

        private void ExecuteNonQuery(string query)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Perform CRUD operations

            // Insert
            string insertQuery = $"INSERT INTO Employee VALUES ({txtId.Text}, '{txtName.Text}', {txtAge.Text}, {txtSalary.Text})";
            ExecuteNonQuery(insertQuery);

            // Update
            string updateQuery = $"UPDATE Employee SET Name = '{txtName.Text}', Age = {txtAge.Text}, Salary = {txtSalary.Text} WHERE Id = {txtId.Text}";
            ExecuteNonQuery(updateQuery);

            // Delete
            string deleteQuery = $"DELETE FROM Employee WHERE Id = {txtId.Text}";
            ExecuteNonQuery(deleteQuery);

            // Reload data after CRUD operations
            LoadData();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            // Display selected row information in TextBoxes for editing
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                txtId.Text = selectedRow.Cells["Id"].Value.ToString();
                txtName.Text = selectedRow.Cells["Name"].Value.ToString();
                txtAge.Text = selectedRow.Cells["Age"].Value.ToString();
                txtSalary.Text = selectedRow.Cells["Salary"].Value.ToString();
            }
        }

        // Add validation logic as needed for your application
    }
}