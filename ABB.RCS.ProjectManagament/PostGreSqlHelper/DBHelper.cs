using ABB.RCS.ProjectManagament.ErrorLogs;
using ABB.RCS.SystemManagament;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ABB.RCS.ProjectManagament.PostGreSqlHelper
{
    public class DBHelper : IDisposable
    {
        string connectionString= GetConfigFileData.GetConfigurationData();
        static IErrorLogger _ErrorLog = null;

        private DBHelper(IErrorLogger LogData)
        {
            _ErrorLog = LogData;
        }

        /// <summary>
        //This method is used to attach array of SqlParameters to a SqlCommand.
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">An array of SqlParameters to be added to command</param>
        private static void AttachParameters(NpgsqlCommand command, NpgsqlParameter[] commandParameters)
        {
            try
            {
                if (commandParameters != null)
                {
                    foreach (NpgsqlParameter p in commandParameters)
                    {
                        if (p != null)
                        {
                            if ((p.Direction == ParameterDirection.InputOutput ||
                             p.Direction == ParameterDirection.Input || p.Direction == ParameterDirection.Output) &&
                             (p.Value == null))
                            {
                                p.Value = DBNull.Value;
                            }
                            command.Parameters.Add(p);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorLog.ExceptionWriteIntoTextFile(ex, "Data Access", "AttachParameters", null);
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of SqlParameters
        /// </summary>
        /// <param name="commandParameters">Array of SqlParameters to be assigned values</param>
        /// <param name="parameterValues">Array of objects holding the values to be assigned</param>
        private static void AssignParameterValues(NpgsqlParameter[] commandParameters, object[] parameterValues)
        {
            try
            {
                if ((commandParameters == null) || (parameterValues == null))
                {
                    return;
                }

                if (commandParameters.Length != parameterValues.Length)
                {
                    throw new ArgumentException("Parameter count does not match Parameter Value count.");
                }

                for (int i = 0, j = commandParameters.Length; i < j; i++)
                {
                    if (parameterValues[i] is IDbDataParameter)
                    {
                        IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
                        if (paramInstance.Value == null)
                        {
                            commandParameters[i].Value = DBNull.Value;
                        }
                        else
                        {
                            commandParameters[i].Value = paramInstance.Value;
                        }
                    }
                    else if (parameterValues[i] == null)
                    {
                        commandParameters[i].Value = DBNull.Value;
                    }
                    else
                    {
                        commandParameters[i].Value = parameterValues[i];
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorLog.ExceptionWriteIntoTextFile(ex, "Data Access", "AssignParameterValues", null);
            }
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a connection, transaction, command type and parameters
        /// to the provided command
        /// </summary>
        /// <param name="command">The SqlCommand to be prepared</param>
        /// <param name="connection">A valid SqlConnection, on which to execute this command</param>
        /// <param name="transaction">A valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">The stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">An array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        /// <param name="mustCloseConnection"><c>true</c> if the connection was opened by the method, otherwose is false.</param>
        public static void PrepareCommand(NpgsqlCommand command, NpgsqlConnection connection, NpgsqlTransaction transaction, CommandType commandType, string commandText, NpgsqlParameter[] commandParameters)
        {
            try
            {
                if (command == null || commandText == null)
                {
                    if (connection.State == ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    command.Connection = connection;
                    command.CommandText = commandText;

                    if (transaction != null)
                    {
                        if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                        command.Transaction = transaction;
                        transaction.Commit();
                    }
                    command.CommandType = commandType;
                    if (commandParameters != null)
                    {
                        AttachParameters(command, commandParameters);
                    }
                }
                return;
            }
            catch (Exception ex)
            {
                _ErrorLog.ExceptionWriteIntoTextFile(ex, "Data Access", "PrepareCommand", null);
            }
            finally
            {
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
 
