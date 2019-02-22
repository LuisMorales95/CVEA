using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace capascccmex
{
    class SqlServer : IDisposable
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;



        protected string GetCadenaConexionWeb()
        {
            string strConnection = global::System.Configuration.ConfigurationManager.ConnectionStrings["CccMexConnStr"].ConnectionString;
            return strConnection;
        }


        public SqlServer(string connectionString)
        {
            try
            {
                if (string.IsNullOrEmpty(connectionString)) throw new ArgumentException("String de conexion vacío");
                _connection = new SqlConnection(connectionString);

                _command = new SqlCommand() { Connection = _connection };
            }
            catch (Exception ex)
            {
                throw new Exception("Error de Conexion con la BD", ex);

            }
        }
        public SqlServer(int? tipo = null)
        {
            try
            {

                _connection = new SqlConnection(GetCadenaConexionWeb());
                _command = new SqlCommand() { Connection = _connection };
            }
            catch (Exception ex)
            {
                throw new Exception("Error de Conexion con la BD", ex);

            }
        }

        public Object executeScalar(string query, CommandType type = CommandType.StoredProcedure)
        {
            _command.CommandText = query;
            _command.CommandType = type;
            _command.Connection.Open();
            return _command.ExecuteScalar();
        }
        public SqlDataReader executeReader(string query, CommandType type = CommandType.StoredProcedure)
        {
            _command.CommandText = query;
            _command.CommandType = type;
            _command.Connection.Open();

            return _command.ExecuteReader();
        }
        public List<SqlParameter> executeNonQuery(string query, CommandType type = CommandType.StoredProcedure)
        {
            _command.CommandText = query;
            _command.CommandType = type;
            _command.Connection.Open();

            _command.ExecuteNonQuery();

            int x = 0;
            List<SqlParameter> values = new List<SqlParameter>();
            for (x = 0; x < _command.Parameters.Count; x++)
            {
                values.Add(_command.Parameters[x]);
            }
            _command.Connection.Close();
            return values;
        }
        public void clearParameters()
        {
            _command.Parameters.Clear();
        }

        public void addParameter(SqlParameter p)
        {

            _command.Parameters.Add(p);
        }

        public object getParameter(string name)
        {
            return _command.Parameters[name].Value;
        }

        string connStr()
        {
            string Retorno = "";
            string path = System.IO.Directory.GetCurrentDirectory();
            string[] parts = path.Split('\\'); path = "";
            for (int i = 0; i < parts.Length - 2; i++)
            { path = path + parts[i].ToString() + "\\"; }

            path = path + "config.xml";

            // Create an isntance of XmlTextReader and call Read method to read the file
            System.Xml.XmlTextReader textReader = new System.Xml.XmlTextReader(path);
            textReader.Read();
            // If the node has value
            while (textReader.Read())
            {
                //  Here we check the type of the node, in this case we are looking for element
                if (textReader.NodeType == System.Xml.XmlNodeType.Element)
                {
                    //  If the element is "profile"
                    if (textReader.Name == "add")
                    {
                        //  Add the attribute value of "username" to the listbox
                        Retorno = textReader.GetAttribute("connectionString");
                    }
                }

            }
            textReader.Close();
            return Retorno;
        }
        #region IDisposable Members

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            _command.Dispose();
        }

        #endregion
    }
}
