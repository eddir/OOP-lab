using OOP_lab;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace OOP
{
    class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Лабораторные работа по дисциплине \"Объетно ориентированное программирование\"");
            Console.WriteLine("Выполнил студент группы бПИНЖ21 Ростков Э.А.");

            Console.WriteLine("\nВыберите номер задачи [4]:");
            Console.WriteLine("4. Обработка стандартных исключений.");

            byte task;

            try
            {
                task = byte.Parse(Console.ReadLine());
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
                }
            } catch (Exception e)
            {
                Console.WriteLine("Произошла неизвестная ошибка. ", e);
            }

        }

    }

}



