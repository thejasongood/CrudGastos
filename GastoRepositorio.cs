using Microsoft.Data.SqlClient;

namespace CrudGastos
{
    public class GastoRepositorio
    {
        // Cambia ProjectModels por MSSQLLocalDB si en tu máquina usas esa instancia
        private string cadenaConexion =
            @"Server=(localdb)\ProjectModels;Database=GastosDB;Trusted_Connection=True;TrustServerCertificate=True;";

        // 1. Insertar (Registrar)
        public void Insertar(Gasto gasto)
        {
            string sql = "INSERT INTO Gastos (Descripcion, Monto, Categoria) " +
                         "VALUES (@descripcion, @monto, @categoria)";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@descripcion", gasto.Descripcion);
                comando.Parameters.AddWithValue("@monto", gasto.Monto);
                comando.Parameters.AddWithValue("@categoria", gasto.Categoria);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // 2. Listar todos
        public List<Gasto> ListarTodos()
        {
            List<Gasto> lista = new List<Gasto>();
            string sql = "SELECT Id, Descripcion, Monto, Categoria FROM Gastos ORDER BY Id";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        lista.Add(new Gasto
                        {
                            Id = lector.GetInt32(0),
                            Descripcion = lector.GetString(1),
                            Monto = lector.GetDecimal(2),
                            Categoria = lector.GetString(3)
                        });
                    }
                }
            }
            return lista;
        }

        // 3. Buscar por Id
        public Gasto? BuscarPorId(int id)
        {
            string sql = "SELECT Id, Descripcion, Monto, Categoria FROM Gastos WHERE Id = @id";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new Gasto
                        {
                            Id = lector.GetInt32(0),
                            Descripcion = lector.GetString(1),
                            Monto = lector.GetDecimal(2),
                            Categoria = lector.GetString(3)
                        };
                    }
                }
            }
            return null;
        }

        // 4. Actualizar
        public bool Actualizar(Gasto gasto)
        {
            string sql = "UPDATE Gastos SET Descripcion = @descripcion, " +
                         "Monto = @monto, Categoria = @categoria WHERE Id = @id";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@descripcion", gasto.Descripcion);
                comando.Parameters.AddWithValue("@monto", gasto.Monto);
                comando.Parameters.AddWithValue("@categoria", gasto.Categoria);
                comando.Parameters.AddWithValue("@id", gasto.Id);

                conexion.Open();
                return comando.ExecuteNonQuery() == 1;
            }
        }

        // 5. Eliminar
        public bool Eliminar(int id)
        {
            string sql = "DELETE FROM Gastos WHERE Id = @id";

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();
                return comando.ExecuteNonQuery() == 1;
            }
        }
    }
}