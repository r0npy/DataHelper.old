using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace Universal.Data
{
    /// <summary>
    /// Clase que permite crear conexion al motor de base de datos SQL Server
    /// </summary>
    public static class OleDbHelper
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
            string comandoSql, params OleDbParameter[] args)
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
            string comandoSql, params OleDbParameter[] args)
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
            string comandoSql, int commandTimeout, params OleDbParameter[] args)
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
            string comandoSql, int commandTimeout, params OleDbParameter[] args)
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
        public static DataSet ExecuteDataSet(OleDbTransaction transaction, CommandType commandType,
            string comandoSql, params OleDbParameter[] args)
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
        public static async Task<DataSet> ExecuteDataSetAsync(OleDbTransaction transaction, CommandType commandType,
            string comandoSql, params OleDbParameter[] args)
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
        public static DataSet ExecuteDataSet(OleDbTransaction transaction, CommandType commandType,
            string comandoSql, int commandTimeout, params OleDbParameter[] args)
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
        public static async Task<DataSet> ExecuteDataSetAsync(OleDbTransaction transaction, CommandType commandType,
            string comandoSql, int commandTimeout, params OleDbParameter[] args)
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
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteDataSet(OleDbCommand, commandType, args).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return (await conexion.ExecuteDataSetAsync(OleDbCommand, commandType, args)).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteDataSet(OleDbCommand, commandType, commandTimeout, args).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return (await conexion.ExecuteDataSetAsync(OleDbCommand, commandType, commandTimeout, args)).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteDataSet(transaction, OleDbCommand, commandType, args).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return (await conexion.ExecuteDataSetAsync(transaction, OleDbCommand, commandType, args)).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteDataSet(transaction, OleDbCommand, commandType, commandTimeout, args).Tables[0];
        }

        /// <summary>
        /// Obtiene un objeto DataTable cargados con datos de la Base de Datos en Uso,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<DataTable> ExecuteDataTableAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return (await conexion.ExecuteDataSetAsync(transaction, OleDbCommand, commandType, commandTimeout, args)).Tables[0];
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// el mismo cierra y destruye la conexion una vez cerrado el DataReader
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteReader(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteReader(OleDbCommand, commandType, args) as OleDbDataReader;
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// el mismo cierra y destruye la conexion una vez cerrado el DataReader
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<OleDbDataReader> ExecuteReaderAsync(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteReaderAsync(OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// el mismo cierra y destruye la conexion una vez cerrado el DataReader
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteReader(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteReader(OleDbCommand, commandType, commandTimeout, args) as OleDbDataReader;
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// el mismo cierra y destruye la conexion una vez cerrado el DataReader
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<OleDbDataReader> ExecuteReaderAsync(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteReaderAsync(OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteReader(transaction, OleDbCommand, commandType, args) as OleDbDataReader;
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<OleDbDataReader> ExecuteReaderAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteReaderAsync(transaction, OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static OleDbDataReader ExecuteReader(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteReader(transaction, OleDbCommand, commandType, commandTimeout, args) as OleDbDataReader;
        }

        /// <summary>
        /// Retorna un IDataReader listo para recorrerlo e implementarlo sobre cualquier proveedor,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<OleDbDataReader> ExecuteReaderAsyn(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteReaderAsync(transaction, OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteScalar(OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteScalarAsync(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteScalarAsync(OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteScalar(OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteScalarAsync(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteScalarAsync(OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteScalar(transaction, OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteScalarAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteScalarAsync(transaction, OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteScalar(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteScalar(transaction, OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un valor escalar desde la base de dato, tipificado como Object,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteScalarAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteScalarAsync(transaction, OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ArrayList ExecuteOutputValues(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteOutputValues(OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ArrayList> ExecuteOutputValuesAsync(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteOutputValuesAsync(OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ArrayList ExecuteOutputValues(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteOutputValues(OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ArrayList> ExecuteOutputValuesAsync(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteOutputValuesAsync(OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ArrayList ExecuteOutputValues(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteOutputValues(transaction, OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ArrayList> ExecuteOutputValuesAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteOutputValuesAsync(transaction, OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ArrayList ExecuteOutputValues(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteOutputValues(transaction, OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ArrayList> ExecuteOutputValuesAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteOutputValuesAsync(transaction, OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteOutputValue(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteOutputValue(OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteOutputValueAsync(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteOutputValueAsync(OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteOutputValue(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteOutputValue(OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteOutputValueAsync(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteOutputValueAsync(OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteOutputValue(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteOutputValue(transaction, OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteOutputValueAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteOutputValueAsync(transaction, OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Object ExecuteOutputValue(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteOutputValue(transaction, OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Retorna un ArrayList, de una fila y con número de columnas variables desde la base de datos,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<Object> ExecuteOutputValueAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteOutputValueAsync(transaction, OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteNonQuery(OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteNonQueryAsync(OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return conexion.ExecuteNonQuery(OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql(connectionString);
            return await conexion.ExecuteNonQueryAsync(OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteNonQuery(transaction, OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteNonQueryAsync(transaction, OleDbCommand, commandType, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return conexion.ExecuteNonQuery(transaction, OleDbCommand, commandType, commandTimeout, args);
        }

        /// <summary>
        /// Ejecuta un comando SQL, devolviendo la cantidad de filas afectadas por la Query,
        /// recibiento el objeto de transaccion
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="OleDbCommand"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(OleDbTransaction transaction, CommandType commandType,
            string OleDbCommand, int commandTimeout, params OleDbParameter[] args)
        {
            var conexion = new UniversalSql();
            return await conexion.ExecuteNonQueryAsync(transaction, OleDbCommand, commandType, commandTimeout, args);
        }
    }
}
