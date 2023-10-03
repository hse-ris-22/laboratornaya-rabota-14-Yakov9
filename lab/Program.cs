using ClassLibrary1;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections;

namespace lab
{
    internal class Program
    {
        /// <summary>
        /// рандомайзер для объектов из 10 лабораторной
        /// </summary>
        /// <returns></returns>
        public static Person RandomizePerson()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 4);
            switch (number)
            {
                case 1:
                    Student student = new Student();
                    student.RandomInit();
                    return student;
                case 2:
                    Pupil pupil = new Pupil();
                    pupil.RandomInit();
                    return pupil;
                case 3:
                    PartTimeStudent partTimeStudent = new PartTimeStudent();
                    partTimeStudent.RandomInit();
                    return partTimeStudent;
                default:
                    return new Person();
            }
        }

        /// <summary>
        /// зполнение коллекции случайными элементами
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        public static Stack<Dictionary<int, Person>> CollectionRandomInitPerson(Stack<Dictionary<int, Person>> coll)
        {
            int collectionSize = coll.Count();
            Stack<Dictionary<int, Person>> newStack = new Stack<Dictionary<int, Person>>(collectionSize);
            Person p1 = new Person("Василий", "Мужской", 100); //для задания с пересечением
            for (int i = 0; i < collectionSize; i++)
            {
                Dictionary<int, Person> dict = coll.Pop();
                for (int j = 0; j < 5; j++)
                {
                    Person p = new Person();
                    do
                    {
                        p = RandomizePerson();
                    } while (dict.ContainsKey(p.Age));
                    dict.Add(p.Age, p);
                }
                if (i != 0)
                {
                    dict.Add(p1.Age, p1);
                }    
                newStack.Push(dict);
            }
            return newStack;
        }

        /// <summary>
        /// вывод коллекций IEnumerable
        /// </summary>
        /// <param name="coll"></param>
        public static void PrintCollection(IEnumerable<KeyValuePair<int, Person>> coll)
        {
            foreach (KeyValuePair<int, Person> p in coll)
                Console.WriteLine(p.Key + ": " + p.Value);
        }

        /// <summary>
        /// вывод изначальной коллекции
        /// </summary>
        /// <param name="coll"></param>
        public static void PrintInitialCollection(Stack<Dictionary<int, Person>> coll)
        {
            if (coll.Count <= 0)
            {
                Console.WriteLine("Коллекция пуста");
                return;
            }
            int numberCity = 1;
            foreach (Dictionary<int, Person> dict in coll)
            {
                Console.WriteLine($"\nГруппа {numberCity}");
                numberCity++;
                foreach (KeyValuePair<int, Person> pair in dict)
                    Console.WriteLine(pair.Key + ": " + pair.Value);
            }
        }

        /// <summary>
        /// очистка консоли + вывод коллекции
        /// </summary>
        /// <param name="coll"></param>
        static void ClearAll(Stack<Dictionary<int, Person>> coll)
        {
            Console.WriteLine("\nНажмите клавишу Enter для просмотра следующих запросов");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                PrintInitialCollection(coll);
            }
        }

        /// <summary>
        /// Поиск всех мужчин (выборка с помощью LINQ)
        /// </summary>
        /// <param name="coll"></param>
        public static void SearchMalesLINQ(Stack<Dictionary<int, Person>> coll)
        {
            var males = from item in coll
                        from person in item
                        where person.Value.Gender == "Мужской"
                        select person;
            PrintCollection(males);
        }

        /// <summary>
        /// Поиск всех мужчин (выборка с помощью метода расширения)
        /// </summary>
        /// <param name="coll"></param>
        public static void SearchMalesExtensionMethod(Stack<Dictionary<int, Person>> coll)
        {
            var males = coll.SelectMany(x => x).
                Where(x => x.Value.Gender == "Мужской");
            PrintCollection(males);
        }

        /// <summary>
        /// Подсчет всех студентов, чей возраст больше 20 (счетчик с помощью LINQ)
        /// </summary>
        /// <param name="coll"></param>
        public static void CountStudentsAgeAboveTwentyLINQ(Stack<Dictionary<int, Person>> coll)
        {
            int countS = (from item in coll
                        from person in item
                        where person.Value is Student
                        where person.Value.Age > 20
                        select person).Count();
            Console.WriteLine(countS);
        }

        /// <summary>
        /// Подсчет всех студентов, чей возраст больше 20 (счетчик c помощью метода расширения)
        /// </summary>
        /// <param name="coll"></param>
        public static void CountStudentsAgeAboveTwentyExtensionMethod(Stack<Dictionary<int, Person>> coll)
        {
            int countS = (coll.SelectMany(x => x).
                Where(x => x.Value.Age > 20).Where(x => x.Value is Student)).Count();
            Console.WriteLine(countS);
        }

        /// <summary>
        /// Поиск пересечений 1 и 2 группы (Операция над множествами с помощью LINQ)
        /// </summary>
        /// <param name="dict1"></param>
        /// <param name="dict2"></param>
        public static void InterSectPeopleGroup12LINQ(Dictionary<int, Person> dict1, Dictionary<int, Person> dict2)
        {
            var people = from item in dict1.Intersect(dict2)
                         select item;
            PrintCollection(people);             
        }

        /// <summary>
        /// Поиск пересечений 1 и 2 группы (Операция над множествами с помощью метода расширения)
        /// </summary>
        /// <param name="dict1"></param>
        /// <param name="dict2"></param>
        public static void InterSectPeopleGroup12ExtensionMethod(Dictionary<int, Person> dict1, Dictionary<int, Person> dict2)
        {
            var people = dict1.Select(x => x)
                .Intersect(dict2);
            PrintCollection(people);
        }


        /// <summary>
        /// поиск максимального возраста учеников в коллекции (Агрегирование данных с помощью LINQ)
        /// </summary>
        /// <param name="coll"></param>
        public static void MaxAgeOfPupilsLINQ(Stack<Dictionary<int, Person>> coll)
        {
            int maxAge = (from item in coll
                          from pair in item
                          where pair.Value is Pupil
                          select pair.Value.Age).Max();
            Console.WriteLine(maxAge);
        }

        /// <summary>
        /// поиск максимального возраста учеников в коллекции (Агрегирование данных с помощью метода расширения)
        /// </summary>
        /// <param name="coll"></param>
        public static void MaxAgeOfPupilsExtensionMethod(Stack<Dictionary<int, Person>> coll)
        {
            int maxAge = coll.SelectMany(x => x).
                Where(x => x.Value is Pupil).Select(x => x.Value.Age).Max();
            Console.WriteLine(maxAge);
        }

        /// <summary>
        /// Разбиение по группам студентов по полу (Группировака данных с помощью LINQ)
        /// </summary>
        /// <param name="coll"></param>
        public static void MakeGroupsByAgeLINQ(Stack<Dictionary<int, Person>> coll)
        {
            var people = from item in coll
                         from pair in item
                         where pair.Value is Student
                         group pair by pair.Value.Gender;
            foreach(IGrouping<string, KeyValuePair<int, Person>> item in people)
            {
                Console.WriteLine(item.Key);
                foreach(var st in item)
                {
                    Console.WriteLine(st);
                }
            }
        }

        /// <summary>
        /// Разбиение по группам студентов по полу (Группировака данных с помощью метода расширения)
        /// </summary>
        /// <param name="coll"></param>
        public static void MakeGroupsByAgeExtensionMethod(Stack<Dictionary<int, Person>> coll)
        {
            var people = coll.SelectMany(x => x).
                Where(x => x.Value is Student).
                GroupBy(x => x.Value.Gender);
            foreach (IGrouping<string, KeyValuePair<int, Person>> item in people)
            {
                Console.WriteLine(item.Key);
                foreach (var st in item)
                {
                    Console.WriteLine(st);
                }
            }
        }

        static void Main(string[] args)
        {
            //создание коллекции
            Stack<Dictionary<int, Person>> coll = new Stack<Dictionary<int, Person>>(3);
            Dictionary<int, Person> dict1 = new Dictionary<int, Person>(5);
            Dictionary<int, Person> dict2 = new Dictionary<int, Person>(5);
            Dictionary<int, Person> dict3 = new Dictionary<int, Person>(5);
            coll.Push(dict1);
            coll.Push(dict2);
            coll.Push(dict3);
            coll = CollectionRandomInitPerson(coll);
            PrintInitialCollection(coll);

            //выборка
            Console.WriteLine("\nПоиск всех мужчин (выборка с помощью LINQ)"); //LINQ
            SearchMalesLINQ(coll);
            Console.WriteLine("\nПоиск всех мужчин (выборка с помощью метода расширения)"); //метод расширения
            SearchMalesExtensionMethod(coll);
            ClearAll(coll);

            //подсчет
            Console.WriteLine("\nПодсчет всех студентов, чей возраст больше 20 (счетчик с помощью LINQ)"); //LINQ
            CountStudentsAgeAboveTwentyLINQ(coll);
            Console.WriteLine("\nПодсчет всех студентов, чей возраст больше 20 (счетчик c помощью метода расширения)"); //метод расширения
            CountStudentsAgeAboveTwentyExtensionMethod(coll);
            ClearAll(coll);

            //поиск пересечений
            Console.WriteLine("\nПоиск пересечений 1 и 2 группы (Операция над множествами с помощью LINQ)"); //LINQ
            InterSectPeopleGroup12LINQ(dict1, dict2);
            Console.WriteLine("\nПоиск пересечений 1 и 2 группы (Операция над множествами с помощью метода расширения)");//метод расширения
            InterSectPeopleGroup12ExtensionMethod(dict1, dict2);
            ClearAll(coll);

            //агрегирование
            Console.WriteLine("\nМаксимальный возраст учеников в коллекции (Агрегирование данных с помощью LINQ)"); //LINQ
            MaxAgeOfPupilsLINQ(coll);
            Console.WriteLine("\nМаксимальный возраст учеников в коллекции (Агрегирование данных с помощью метода расширения)"); //метод расширения
            MaxAgeOfPupilsExtensionMethod(coll);
            ClearAll(coll);

            //группировка
            Console.WriteLine("\nРазбиение по группам студентов по полу (Группировака данных с помощью LINQ)"); //LINQ
            MakeGroupsByAgeLINQ(coll);
            Console.WriteLine("\nРазбиение по группам студентов по полу (Группировака данных с помощью метода расширения)");//метод расширения
            MakeGroupsByAgeExtensionMethod(coll);
        }
    }
}