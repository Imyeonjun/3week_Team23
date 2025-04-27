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
            Console.Clear();
            Console.WriteLine("주인장: 숙박하실겁니까?\n\n");

            bool re = false;
            do
            {
                Console.WriteLine(" == 태양의 스튜 여관 ==\n");

                Console.Write("\n1. 퀘스트 확인\n" +
                              "2. 휴식\n" +
                              "0. 나가기\n\n" +
                              "선택 >> ");

                int.TryParse(Console.ReadLine(), out int input);
                if (input > 0 && input <= 2)
                {
                    switch (input)
                    {
                        case 1:
                            QuestInfo(questMenu, player);
                            break;
                        case 2:
                            Rest(player);
                            break;
                    }
                }
                else if (input == 0)
                {
                    Console.WriteLine("마을로 돌아갑니다.");
                    Console.WriteLine("Enter키를 눌러주세요");
                    break;
                }
                else
                {
                    BranchManager.ErrorMessage("잘못 입력했습니다. Enter를 누른 후 다시 입력해 주세요");
                    
                }
            }
            while (!re);
           

            
        }
        private void Rest(Player player)
        {
            Console.Clear();
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
            Console.WriteLine($"하루가 지나 체력을 회복했습니다. 현재 체력 : {player.CurrentHp}");
            
        }
        private void QuestInfo(QuestMenu questMenu, Player player)
        {
            Console.Clear();
            questMenu.ShowAllQuests(player);
            Console.Clear();
            return;
        }
    }
}