using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Homework10_11
{
    class Check_Knowledge
    {
        private int cV;
        private int cT;
        public Check_Knowledge(int cntVar, int cntTasks)
        {
            Gen_Var.Gen_Vars(cntVar, cntTasks);
            cV = cntVar;
            cT = cntTasks;
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

        public void Check_Test(int num_test)
        {
            Console.WriteLine("Ваш вариант контрольной: ");
            Get_Variant(num_test);
            while (true)
            {
                Console.WriteLine("\nПодумайте и отправте '0' когда будете готовы предоставить ответы или отправте номер вопроса чтобы получить подсказку: ");
                var nums_t = int.Parse(Console.ReadLine());
                if (nums_t == 0)
                    break;
                else 
                    Get_Tip(num_test, nums_t);
            }
            int ind = 1;
            while (ind < cT) 
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
                else { Console.WriteLine("Вы ответили неверно :(("); Console.WriteLine("Верный ответ : " + Get_Answer(num_test, ind));  }
                ind++;
            }
        }
    }
}
