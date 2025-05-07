using System.IO;

namespace Homework10_11;
public class Trainer
{
    private string Path { get; }
    private int Theme { get; }
    private Dictionary<string, List<Formula>> Formulas;
    private Dictionary<Formula, int> WrongAnwsers;
    private Dictionary<Formula, int> CorrectAnwsers;

    public Trainer(string path, int theme)
    {
        Path = path;
        Theme = theme;
    }

    public void Training()
    {
        var Bank = GetFormulas(); //Заполняем банк формул
        if (!Bank.ContainsKey("Ошибка")) //Проверям на наличие ошибки
        {
            var Themes = GetThemes(); //Получаем соответствие тем с номерами
            var T = Themes[Theme - 1];
            Console.WriteLine($"Тестирование на тему: {T}");
            var Questions = Formulas[T];
            var queue = new Queue<Formula>();
            var r = new Random();
            while (Questions.Count > 0) //Заполняем очередь 
            {
                var t = r.Next(0, Questions.Count - 1);
                queue.Enqueue(Questions[t]);
                Questions.RemoveAt(t);
            }
            
            Console.WriteLine("Подтвердите готовность - напишите Готов");
            string rr = Console.ReadLine();
            bool Ready = false; // тут ввод
            if (rr == "Готов")
            {
                Ready = true;
                Console.Clear();
                Console.WriteLine($"Тестирование на тему: {T}");
            }
            if (Ready)
            {
                while (queue.Count > 0)
                {
                    var q = queue.Dequeue();
                    q.PrintName();
                    Console.WriteLine("Чтобы увидеть ответ - введите 1"); // тут трупарс
                    if (int.Parse(Console.ReadLine()) == 1)
                    {
                        q.PrintAnwser();
                        Console.WriteLine("Если ответ совпал введите 1, иначе 2"); // и тут ещё
                        if (int.Parse(Console.ReadLine()) == 1)
                        {
                            
                        }

                        if (int.Parse(Console.ReadLine()) == 2)
                        {
                            queue.Enqueue(q);
                        }
                    }
                }
            }
        }
    }

    public Dictionary<string, List<Formula>> GetFormulas()
    {
        Dictionary<string, List<Formula>> F = new Dictionary<string, List<Formula>>();
        
        IEnumerable<string> file = File.ReadLines(Path);
        string k = "";
        List<Formula> ff = new List<Formula>();
        int q = 0;
        foreach (string line in file)
        {
            q++;
            if (!(line == "======") || !(line[0] == '|') || !(line[0] == 'T') || (line == ""))
            {
                Console.WriteLine($"Некорректный файл! Уберите лишние символы на строке {q}.");
                Dictionary<string, List<Formula>> FF = new Dictionary<string, List<Formula>>();
                FF.Add("Ошибка", new List<Formula>());
                return FF;
            }
            if (line == "") continue;
            if (line[0] == 'T') k = line.Substring(1);
            if (line[0] == '|')
            {
                var i = line.Substring(1).Split("|");
                ff.Add(new Formula(i[0], i[1]));
            }
            if (line == "======") F.Add(k, ff);
        }
        
        return F;
    }

    public string[] GetThemes()
    {
        IEnumerable<string> file = File.ReadLines(Path);
        int i = 0;
        foreach (string line in file)
        {
            if (line[0] == 'T') i++;
        }
        string[] themes = new string[i];
        int q = 0;
        foreach (string line in file)
        {
            if (line[0] == 'T')
            {
                themes[q] = line.Substring(1);
                q++;
            }
        }
        return themes;
    }
}