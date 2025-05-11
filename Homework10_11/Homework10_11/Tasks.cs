using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework10_11
{
    class Tasks
    {
        private const string PATH_BANK = "../../../files/Task/bank.txt";
        private const string PATH_TIPS = "../../../files/Task/tips.txt";
        private int cV;
        private int cT;
        private Dictionary<int, Task> bank_questions;
        private Dictionary<int, string> tips_bank;

        public Tasks(int cntVar, int cntTasks)
        {
            bank_questions = new Dictionary<int, Task>();
            Get_Tasks_In_File();
            tips_bank = new Dictionary<int, string>();
            Get_Tips();
            cV = cntVar;
            cT = cntTasks;
            Gen_Vars(cntVar, cntTasks);
        }

        private void Get_Tips() 
        {
            using (var sr = new StreamReader(PATH_TIPS))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split('|', StringSplitOptions.RemoveEmptyEntries);
                    if (int.TryParse(line[0], out int id))
                        tips_bank[id] = line[1];
                    else
                       throw new Exception("Incorect file!");
                }
                return;
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

        private void Gen_Vars(int cntVar, int cntTasks) 
        {
            var rnd = new Random();
            for (int i = 0; i < cntVar; i++)
            {
                Directory.CreateDirectory($"../../../files/Var/Var{i+1}");
                using (var fsV = File.Open($"../../../files/Var/Var{i+1}/variant.txt", FileMode.Create))
                using (var fsT = File.Open($"../../../files/Var/Var{i+1}/tipsForVar.txt", FileMode.Create))
                using (var fsAns = File.Open($"../../../files/Var/Var{i+1}/AnsForVar.txt", FileMode.Create))
                using (var swV = new StreamWriter(fsV))
                using (var swT = new StreamWriter(fsT))
                using (var swAns = new StreamWriter(fsAns))
                {
                    var quests = new HashSet<int>();
                    Console.ForegroundColor = ConsoleColor.Green;
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
                                if (new int[] { 20, 21, 22 }.Contains(num))
                                {
                                    swV.WriteLine((num) + "|" + bank_questions[num].СondTask);
                                    swAns.WriteLine((num) + "|" + "РО");
                                    swT.WriteLine((num) + "|" + tips_bank[num]);
                                    quests.Add(num);
                                }
                                else
                                {
                                    swV.WriteLine((num) + "|" + bank_questions[num].СondTask);
                                    swAns.WriteLine((num) + "|" + bank_questions[num].AnsTask);
                                    swT.WriteLine((num) + "|" + tips_bank[num]);
                                    quests.Add(num);
                                }
                                cnt++;
                            }
                            else num = rnd.Next(1, 23);
                        }
                    }
                }
            }
            Console.WriteLine("Варианты сгенерированы)");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void Get_Variant(int num)
        {
            using (var sr = new StreamReader($"../../../files/Var/Var{num}/variant.txt"))
            {
                int num_q = 1;
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split('|');
                    Console.WriteLine(num_q + "." + line[1]);
                    num_q++;
                }
            }
        }

        private void Get_Tip(int num_var, int num_task)
        {
            using (var sr = new StreamReader($"../../../files/Var/Var{num_var}/tipsForVar.txt"))
            {
                int i = 1;
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split('|');
                    if (num_task == i)
                    {
                        Console.WriteLine(line[1]);
                        break;
                    }
                    i++;
                }
            }
        }

        private string Get_Answer(int num_var, int num_task)
        {
            using (var sr = new StreamReader($"../../../files/Var/Var{num_var}/AnsForVar.txt"))
            {
                int i = 1;
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split('|');
                    if (num_task == i)
                    {
                        return line[1];
                    }
                    i++;
                }
                return null;
            }
        }

        private void Check_Test()
        {
            var rnd = new Random();
            var num_test = rnd.Next(1, cV + 1);
            Console.WriteLine("Ваш вариант контрольной: ");
            Get_Variant(num_test);
            while (true)
            {
                int nums_t;
                Console.WriteLine("\nПодумайте и отправте '0' когда будете готовы предоставить ответы или отправте номер вопроса чтобы получить подсказку: ");
                while(true) 
                {
                    if (int.TryParse(Console.ReadLine(), out nums_t))
                    {
                        break;
                    }
                    else Console.WriteLine("Неверный ввод!");
                }
                if (nums_t == 0)
                    break;
                else if (nums_t <= cT)
                {
                    Get_Tip(num_test, nums_t);
                }
                else Console.WriteLine("Неверно введен номер вопроса!");
            }
            int ind = 1;
            while (ind <= cT)
            {
                Console.WriteLine($"Введите ответ на задачу номер {ind}: ");
                var ans = Console.ReadLine();
                if (Get_Answer(num_test, ind) == "РО")
                {
                    Console.WriteLine("Данный тип задания проверяйте по ответу: ");
                    Console.WriteLine(Get_Answer(num_test, ind));
                }
                else if (ans.ToLower() == Get_Answer(num_test, ind))
                    Console.WriteLine("Вы ответили верно!");
                else { Console.WriteLine("Вы ответили неверно :(("); Console.WriteLine("Верный ответ : " + Get_Answer(num_test, ind)); }
                ind++;
            }
        }

        private void Get_Bank_Quest()
        {
            foreach (var key in bank_questions.Keys)
            {
                Console.WriteLine(key + "." + bank_questions[key].СondTask);
            }
        }

        public void CheckKnowledge()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Выберите действие(введите цыфру): \n1.Сгенерировать вариант контрольной. " +
                    "\n2.Начать контрольную с автопроверкой. \n3.Вывести вес возможные вопросы. \n0.Выйти с раздела.");
                string act = "";
                while (true) 
                {
                    act = Console.ReadLine();
                    if (new string[] {"0", "1", "2", "3" }.Contains(act)) break;
                    Console.WriteLine("Неверный ввод! \n Повторите попытку!");
                }
                switch (act) 
                {
                    case "0":
                        flag = false;
                        break;
                    case "1":
                        { 
                            Console.WriteLine("Введите номер варианта: ");
                            int num = 0;
                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out num) && num > 0 && num < cV)
                                    break;
                                Console.WriteLine("Неыерный ввод! Повторите попытку!");
                            }
                            Get_Variant(num);
                        }
                        break;
                    case "2":
                        Check_Test();
                        break;
                    case "3":
                        Get_Bank_Quest();
                        break;
                }
                if (flag)
                {
                    Console.WriteLine("Нажмите Enter чтобы продолжить");
                    Console.ReadLine();
                    Console.Clear();
                }
                else Console.Clear();
            }
        }
    }
}
