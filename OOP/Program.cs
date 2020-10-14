using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace OOP
{
    class Program
    {

        static void Main(string[] args)
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
                            k = (byte) (solution / Math.Pow(8, i) % 8);
                            matrix[i, k] = 1;
                        }

                        if (solution == 8937054)
                        {
                            printMatrix(matrix);

                        }
                        if (Program.TestSolution(matrix))
                        {
                            printMatrix(matrix);
                            amount++;
                        }
                        
                        solution++;
                    }

                    Console.WriteLine("amount = {0}", amount);
                } catch (StackOverflowException e)
                {
                    Program.writeError("Произошло переполнение стека.",e);
                } catch (OverflowException e)
                {
                    Program.writeError("Произошло переполнение.", e);
                } catch (Exception e)
                {
                    // Get stack trace for the exception with source file information
                    var st = new StackTrace(e, true);
                    // Get the top stack frame
                    var frame = st.GetFrame(0);
                    // Get the line number from the stack frame
                    var line = frame.GetFileLineNumber();
                    Console.WriteLine("Ошибка на строке {0}", line);
                }

            } catch (OverflowException) {
                Program.writeError("Введено слишком большое число решений.");
            } catch (ArgumentNullException) {
                Program.writeError("Количество решений не введено.");
            } catch (FormatException) {
                Program.writeError("Введено некорректное значение решений.");
            } catch (Exception e)
            {
                Program.writeError("Произошла неизвестная ошибка.", e);
            }
        }

        public static byte countBits(uint number)
        {
            byte count = 0;
            while (number != 0)
            {
                count++;
                number &= (byte) (number - 1);
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
                    if (solution[i,k] == 1)
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
                        for (j = 1; j < 8; j++)
                        {
                            x = (byte)((byte)(i + j) % 8);
                            y = (byte)((byte)(k + j) % 8);
                            if (x == i)
                            {
                                break;
                            }
                            else if (solution[x, y] == 1)
                            {
                                return false;
                            }
                        }
                        for (j = 1; j < 8; j++)
                        {
                            x = (byte)(7 - (byte)((byte)(i + j) % 8));
                            y = (byte)(7 - (byte)((byte)(k + j) % 8));
                            if (y == k)
                            {
                                break;
                            }
                            else if (solution[x, y] == 1)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public static void printMatrix(byte[,] solution)
        {
            byte x, y, j;
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

        public static void writeError(string message)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("     Ошибка!\n");
            Console.WriteLine(message);
        }

        public static void writeError(string message, Exception exception)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("     Ошибка!\n");
            Console.WriteLine(message);
            Console.WriteLine("Описание ошибки: {0}\nИсточник: {1}", exception.Message, exception.Source);
        }

    }

}
