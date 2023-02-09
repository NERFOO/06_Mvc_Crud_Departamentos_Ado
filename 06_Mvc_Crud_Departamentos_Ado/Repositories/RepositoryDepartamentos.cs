using _06_Mvc_Crud_Departamentos_Ado.Models;
using System.Data;
using System.Data.SqlClient;

namespace _06_Mvc_Crud_Departamentos_Ado.Repositories
{
    public class RepositoryDepartamentos
    {

        SqlCommand command;
        SqlConnection connection;
        SqlDataReader reader;

        public RepositoryDepartamentos()
        {
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=sa;Password=MCSD2022";
            this.connection = new SqlConnection(connectionString);
            this.command = new SqlCommand();
            this.command.Connection = this.connection;
        }

        public List<Departamento> GetDepartamentos()
        {
            string consulta = "SELECT * FROM DEPT";

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.reader = this.command.ExecuteReader();

            List<Departamento> departamentos = new List<Departamento>();

            while(this.reader.Read())
            {
                Departamento dept = new Departamento();
                dept.IdDepartamento = int.Parse(reader["DEPT_NO"].ToString());
                dept.Nombre = reader["DNOMBRE"].ToString();
                dept.Localidad = reader["LOC"].ToString();

                departamentos.Add(dept);
            }
            this.reader.Close();
            this.connection.Close();

            return departamentos;
        }

        public Departamento FindDepartamento(int idDepartamento)
        {
            string consulta = "SELECT * FROM DEPT WHERE DEPT_NO = @IDDEPARTAMENTO";

            SqlParameter paramId = new SqlParameter("@IDDEPARTAMENTO", idDepartamento);
            this.command.Parameters.Add(paramId);

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();

            this.reader = this.command.ExecuteReader();
            this.reader.Read();

            Departamento dept = new Departamento();
            dept.IdDepartamento = int.Parse(reader["DEPT_NO"].ToString());
            dept.Nombre = reader["DNOMBRE"].ToString();
            dept.Localidad = reader["LOC"].ToString();

            this.reader.Close();
            this.connection.Close();
            this.command.Parameters.Clear();

            return dept;
        }

        public void CreateDepartamento(int id, string nombre, string localidad)
        {
            string consulta = "INSERT INTO DEPT VALUES (@ID, @NOM, @LOC)";

            SqlParameter paramId = new SqlParameter("@ID", id);
            SqlParameter paramNom = new SqlParameter("@NOM", nombre);
            SqlParameter paramLoc = new SqlParameter("@LOC", localidad);
            this.command.Parameters.Add(paramId);
            this.command.Parameters.Add(paramNom);
            this.command.Parameters.Add(paramLoc);

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.command.ExecuteNonQuery();

            this.connection.Close();
            this.command.Parameters.Clear();
        }
        public void UpdateDepartamento(int id, string nombre, string localidad)
        {
            string consulta = "UPDATE DEPT SET DNOMBRE = @NOM, LOC = @LOC WHERE DEPT_NO = @ID";

            SqlParameter paramId = new SqlParameter("@ID", id);
            SqlParameter paramNom = new SqlParameter("@NOM", nombre);
            SqlParameter paramLoc = new SqlParameter("@LOC", localidad);
            this.command.Parameters.Add(paramId);
            this.command.Parameters.Add(paramNom);
            this.command.Parameters.Add(paramLoc);

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.command.ExecuteNonQuery();

            this.connection.Close();
            this.command.Parameters.Clear();
        }

        public void DeleteDepartamento(int id)
        {
            string consulta = "DELETE FROM DEPT WHERE DEPT_NO = @ID";

            SqlParameter paramId = new SqlParameter("@ID", id);
            this.command.Parameters.Add(paramId);

            this.command.CommandType = CommandType.Text;
            this.command.CommandText = consulta;

            this.connection.Open();
            this.command.ExecuteNonQuery();

            this.connection.Close();
            this.command.Parameters.Clear();
        }

    }
}
