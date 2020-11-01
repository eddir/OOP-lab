using OOP_lab;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace OOP
{
    class Program
    {
        // Включить ли режим быстрой отладки
        static public readonly bool TEST_MODE = true;

        public static void Main(string[] args)
        {
            Console.WriteLine("Лабораторные работа по дисциплине \"Объетно ориентированное программирование\"");
            Console.WriteLine("Выполнил студент группы бПИНЖ21 Ростков Э.А.");


            Console.WriteLine("\nВыберите номер задачи [4]:");
            Console.WriteLine("4. Обработка стандартных исключений (задача о 8 ферзях).");
            Console.WriteLine("5. Генерация исключений (общественный транспорт Казани).");
            Console.WriteLine("6. Одномерные массивы (неубывающая последовательность).");
            Console.WriteLine("7. Многомерные массивы (произведения элементов).");
            Console.WriteLine("8. Строки (поиск вхождений в словах).");
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
                    task = 9;
                }
            }
            catch
            {
                Console.WriteLine("Введено неверное значение. Будет использовано значение по умолчанию (4).");
                task = 4;
            }

            try
            {
                switch (task)
                {
                    case 4:
                        Task4.Start();
                        break;
                    case 5:
                        Task5.Start();
                        break;
                    case 6:
                        Task6.Start();
                        break;
                    case 7:
                        Task7.Start();
                        break;
                    case 8:
                        Task8.Start();
                        break;
                    default:
                        Console.WriteLine("Задача не найдена.");
                        break;
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



