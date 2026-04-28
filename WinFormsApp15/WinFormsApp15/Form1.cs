namespace WinFormsApp15
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Declare inputs
            string name = textBox1.Text;
            int age = int.Parse(textBox2.Text);
            string username = textBox3.Text;
            string password = textBox4.Text;
            //Validate inputs if empty
            if (string.IsNullOrEmpty(name)
                || string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill all fields");
                return; //Stop process
            }
            // Validate input for age 
            if (int.TryParse(textBox3.Text.Trim(), out age))
            {
                MessageBox.Show("Please enter a valid age");
                return; //Stop process
            }
            //Call Database Connection
            DBConnect db = new DBConnect();
            try
            {
                db.Open();
                string query = "INSERT INTO students(name, age, username, password) " +
                    "VALUES(@name, @age, @username, @password)";
                // Create MySQL query
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, db.Connection);
                // Add parameters
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                //Execute cmd
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student added successfully");

                // clear fields after save
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                db.Close();
            }
        }
    }
}

