using ClassLibrary1;
using lab;

internal class Program
{
    /// <summary>
    /// добавление случайного человека/людей (Person) в таблицу
    /// </summary>
    /// <param name="hTable"></param>
    public static void AddRandomPeople(ref HTable<Person> hTable, int numberOfElementsAdd)
    {
        for (int i = 0; i < numberOfElementsAdd; i++)
        {
            Person p = new Person();
            p.RandomInit();
            int initialSize = hTable.Size;
            hTable.Add(p);
            //if (hTable.Size > initialSize)
            //    Console.WriteLine("В хэш таблице недостаточно мест, она увеличена");
            //Console.WriteLine("Элемент был добавлен.");
        }
    }

    /// <summary>
    /// вывод полученной коллекции
    /// </summary>
    /// <param name="coll"></param>
    public static void PrintCollection(IEnumerable<PointHTable<Person>> coll)
    {
        foreach (PointHTable<Person> p in coll)
            Console.WriteLine(p.key + ": " + p.data);
    }

    /// <summary>
    /// вывод полученной коллекции
    /// </summary>
    /// <param name="coll"></param>
    public static void PrintCollection(IEnumerable<Person> coll)
    {
        foreach (Person p in coll)
            Console.WriteLine(p);
    }

    /// <summary>
    /// очистка консоли + вывод хэш таблицы
    /// </summary>
    /// <param name="hTable"></param>
    static void ClearAll(HTable<Person> hTable)
    {
        Console.WriteLine("\nНажмите клавишу Enter для просмотра следующих запросов");
        var key = Console.ReadKey();
        if (key.Key == ConsoleKey.Enter)
        {
            Console.Clear();
            hTable.PrintHTable();
        }
    }

    /// <summary>
    /// Поиск женщин в хэш таблице (Выборка с помощью LINQ)
    /// </summary>
    /// <param name="hTable"></param>
    public static void SearchFemalesLINQ(HTable<Person> hTable)
    {
        var females = from item in hTable.table
                    where item != null
                    where item.data.Gender == "Женский"
                    select item;
        PrintCollection(females);
    }

    /// <summary>
    /// Поиск женщин в хэш таблице (Выборка с помощью метода расширения
    /// </summary>
    /// <param name="hTable"></param>
    public static void SearchFemalesExtensionMethod(HTable<Person> hTable)
    {
        var females = hTable.table.Select(x => x).
            Where(x => x != null).
            Where(x => x.data.Gender == "Женский");
        PrintCollection(females);
    }

    /// <summary>
    /// Подсчет всех учеников мужского пола (Cчетчик с помощью LINQ)
    /// </summary>
    /// <param name="hTable"></param>
    public static void CountMalesUnder30LINQ(HTable<Person> hTable)
    {
        int countPeople = (from item in hTable.table
                      where item != null
                      where item.data.Age < 30
                      where item.data.Gender == "Мужской"
                      select item).Count();
        Console.WriteLine(countPeople);
    }

    /// <summary>
    /// Подсчет всех учеников мужского пола (Cчетчик с помощью метода расширения)
    /// </summary>
    /// <param name="hTable"></param>
    public static void CountMalesUnder30ExtensionMethod(HTable<Person> hTable)
    {
        int countPeople = (hTable.table.Select(x => x).
            Where(x => x != null).
            Where(x => x.data.Gender == "Мужской").
            Where(x => x.data.Age < 30)).Count();
        Console.WriteLine(countPeople);
    }

    /// <summary>
    /// Поиск пересечений 1 и 2 хэш таблицы (Операция над множествами с помощью LINQ)
    /// </summary>
    /// <param name="hTable1"></param>
    /// <param name="hTable2"></param>
    public static void InterSectPeopleGroup12LINQ(HTable<Person> hTable1, HTable<Person> hTable2)
    {
        var people = from item in hTable1.Intersect(hTable2)
                     where item != null
                     select item;
        PrintCollection(people);
    }

    /// <summary>
    /// Поиск пересечений 1 и 2 хэш таблицы (Операция над множествами с помощью метода расширения)
    /// </summary>
    /// <param name="hTable1"></param>
    /// <param name="hTable2"></param>
    public static void InterSectPeopleGroup12ExtensionMethod(HTable<Person> hTable1, HTable<Person> hTable2)
    {
        var people = hTable1.Select(x => x).
            Where(x => x != null).
            Intersect(hTable2);
        PrintCollection(people);
    }

    /// <summary>
    /// Максимальный возраст среди женского пола (Агрегирование данных с помощью LINQ)
    /// </summary>
    /// <param name="hTable"></param>
    public static void MaxAgeFemalesLINQ(HTable<Person> hTable)
    {
        int maxAge = (from item in hTable.table
                           where item != null
                           where item.data.Gender == "Женский"
                           select item.data.Age).Max();
        Console.WriteLine(maxAge);
    }

    /// <summary>
    /// Максимальный возраст среди женского пола (Агрегирование данных с помощью метода расширения
    /// </summary>
    /// <param name="hTable"></param>
    public static void MaxAgeFemalesExtensionMethod(HTable<Person> hTable)
    {
        int maxAge = (hTable.table.Select(x => x).
            Where(x => x != null).
            Where(x => x.data.Gender == "Женский").
            Select(x => x.data.Age)).Max();
        Console.WriteLine(maxAge);
    }

    /// <summary>
    /// Группировка людей по именам (Группировака данных с помощью LINQ)
    /// </summary>
    /// <param name="hTable"></param>
    public static void MakeGroupsByAgeLINQ(HTable<Person> hTable)
    {
        var people = from item in hTable.table
                     group item by item.data.Name;
        foreach (IGrouping<string,PointHTable<Person>> item in people)
        {
            Console.WriteLine(item.Key);
            foreach (var st in item)
            {
                Console.WriteLine(st);
            }
        }
    }

    /// <summary>
    /// Группировка людей по именам (Группировака данных с помощью метода расширения)
    /// </summary>
    /// <param name="hTable"></param>
    public static void MakeGroupsByAgeExtensionMethod(HTable<Person> hTable)
    {
        var people = hTable.table.Select(x => x).
                GroupBy(x => x.data.Name);
        foreach (IGrouping<string, PointHTable<Person>> item in people)
        {
            Console.WriteLine(item.Key);
            foreach (var st in item)
            {
                Console.WriteLine(st);
            }
        }
    }

    private static void Main(string[] args)
    {
        //инициализация хэш таблицы
        HTable<Person> hTable = new HTable<Person>(10);
        AddRandomPeople(ref hTable, 19);
        hTable.PrintHTable();

        //выборка
        Console.WriteLine("\nПоиск женщин в хэш таблице (Выборка с помощью LINQ)"); //LINQ
        SearchFemalesLINQ(hTable);
        Console.WriteLine("\nПоиск женщин в хэш таблице (Выборка с помощью метода расширения)"); //метод расширения
        SearchFemalesExtensionMethod(hTable);

        //счетчик
        ClearAll(hTable);
        Console.WriteLine("\nПодсчет всех учеников мужского пола (Cчетчик с помощью LINQ)"); //LINQ
        CountMalesUnder30LINQ(hTable);
        Console.WriteLine("\nПодсчет всех учеников мужского пола (Cчетчик с помощью метода расширения)"); //метод расширения
        CountMalesUnder30ExtensionMethod(hTable);

        //создание доп хэш таблицы для поиска пересечений
        Person person = new Person();
        person.RandomInit();
        hTable.Add(person);
        HTable<Person> hTable2 = new HTable<Person>(10);
        AddRandomPeople(ref hTable2, 19);
        hTable2.Add(person);
        ClearAll(hTable);
        Console.WriteLine("\nВторая хэш таблица");
        hTable2.PrintHTable();

        //поиск пересечений
        Console.WriteLine("\nПоиск пересечений 1 и 2 хэш таблицы (Операция над множествами с помощью LINQ)"); //LINQ
        InterSectPeopleGroup12LINQ(hTable, hTable2);
        Console.WriteLine("\nПоиск пересечений 1 и 2 хэш таблицы (Операция над множествами с помощью метода расширения)"); //метод расширения
        InterSectPeopleGroup12ExtensionMethod(hTable, hTable2);

        //агрегирование
        ClearAll(hTable);
        Console.WriteLine("\nМаксимальный возраст среди женского пола (Агрегирование данных с помощью LINQ)"); //LINQ
        MaxAgeFemalesLINQ(hTable);
        Console.WriteLine("\nМаксимальный возраст среди женского пола (Агрегирование данных с помощью метода расширения)"); //метод расширения
        MaxAgeFemalesExtensionMethod(hTable);

        //группировка
        ClearAll(hTable);
        Console.WriteLine("\nГруппировка людей по именам (Группировака данных с помощью LINQ)"); //LINQ
        MakeGroupsByAgeLINQ(hTable);
        Console.WriteLine("\nГруппировка людей по именам (Группировака данных с помощью метода расширения)"); //метод расширения
        MakeGroupsByAgeExtensionMethod(hTable);
    }
}