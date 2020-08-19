using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.Threading.Tasks;

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
    private OleDbConnection _conexion;
    public string CadenaConexion { get; set; }

    #endregion

    #region Métodos Abstractos
    protected abstract OleDbConnection CrearConexion(string connectionString);
    protected abstract OleDbCommand Comando(string OleDbCommand, CommandType commandType);
    protected abstract OleDbCommand Comando(string OleDbCommand, CommandType commandType, int commandTimeout);
    protected abstract OleDbCommand Comando(OleDbTransaction transaction, string OleDbCommand, CommandType commandType);
    protected abstract OleDbCommand Comando(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout);
    protected abstract OleDbDataAdapter CrearDataAdapter(string OleDbCommand, CommandType commandType, params OleDbParameter[] args);

    protected abstract OleDbDataAdapter CrearDataAdapter(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args);
    protected abstract OleDbDataAdapter CrearDataAdapter(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args);
    protected abstract OleDbDataAdapter CrearDataAdapter(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args);
    protected abstract void CargarParametros(OleDbCommand command, params OleDbParameter[] args);

    #endregion

    #region Getters y Setters
    /// <summary>
    /// Crea u obtiene un objeto para conectarse a la base de datos. 
    /// </summary>
    protected OleDbConnection Conexion
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public DataSet ExecuteDataSet(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(OleDbCommand, commandType, args);
            if (da.SelectCommand.Connection.State != ConnectionState.Open)
                da.SelectCommand.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        finally
        {
            CerrarConexion();
        }
    }

    /// <summary>
    /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL. 
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<DataSet> ExecuteDataSetAsync(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(OleDbCommand, commandType, args);
            if (da.SelectCommand.Connection.State != ConnectionState.Open)
                await da.SelectCommand.Connection.OpenAsync();
            da.Fill(ds);
            return ds;
        }
        finally
        {
            CerrarConexion();
        }
    }

    /// <summary>
    /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL. 
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public DataSet ExecuteDataSet(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(OleDbCommand, commandType, commandTimeout, args);
            if (da.SelectCommand.Connection.State != ConnectionState.Open)
                da.SelectCommand.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        finally
        {
            CerrarConexion();
        }
    }

    /// <summary>
    /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL. 
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<DataSet> ExecuteDataSetAsync(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(OleDbCommand, commandType, commandTimeout, args);
            if (da.SelectCommand.Connection.State != ConnectionState.Open)
                await da.SelectCommand.Connection.OpenAsync();
            da.Fill(ds);
            return ds;
        }
        finally
        {
            CerrarConexion();
        }
    }

    /// <summary>
    /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL, 
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public DataSet ExecuteDataSet(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(transaction, OleDbCommand, commandType, args);
            if (da.SelectCommand.Connection.State != ConnectionState.Open)
                da.SelectCommand.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        finally
        {
            CerrarConexion();
        }
    }

    /// <summary>
    /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL, 
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<DataSet> ExecuteDataSetAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(transaction, OleDbCommand, commandType, args);
            if (da.SelectCommand.Connection.State != ConnectionState.Open)
                await da.SelectCommand.Connection.OpenAsync();
            da.Fill(ds);
            return ds;
        }
        finally
        {
            CerrarConexion();
        }
    }

    /// <summary>
    /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL, 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public DataSet ExecuteDataSet(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(transaction, OleDbCommand, commandType, commandTimeout, args);
            if (da.SelectCommand.Connection.State != ConnectionState.Open)
                da.SelectCommand.Connection.Open();
            da.Fill(ds);
            return ds;
        }
        finally
        {
            CerrarConexion();
        }
    }

    /// <summary>
    /// Obtiene un DataSet a partir de un Procedimiento Almacenado o Query SQL, 
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<DataSet> ExecuteDataSetAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var ds = new DataSet();
            var da = CrearDataAdapter(transaction, OleDbCommand, commandType, commandTimeout, args);
            if (da.SelectCommand.Connection.State != ConnectionState.Open)
                await da.SelectCommand.Connection.OpenAsync();
            da.Fill(ds);
            return ds;
        }
        finally
        {
            CerrarConexion();
        }
    }

    /// <summary>
    /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public OleDbDataReader ExecuteReader(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(OleDbCommand, commandType);
        CargarParametros(com, args);
        AbrirConexion();
        return com.ExecuteReader(CommandBehavior.CloseConnection);
    }

    /// <summary>
    /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<OleDbDataReader> ExecuteReaderAsync(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(OleDbCommand, commandType);
        CargarParametros(com, args);
        await AbrirConexionAsync();
        return (OleDbDataReader)await com.ExecuteReaderAsync(CommandBehavior.CloseConnection);
    }

    /// <summary>
    /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public OleDbDataReader ExecuteReader(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        AbrirConexion();
        return com.ExecuteReader(CommandBehavior.CloseConnection);
    }

    /// <summary>
    /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<OleDbDataReader> ExecuteReaderAsync(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        await AbrirConexionAsync();
        return (OleDbDataReader)await com.ExecuteReaderAsync(CommandBehavior.CloseConnection);
    }

    /// <summary>
    /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL,
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public OleDbDataReader ExecuteReader(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        return com.ExecuteReader(CommandBehavior.Default);
    }

    /// <summary>
    /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL,
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<OleDbDataReader> ExecuteReaderAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        return (OleDbDataReader)await com.ExecuteReaderAsync(CommandBehavior.Default);
    }

    /// <summary>
    /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL,
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public OleDbDataReader ExecuteReader(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        return com.ExecuteReader(CommandBehavior.Default);
    }

    /// <summary>
    /// Obtiene un DataReader a partir de un Procedimiento Almacenado o Query SQL,
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<OleDbDataReader> ExecuteReaderAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        return (OleDbDataReader)await com.ExecuteReaderAsync(CommandBehavior.Default);
    }

    /// <summary>
    /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public ArrayList ExecuteOutputValues(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var result = new ArrayList();

            var com = Comando(OleDbCommand, commandType);
            CargarParametros(com, args);
            AbrirConexion();
            com.ExecuteNonQuery();

            foreach (OleDbParameter param in com.Parameters)
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<ArrayList> ExecuteOutputValuesAsync(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var result = new ArrayList();

            var com = Comando(OleDbCommand, commandType);
            CargarParametros(com, args);
            await AbrirConexionAsync();
            await com.ExecuteNonQueryAsync();

            foreach (OleDbParameter param in com.Parameters)
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public ArrayList ExecuteOutputValues(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var result = new ArrayList();

            var com = Comando(OleDbCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            AbrirConexion();
            com.ExecuteNonQuery();

            foreach (OleDbParameter param in com.Parameters)
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<ArrayList> ExecuteOutputValuesAsync(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var result = new ArrayList();

            var com = Comando(OleDbCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            await AbrirConexionAsync();
            await com.ExecuteNonQueryAsync();

            foreach (OleDbParameter param in com.Parameters)
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public ArrayList ExecuteOutputValues(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var result = new ArrayList();
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        com.ExecuteNonQuery();

        foreach (OleDbParameter param in com.Parameters)
            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                result.Add(param.Value);

        return result;
    }

    /// <summary>
    /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<ArrayList> ExecuteOutputValuesAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var result = new ArrayList();
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        await com.ExecuteNonQueryAsync();

        foreach (OleDbParameter param in com.Parameters)
            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                result.Add(param.Value);

        return result;
    }

    /// <summary>
    /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public ArrayList ExecuteOutputValues(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var result = new ArrayList();
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        com.ExecuteNonQuery();

        foreach (OleDbParameter param in com.Parameters)
            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                result.Add(param.Value);

        return result;
    }

    /// <summary>
    /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<ArrayList> ExecuteOutputValuesAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var result = new ArrayList();
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        await com.ExecuteNonQueryAsync();

        foreach (OleDbParameter param in com.Parameters)
            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                result.Add(param.Value);

        return result;
    }

    /// <summary>
    /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public Object ExecuteOutputValue(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        com.ExecuteNonQuery();

        foreach (OleDbParameter param in com.Parameters)
            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                return param.Value;
        throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
    }

    /// <summary>
    /// Obtiene un Valor a partir de un Procedimiento Almacenado o Query SQL,
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<Object> ExecuteOutputValueAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        await com.ExecuteNonQueryAsync();

        foreach (OleDbParameter param in com.Parameters)
            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                return param.Value;
        throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
    }

    public Object ExecuteOutputValue(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        com.ExecuteNonQuery();

        foreach (OleDbParameter param in com.Parameters)
            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                return param.Value;
        throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
    }

    public async Task<Object> ExecuteOutputValueAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        await com.ExecuteNonQueryAsync();

        foreach (OleDbParameter param in com.Parameters)
            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                return param.Value;
        throw new Exception("El Procedimiento Almacendado o Query SQL invocado no tenía parametros OUTPUT o INPUTOUTPUT");
    }

    /// <summary>
    /// Obtiene un Valor a partir de un Procedimiento Almacenado, y sus parámetros. 
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public Object ExecuteOutputValue(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType);
            CargarParametros(com, args);
            AbrirConexion();
            com.ExecuteNonQuery();

            foreach (OleDbParameter param in com.Parameters)
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<Object> ExecuteOutputValueAsync(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType);
            CargarParametros(com, args);
            await AbrirConexionAsync();
            await com.ExecuteNonQueryAsync();

            foreach (OleDbParameter param in com.Parameters)
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public Object ExecuteOutputValue(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            AbrirConexion();
            com.ExecuteNonQuery();

            foreach (OleDbParameter param in com.Parameters)
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<Object> ExecuteOutputValueAsync(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType, commandTimeout);
            CargarParametros(com, args);
            await AbrirConexionAsync();
            await com.ExecuteNonQueryAsync();

            foreach (OleDbParameter param in com.Parameters)
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public Object ExecuteScalar(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType);
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<Object> ExecuteScalarAsync(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType);
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public Object ExecuteScalar(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType, commandTimeout);
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<Object> ExecuteScalarAsync(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType, commandTimeout);
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public Object ExecuteScalar(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        return com.ExecuteScalar();
    }

    /// <summary>
    /// Obtiene un Valor de una funcion Escalar a partir de un Procedimiento Almacenado o Query SQL, 
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<Object> ExecuteScalarAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        return await com.ExecuteScalarAsync();
    }

    public Object ExecuteScalar(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        return com.ExecuteScalar();
    }

    public async Task<Object> ExecuteScalarAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        return await com.ExecuteScalarAsync();
    }

    #endregion

    #region Acciones

    /// <summary>
    /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos 
    /// </summary>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public int ExecuteNonQuery(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType);
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<int> ExecuteNonQueryAsync(string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType);
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public int ExecuteNonQuery(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType, commandTimeout);
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<int> ExecuteNonQueryAsync(string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        try
        {
            var com = Comando(OleDbCommand, commandType, commandTimeout);
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
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public int ExecuteNonQuery(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        return com.ExecuteNonQuery();
    }

    /// <summary>
    /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos, 
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<int> ExecuteNonQueryAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType);
        CargarParametros(com, args);
        return await com.ExecuteNonQueryAsync();
    }

    /// <summary>
    /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos, 
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public int ExecuteNonQuery(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        return com.ExecuteNonQuery();
    }

    /// <summary>
    /// Ejecuta un Procedimiento Almacenado o Query SQL en la base de datos, 
    /// recibiendo la transaccion en la misma
    /// </summary>
    /// <param name="transaction"></param>
    /// <param name="OleDbCommand"></param>
    /// <param name="commandType"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    public async Task<int> ExecuteNonQueryAsync(OleDbTransaction transaction, string OleDbCommand, CommandType commandType, int commandTimeout, params OleDbParameter[] args)
    {
        var com = Comando(transaction, OleDbCommand, commandType, commandTimeout);
        CargarParametros(com, args);
        return await com.ExecuteNonQueryAsync();
    }
    #endregion
}

