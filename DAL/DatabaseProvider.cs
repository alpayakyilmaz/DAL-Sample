using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseProvider
    {
        private ArrayList _paramters = new ArrayList();
        private SqlCommand CreateCommand(string commandText, CommandType commandType)
        {
            SqlCommand command = ConnectionProvider.CreateConnection().CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }

        private void ProcessParameters(SqlCommand command)
        {
            foreach (SqlParameter myParameter in _paramters )
            {
                command.Parameters.Add(myParameter);
            }


        }

        public void AddinParameters(string parameterName, DbType dbType, object value )
        {
            SqlParameter myParameter = new SqlParameter();
            myParameter.ParameterName = parameterName;
            myParameter.DbType = dbType;
            myParameter.Value = value;
            myParameter.Direction = ParameterDirection.Output;
            _paramters.Add(myParameter);
        }

        public void AddOutParameter(string parameterName, DbType dbType)
        {

            SqlParameter myParameter = new SqlParameter();
            myParameter.ParameterName = parameterName;
            myParameter.DbType = dbType;            
            _paramters.Add(myParameter);
        }

        public object GetParameterValue(string ParameterName)
        {
            object returnValue = null;
            foreach (SqlParameter  myParameter in _paramters)
            {

                if (myParameter.ParameterName == ParameterName)
                {
                    returnValue = myParameter.Value;
                }
            }

            return returnValue;
        }

        public void ClearParameters()
        {
            _paramters.Clear();
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            int returnValue = 0;
            SqlCommand myCommand = this.CreateCommand(commandText, commandType);
            this.ProcessParameters(myCommand);
            try
            {
                myCommand.Connection.Open();
                returnValue = myCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new DatabaseException(ex.Message);
            }
            finally
            {
                myCommand.Connection.Close();
                
            }

            return returnValue;
        }

        public object ExecuteScalar(string commandText, CommandType commandType)
        {

            object returnValue = null;
            SqlCommand myCommand = this.CreateCommand(commandText, commandType);
            this.ProcessParameters(myCommand);
            try
            {
                myCommand.Connection.Open();
                returnValue = myCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw new DatabaseException(ex.Message);
            }
            finally
            {
                myCommand.Connection.Close();
            }

            return returnValue;
        }

        public IDataReader ExecuteReader(string commandText, CommandType commandType)
        {

            IDataReader returnValue;
            SqlCommand myCommand = this.CreateCommand(commandText, commandType);
            this.ProcessParameters(myCommand);
            try
            {
                myCommand.Connection.Open();
                returnValue = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {

                throw new DatabaseException(ex.Message);
            }

        

            return returnValue;
        }

        public DataSet ExecuteDataSet(string commandText,CommandType commandType)
        {


            return ExecuteDataSet(commandText,commandType,"");
        }

        public DataSet ExecuteDataSet(string commandText, CommandType commandType, string dataTableName)
        {
            DataSet returnValue = new DataSet();
            SqlDataAdapter myAdapter = new SqlDataAdapter();
            myAdapter.SelectCommand = this.CreateCommand(commandText, commandType);
            this.ProcessParameters(myAdapter.SelectCommand);
            try
            {
                myAdapter.SelectCommand.Connection.Open();
                if (dataTableName == "")
                {

                    myAdapter.Fill(returnValue);
                }
                else
                {
                    myAdapter.Fill(returnValue, dataTableName);
                }
                
            }
            catch (Exception ex)
            {

                throw new DatabaseException(ex.Message);
            }

            finally
            {
                myAdapter.SelectCommand.Connection.Close();
            }

            return returnValue;
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType)
        {
            return ExecuteDataTable(commandText, commandType);
        }

        public DataTable ExecuteDataTable(string commandText, CommandType commandType, string dataTableName)
        {
            DataTable returnValue;
            if (dataTableName == "")
            {
                returnValue = ExecuteDataSet(commandText, commandType).Tables[0];
            }
            else
            {
                returnValue = ExecuteDataSet(commandText, commandType, dataTableName).Tables[dataTableName];
            }

        

            return returnValue;
        }
    }
}
