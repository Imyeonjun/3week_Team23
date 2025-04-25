using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class Inn
    {
        public void Selection(Player player, QuestMenu questMenu)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine(" - 어서오세요 - ");
                Console.WriteLine("1. 퀘스트 확인 2. 휴식 0. 나가기");

                int.TryParse(Console.ReadLine(), out int input);
                switch (input)
                {
                    case 1:
                        //Console.Clear();
                        QuestInfo(questMenu, player);
                        break;
                    case 2:
                        //Console.Clear();
                        Rest(player);
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("잘못 입력 했습니다.");
                        break;
                }
            }
        }
        private void Rest(Player player)
        {
            // 만약 day가 있으면 day ++ 
            // 플레이어의 체력이 30%미만 70%회복 60% 미만이면 80% 체력회복 이상이면 100%회복
            double hpRotio = (double)player.CurrentHp / player.MaxHp;
            if (hpRotio <= 0.2)
            {
                player.CurrentHp = (int)(player.MaxHp * 0.5);
            }
            else if (hpRotio > 0.2 && hpRotio <= 0.5)
            {
                player.CurrentHp = (int)(player.MaxHp * 0.7);
            }
            else if (hpRotio > 0.5 && hpRotio <= 0.7)
            {
                player.CurrentHp = (int)(player.MaxHp * 0.9);
            }
            else
            {
                player.CurrentHp = player.MaxHp;
            }
            Console.WriteLine($"💤 하루가 지나 체력을 회복했습니다. 현재 체력 : {player.CurrentHp}");
        }
        private void QuestInfo(QuestMenu questMenu, Player player)
        {
            //while (true)
            //{
                //Console.Clear();
                questMenu.ShowAllQuests(player);
            //}
        }
    }
}