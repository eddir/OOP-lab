using OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_lab
{
    class Task9 : Task
    {

        public Task9()
        {
            number = '9';
            description = "Описание классов (описать класс СТУДЕНТ)";
        }

        public override void Start()
        {
            List<Student> students = new List<Student>();
            while (true)
            {
                Console.WriteLine("Выберите пункт:");
                Console.WriteLine("1. Ввод новых студентов.");
                Console.WriteLine("2. Удаление студента.");
                Console.WriteLine("3. Разница в возрасте.");
                Console.WriteLine("4. Поиск по группам.");
                Console.WriteLine("5. Вывод студентов.");
                Console.WriteLine("6. Выход.");

                byte option = byte.Parse(Console.ReadKey().KeyChar.ToString());
                Console.WriteLine();

                // Ввод новых студентов
                if (option == 1)
                {
                    bool forceTestArguments = false;
                    while (true)
                    {
                        if (!Program.TEST_MODE)
                        {
                            try
                            {
                                students.Add(Student.FromKeyboard());
                            }
                            catch
                            {
                                Console.WriteLine("Не удалось распознать введённые значения. Будут использованы значения по умолчанию.");
                                forceTestArguments = true;
                            }
                        }

                        if (Program.TEST_MODE || forceTestArguments)
                        {
                            try
                            {
                                Random rnd = new Random();
                                Student ss = new Student(Program.RandomFio(), Program.RandomDate(), (rnd.Next() % 100).ToString());
                                students.Add(ss);
                                Console.WriteLine();
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Произошло ошибка в ходе работе с классом СТУДЕНТ. " + e.Message);
                            }

                        }

                        Console.WriteLine("Продолжить ввод? [Д,н]");
                        if (!Console.ReadKey().KeyChar.Equals('Д'))
                        {
                            break;
                        }
                        Console.WriteLine();
                    }
                    DumpStudents(students);
                }
                // Удаление студента.
                else if (option == 2)
                {
                    Console.WriteLine("Для удаления студента введите ФИО:");
                    students = Student.RemoveStudent(students, Console.ReadLine());
                    DumpStudents(students);
                }
                // Разница в возрасте.
                else if (option == 3)
                {
                    Console.WriteLine("Введите ФИО первого студента:");
                    Student student1 = Student.getByFio(students, Console.ReadLine());

                    if (student1 != null)
                    {
                        Console.WriteLine("Введите ФИО второго студента:");
                        Student student2 = Student.getByFio(students, Console.ReadLine());

                        if (student2 != null)
                        {
                            Console.WriteLine("Разница в возрасте {0} лет.", Math.Abs(student1.DifferenceAge(student2)));
                        }
                        else
                        {
                            Console.WriteLine("Не удалось найти такого студента.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не удалось найти такого студента.");
                    }
                }
                // Поиск по группам.
                else if (option == 4)
                {
                    Console.WriteLine("Для поиска по группам введите группу:");
                    foreach (Student student in Student.SearchGroup(students, Console.ReadLine()))
                    {
                        student.Dump();
                    }
                }
                else if (option == 5)
                {
                    DumpStudents(students);
                }
                else if (option == 6)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Пункт не найден.");
                }
                Console.WriteLine();
            }

            static void DumpStudents(List<Student> students)
            {

                Console.WriteLine("Введённые данные:");
                foreach (Student student in students)
                {
                    student.Dump();
                }
            }

        }

        class Student
        {
            private string fio;

            private DateTime birthdate;

            private string group;

            public Student(string fio, DateTime birthdate, string group)
            {
                SetFio(fio);
                SetBirthdate(birthdate);
                SetGroup(group);
            }

            public Student(string first_name, string last_name, string patronymic, DateTime birthdate, string group)
            {
                SetFio(string.Format("{0} {1} {2}", first_name, last_name, patronymic));
                SetBirthdate(birthdate);
                SetGroup(group);
            }

            public Student(string first_name, string last_name, DateTime birthdate, string group)
            {
                SetFio(string.Format("{0} {1}", first_name, last_name));
                SetBirthdate(birthdate);
                SetGroup(group);
            }

            public Student(string first_name, string last_name, int day, int month, int year, string group)
            {
                SetFio(string.Format("{0} {1}", first_name, last_name));
                SetBirthdate(new DateTime(year, month, day));
                SetGroup(group);
            }

            public void SetFio(string fio)
            {

                fio = fio.Trim();

                if (fio.Length < 1)
                {
                    throw new ArgumentException("ФИО не может быть пустым.");
                }

                if (fio.Split(" ").Length < 2)
                {
                    throw new ArgumentException("ФИО должно содержать имя и фамилию, разделённые пробелом.");
                }

                this.fio = fio;
            }

            public void SetBirthdate(DateTime birthdate)
            {
                if (birthdate.Year < DateTime.Now.Year - 100)
                {
                    throw new ArgumentException("Не принимаются люди старше 100 лет.");
                }

                this.birthdate = birthdate;
            }

            public void SetGroup(string group)
            {
                group = group.Trim();

                if (group.Length < 1)
                {
                    throw new ArgumentException("Группа не может быть пустой.");
                }

                this.group = group;
            }

            public string getFio()
            {
                return this.fio;
            }

            public DateTime getBirthdate()
            {
                return this.birthdate;
            }

            public string getGroup()
            {
                return this.group;
            }

            public int DifferenceAge(Student student)
            {
                return this.birthdate.Year - student.birthdate.Year;
            }

            public static List<Student> RemoveStudent(List<Student> students, Student student)
            {
                for (int i = 0; i < students.Count; i++)
                {
                    if (students[i].getFio().Equals(student.getFio()))
                    {
                        students.RemoveAt(i);
                        break;
                    }
                }

                return students;
            }

            public static List<Student> RemoveStudent(List<Student> students, string fio)
            {
                for (int i = 0; i < students.Count; i++)
                {
                    if (students[i].getFio().Equals(fio))
                    {
                        students.RemoveAt(i);
                        break;
                    }
                }

                return students;
            }

            public static List<Student> SearchGroup(List<Student> students, string group)
            {
                group = group.Trim();
                List<Student> found = new List<Student>();

                foreach (Student s in students)
                {
                    if (s.getGroup().Equals(group))
                    {
                        found.Add(s);
                    }
                }
                return found;
            }

            public static Student getByFio(List<Student> students, string fio)
            {
                fio = fio.Trim();
                foreach (Student student in students)
                {
                    if (student.getFio().Equals(fio))
                    {
                        return student;
                    }
                }
                return null;
            }

            public static Student FromKeyboard()
            {
                Console.WriteLine("Введите имя студента:");
                string first_name = Console.ReadLine();

                Console.WriteLine("Введите фамилию студента:");
                string last_name = Console.ReadLine();

                Console.WriteLine("Введите отчество студента:");
                string patronymic = Console.ReadLine();

                DateTime birthdate = new DateTime();

                byte tryCount = 3;
                while (tryCount > 0)
                {
                    try
                    {
                        Console.WriteLine("Введите дату рождения студента:");
                        birthdate = DateTime.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        if (--tryCount > 0)
                        {
                            Console.WriteLine("Не удалось считать данные. Попробуйте ещё раз.");
                            continue;
                        }
                        else
                        {
                            // допустимо исключение внутри catch? Исключение при обработке исключения.
                            throw new ArgumentException("Не удалось распознать ввода даты с клавиаутры.");
                        }
                    }
                    break;
                }

                Console.WriteLine("Введите группу студента:");
                string group = Console.ReadLine();

                return new Student(first_name, last_name, patronymic, birthdate, group);
            }

            public void Dump()
            {
                Console.WriteLine(this.ToString());
            }

            public override string ToString()
            {
                return string.Format("Студент: {0}, дата рождения: {1}, группа: {2}", this.getFio(), this.getBirthdate().Date, this.getGroup());
            }

            public Student FromString(string input)
            {
                string[] parameters = input.Trim().Split(" ");
                if (parameters.Length == 5)
                {
                    // можно ли передать массив параметров как аргументы наподобие оператора ... в Java (остаточные параметры)?
                    return new Student(parameters[0], parameters[1], parameters[2], DateTime.Parse(parameters[3]), parameters[4]);
                }
                else if (parameters.Length == 4)
                {
                    return new Student(parameters[0], parameters[1], DateTime.Parse(parameters[2]), parameters[3]);
                }
                return null;
            }
        }
    }
}
