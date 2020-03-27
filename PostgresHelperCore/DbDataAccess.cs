using System;
using System.Collections;
using System.Data;
using Npgsql;
using System.Threading.Tasks;

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
        private NpgsqlConnection _conexion;
        public string CadenaConexion { get; set; }

        #endregion

        #region Métodos Abstractos
        protected abstract NpgsqlConnection CrearConexion(string connectionString);
        protected abstract NpgsqlCommand Comando(string sqlCommand, CommandType commandType);
        protected abstract NpgsqlCommand Comando(string sqlCommand, CommandType commandType, int commandTimeout);
        protected abstract NpgsqlCommand Comando(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType);
        protected abstract NpgsqlCommand Comando(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout);
        protected abstract NpgsqlDataAdapter CrearDataAdapter(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args);

        protected abstract NpgsqlDataAdapter CrearDataAdapter(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args);
        protected abstract NpgsqlDataAdapter CrearDataAdapter(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args);
        protected abstract NpgsqlDataAdapter CrearDataAdapter(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args);
        protected abstract void CargarParametros(NpgsqlCommand command, params NpgsqlParameter[] args);

        #endregion

        #region Getters y Setters
        /// <summary>
        /// Crea u obtiene un objeto para conectarse a la base de datos. 
        /// </summary>
        protected NpgsqlConnection Conexion
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

        public async Task<bool> AbrirConexionAsync()
        {
            if (Conexion == null) return false;
            if (Conexion.State != ConnectionState.Open)
                await Conexion.OpenAsync();
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
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(sqlCommand, commandType, args).Fill(mDataSet);
            return mDataSet;
        }

        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<DataSet> ExecuteDataSetAsync(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(sqlCommand, commandType, args);
            await da.SelectCommand.Connection.OpenAsync();
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var mDataSet = new DataSet();
            CrearDataAdapter(sqlCommand, commandType, commandTimeout, args).Fill(mDataSet);
            return mDataSet;
        }
        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<DataSet> ExecuteDataSetAsync(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(sqlCommand, commandType, commandTimeout, args);
            await da.SelectCommand.Connection.OpenAsync();
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL, 
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var mDataSet = new DataSet();
            var da = CrearDataAdapter(transaction, sqlCommand, commandType, args);
            da.Fill(mDataSet);
            return mDataSet;
        }

        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL, 
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<DataSet> ExecuteDataSetAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(transaction, sqlCommand, commandType, args);
            await da.SelectCommand.Connection.OpenAsync();
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL, 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var mDataSet = new DataSet();
            var da = CrearDataAdapter(transaction, sqlCommand, commandType, commandTimeout, args);
            da.Fill(mDataSet);
            return mDataSet;
        }

        /// <summary>
        /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL, 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<DataSet> ExecuteDataSetAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(transaction, sqlCommand, commandType, commandTimeout, args);
            await da.SelectCommand.Connection.OpenAsync();
            da.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public NpgsqlDataReader ExecuteReader(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(sqlCommand, commandType);
            CargarParametros(com, args);
            AbrirConexion();
            return com.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<NpgsqlDataReader> ExecuteReaderAsync(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(sqlCommand, commandType);
            CargarParametros(com, args);
            await AbrirConexionAsync();
            return await com.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public NpgsqlDataReader ExecuteReader(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            AbrirConexion();
            return com.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<NpgsqlDataReader> ExecuteReaderAsync(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            await AbrirConexionAsync();
            return await com.ExecuteReaderAsync(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL,
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public NpgsqlDataReader ExecuteReader(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            return com.ExecuteReader(CommandBehavior.Default);
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL,
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<NpgsqlDataReader> ExecuteReaderAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            return await com.ExecuteReaderAsync(CommandBehavior.Default);
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL,
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public NpgsqlDataReader ExecuteReader(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            return com.ExecuteReader(CommandBehavior.Default);
        }

        /// <summary>
        /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL,
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<NpgsqlDataReader> ExecuteReaderAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            return await com.ExecuteReaderAsync(CommandBehavior.Default);
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ArrayList ExecuteOutputValues(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            try
            {
                var result = new ArrayList();

                var com = Comando(sqlCommand, commandType);
                CargarParametros(com, args);
                AbrirConexion();
                com.ExecuteNonQuery();

                foreach (NpgsqlParameter param in com.Parameters)
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
        /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<ArrayList> ExecuteOutputValuesAsync(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            try
            {
                var result = new ArrayList();

                var com = Comando(sqlCommand, commandType);
                CargarParametros(com, args);
                await AbrirConexionAsync();
                await com.ExecuteNonQueryAsync();

                foreach (NpgsqlParameter param in com.Parameters)
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
        /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ArrayList ExecuteOutputValues(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            try
            {
                var result = new ArrayList();

                var com = Comando(sqlCommand, commandType, commandTimeout);
                CargarParametros(com, args);
                AbrirConexion();
                com.ExecuteNonQuery();

                foreach (NpgsqlParameter param in com.Parameters)
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
        /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<ArrayList> ExecuteOutputValuesAsync(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            try
            {
                var result = new ArrayList();

                var com = Comando(sqlCommand, commandType, commandTimeout);
                CargarParametros(com, args);
                await AbrirConexionAsync();
                await com.ExecuteNonQueryAsync();

                foreach (NpgsqlParameter param in com.Parameters)
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
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ArrayList ExecuteOutputValues(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var result = new ArrayList();
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            com.ExecuteNonQuery();

            foreach (NpgsqlParameter param in com.Parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    result.Add(param.Value);

            return result;
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<ArrayList> ExecuteOutputValuesAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var result = new ArrayList();
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            await com.ExecuteNonQueryAsync();

            foreach (NpgsqlParameter param in com.Parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    result.Add(param.Value);

            return result;
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ArrayList ExecuteOutputValues(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var result = new ArrayList();
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            com.ExecuteNonQuery();

            foreach (NpgsqlParameter param in com.Parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    result.Add(param.Value);

            return result;
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<ArrayList> ExecuteOutputValuesAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var result = new ArrayList();
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            await com.ExecuteNonQueryAsync();

            foreach (NpgsqlParameter param in com.Parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    result.Add(param.Value);

            return result;
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Object ExecuteOutputValue(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            com.ExecuteNonQuery();

            foreach (NpgsqlParameter param in com.Parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    return param.Value;
            throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<Object> ExecuteOutputValueAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            await com.ExecuteNonQueryAsync();

            foreach (NpgsqlParameter param in com.Parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    return param.Value;
            throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
        }

        public Object ExecuteOutputValue(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            com.ExecuteNonQuery();

            foreach (NpgsqlParameter param in com.Parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    return param.Value;
            throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
        }

        public async Task<Object> ExecuteOutputValueAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            await com.ExecuteNonQueryAsync();

            foreach (NpgsqlParameter param in com.Parameters)
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                    return param.Value;
            throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Object ExecuteOutputValue(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType);
                CargarParametros(com, args);
                AbrirConexion();
                com.ExecuteNonQuery();

                foreach (NpgsqlParameter param in com.Parameters)
                    if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                        return param.Value;

                throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
            }
            finally
            {
                CerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<Object> ExecuteOutputValueAsync(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType);
                CargarParametros(com, args);
                await AbrirConexionAsync();
                await com.ExecuteNonQueryAsync();

                foreach (NpgsqlParameter param in com.Parameters)
                    if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                        return param.Value;

                throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
            }
            finally
            {
                CerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Object ExecuteOutputValue(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType, commandTimeout);
                CargarParametros(com, args);
                AbrirConexion();
                com.ExecuteNonQuery();

                foreach (NpgsqlParameter param in com.Parameters)
                    if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                        return param.Value;

                throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
            }
            finally
            {
                CerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<Object> ExecuteOutputValueAsync(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType, commandTimeout);
                CargarParametros(com, args);
                await AbrirConexionAsync();
                await com.ExecuteNonQueryAsync();

                foreach (NpgsqlParameter param in com.Parameters)
                    if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                        return param.Value;

                throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
            }
            finally
            {
                CerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Object ExecuteScalar(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType);
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
        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<Object> ExecuteScalarAsync(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType);
                CargarParametros(com, args);
                await AbrirConexionAsync();
                return await com.ExecuteScalarAsync();
            }
            finally
            {
                CerrarConexion();
            }
        }

        /// <summary>
        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Object ExecuteScalar(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType, commandTimeout);
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
        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado o Query SQL
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<Object> ExecuteScalarAsync(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType, commandTimeout);
                CargarParametros(com, args);
                await AbrirConexionAsync();
                return await com.ExecuteScalarAsync();
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
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public Object ExecuteScalar(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            return com.ExecuteScalar();
        }

        /// <summary>
        /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado o Query SQL, 
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<Object> ExecuteScalarAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            return await com.ExecuteScalarAsync();
        }

        public Object ExecuteScalar(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            return com.ExecuteScalar();
        }

        public async Task<Object> ExecuteScalarAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            return await com.ExecuteScalarAsync();
        }

        #endregion

        #region Acciones

        /// <summary>
        /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType);
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
        /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType);
                CargarParametros(com, args);
                await AbrirConexionAsync();
                return await com.ExecuteNonQueryAsync();
            }
            finally
            {
                CerrarConexion();
            }
        }

        /// <summary>
        /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType, commandTimeout);
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
        /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            try
            {
                var com = Comando(sqlCommand, commandType, commandTimeout);
                CargarParametros(com, args);
                await AbrirConexionAsync();
                return await com.ExecuteNonQueryAsync();
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
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            return com.ExecuteNonQuery();
        }

        /// <summary>
        /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos, 
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType);
            CargarParametros(com, args);
            return await com.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos, 
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            return com.ExecuteNonQuery();
        }

        /// <summary>
        /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos, 
        /// recibiendo la transaccion en la misma
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandType"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task<int> ExecuteNonQueryAsync(NpgsqlTransaction transaction, string sqlCommand, CommandType commandType, int commandTimeout, params NpgsqlParameter[] args)
        {
            var com = Comando(transaction, sqlCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            return await com.ExecuteNonQueryAsync();
        }
        #endregion
    }
}
