using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BCP
{
    public class usuarioBCP
    {
        public static DataTable loginUsuario(string user, string pass)
        {
            DataSet dtsRetorno = new DataSet();
            try
            {
                //string passwordEncryptado = Util.encriptar(pass);

                DAO.SqlServer.SqlserverDAO sql = new DAO.SqlServer.SqlserverDAO();
                sql.addParametro("@login", user);
                sql.addParametro("@password", pass);
                dtsRetorno = sql.querySPDataset("SVC_QRY_CONSULTALOGIN");
                sql.Desconectar();

            }
            catch (Exception ex)
            {
            }
            return dtsRetorno.Tables[0];
        }
    }
}
