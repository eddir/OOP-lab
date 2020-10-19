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
        static public readonly bool TEST_MODE = false;

        public static void Main(string[] args)
        {
            Console.WriteLine("Лабораторные работа по дисциплине \"Объетно ориентированное программирование\"");
            Console.WriteLine("Выполнил студент группы бПИНЖ21 Ростков Э.А.");

            Console.WriteLine("\nВыберите номер задачи [4]:");
            Console.WriteLine("4. Обработка стандартных исключений (задача о 8 ферзях).");
            Console.WriteLine("5. Генерация исключений (общественный транспорт Казани).");
            Console.WriteLine("6. Задание 6.");

            byte task;

            try
            {
                if (!TEST_MODE)
                {
                    task = byte.Parse(Console.ReadLine());
                }
                else
                {
                    task = 5;
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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла неизвестная ошибка. {0}", e.Message);
            }

        }

    }

}



