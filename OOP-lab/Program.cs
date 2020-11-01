using OOP_lab;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Task = OOP_lab.Task;

namespace OOP
{
    class Program
    {
        // Включить ли режим быстрой отладки
        static public readonly bool TEST_MODE = false;

        public static void Main(string[] args)
        {
            Console.WriteLine("Лабораторные работа по дисциплине \"Объетно ориентированное программирование\"");
            Console.WriteLine("Выполнил студент группы бПИНЖ21 Ростков Э.А.");

            List<Task> tasks = new List<Task>{
                new Task4(),
                new Task5(),
                new Task6(),
                new Task7(),
                new Task8(),
                new Task9(),
            };

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine("{0}. {1}", tasks[i].getNumber(), tasks[i].getDescription());
            }
            Console.WriteLine("");

            byte task;

            try
            {
                if (!TEST_MODE)
                {
                    task = byte.Parse(Console.ReadLine());
                }
                else
                {
                    task = 8;
                }
            }
            catch
            {
                Console.WriteLine("Введено неверное значение. Будет использовано значение по умолчанию (4).");
                task = 4;
            }

            try
            {
                bool exc = false;
                foreach (Task t in tasks)
                {
                    if (t.getNumber() == task)
                    {
                        t.Start();
                        exc = true;
                    }
                }
                if (!exc)
                {
                    Console.WriteLine("Задача не найдена.");
                }
            }
            catch (Exception e)
            {
                var st = new StackTrace(e, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                Console.WriteLine("Произошла неизвестная ошибка. \n{0} \n{1}, {2}", e.Message, frame, line);
            }

            Console.WriteLine("\n");
        }

        public static byte[] RandomArray(int size, int max)
        {
            Random rnd = new Random();
            byte[] array = new byte[size];

            for (byte i = 0; i < size; i++)
            {
                array[i] = (byte)(rnd.Next() % max);
            }

            return array;
        }

        public static byte[] RandomArray(int size)
        {
            return RandomArray(size, 100);
        }

        public static byte[][] Random2DArray(int sizeX, int sizeY, int max)
        {
            byte[][] array = new byte[sizeX][];

            for (byte i = 0; i < sizeX; i++)
            {
                array[i] = Program.RandomArray(sizeY, max);
            }

            return array;
        }

        public static byte[][] Random2DArray(int sizeX, int sizeY)
        {
            return Random2DArray(sizeX, sizeY, 100);
        }

        public static void printArray(byte[] elements)
        {
            for (byte i = 0; i < elements.Length; i++)
            {
                Console.Write("{0}", elements[i]);
                if (i < elements.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }

        public static void WriteError(string message)
        {
            Console.Clear();
            Console.Write("     ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка!\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(message);
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void WriteError(string message, Exception exception)
        {
            Console.Clear();
            Console.Write("     ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка!\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(message);
            Console.WriteLine("{2}\nОписание ошибки: {0}\nИсточник: {1}", exception.Message, exception.Source, exception.GetType().Name);
        }

    }

}



