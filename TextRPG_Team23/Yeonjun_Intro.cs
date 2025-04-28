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
            // 이름이 공백도 가능 안되게 수정
            string name = "";

            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("이름을 입력하세요.");
                name = Console.ReadLine();
            }

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
                        BranchManager.ErrorMessage("잘못된 입력입니다.");
                        //Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                        //Console.ReadLine();
                        //Console.Clear();
                        break;
                }
            }
            player = new Player(name, job);

            Console.Clear();
            
            Console.Write($"[ 23 Loop Dungeon ]\n\n" +
                          $"오랜 세월 평화를 지켜온 태양의 왕국은 한명의 폭군에 의해 그 빛을 영원히 잃고 말았다...\n\n" +
                          $"하지만 눈을 가렸다고 한들 태양이 없어지지는 않는 법\n\n" +
                          $"왕국을 구원할 용사가 폭군의 왕좌에 여정을 떠났으니\n\n" +
                          $"왕국의 마지막 희망 그것은 바로 [ {job} {player.Name} ] 당신이다.\n\n\n" +
                          $"게임을 진행하기 위해 아무키나 입력하세요.\n" +
                          $">>>");

            Console.ReadKey();
            


        }
    }
}
