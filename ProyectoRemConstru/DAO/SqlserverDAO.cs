using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DAO.SqlServer
{
    public class SqlserverDAO
    {
        public SqlConnection cnn;
        public string _QueryErrorDescripcion;
        public int _QueryErrorNumero;
        public bool _QueryStatus = false;
        private SqlCommand cmm;

        public SqlserverDAO()
        {
            try
            {
                this.cnn = new SqlConnection(ConfigurationManager.AppSettings["conexionSQLServer"].ToString());
                cmm = new SqlCommand();
            }
            catch (SqlException sqlExcp)
            {
                this._QueryErrorDescripcion = sqlExcp.Message;
            }
        }

        /// <summary>
        ///Devuelve coneccion para utilizar en procedimientos almacenados
        /// </summary>        
        public SqlConnection conStoreProcedure(string servidor)
        {
            try
            {
                SqlConnection conex = new SqlConnection(ConfigurationManager.AppSettings["conexionSQLServer"].ToString());
                conex.Open();
                return conex;
            }
            catch (SqlException sqlExcp)
            {
                this._QueryErrorDescripcion = sqlExcp.Message;
                return null;
            }
        }

        ~SqlserverDAO()
        {
            this.Desconectar();
        }

        public void Conectar()
        {
            try
            {
                if (this.cnn.State == ConnectionState.Closed)
                {
                    this.cnn.Open();
                }
            }
            catch (SqlException sqlExcp)
            {
                this._QueryErrorDescripcion = sqlExcp.Message;
            }
        }

        public void Desconectar()
        {
            if (this.cnn.State == ConnectionState.Open)
                try
                {
                    this.cnn.Close();
                }
                catch (SqlException discExcp)
                {
                    this._QueryErrorDescripcion = discExcp.Message;
                }
        }

        public DataSet queryDataset(string pCommand)
        {
            cmm = new SqlCommand();
            _QueryStatus = false;
            DataSet rtn_queryDataset = new DataSet();
            this.Conectar();
            if (this.cnn.State == ConnectionState.Open)
            {
                try
                {
                    cmm.CommandText = pCommand;
                    cmm.CommandTimeout = 0;
                    cmm.CommandType = CommandType.Text;
                    cmm.Connection = this.cnn;
                    SqlDataAdapter sqlda = new SqlDataAdapter(cmm);
                    sqlda.Fill(rtn_queryDataset);
                    sqlda = null;
                    //cmm = null;
                    cmm.Dispose();
                    cmm.Parameters.Clear();
                    _QueryStatus = true;
                }
                catch (SqlException sqlExcp)
                {
                    this._QueryErrorNumero = sqlExcp.Number;
                    this._QueryErrorDescripcion = sqlExcp.Message;
                    _QueryStatus = false;
                }
            }
            return rtn_queryDataset;
        }

        public void ejecutarNoReturn(string pCommand)
        {
            _QueryStatus = false;
            this.Conectar();
            if (this.cnn.State == ConnectionState.Open)
            {
                cmm.CommandText = pCommand;
                cmm.CommandTimeout = 0;
                cmm.CommandType = CommandType.Text;
                cmm.Connection = this.cnn;

                try
                {
                    cmm.ExecuteReader();
                    _QueryStatus = true;
                }
                catch (SqlException sqlExcp)
                {
                    this._QueryErrorNumero = sqlExcp.Number;
                    this._QueryErrorDescripcion = sqlExcp.Message;
                    _QueryStatus = false;
                }
                //cmm = null;
                cmm.Dispose();
                cmm.Parameters.Clear();
            }
        }

        /// <summary>
        ///Agrega parámetros para la ejecución de proc. almacenado
        /// </summary>        
        public void addParametro(String nombre, String valor)
        {
            IDataParameter dbParam_param = new SqlParameter();
            dbParam_param.ParameterName = nombre;
            //if (valor == null) valor = "";
            dbParam_param.Value = valor;
            dbParam_param.DbType = System.Data.DbType.String;
            cmm.Parameters.Add(dbParam_param);
        }

        public DataSet querySPDataset(string pCommand)
        {
            _QueryStatus = false;
            DataSet rtn_queryDataset = new DataSet();
            this.Conectar();
            if (this.cnn.State == ConnectionState.Open)
            {
                try
                {
                    cmm.CommandText = pCommand;
                    cmm.CommandTimeout = 0;
                    cmm.CommandType = CommandType.StoredProcedure;


                    cmm.Connection = this.cnn;
                    SqlDataAdapter sqlda = new SqlDataAdapter(cmm);
                    sqlda.Fill(rtn_queryDataset);
                    sqlda = null;
                    //cmm = null;
                    cmm.Dispose();
                    cmm.Parameters.Clear();
                    _QueryStatus = true;
                }
                catch (SqlException sqlExcp)
                {
                    this._QueryErrorNumero = sqlExcp.Number;
                    this._QueryErrorDescripcion = sqlExcp.Message;
                    _QueryStatus = false;
                }
            }
            return rtn_queryDataset;
        }

        public void ejecutarSPNoReturn(string pCommand)
        {
            _QueryStatus = false;
            this.Conectar();
            if (this.cnn.State == ConnectionState.Open)
            {
                cmm.CommandText = pCommand;
                cmm.CommandTimeout = 0;
                cmm.CommandType = CommandType.StoredProcedure;
                cmm.Connection = this.cnn;

                try
                {
                    cmm.ExecuteReader();
                    _QueryStatus = true;
                }
                catch (SqlException sqlExcp)
                {
                    this._QueryErrorNumero = sqlExcp.Number;
                    this._QueryErrorDescripcion = sqlExcp.Message;
                    _QueryStatus = false;
                }

                //cmm = null;
                cmm.Dispose();
                cmm.Parameters.Clear();
            }
        }

        public string ejecutarSPReturn(string pCommand)
        {
            _QueryStatus = false;
            string rtn_ejecutarReturn = "";
            this.Conectar();
            if (this.cnn.State == ConnectionState.Open)
            {
                try
                {
                    cmm.CommandText = pCommand;
                    cmm.CommandTimeout = 0;
                    cmm.CommandType = CommandType.StoredProcedure;
                    cmm.Connection = this.cnn;
                    rtn_ejecutarReturn = cmm.ExecuteScalar().ToString();
                    //cmm = null;
                    cmm.Dispose();
                    cmm.Parameters.Clear();
                    _QueryStatus = true;
                }
                catch (SqlException sqlExcp)
                {
                    this._QueryErrorNumero = sqlExcp.Number;
                    this._QueryErrorDescripcion = sqlExcp.Message;
                    _QueryStatus = false;
                }
            }
            return rtn_ejecutarReturn;
        }

    }
}
