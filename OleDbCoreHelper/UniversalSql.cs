using System.Data;
using System.Data.OleDb;

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
        protected override void CargarParametros(OleDbCommand command, params OleDbParameter[] args)
        {
            command.Parameters.Clear();
            foreach (OleDbParameter param in args)
                command.Parameters.Add(param);
        }

        /// <summary>
        /// Prepara el Objeto Command especializa para la Base de Datos
        /// </summary>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected override OleDbCommand Comando(string OleDbCommand, CommandType commandType)
        {
            return new OleDbCommand(OleDbCommand)
            {
                CommandType = commandType,
                Connection = Conexion
            };
        }

        /// <summary>
        /// Prepara el Objeto Command especializa para la Base de Datos
        /// </summary>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected override OleDbCommand Comando(string OleDbCommand, CommandType commandType, int commandTimeout)
        {
            return new OleDbCommand(OleDbCommand)
            {
                CommandType = commandType,
                CommandTimeout = commandTimeout,
                Connection = Conexion
            };
        }

        /// <summary>
        /// Prepara el Objeto Command especializa para la Base de Datos, recibiendo el objeto de transaccion.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected override OleDbCommand Comando(OleDbTransaction transaction, string OleDbCommand, CommandType commandType)
        {
            return new OleDbCommand(OleDbCommand)
            {
                CommandType = commandType,
                Connection = transaction.Connection,
                Transaction = transaction
            };
        }

        /// <summary>
        /// Prepara el Objeto Command especializa para la Base de Datos, recibiendo el objeto de transaccion.
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        protected override OleDbCommand Comando(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout)
        {
            return new OleDbCommand(OleDbCommand)
            {
                CommandType = commandType,
                CommandTimeout = commandTimeout,
                Connection = (OleDbConnection)transaction.Connection,
                Transaction = (OleDbTransaction)transaction
            };
        }

        /// <summary>
        /// Se devuelve una nueva instancia del 
        /// objeto Conexión de SqlClient, utilizando la cadena de conexión del objeto.
        /// </summary>
        /// <param name="connectionString">Cadena de Conexión para conectarse a la base de datos</param>
        /// <returns></returns>
        protected override OleDbConnection CrearConexion(string connectionString)
        { return new OleDbConnection(connectionString); }

        /// <summary>
        /// Aprovecha el método Comando para crear el comando necesario.
        /// </summary>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override OleDbDataAdapter CrearDataAdapter(string OleDbCommand,
            CommandType commandType, params OleDbParameter[] args)
        {
            var da = new OleDbDataAdapter(Comando(OleDbCommand, commandType));
            if (args.Length != 0)
                CargarParametros(da.SelectCommand, args);
            return da;
        }

        /// <summary>
        /// Aprovecha el método Comando para crear el comando necesario.
        /// </summary>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override OleDbDataAdapter CrearDataAdapter(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
        {
            var da = new OleDbDataAdapter(Comando(OleDbCommand, commandType, commandTimeout));
            if (args.Length != 0)
                CargarParametros(da.SelectCommand, args);
            return da;
        }

        /// <summary>
        /// Aprovecha el método Comando para crear el comando necesario, recibiendo el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override OleDbDataAdapter CrearDataAdapter(OleDbTransaction transaction, string OleDbCommand,
            CommandType commandType, params OleDbParameter[] args)
        {
            var da = new OleDbDataAdapter(((OleDbCommand)Comando(transaction, OleDbCommand, commandType)));
            if (args.Length != 0)
                CargarParametros(da.SelectCommand, args);
            return da;
        }

        /// <summary>
        /// Aprovecha el método Comando para crear el comando necesario, recibiendo el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        protected override OleDbDataAdapter CrearDataAdapter(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
        {
            var da = new OleDbDataAdapter(((OleDbCommand)Comando(transaction, OleDbCommand, commandType, commandTimeout)));
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
