using OOP;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OOP_lab
{
    /// <summary>
    /// Задача 12 - работа с файлами
    /// </summary>
    class Task12 : Task
    {
        public Task12()
        {
            number = 'd';
            description = "Работа с файлами.";
        }

        public override void Start()
        {

            List<Date> Dates = new List<Date>();
            while (true)
            {
                Console.WriteLine("Выберите пункт:");
                Console.WriteLine("1. Ввод новых дат в ручную.");
                Console.WriteLine("2. Ввод новых дат из файла.");
                Console.WriteLine("3. Удаление даты.");
                Console.WriteLine("4. Дата через номер дня в году.");
                Console.WriteLine("5. Поиск наибольшой даты.");
                Console.WriteLine("6. Сортировка даты по полной дате.");
                Console.WriteLine("7. Сортировка даты по году.");
                Console.WriteLine("8. Сохранить данные в файл.");
                Console.WriteLine("9. Вывод дат.");
                Console.WriteLine("10. Выход.");

                byte option = byte.Parse(Console.ReadLine());
                Console.WriteLine();

                // Ввод новых студентов
                if (option == 1)
                {
                    bool ForceTestArguments = false;
                    while (true)
                    {
                        if (!Program.TEST_MODE)
                        {
                            try
                            {
                                Dates.Add(Date.FromKeyboard());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Не удалось распознать введённые значения. Будут использованы значения по умолчанию. " + e.Message);
                                ForceTestArguments = true;
                            }
                        }

                        if (Program.TEST_MODE || ForceTestArguments)
                        {
                            try
                            {
                                Random rnd = new Random();
                                Date date = new Date(Program.RandomDate());
                                Dates.Add(date);
                                Console.WriteLine();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Произошло ошибка в ходе работе с классом ДАТА. " + e.Message);
                            }

                        }

                        Console.WriteLine("Продолжить ввод? [Д,Y,н,n]");
                        if (!Console.ReadKey().KeyChar.Equals('Д') && !Console.ReadKey().KeyChar.Equals('Y'))
                        {
                            break;
                        }
                        Console.WriteLine();
                    }
                    DumpDates(Dates);
                }
                // Ввод новых дат из файла
                else if (option == 2)
                {
                    Console.WriteLine("Введите путь до файла (по умолчанию './input.txt'):");
                    string path = Console.ReadLine();

                    if (path.Trim() == "")
                    {
                        path = "./input.txt";
                    }

                    try
                    {
                        Dates = Date.FromFile(path);
                        DumpDates(Dates);
                    } catch (ArgumentException)
                    {
                        Console.WriteLine("Файл не существует или повреждён.");
                    }
                }
                // Удаление студента.
                else if (option == 3)
                {
                    Console.WriteLine("Для удаления даты введите её:");
                    Dates = Date.RemoveDate(Dates, Console.ReadLine());
                    DumpDates(Dates);
                }
                // Дата через номер дня в году
                else if (option == 4)
                {
                    Console.WriteLine("Введите номер дня:");
                    short number = short.Parse(Console.ReadLine());

                    Console.WriteLine("Введите номер года:");
                    short year = short.Parse(Console.ReadLine());

                    Date date = Date.GetDate(number, year);
                    Dates.Add(date);
                    date.Dump();
                }
                // Поиск поздней даты
                else if (option == 5)
                {
                    Date.FindOlder(Dates).Dump();
                }
                // Сортировка дат по полной дате
                else if (option == 6)
                {
                    Dates.Sort();
                    DumpDates(Dates);
                }
                // Сортировка даты по году
                else if (option == 7)
                {
                    // Как иначе?
                    Dates.Sort((x, y) => x.Year.CompareTo(y.Year));
                    DumpDates(Dates);
                }
                // Сохранить данные в файл
                else if (option == 8)
                {
                    Console.WriteLine("Введите путь до файла (по умолчанию './input.txt'):");
                    string path = Console.ReadLine();

                    if (path.Trim() == "")
                    {
                        path = "./input.txt";
                    }

                    Date.DumpFile(Dates, path);

                    Console.WriteLine("Данные сохранены");
                }
                // Вывод всех дат
                else if (option == 9)
                {
                    DumpDates(Dates);
                }

                // Выход
                else if (option == 10)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Пункт не найден.");
                }
                Console.WriteLine();
            }

        }

        /// <summary>
        /// Выводит массив дат в консоль
        /// </summary>
        /// <param name="dates"></param>
        static void DumpDates(List<Date> dates)
        {

            Console.WriteLine("Введённые данные:");
            foreach (Date date in dates)
            {
                date.Dump();
            }

        }

        /// <summary>
        /// Класс для работы с датой
        /// </summary>
        class Date : IComparable, IComparer
        {
            /// <summary>
            /// Число
            /// </summary>
            private byte day;

            /// <summary>
            /// Месяц
            /// </summary>
            private byte month;

            /// <summary>
            /// Год
            /// </summary>
            private short year;

            public byte Day
            {
                get
                {
                    return this.day;
                }
                set
                {
                    if (value >= 1 && value <= 32)
                    {
                        byte days = (byte)(28 + ((0x3bbeecc >> (((int)this.month) * 2)) & 3));

                        if (value > days)
                        {
                            this.day = 1;
                            this.Month = ++this.month;
                        }
                        else
                        {
                            this.day = value;
                        }

                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Day should be beetwen 0 and 31.");
                    }
                }
            }

            public byte Month
            {
                get
                {
                    return this.month;
                }
                set
                {
                    if (value >= 1 && value <= 12)
                    {
                        if (value == 12)
                        {
                            this.month = 0;
                            this.Year = ++this.Year;
                        }
                        else
                        {
                            this.month = value;
                        }
                        this.month = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Month should be beetwen 0 and 11.");
                    }
                }
            }

            public short Year
            {
                get
                {
                    return this.year;
                }
                set
                {
                    if (value >= 1900)
                    {
                        this.year = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Year should be more than 1900.");
                    }
                }
            }

            public static Date operator ++(Date date)
            {
                date.Day = ++date.day;

                return date;
            }

            public static bool operator >(Date date1, Date date2)
            {
                return date1.CompareTo(date2) == 1;
            }

            public static bool operator <(Date date1, Date date2)
            {
                return date2.CompareTo(date1) == 1;
            }

            public static bool operator ==(Date date1, Date date2)
            {
                return date1 is Date && date2 is Date && date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
            }

            public static bool operator !=(Date date1, Date date2)
            {
                return !(date1 == date2);
            }

            /// <summary>
            /// Сравнение даты с объектом
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(Object obj)
            {
                return obj is Date && this == (Date)obj;
            }

            public override int GetHashCode()
            {
                return Tuple.Create(this.day, this.month, this.year).GetHashCode();
            }

            /// <summary>
            /// Сравнение дат
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                Date date = obj as Date;
                if (date != null)
                {
                    if (this.Equals(obj))
                    {
                        return 0;
                    }
                    else
                    {
                        if (this.Year == date.Year)
                        {
                            if (this.Month == date.Month)
                            {
                                return this.Day == date.Day ? 0 : (this.Day > date.Day ? 1 : -1);
                            }
                            else
                            {
                                return this.Month == date.Month ? 0 : (this.Month > date.Month ? 1 : -1);
                            }
                        }
                        else
                        {
                            return this.Year == date.Year ? 0 : (this.Year > date.Year ? 1 : -1);
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Object is not a Date");
                }
            }

            /// <summary>
            /// Сравнение дат
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            /// <returns></returns>
            public int Compare(object x, object y)
            {
                // Оправданно?
                if (x.GetType() == typeof(Date))
                {
                    // DRY принцип
                    return ((Date)x).CompareTo(y);
                }
                else if (y.GetType() == typeof(Date))
                {
                    return ((Date)y).CompareTo(x);
                }

                return 0;
            }

            public static explicit operator string(Date date)
            {
                return string.Format("{0}.{1}.{2}", date.Day, date.Month, date.Year);
            }

            public Date(byte day, byte month, short year)
            {
                this.SetDate(day, month, year);
            }

            public Date(byte day, byte month)
            {
                this.SetDate(day, month, (short)DateTime.Now.Year);
            }


            public Date(DateTime date)
            {
                this.SetDate(date);
            }

            private Date()
            {
                this.SetDate(DateTime.Now);
            }

            // Оправдано введение дополнительной функции?
            public void SetDate(byte day, byte month, short year)
            {
                this.Day = day;
                this.Month = month;
                this.Year = year;
            }

            public void SetDate(DateTime date)
            {
                this.Day = (byte)date.Day;
                this.Month = (byte)date.Month;
                this.Year = (short)date.Year;
            }

            public static Date FromKeyboard()
            {
                Date date = new Date();

                Console.WriteLine("Введите число:");
                date.Day = byte.Parse(Console.ReadLine());

                Console.WriteLine("Введите месяц:");
                date.Month = byte.Parse(Console.ReadLine());

                Console.WriteLine("Введите год:");
                date.Year = short.Parse(Console.ReadLine());

                return date;
            }

            /// <summary>
            /// Ввод дат из файла
            /// </summary>
            /// <param name="path">Полный путь до файла</param>
            /// <returns>Список экземпляров класса Date</returns>
            /// <exception cref="ArgumentException"></exception>
            public static List<Date> FromFile(string path)
            {
                List<Date> dates = new List<Date> { };

                try
                {
                    StreamReader f1 = new StreamReader(path); // входной поток
                    string st;
                    string[] buf;
                    while ((st = f1.ReadLine()) != null) // чтение из файла
                    {
                        try
                        {
                            buf = st.Split(' ');
                            dates.Add(new Date(byte.Parse(buf[0]), byte.Parse(buf[1]), short.Parse(buf[2])));
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    f1.Close();

                }
                catch
                {
                    // Уместно throw внутри catch?
                    throw new ArgumentException("File at the given path not found or corrupted.");
                }

                return dates;
            }

            public void Dump()
            {
                Console.WriteLine((string)this);
            }

            /// <summary>
            /// Запись массива дат в файл
            /// </summary>
            /// <param name="dates">Список дат</param>
            /// <param name="path">Полный путь до файла</param>
            public static void DumpFile(List<Date> dates, string path)
            {
                try
                {
                    StreamWriter f = new StreamWriter(path);

                    foreach (Date date in dates)
                        f.WriteLine(string.Format("{0} {1} {2}", date.Day, date.Month, date.Year));

                    f.Close();
                }
                catch (FileNotFoundException e)
                { 
                    Console.WriteLine(e.Message); Console.WriteLine("Проверьте правильность имени файла"); 
                }
                catch (Exception e)
                { 
                    Console.WriteLine("Error: " + e.Message); 
                }
            }

            public static Date GetDate(short number, short year)
            {
                byte Month = 1;
                byte DaysInMonth = GetDaysInMonth(Month);

                while (number > DaysInMonth)
                {
                    number -= DaysInMonth;
                    DaysInMonth = GetDaysInMonth(++Month);
                }

                return new Date((byte)number, Month, year);
            }

            public static Date FindOlder(List<Date> dates)
            {
                Date Older = dates[0];

                foreach (Date Date in dates)
                {
                    if (Date > Older)
                    {
                        Older = Date;
                    }
                }

                return Older;
            }

            public static List<Date> RemoveDate(List<Date> dates, string date)
            {
                for (int i = 0; i < dates.Count; i++)
                {
                    if (((string)dates[i]).Equals(date))
                    {
                        dates.RemoveAt(i);
                        break;
                    }
                }

                return dates;
            }

            public static byte GetDaysInMonth(byte month)
            {
                byte days = 0;

                if (month < 1 || month > 12)
                {
                    throw new ArgumentOutOfRangeException();
                }

                switch (month + 1)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        days = 31;
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        days = 30;
                        break;
                    case 2:
                        days = 28;
                        break;
                }

                return days;
            }
        }
    }
}
