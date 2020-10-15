using System;
using System.Diagnostics;

namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            uint solution = 9032142;
            byte[,] matrix = new byte[8, 8];
            int i;
            for (i = 0; i < 8; i++)
            {
                byte k = (byte)(solution / Math.Pow(8, i) % 8);
                matrix[i, k] = 1;
            }

            if (TestSolution(matrix))
            {
                Console.WriteLine("Успех");
            }
            else
            {
                Console.WriteLine("Неудача");
            }
            PrintMatrix(matrix);
        }

        static void Mainn(string[] args)
        {
            string answer;
            Console.WriteLine("Задача о восьми ферзях");


            try
            {
                Console.WriteLine("Укажите количество решений:");
                //answer = Console.ReadLine();
                answer = "2";
                uint solution = 0;
                uint limit = uint.Parse(answer);

                int amount = 0;
                try
                {
                    byte k = 0;
                    byte i = 0;
                    while (solution < 16777216 && amount < limit)
                    {
                        byte[,] matrix = new byte[8, 8];
                        for (i = 0; i < 8; i++)
                        {
                            k = (byte)(solution / Math.Pow(8, i) % 8);
                            matrix[i, k] = 1;
                        }

                        if (solution == 8937054)
                        {
                            PrintMatrix(matrix);

                        }
                        if (Program.TestSolution(matrix))
                        {
                            PrintMatrix(matrix);
                            amount++;
                        }

                        solution++;
                    }

                    Console.WriteLine("amount = {0}", amount);
                }
                catch (StackOverflowException e)
                {
                    Program.WriteError("Произошло переполнение стека.", e);
                }
                catch (OverflowException e)
                {
                    Program.WriteError("Произошло переполнение.", e);
                }
                catch (Exception e)
                {
                    // Get stack trace for the exception with source file information
                    var st = new StackTrace(e, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();
                    Console.WriteLine("Ошибка на строке {0}", line);
                }

            }
            catch (OverflowException)
            {
                Program.WriteError("Введено слишком большое число решений.");
            }
            catch (ArgumentNullException)
            {
                Program.WriteError("Количество решений не введено.");
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

        public static bool TestSolution(byte[,] solution)
        {
            byte x, y, j;
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
                        byte ci, ck;
                        ci = (byte)((k < i) ? i - k : 0);
                        ck = (byte)((i < k) ? k - i : 0);
                        for (j = 0; j < 8; j++)
                        {
                            x = (byte)(j + ci);
                            y = (byte)(j + ck);
                            /*
                            if (x != i && solution[x, y] == 1)
                            {
                                return false;
                            }*/
                        }

                        for (j = 0; j < 8; j++)
                        {
                            x = (byte)(j - ci);
                            y = (byte)(ck - j);
                            if (!(x == i && y == k) && !(y > 7 || x > 7) && solution[x, y] == 1)
                            {
                                return false;
                            }
                        }

                    }
                }
            }
            return true;
        }

        public static void PrintMatrix(byte[,] solution)
        {
            for (byte i = 0; i < 8; i++)
            {
                for (byte k = 0; k < 8; k++)
                {
                    Console.Write("{0} ", solution[i, k]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void WriteError(string message)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("     Ошибка!\n");
            Console.WriteLine(message);
        }

        public static void WriteError(string message, Exception exception)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("     Ошибка!\n");
            Console.WriteLine(message);
            Console.WriteLine("Описание ошибки: {0}\nИсточник: {1}", exception.Message, exception.Source);
        }

    }

}



