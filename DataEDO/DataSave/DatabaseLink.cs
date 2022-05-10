using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using DataEDO.Model.Todo;
using DevExpress.XtraEditors;

namespace DataEDO.DataSave
{
    public class DatabaseLink : IDataStore
    {
        public List<ToDo> LoadToDoList(string connString = "Server= localhost; Database= master; Integrated Security=True;")
        {
            List<ToDo> todos = new List<ToDo>();

            
            string sql = "select * from dbo.todolist";

            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    AddTodoFromDataReader(todos, dataReader);

                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }
            return todos;

        }

        private static void AddTodoFromDataReader(List<ToDo> todos, SqlDataReader dataReader)
        {
            todos.Add(new ToDo()
            {
                Id = Convert.ToInt32(dataReader["id"].ToString()),
                Title = dataReader["title"].ToString(),
                Description = dataReader["description"].ToString(),
                Date = dataReader["date"] != DBNull.Value ? Convert.ToDateTime(dataReader["date"]) : null
            });
        }

        public void SaveToDoList(List<ToDo> todos, string connString)
        {
            string sql = "insert into dbo.todolist(title, description, date) " +
                "values(@title, @description, @date) ";

            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                SqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                foreach (ToDo item in todos.Where(x => x.IsNew).ToList())
                {
                    command.Parameters.Clear();
                    command.Parameters.Add("@title", System.Data.SqlDbType.VarChar, 150).Value = item.Title;
                    command.Parameters.Add("@description", System.Data.SqlDbType.Text).Value = item.Description;
                    command.Parameters.Add("@date", System.Data.SqlDbType.Date).Value = item.Date == null? DBNull.Value: item.Date;

                    command.ExecuteNonQuery();
                    item.IsNew = false;
                }

                transaction.Commit();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }
        }

        public List<ToDo> LoadToDoListWhereTitle(string titleSearch, string connString = "Server= localhost; Database= master; Integrated Security=True;")
        {
            List<ToDo> todos = new List<ToDo>();

            string sql = "select * from dbo.todolist where title like @SEARCH";

            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@SEARCH", "%" + titleSearch + "%");

                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    AddTodoFromDataReader(todos, dataReader);
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }
            return todos;

        }

        public List<ToDo> LoadToDoListWhereDescritpion(string searchInDescription, string connString)
        {
            List<ToDo> todos = new List<ToDo>();

            string sql = "select * from dbo.todolist where description like @SEARCH";

            SqlConnection connection = new SqlConnection(connString);
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@SEARCH", "%" + searchInDescription + "%");

                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                   AddTodoFromDataReader(todos, dataReader);}
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }
            return todos;
        }

        public bool CheckConnectionLink(string connString)
        {
            bool check = true;
            try
            {
                DbConnectionStringBuilder csb = new DbConnectionStringBuilder();
                csb.ConnectionString = connString; 
            }catch (Exception ex)
            {
                //any error
                check = false;
            }
            return check;
        }
    }
}
