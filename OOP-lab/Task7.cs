using OOP;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_lab
{
    class Task7: Task
    {

        public Task7()
        {
            number = '7';
            description = "Многомерные массивы (произведения элементов).";
        }

        public override void Start()
        {
            int sizeX = 10, sizeY = 10;
            Console.WriteLine("Написать и протестировать метод, находящий произведение  элементов каждой строки заданного " +
                    "целочисленного ступенчатого массива и возвращающий массив этих произведений. Метод должен генерировать хотя " +
                    "бы одно исключение.\n");

            try
            {
                byte[][] elements = Program.Random2DArray(sizeX, sizeY, 100);
                int[] products = Task7.arrayProducts(elements);
                int j = 0;

                foreach (byte[] element in elements)
                {
                    for (byte i = 0; i < elements.Length; i++)
                    {
                        Console.Write("{0}", element[i]);
                        if (i < elements[i].Length - 1)
                        {
                            Console.Write(" * ");
                        }
                        else
                        {
                            Console.WriteLine(" = {0}", products[j++]);
                        }
                    }
                }
            } catch (ArgumentException e)
            {
                Console.WriteLine("Недопустимое значение - {0}", e.Message);
            }
        }

        public static int[] arrayProducts(byte[][] elements)
        {
            int[] products = new int[elements.Length];
            int i = 0;
            foreach (byte[] el in elements)
            {
                products[i] = 1;
                foreach (byte el2 in el)
                {
                    if (el2 != 0)
                    {
                        if (el2 < 100) {
                            try
                            {
                                products[i] = checked((int)el2);
                            } catch
                            {
                                products[i] = -1;
                            }
                        } else
                        {
                            throw new ArgumentOutOfRangeException(String.Format("Недопустимый диапозон числа {0}.", el2));
                        }
                    } else
                    {
                        throw new ArgumentException("Недопустимый элемент массива с нулевым значением.");
                    }
                }
                i++;
            }

            return products;
        }
    }
}
