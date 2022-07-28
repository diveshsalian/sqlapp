using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{

    // This service will interact with our Product data in the SQL database
    public class ProductService
    {
        private static string db_source = "firstsql.database.windows.net";
        private static string db_user = "sysadmin";
        private static string db_password = "login@123";
        private static string db_database = "firstsql";

        private SqlConnection GetConnection()
        {

            var _builder = new SqlConnectionStringBuilder
            {
                DataSource = db_source,
                UserID = db_user,
                Password = db_password,
                InitialCatalog = db_database
            };
            return new SqlConnection(_builder.ConnectionString);
        }
        public List<Product> GetProducts()
        {
            List<Product> _product_lst = new();
            string _statement = "SELECT ProductID,ProductName,Quantity from Products";
            SqlConnection _connection = GetConnection();
            
            _connection.Open();
            
            SqlCommand _sqlcommand = new SqlCommand(_statement, _connection);
            
            using (SqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = _reader.GetInt32(0),
                        ProductName = _reader.GetString(1),
                        Quantity = _reader.GetInt32(2)
                    };

                    _product_lst.Add(_product);
                }
            }
            _connection.Close();
            return _product_lst;
        }

    }
}

