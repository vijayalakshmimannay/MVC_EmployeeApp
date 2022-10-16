using Microsoft.Extensions.Configuration;
using MVC_EmployeeApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MVC_EmployeeApp.Repository
{
    public class EmployeeRL : IEmployeeRL
    {
        string dbpath = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeDB;Integrated Security=True";

        SqlConnection sqlConnection;

        //public EmployeeRL(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }



        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            try
            {
                sqlConnection = new SqlConnection(dbpath);
                SqlCommand command = new SqlCommand("spAddEmployee ", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
               

                sqlConnection.Open();

                command.Parameters.AddWithValue("@EmpName", employee.EmpName);
                command.Parameters.AddWithValue("@Profile", employee.Profile);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Dept", employee.Dept);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@StartDate", employee.StartDate);

                command.ExecuteNonQuery();
                return employee;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public EmployeeModel UpdateEmployee(EmployeeModel employee, int EmployeeId)
        {

            try
            {

                SqlCommand command = new SqlCommand("spUpdateEmployee", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();
                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@EmpName", employee.EmpName);
                command.Parameters.AddWithValue("@Profile", employee.Profile);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Dept", employee.Dept);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@StartDate", employee.StartDate);
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    return employee;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public string DeleteEmployee(EmployeeModel employee)
        {
            try
            {

                SqlCommand command = new SqlCommand("spDeleteEmployee ", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();

                command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);

                command.ExecuteNonQuery();
                var result = command.ExecuteNonQuery();
                if (result > 0)
                {
                    return "Employee deleted";
                }
                else
                {
                    return "Failed to delete Employee";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public List<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();

            sqlConnection = new SqlConnection(dbpath);
                try
                {
                    SqlCommand command = new SqlCommand("spGetAllEmployees ", sqlConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();


                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            employees.Add(new EmployeeModel
                            {
                                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                                EmpName = reader["EmpName"].ToString(),
                                Profile = reader["Profile"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Dept = (reader["Dept"]).ToString(),
                                Salary = (reader["Salary"]).ToString(),
                                StartDate = ((DateTime)reader["StartDate"]),
                            });
                        }
                        return employees;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }

            
        }

    }
}





