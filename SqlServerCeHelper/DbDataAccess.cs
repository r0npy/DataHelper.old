using System;
using System.Collections;
using System.Data;

namespace Universal.Data
{
    /// <summary>
    /// Clase genérica para conexión a bases de datos, la misma debe ser heredada para
    /// especializar el motor a utilizar
    /// </summary>
    abstract class DbDataAccess
    {
        #region Declaración de Variables

        protected string Servidor { get; set; }
        protected string BaseDatos { get; set; }
        protected string Usuario { get; set; }
        protected string Password { get; set; }
        private IDbConnection _conexion;
        public string CadenaConexion { get; set; }

        #endregion

        #region Métodos Abstractos
        protected abstract IDbConnection CrearConexion(string connectionString);
        protected abstract IDbCommand Comando(string SqlCeCommand, CommandType commandType);
        protected abstract IDbCommand Comando(IDbTransaction transaction, string SqlCeCommand, CommandType commandType);
        protected abstract IDataAdapter CrearDataAdapter(string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args);
        protected abstract IDataAdapter CrearDataAdapter(IDbTransaction transaction, string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args);
        protected abstract void CargarParametros(IDbCommand command, params IDbDataParameter[] args);

        #endregion

        #region Getters y Setters
        /// <summary>
        /// Crea u obtiene un objeto para conectarse a la base de datos. 
        /// </summary>
        protected IDbConnection Conexion
        {
            get { return _conexion ?? (_conexion = CrearConexion(CadenaConexion)); }
            set { _conexion = value; }
        }

        #endregion

        #region Estado de Conexión

        /// <summary>
        /// Abre la conexión a la base de datos
        /// </summary>
        /// <returns></returns>
        public bool AbrirConexion()
        {
            if (Conexion == null) return false;
            if (Conexion.State != ConnectionState.Open)
                Conexion.Open();
            return true;
        }

        /// <summary>
        /// Cierra la conexión a la base de datos
        /// </summary>
        /// <returns></returns>
        public void CerrarConexion()
        {
            if (Conexion == null) return;
            if (Conexion.State != ConnectionState.Closed)
                Conexion.Close();
            Conexion.Dispose();
        }

        #endregion

        #region Lecturas
        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL. 
        /// </summary>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(SqlCeCommand, commandType, args).Fill(mDataSet);
            return mDataSet;
        }

        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL, 
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(IDbTransaction transaction, string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            var mDataSet = new DataSet();
            var da = CrearDataAdapter(transaction, SqlCeCommand, commandType, args);
            da.Fill(mDataSet);
            return mDataSet;
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            var com = Comando(SqlCeCommand, commandType);
            CargarParametros(com, args);
            AbrirConexion();
            return com.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL,
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(IDbTransaction transaction, string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            var com = Comando(transaction, SqlCeCommand, commandType);
            CargarParametros(com, args);
            return com.ExecuteReader(CommandBehavior.Default);
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        /// </summary>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ArrayList ExecuteOutputValue(string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            try
            {
                var result = new ArrayList();

                var com = Comando(SqlCeCommand, commandType);
                CargarParametros(com, args);
                AbrirConexion();
                com.ExecuteNonQuery();

                foreach (IDataParameter param in com.Parameters)
                    if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                        result.Add(param.Value);

                return result;
            }
            finally
            {
                CerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ArrayList ExecuteOutputValue(IDbTransaction transaction, string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            var result = new ArrayList();
            var com = Comando(transaction, SqlCeCommand, commandType);
            CargarParametros(com, args);
            com.ExecuteNonQuery();

            foreach (IDataParameter param in com.Parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    result.Add(param.Value);

            return result;
        }

        /// <summary>
        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Object ExecuteScalar(string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            try
            {
                var com = Comando(SqlCeCommand, commandType);
                CargarParametros(com, args);
                AbrirConexion();
                return com.ExecuteScalar();
            }
            finally
            {
                CerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado o Query SQL, 
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Object ExecuteScalar(IDbTransaction transaction, string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            var com = Comando(transaction, SqlCeCommand, commandType);
            CargarParametros(com, args);
            return com.ExecuteScalar();
        }

        #endregion

        #region Acciones

        /// <summary>
        /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos 
        /// </summary>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            try
            {
                var com = Comando(SqlCeCommand, commandType);
                CargarParametros(com, args);
                AbrirConexion();
                return com.ExecuteNonQuery();
            }
            finally
            {
                CerrarConexion();
            }
        }

        /// <summary>
        /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos, 
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="SqlCeCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(IDbTransaction transaction, string SqlCeCommand, CommandType commandType, params IDbDataParameter[] args)
        {
            var com = Comando(transaction, SqlCeCommand, commandType);
            CargarParametros(com, args);
            return com.ExecuteNonQuery();
        }

        #endregion
    }
}
