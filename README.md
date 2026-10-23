# CrudGastos

Proyecto de la Clase 12 - CRUD completo de Gastos con C# y SQL Server LocalDB.

## Descripción

Aplicación de consola que permite:
- Registrar gastos
- Listar todos los gastos
- Buscar un gasto por Id
- Actualizar el monto de un gasto
- Eliminar un gasto

Se usa el patrón Repositorio y consultas parametrizadas.  
Todas las operaciones con la base de datos están protegidas con try/catch (SqlException).

## Cómo ejecutar

1. Abrir el proyecto en Visual Studio Community 2026
2. Asegurarse de tener la base de datos `GastosDB` creada (ejecutar el script `CrearGastosDB.sql`)
3. Verificar que la cadena de conexión use la instancia correcta:
   - `(localdb)\ProjectModels` o `(localdb)\MSSQLLocalDB`
4. Presionar F5 o Ctrl + F5

## Escenarios de prueba

| # | Acción | Resultado esperado |
|---|--------|--------------------|
| 1 | Buscar Id 2 | Muestra Gasolina, Q 200.00, Transporte |
| 2 | Buscar Id 99 | "No existe ese Id." |
| 3 | Actualizar Id 2 a 275.00 y listar | "Gasto actualizado." + TOTAL GASTADO: Q 445.50 |
| 4 | Eliminar Id 3 y volver a eliminar | "Gasto eliminado." + luego "No existe ese Id." |
| 5 | Cambiar nombre de la base y listar | Muestra error y vuelve al menú (no se cae) |

## Video

https://drive.google.com/file/d/1hnFwxUqtwBpY0zkiyR_ZgqDEvpAK753N/view?usp=drive_link

## Autor

Jeison Hair Donis Martinez 
