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
        static private Dictionary<int, Task> bank_questions;
        static private Dictionary<int, string> tips_bank;

        static Gen_Var()
        {
            bank_questions = new Dictionary<int, Task>();
            Get_Tasks_In_File();
            tips_bank = new Dictionary<int, string>();
            Get_Tips();
        }

        static private void Get_Tips() 
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

        static private void Get_Tasks_In_File()
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

        static public void Gen_Vars(int cntVar, int cntTasks) 
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
    }
}
