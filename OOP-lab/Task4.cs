using OOP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OOP_lab
{
    class Task4
    {

        // Перебор всех подстановок 8 ферзей на 64 клеточной доски
        static private readonly string METHOD_BRUTE_FORCE_ALL = "1";

        // Перебор всех подстановок за исключением повторов по вертикали и горизонтали
        static private readonly string METHOD_EXCLUDE_LINES_DUBLICATE = "2";

        public static void Start()
        {
            string answer, method;
            byte limit;
            Console.WriteLine("Задача о восьми ферзях");


            try
            {
                if (!Program.TEST_MODE)
                {
                    Console.WriteLine("Укажите количество решений:");
                    answer = Console.ReadLine();
                    limit = byte.Parse(answer);

                    do
                    {
                        Console.WriteLine("Выберите метод решения [1,2]: \n\n1. Перебор всех расположений 8 ферзей на 64 клетках\n" +
                        "2. Исключить повторы на вертикалях и горизонталях");
                        method = Console.ReadLine();
                    } while (!(method == METHOD_BRUTE_FORCE_ALL || method == METHOD_EXCLUDE_LINES_DUBLICATE));
                }
                else
                {
                    limit = 92;
                    method = METHOD_EXCLUDE_LINES_DUBLICATE;
                }

                try
                {
                    var watch = System.Diagnostics.Stopwatch.StartNew();
                    int amount = 0;

                    if (method == METHOD_BRUTE_FORCE_ALL)
                    {
                        amount = MethodBruteForceAll(limit);
                    }
                    else if (method == METHOD_EXCLUDE_LINES_DUBLICATE)
                    {
                        amount = MethodExcludeLinesDublicate(limit);
                    }
                    watch.Stop();
                    Console.WriteLine("Решений найдено = {0}\nЗатрачено времени = {1} сек.", amount, watch.ElapsedMilliseconds / 1000);
                }
                // Убраны OverflowException. Практически невозможно написать управляемый код, который остается единообразным при наличии произвольных переходящих стеков стека.
                catch (Exception e)
                {
                    // Get stack trace for the exception with source file information
                    var st = new StackTrace(e, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();
                    Console.WriteLine("Ошибка на строке {0}, {1}", line, e.Message);
                }

            }
            catch (ArgumentNullException)
            {
                Program.WriteError("Количество решений не введено.");
            }
            catch (OverflowException)
            {
                Program.WriteError("Введено слишком большое число решений.");
            }
            catch (FormatException)
            {
                Program.WriteError("Введено некорректное значение решений.");
            }
            catch (Exception e)
            {
                Program.WriteError("Произошла неизвестная ошибка.", e);
            }
        }

        public static int MethodBruteForceAll(byte limit)
        {
            uint solution = 0;
            int amount = 0;

            byte i, k;
            while (solution < 16777216 && amount < limit)
            {
                byte[,] matrix = new byte[8, 8];
                for (i = 0; i < 8; i++)
                {
                    k = (byte)(solution / Math.Pow(8, i) % 8);
                    matrix[i, k] = 1;
                }
                if (Task4.TestSolution(matrix))
                {
                    Console.WriteLine("{0}.", ++amount);
                    PrintMatrix(matrix);
                }

                solution++;
            }
            return amount;
        }

        public static int MethodExcludeLinesDublicate(byte limit)
        {
            uint solution = 0;
            int amount = 0;

            byte i, k, j;
            bool skeep;
            while (solution < 16777216 && amount < limit)
            {
                skeep = false;
                byte[,] matrix = new byte[8, 8];
                for (i = 0; i < 8 && skeep == false; i++)
                {
                    k = (byte)(solution / Math.Pow(8, i) % 8);
                    matrix[i, k] = 1;
                    for (j = 0; j < i && skeep == false; j++)
                    {
                        if (matrix[j, k] == 1)
                        {
                            skeep = true;
                        }
                    }
                }
                if (skeep == false && Task4.TestSolutionHorizontal(matrix))
                {
                    Console.WriteLine("{0}.", ++amount);
                    PrintMatrix(matrix);
                }

                solution++;
            }
            return amount;
        }

        public static byte CountBits(uint number)
        {
            byte count = 0;
            while (number != 0)
            {
                count++;
                number &= (byte)(number - 1);
            }
            return count;
        }

        public static bool TestSolutionHorizontal(byte[,] solution)
        {
            byte x, y, j;
            for (byte i = 0; i < 8; i++)
            {
                for (byte k = 0; k < 8; k++)
                {
                    if (solution[i, k] == 1)
                    {
                        byte ci, ck;
                        ci = (byte)((k < i) ? i - k : 0);
                        ck = (byte)((i < k) ? k - i : 0);
                        for (j = 0; j <= 7 - Math.Abs(i - k); j++)
                        {
                            x = (byte)(j + ci);
                            y = (byte)(j + ck);
                            if (x != i && solution[x, y] == 1)
                            {
                                return false;
                            }
                        }

                        ci = (byte)((7 - k < i) ? i - (7 - k) : 0);
                        ck = (byte)((7 - k < i) ? 7 : i + k);
                        for (j = 0; j <= Math.Abs(ci - ck); j++)
                        {
                            x = (byte)(j + ci);
                            y = (byte)(ck - j);
                            if (!(x == i && y == k) && solution[x, y] == 1)
                            {
                                return false;
                            }
                        }

                    }
                }
            }
            return true;
        }

        public static bool TestSolution(byte[,] solution)
        {
            byte x, y;
            for (byte i = 0; i < 8; i++)
            {
                for (byte k = 0; k < 8; k++)
                {
                    if (solution[i, k] == 1)
                    {
                        for (x = 0; x < 8; x++)
                        {
                            if (x != i && solution[x, k] == 1)
                            {
                                return false;
                            }
                        }
                        for (y = 0; y < 8; y++)
                        {
                            if (y != k && solution[i, y] == 1)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return TestSolutionHorizontal(solution);
        }

        public static void PrintMatrix(byte[,] solution)
        {
            for (byte i = 0; i < 8; i++)
            {
                for (byte k = 0; k < 8; k++)
                {
                    Console.Write("{0}", solution[i, k] == 0 ? "|_" : "|#");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine();
        }

    }
}
