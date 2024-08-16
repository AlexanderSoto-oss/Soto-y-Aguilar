using System;
using System.IO;

namespace Jairo_Soto_y_Angel_Aguilar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] lastResult = null; // Variable para almacenar la última matriz resultante de las operaciones.
            bool exitProgram = false; // Variable para controlar la salida del programa.

            // Bucle principal del programa que se ejecuta hasta que el usuario decida salir.
            while (!exitProgram)
            {
                // Mostrar el menú principal de operaciones de matrices.
                Console.WriteLine("Menú de Operaciones de Matrices:");
                Console.WriteLine("1. Sumar Matrices");
                Console.WriteLine("2. Restar Matrices");
                Console.WriteLine("3. Multiplicar Matrices");
                Console.WriteLine("4. Mostrar Última Operación");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");

                // Leer la opción seleccionada por el usuario.
                string option = Console.ReadLine();

                // Ejecutar la acción basada en la opción seleccionada.
                switch (option)
                {
                    case "1":
                        // Realizar la suma de matrices.
                        lastResult = PerformMatrixOperation("suma");
                        break;
                    case "2":
                        // Realizar la resta de matrices.
                        lastResult = PerformMatrixOperation("resta");
                        break;
                    case "3":
                        // Realizar la multiplicación de matrices.
                        lastResult = PerformMatrixOperation("multiplicación");
                        break;
                    case "4":
                        // Mostrar la última matriz resultante si existe.
                        if (lastResult != null)
                        {
                            Console.WriteLine("Última matriz resultante:");
                            PrintMatrix(lastResult);
                        }
                        else
                        {
                            Console.WriteLine("No hay ninguna operación realizada.");
                        }
                        break;
                    case "5":
                        // Salir del programa.
                        exitProgram = true;
                        Console.WriteLine("Gracias por usar el programa. ¡Adiós!");
                        break;
                    default:
                        // Manejar opciones no válidas.
                        Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                        break;
                }

                // Preguntar al usuario si desea realizar otra operación.
                if (!exitProgram)
                {
                    Console.WriteLine("¿Desea realizar otra operación? (s/n): ");
                    string continueOption = Console.ReadLine().ToLower();
                    if (continueOption != "s")
                    {
                        exitProgram = true;
                        Console.WriteLine("Gracias por usar el programa. ¡Adiós!");
                    }
                }
            }
        }

        // Método para realizar una operación de matrices (suma, resta o multiplicación).
        static int[,] PerformMatrixOperation(string operation)
        {
            // Solicitar dimensiones de las matrices.
            Console.Write("Introduzca el número de filas: ");
            int rows = int.Parse(Console.ReadLine());

            Console.Write("Introduzca el número de columnas: ");
            int cols = int.Parse(Console.ReadLine());

            // Crear y llenar las matrices A y B.
            int[,] matrixA = ReadMatrix(rows, cols, "A");
            int[,] matrixB = ReadMatrix(rows, cols, "B");

            int[,] resultMatrix;

            // Realizar la operación seleccionada.
            if (operation == "suma")
            {
                resultMatrix = AddMatrices(matrixA, matrixB); // Sumar matrices.
            }
            else if (operation == "resta")
            {
                resultMatrix = SubtractMatrices(matrixA, matrixB); // Restar matrices.
            }
            else // operación == "multiplicación"
            {
                // Para la multiplicación, las dimensiones deben ser compatibles.
                if (matrixA.GetLength(1) != matrixB.GetLength(0))
                {
                    Console.WriteLine("Las dimensiones de las matrices no son compatibles para multiplicación.");
                    return null;
                }
                resultMatrix = MultiplyMatrices(matrixA, matrixB); // Multiplicar matrices.
            }

            // Mostrar y guardar la matriz resultante.
            Console.WriteLine("Resultado de la operación:");
            PrintMatrix(resultMatrix);
            SaveMatrixToFile(resultMatrix); // Guardar la matriz resultante en un archivo.
            Console.WriteLine("La Matriz Resultante del Cálculo Elegido fue almacenada en el archivo matriz_resultante.txt.");

            return resultMatrix;
        }

        // Método para leer los elementos de una matriz desde la entrada del usuario.
        static int[,] ReadMatrix(int rows, int cols, string matrixName)
        {
            int[,] matrix = new int[rows, cols];
            Console.WriteLine($"Introduzca los valores para la matriz {matrixName}:");

            // Leer cada elemento de la matriz desde la entrada del usuario.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"Elemento [{i + 1},{j + 1}]: ");
                    matrix[i, j] = int.Parse(Console.ReadLine());
                }
            }

            return matrix;
        }

        // Método para sumar dos matrices.
        static int[,] AddMatrices(int[,] matrixA, int[,] matrixB)
        {
            int rows = matrixA.GetLength(0); // Número de filas en la matriz A.
            int cols = matrixA.GetLength(1); // Número de columnas en la matriz A.
            int[,] result = new int[rows, cols]; // Matriz para almacenar el resultado de la suma.

            // Realizar la suma elemento a elemento.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrixA[i, j] + matrixB[i, j];
                }
            }

            return result;
        }

        // Método para restar dos matrices.
        static int[,] SubtractMatrices(int[,] matrixA, int[,] matrixB)
        {
            int rows = matrixA.GetLength(0); // Número de filas en la matriz A.
            int cols = matrixA.GetLength(1); // Número de columnas en la matriz A.
            int[,] result = new int[rows, cols]; // Matriz para almacenar el resultado de la resta.

            // Realizar la resta elemento a elemento.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrixA[i, j] - matrixB[i, j];
                }
            }

            return result;
        }

        // Método para multiplicar dos matrices.
        static int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0); // Número de filas en la matriz A.
            int colsA = matrixA.GetLength(1); // Número de columnas en la matriz A.
            int colsB = matrixB.GetLength(1); // Número de columnas en la matriz B.
            int[,] result = new int[rowsA, colsB]; // Matriz para almacenar el resultado de la multiplicación.

            // Realizar la multiplicación de matrices.
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        result[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return result;
        }

        // Método para imprimir una matriz en la consola.
        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0); // Número de filas en la matriz.
            int cols = matrix.GetLength(1); // Número de columnas en la matriz.

            // Imprimir cada elemento de la matriz.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "\t"); // Imprimir cada elemento con tabulación.
                }
                Console.WriteLine(); // Nueva línea al final de cada fila.
            }
        }

        // Método para guardar una matriz en un archivo de texto.
        static void SaveMatrixToFile(int[,] matrix)
        {
            // Usar StreamWriter para escribir en el archivo "matriz_resultante.txt".
            using (StreamWriter sw = new StreamWriter("matriz_resultante.txt"))
            {
                int rows = matrix.GetLength(0); // Número de filas en la matriz.
                int cols = matrix.GetLength(1); // Número de columnas en la matriz.

                // Escribir cada elemento de la matriz en el archivo.
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        sw.Write(matrix[i, j] + "\t"); // Escribir cada elemento con tabulación.
                    }
                    sw.WriteLine(); // Nueva línea al final de cada fila.
                }
            }
        }
    }
}

