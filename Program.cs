using Microsoft.Data.SqlClient;

namespace CrudGastos
{
    public class Program
    {
        static void Main(string[] args)
        {
            GastoRepositorio repo = new GastoRepositorio();
            string opcion;

            do
            {
                Console.WriteLine();
                Console.WriteLine("--- CONTROL DE GASTOS ---");
                Console.WriteLine("1. Registrar gasto");
                Console.WriteLine("2. Listar gastos");
                Console.WriteLine("3. Buscar gasto por Id");
                Console.WriteLine("4. Actualizar monto");
                Console.WriteLine("5. Eliminar gasto");
                Console.WriteLine("6. Salir");
                Console.Write("Elige una opción: ");
                opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        RegistrarGasto(repo);
                        break;
                    case "2":
                        ListarGastos(repo);
                        break;
                    case "3":
                        BuscarGasto(repo);
                        break;
                    case "4":
                        ActualizarMonto(repo);
                        break;
                    case "5":
                        EliminarGasto(repo);
                        break;
                    case "6":
                        Console.WriteLine("Cerrando el sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción incorrecta, intente de nuevo.");
                        break;
                }

            } while (opcion != "6");
        }

        static void RegistrarGasto(GastoRepositorio repo)
        {
            Console.Write("Descripción: ");
            string descripcion = Console.ReadLine() ?? "";

            Console.Write("Monto: ");
            string textoMonto = Console.ReadLine() ?? "";

            Console.Write("Categoría: ");
            string categoria = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(descripcion) ||
                string.IsNullOrWhiteSpace(categoria) ||
                !decimal.TryParse(textoMonto, out decimal monto) ||
                monto <= 0)
            {
                Console.WriteLine("Error: los datos no son válidos.");
                return;
            }

            try
            {
                Gasto gasto = new Gasto
                {
                    Descripcion = descripcion.Trim(),
                    Monto = monto,
                    Categoria = categoria.Trim()
                };

                repo.Insertar(gasto);
                Console.WriteLine("El gasto se guardó correctamente.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al registrar: {ex.Message}");
            }
        }

        static void ListarGastos(GastoRepositorio repo)
        {
            try
            {
                List<Gasto> lista = repo.ListarTodos();

                if (lista.Count == 0)
                {
                    Console.WriteLine("No se encontraron gastos.");
                    return;
                }

                decimal total = 0;
                foreach (Gasto g in lista)
                {
                    Console.WriteLine(g.ToString());
                    total += g.Monto;
                }

                Console.WriteLine($"TOTAL GASTADO: Q {total:N2}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al listar: {ex.Message}");
            }
        }

        static void BuscarGasto(GastoRepositorio repo)
        {
            Console.Write("Id del gasto a buscar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                Gasto? gasto = repo.BuscarPorId(id);

                if (gasto == null)
                    Console.WriteLine("No existe ese Id.");
                else
                    Console.WriteLine(gasto.ToString());
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al buscar: {ex.Message}");
            }
        }

        static void ActualizarMonto(GastoRepositorio repo)
        {
            Console.Write("Id del gasto a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                Gasto? gasto = repo.BuscarPorId(id);

                if (gasto == null)
                {
                    Console.WriteLine("No existe ese Id.");
                    return;
                }

                Console.WriteLine($"Gasto actual: {gasto}");
                Console.Write("Nuevo monto: ");

                if (!decimal.TryParse(Console.ReadLine(), out decimal nuevoMonto) || nuevoMonto <= 0)
                {
                    Console.WriteLine("Monto inválido.");
                    return;
                }

                gasto.Monto = nuevoMonto;

                if (repo.Actualizar(gasto))
                    Console.WriteLine("Gasto actualizado.");
                else
                    Console.WriteLine("No se actualizó.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}");
            }
        }

        static void EliminarGasto(GastoRepositorio repo)
        {
            Console.Write("Id del gasto a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id inválido.");
                return;
            }

            try
            {
                if (repo.Eliminar(id))
                    Console.WriteLine("Gasto eliminado.");
                else
                    Console.WriteLine("No existe ese Id.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error al eliminar: {ex.Message}");
            }
        }
    }
}