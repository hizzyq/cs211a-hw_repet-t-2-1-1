using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10_11
{
    class Gen_Var
    {
        private const string PATH_BANK = "../../../files/Task/bank.txt";
        private const string PATH_TIPS = "../../../files/Task/tips.txt";
        Dictionary<int, Task> bank_questions;

        public Gen_Var()
        {
            bank_questions = new Dictionary<int, Task>();
            Get_Tasks_In_File();
        }

        private string Get_Tips(int numb) 
        {
            using (var sr = new StreamReader(PATH_TIPS))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('|', StringSplitOptions.RemoveEmptyEntries);
                    if (int.TryParse(line[0], out int id))
                    {
                        if (numb == id) return line[1];
                    }
                    else
                    {
                       throw new Exception("Incorect file!");
                       return null;
                    }
                }
                return null;
            }
        }

        private void Get_Tasks_In_File()
        {
            using (var sr = new StreamReader(PATH_BANK))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('|', StringSplitOptions.RemoveEmptyEntries);
                    bank_questions[int.Parse(line[0])] = new Task(line[1], line[2]);
                }
            }
        }

        public void Gen_Vars()
        {
            int cntVar = 0;
            while (true)
            {
                Console.Write("Введите кол-во вариантов:");
                var f = int.TryParse(Console.ReadLine(),out cntVar);
                if (f) break;
                Console.Clear();
            }
            int cntTasks = 0;
            while (true)
            {
                Console.Write("Введите кол-во заданий:");
                var f = int.TryParse(Console.ReadLine(), out cntTasks);
                if (f) break;
                Console.Clear();
            }
            var rnd = new Random();
            for (int i = 0; i < cntVar; i++)
            {
                Directory.CreateDirectory($"../../../files/Var/Var{i+1}");
                using (var fsV = File.Open($"../../../files/Var/Var{i+1}/variant{i + 1}.txt", FileMode.Create))
                using (var fsT = File.Open($"../../../files/Var/Var{i+1}/tipsForVar{i + 1}.txt", FileMode.Create))
                using (var fsAns = File.Open($"../../../files/Var/Var{i+1}/AnsForVar{i + 1}.txt", FileMode.Create))
                using (var swV = new StreamWriter(fsV))
                using (var swT = new StreamWriter(fsT))
                using (var swAns = new StreamWriter(fsAns))
                {
                    var quests = new HashSet<int>();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Вариант номер {i + 1}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    for (int j = 0; j < cntTasks; j++)
                    {
                        fsV.Seek(0, SeekOrigin.End);
                        fsT.Seek(0, SeekOrigin.End);
                        fsAns.Seek(0, SeekOrigin.End);
                        int num = rnd.Next(1, 23);
                        int cnt = 0;
                        while (cnt < 1)
                        {
                            if (!quests.Contains(num))
                            {
                                Console.WriteLine($"Задание номер {j + 1} - {bank_questions[num].СondTask}");
                                swV.WriteLine((j+1) + "." + " " + bank_questions[num].СondTask);
                                swAns.WriteLine((j + 1) + "." + " " + bank_questions[num].AnsTask);
                                swT.WriteLine((j + 1) + "." + " " + Get_Tips(num));   
                                quests.Add(num);
                                cnt++;
                            }
                            else num = rnd.Next(1, 23);
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
