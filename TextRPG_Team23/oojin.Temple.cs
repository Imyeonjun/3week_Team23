using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class Temple
    {
        private int gold;
        public void Selection(Player player)
        {
            // 기능 추가 수정
            Console.WriteLine(" - @@ 신전에 어서오세요 - \n");
            while (true)
            {
                Console.WriteLine("1. 헌금 2. 세레 3. 축복 0. 나가기 \n");

                int.TryParse(Console.ReadLine(), out int input);// 헌금만 해서 돈이 싸이면 버프의 효과가 상승 세례(버프) 축복(상태이상해제)
                if (input > 0 && input <= 3)
                {
                    switch (input)
                    {
                        case 1:
                            Offering(player);
                            break;
                        case 2:
                            Buff(player);
                            break;
                        case 3:
                            Console.WriteLine("미구현");
                            break;
                    }
                }
                else if (input == 0)
                {
                    Console.WriteLine("마을로 돌아갑니다.");
                    Console.WriteLine("Enter키를 눌러주세요");
                    return;
                }
                else
                {
                    BranchManager.ErrorMessage("잘못된 번호입니다, Enter를 누른 후 다시 입력해주세요.");
                    //Console.WriteLine("잘못 입력하였습니다.");
                }
            }
        }
        private void Offering(Player player)
        {
            Console.Write("현재 가지고 있는 금액 : ");

            Console.WriteLine($"{player.Gold}G");

            Console.WriteLine(" == 헌금을 하시겠습니까? ==\n");
            Console.WriteLine("1. YES 2.NO");

            int.TryParse(Console.ReadLine(), out int input);
            if (input > 0 && input <= 2)
            {
                if (input == 1)
                {
                    Console.WriteLine(" == 헌금 할 금액을 입력해주세요 ==\n");
                    int.TryParse(Console.ReadLine(), out int goldInput);

                    player.Gold -= goldInput;

                    gold += goldInput;
                    Console.WriteLine($"기부된 현재 금액 : {gold}");
                }
                else
                {
                    return;
                }
            }
            else
            {
                BranchManager.ErrorMessage("잘못 입력했습니다, Enter를 누른 후 다시 입력해주세요");
                //Console.WriteLine("잘못 입력했습니다.");
            }
        }
        private void Buff(Player player)
        {

        }
    }
}
