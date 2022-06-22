using Microsoft.Data.SqlClient;
using Projekt.Models;
using Projekt.Pages;
using System.Data;

namespace Projekt.DAL
{
    public class EmployeeSqlDB : IEmployeeDB
    {
        public IConfiguration configuration { get; }
        

        private string ProjektDBcs;
        public EmployeeSqlDB(IConfiguration _configuration)
        {
            configuration = _configuration;
            ProjektDBcs = configuration.GetConnectionString("ProjektContext");
        }
        public List<Employee> List()
        {
            List<Employee> employees = new List<Employee>();


            //string myCompanyDBcs = _configuration.GetConnectionString("ProjektContext");

            SqlConnection con = new SqlConnection(ProjektDBcs);
           // string sql = "SELECT * FROM Employee";
            SqlCommand cmd = new SqlCommand("sp_employeeDisplay", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Employee _user;

            while (reader.Read())
            {
                _user = new Employee();
                _user.Id = int.Parse(reader["Id"].ToString());
                _user.userName = reader["userName"].ToString();
                _user.password = reader["password"].ToString();

                _user.role = Employee.convertRole((int)(reader["role"]));

                employees.Add(_user);
            }
            reader.Close(); con.Close();

            return employees;
        }
        public Employee getUser(string userName)
        {
           return List().FirstOrDefault(x=>x.userName==userName);
        }


        public int addNewUser(Employee user)
        {
            var hash = SecurePasswordHasher.Hash(user.password);

            SqlConnection con = new SqlConnection(ProjektDBcs);
            SqlCommand cmd = new SqlCommand("sp_userAdd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter name_SqlParam = new SqlParameter("@userName", SqlDbType.VarChar,
            100);
            name_SqlParam.Value = user.userName;
            cmd.Parameters.Add(name_SqlParam);

            SqlParameter password_SqlParam = new SqlParameter("@password", SqlDbType.VarChar,
            100);
            password_SqlParam.Value = hash;
            cmd.Parameters.Add(password_SqlParam);

            SqlParameter productID_SqlParam = new SqlParameter("@Id",
            SqlDbType.Int);
            productID_SqlParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(productID_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            return 1;
        }
    }
}
