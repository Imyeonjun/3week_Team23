using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class Intro
    {
        static Player player;

        public string[] jobOptions = {
                "전사",
                "마법사"
            };

        public void CreateCharacter(out Player player)
        {
            Console.WriteLine("이름을 입력하세요.");
            string name = Console.ReadLine();

            Job job = null;
            bool selected = false;

            while (!selected)
            {
                Console.WriteLine("\n직업을 선택하세요.");
                int result = BranchManager.ReturnSelect(jobOptions, false, " ");


                switch (result)
                {
                    case 1:
                        job = new Warrior();
                        selected = true;
                        break;
                    case 2:
                        job = new Magician();
                        selected = true;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                        Console.ReadLine();
                        //Console.Clear();
                        break;
                }
            }
            player = new Player(name, job);
        }
    }
}
