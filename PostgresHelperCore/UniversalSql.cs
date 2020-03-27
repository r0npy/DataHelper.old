using System.Data;
using Npgsql;

namespace Universal.Data
{
    /// <summary>
    /// Clase especializada en obtención y manipulación de datos de SQL Server
    /// </summary>
    internal class UniversalSql : DbDataAccess
    {
        /// <summary>
        /// Carga dinamicamente los parametros al objeto comando que se utilizará para ejecutar
        /// </summary>
        /// <param name="command"></param>
        /// <param name="args"></param>
        protected override void CargarParametros(NpgsqlCommand command, params NpgsqlParameter[] args)
        {
            command.Parameters.Clear();
            foreach (NpgsqlParameter param in args)
                command.Parameters.Add(param);
        }

        /// <summary>
        /// Prepara el Objeto Command especializa para la Base de Datos
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected override NpgsqlCommand Comando(string sqlCommand, CommandType commandType)
        {
            return new NpgsqlCommand(sqlCommand)
            {
                CommandType = commandType,
                Connection = (NpgsqlConnection)Conexion
            };
        }

        /// <summary>
        /// Prepara el Objeto Command especializa para la Base de Datos
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected override NpgsqlCommand Comando(string sqlCommand, CommandType commandType, int commandTimeout)
        {
            return new NpgsqlCommand(sqlCommand)
            {
                CommandType = commandType,
                CommandTimeout = commandTimeout,
                Connection = (NpgsqlConnection)Conexion
            };
        }

        /// <summary>
        /// Prepara el Objeto Command especializa para la Base de Datos, recibiendo el objeto de transaccion.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected override NpgsqlCommand Comando(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType)
        {
            return new NpgsqlCommand(sqlCommand)
            {
                CommandType = commandType,
                Connection = (NpgsqlConnection)transaction.Connection,
                Transaction = (NpgsqlTransaction)transaction
            };
        }

        /// <summary>
        /// Prepara el Objeto Command especializa para la Base de Datos, recibiendo el objeto de transaccion.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected override NpgsqlCommand Comando(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout)
        {
            return new NpgsqlCommand(sqlCommand)
            {
                CommandType = commandType,
                CommandTimeout = commandTimeout,
                Connection = (NpgsqlConnection)transaction.Connection,
                Transaction = (NpgsqlTransaction)transaction
            };
        }

        /// <summary>
        /// Se devuelve una nueva instancia del 
        /// objeto Conexión de SqlClient, utilizando la cadena de conexión del objeto.
        /// </summary>
        /// <param name="connectionString">Cadena de Conexión para conectarse a la base de datos</param>
        /// <returns></returns>
        protected override NpgsqlConnection CrearConexion(string connectionString)
        { return new NpgsqlConnection(connectionString); }

        /// <summary>
        /// Aprovecha el método Comando para crear el comando necesario.
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override NpgsqlDataAdapter CrearDataAdapter(string sqlCommand,
            CommandType commandType, params NpgsqlParameter[] args)
        {
            var da = new NpgsqlDataAdapter((NpgsqlCommand)Comando(sqlCommand, commandType));
            if (args.Length != 0)
                CargarParametros(da.SelectCommand, args);
            return da;
        }

        /// <summary>
        /// Aprovecha el método Comando para crear el comando necesario.
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override NpgsqlDataAdapter CrearDataAdapter(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var da = new NpgsqlDataAdapter((NpgsqlCommand)Comando(sqlCommand, commandType, commandTimeout));
            if (args.Length != 0)
                CargarParametros(da.SelectCommand, args);
            return da;
        }

        /// <summary>
        /// Aprovecha el método Comando para crear el comando necesario, recibiendo el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override NpgsqlDataAdapter CrearDataAdapter(NpgsqlTransaction transaction, string sqlCommand,
            CommandType commandType, params NpgsqlParameter[] args)
        {
            var da = new NpgsqlDataAdapter(((NpgsqlCommand)Comando(transaction, sqlCommand, commandType)));
            if (args.Length != 0)
                CargarParametros(da.SelectCommand, args);
            return da;
        }

        /// <summary>
        /// Aprovecha el método Comando para crear el comando necesario, recibiendo el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override NpgsqlDataAdapter CrearDataAdapter(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var da = new NpgsqlDataAdapter(((NpgsqlCommand)Comando(transaction, sqlCommand, commandType, commandTimeout)));
            if (args.Length != 0)
                CargarParametros(da.SelectCommand, args);
            return da;
        }

        /// <summary>
        /// Constructor que recibe los datos de conexion separados
        /// </summary>
        /// <param name="connectionString">Cadena de Conexión para conectarse a la base de datos</param>
        public UniversalSql(string connectionString)
        { CadenaConexion = connectionString; }

        /// <summary>
        /// Constructor vacío
        /// </summary>
        public UniversalSql()
        {
            Conexion = null;
        }
    }
}
