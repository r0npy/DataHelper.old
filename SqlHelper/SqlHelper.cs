using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Universal.Data
{
    /// <summary>
    /// Clase que permite crear conexion al motor de base de datos SQL Server
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// Obtiene un objeto DataSet cargados con datos de la Base de Datos en Uso
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="comandoSql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType,
            string comandoSql, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteDataSet(comandoSql, commandType, args);
        }

        /// <summary>
        /// Obtiene un objeto DataSet cargados con datos de la Base de Datos en Uso
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="comandoSql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataSetAsync(string connectionString, CommandType commandType,
            string comandoSql, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteDataSetAsync(comandoSql, commandType, args);
        }

        /// <summary>
        /// Obtiene un objeto DataSet cargados con datos de la Base de Datos en Uso
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="comandoSql"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType commandType,
            string comandoSql, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteDataSet(comandoSql, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Obtiene un objeto DataSet cargados con datos de la Base de Datos en Uso
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="comandoSql"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataSetAsync(string connectionString, CommandType commandType,
            string comandoSql, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteDataSetAsync(comandoSql, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Obtiene un objeto DataSet cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="comandoSql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType,
            string comandoSql, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteDataSet(transaction, comandoSql, commandType, args);
        }

        /// <summary>
        /// Obtiene un objeto DataSet cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="comandoSql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataSetAsync(SqlTransaction transaction, CommandType commandType,
            string comandoSql, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteDataSetAsync(transaction, comandoSql, commandType, args);
        }

        /// <summary>
        /// Obtiene un objeto DataSet cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="comandoSql"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType,
            string comandoSql, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteDataSet(transaction, comandoSql, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Obtiene un objeto DataSet cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="comandoSql"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataSetAsync(SqlTransaction transaction, CommandType commandType,
            string comandoSql, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteDataSetAsync(transaction, comandoSql, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteDataSet(sqlCommand, commandType, args).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return (await conexion.ExecuteDataSetAsync(sqlCommand, commandType, args)).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteDataSet(sqlCommand, commandType, commandTimeout, args).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return (await conexion.ExecuteDataSetAsync(sqlCommand, commandType, commandTimeout, args)).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteDataSet(transaction, sqlCommand, commandType, args).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return (await conexion.ExecuteDataSetAsync(transaction, sqlCommand, commandType, args)).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteDataSet(transaction, sqlCommand, commandType, commandTimeout, args).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return (await conexion.ExecuteDataSetAsync(transaction, sqlCommand, commandType, commandTimeout, args)).Tables[0];
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// el mismo cierra y destruye la conexion una vez cerrado el DataReader
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteReader(sqlCommand, commandType, args) as SqlDataReader;
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// el mismo cierra y destruye la conexion una vez cerrado el DataReader
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<SqlDataReader> ExecuteReaderAsync(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteReaderAsync(sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// el mismo cierra y destruye la conexion una vez cerrado el DataReader
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteReader(sqlCommand, commandType, commandTimeout, args) as SqlDataReader;
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// el mismo cierra y destruye la conexion una vez cerrado el DataReader
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<SqlDataReader> ExecuteReaderAsync(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteReaderAsync(sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteReader(transaction, sqlCommand, commandType, args) as SqlDataReader;
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<SqlDataReader> ExecuteReaderAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteReaderAsync(transaction, sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteReader(transaction, sqlCommand, commandType, commandTimeout, args) as SqlDataReader;
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<SqlDataReader> ExecuteReaderAsyn(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteReaderAsync(transaction, sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteScalar(sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteScalarAsync(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteScalarAsync(sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteScalar(sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteScalarAsync(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteScalarAsync(sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteScalar(transaction, sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteScalarAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteScalarAsync(transaction, sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteScalar(transaction, sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteScalarAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteScalarAsync(transaction, sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ArrayList ExecuteOutputValues(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteOutputValues(sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ArrayList> ExecuteOutputValuesAsync(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteOutputValuesAsync(sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ArrayList ExecuteOutputValues(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteOutputValues(sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ArrayList> ExecuteOutputValuesAsync(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteOutputValuesAsync(sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ArrayList ExecuteOutputValues(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteOutputValues(transaction, sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ArrayList> ExecuteOutputValuesAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteOutputValuesAsync(transaction, sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ArrayList ExecuteOutputValues(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteOutputValues(transaction, sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ArrayList> ExecuteOutputValuesAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteOutputValuesAsync(transaction, sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteOutputValue(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteOutputValue(sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteOutputValueAsync(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteOutputValueAsync(sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteOutputValue(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteOutputValue(sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteOutputValueAsync(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteOutputValueAsync(sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteOutputValue(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteOutputValue(transaction, sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteOutputValueAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteOutputValueAsync(transaction, sqlCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteOutputValue(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteOutputValue(transaction, sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteOutputValueAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteOutputValueAsync(transaction, sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteNonQuery(sqlCommand, commandType, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteNonQueryAsync(sqlCommand, commandType, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteNonQuery(sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteNonQueryAsync(sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteNonQuery(transaction, sqlCommand, commandType, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteNonQueryAsync(transaction, sqlCommand, commandType, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteNonQuery(transaction, sqlCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(SqlTransaction transaction, CommandType commandType,
            string sqlCommand, int commandTimeout, params SqlParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteNonQueryAsync(transaction, sqlCommand, commandType, commandTimeout, args);
        }
    }
}
