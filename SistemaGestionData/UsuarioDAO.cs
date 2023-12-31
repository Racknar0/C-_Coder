﻿using System.Data.SqlClient;
using System.Data;
using SistemaGestionEntities;

namespace SistemaGestionData
{
    public class UsuarioDAO
    {
        public static Usuario GetUsuario(int Id)
        {
            string query = "SELECT * FROM Usuarios WHERE id = @Id";
            Usuario usuarioObtenido = new Usuario();

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = Id });

                        using (SqlDataReader dataReader = comando.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    // Recuperar datos del usuario
                                    usuarioObtenido.Id = Convert.ToInt32(dataReader["Id"]);
                                    usuarioObtenido.Nombre = Convert.ToString(dataReader["Nombre"]);
                                    usuarioObtenido.Apellido = Convert.ToString(dataReader["Apellido"]);
                                    usuarioObtenido.NombreUsuario = Convert.ToString(dataReader["NombreUsuario"]);
                                    usuarioObtenido.Contrasenia = Convert.ToString(dataReader["Contrasenia"]);
                                    usuarioObtenido.Mail = Convert.ToString(dataReader["Mail"]);
                                }
                            }

                        }
                    }

                }

                return usuarioObtenido;

            } catch (Exception)
            {

                throw new Exception("Error al obtener el usuario");
            }

        }

        public static List<Usuario> GetAllUsuarios()
        {
            string query = "SELECT * FROM Usuarios";
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        using (SqlDataReader dataReader = comando.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    // Recuperar datos del usuario
                                    Usuario usuarioObtenido = new Usuario();
                                    usuarioObtenido.Id = Convert.ToInt32(dataReader["Id"]);
                                    usuarioObtenido.Nombre = Convert.ToString(dataReader["Nombre"]);
                                    usuarioObtenido.Apellido = Convert.ToString(dataReader["Apellido"]);
                                    usuarioObtenido.NombreUsuario = Convert.ToString(dataReader["NombreUsuario"]);
                                    usuarioObtenido.Contrasenia = Convert.ToString(dataReader["Contrasenia"]);
                                    usuarioObtenido.Mail = Convert.ToString(dataReader["Mail"]);

                                    usuarios.Add(usuarioObtenido);
                                }
                            }

                        }
                    }
                }

                return usuarios;
            } catch (Exception)
            {

                throw new Exception("Error al obtener los usuarios");
            }
        }

        public static int AddUsuario(Usuario usuario)
        {
            string query = "INSERT INTO Usuarios (Nombre, Apellido, NombreUsuario, Contrasenia, Mail) VALUES (@Nombre, @Apellido, @NombreUsuario, @Contrasenia, @Mail); SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                        comando.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                        comando.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                        comando.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                        comando.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Mail });

                        // Ejecutar la inserción y obtener el ID generado automáticamente
                        int idUsuarioNuevo = Convert.ToInt32(comando.ExecuteScalar());

                        return idUsuarioNuevo;
                    }
                }


            } catch (Exception)
            {
                throw new Exception("Error al agregar el usuario");
            }
        }


        public static Usuario UpdateUsuario(Usuario usuario)
        {
            string updateQuery = "UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, NombreUsuario = @NombreUsuario, Contrasenia = @Contrasenia, Mail = @Mail WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand updateComando = new SqlCommand(updateQuery, conexion))
                    {
                        updateComando.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = usuario.Id });
                        updateComando.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                        updateComando.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                        updateComando.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                        updateComando.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                        updateComando.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usuario.Mail });

                        updateComando.ExecuteNonQuery();
                    }

                    // Llamar a GetProducto para obtener el producto actualizado
                    return GetUsuario(usuario.Id);
                }
            } catch (Exception)
            {
                throw new Exception("Error al actualizar el producto");
            }
        }

        public static int DeleteUsuario(int id)
        {
            string deleteQuery = "DELETE FROM Usuarios WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand deleteComando = new SqlCommand(deleteQuery, conexion))
                    {
                        deleteComando.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = id });

                        deleteComando.ExecuteNonQuery();
                    }

                    return id;
                }
            } catch (Exception)
            {
                throw new Exception("Error al eliminar el usuario");
            }
        }

        public static string GetUsername(int id)
        {
            string query = "SELECT NombreUsuario FROM Usuarios WHERE Id = @Id";

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = id });

                        using (SqlDataReader dataReader = comando.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    // Recuperar datos del usuario
                                    string username = Convert.ToString(dataReader["NombreUsuario"]);

                                    return username;
                                }
                            }

                        }
                    }

                }

                return null;

            } catch (Exception)
            {

                throw new Exception("Error al obtener el usuario");
            }
        }

        // Login
        public static Usuario LoginUsuario(string nombreUsuario, string contrasenia)
        {
            string query = "SELECT * FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Contrasenia = @Contrasenia";
            Usuario usuarioObtenido = new Usuario();

            try
            {
                using (SqlConnection conexion = ConexionDB.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = nombreUsuario });
                        comando.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = contrasenia });

                        using (SqlDataReader dataReader = comando.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    // Recuperar datos del usuario
                                    usuarioObtenido.Id = Convert.ToInt32(dataReader["Id"]);
                                    usuarioObtenido.Nombre = Convert.ToString(dataReader["Nombre"]);
                                    usuarioObtenido.Apellido = Convert.ToString(dataReader["Apellido"]);
                                    usuarioObtenido.NombreUsuario = Convert.ToString(dataReader["NombreUsuario"]);
                                    usuarioObtenido.Contrasenia = Convert.ToString(dataReader["Contrasenia"]);
                                    usuarioObtenido.Mail = Convert.ToString(dataReader["Mail"]);
                                }
                            } else
                            {
                                // Retornar nil o null, según lo que necesites
                                return null;
                            }
                        }
                    }
                }

                return usuarioObtenido;

            } catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario: " + ex.Message);
            }
        }

    }
}
