﻿using System.Data.SqlClient;

namespace Proyecto_Coder.DataAccess
{
    public class ConexionDB
    {
        private static string CadenaConexion = @"Server=RACKNARO-PC;Database=proyectocoder;Trusted_Connection=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(CadenaConexion);
        }
    }
}
