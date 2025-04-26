using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class Temple
    {
        private int gold;
        float totalBuffATk = 10.0f;
        float totalBuffDef = 10.0f;

        float buffAtkVelue = 0.0f;
        int buffDefVelue = 0;
        public void Selection(Player player)
        {
            // 기능 추가 수정
            Console.WriteLine(" - @@ 신전에 어서오세요 - \n");
            while (true)
            {
                Console.WriteLine("1. 헌금 2. 버프 3. 축복 0. 나가기 \n");

                int.TryParse(Console.ReadLine(), out int input);// 헌금만 해서 돈이 싸이면 버프의 효과가 상승 세례(버프) 축복(상태이상해제)
                if (input > 0 && input <= 3)
                {
                    switch (input)
                    {
                        case 1:
                            Offering(player);
                            break;
                        case 2:
                            player.buff = false;
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
            Console.WriteLine("\n버프를 받으시겠습니까?");
            Console.WriteLine($"현재 남아있는 골드 : {player.Gold} | 차감되는 골드 : 1000G");
            Console.Write("1. YES 2. NO \n>>> ");

            int.TryParse(Console.ReadLine(), out int input);
            if (input == 1)
            {
                if (player.Gold < 1000)
                {
                    Console.WriteLine("돈이 부족하여 버프를 받을 수 없습니다.");
                    return;
                }    
                else
                {
                    player.buff = true;
                    player.Gold -= 1000;
                    if (gold >= 16000)
                    {
                        totalBuffATk *= 5.0f;
                        totalBuffDef *= 5.0f;
                    }
                    else if (gold >= 12000)
                    {
                        totalBuffATk *= 3.5f;
                        totalBuffDef *= 3.5f;
                    }
                    else if (gold >= 80000)
                    {
                        totalBuffATk *= 2.5f;
                        totalBuffDef *= 2.5f;
                    }
                    else if (gold >= 5000)
                    {
                        totalBuffATk *= 2.0f;
                        totalBuffDef *= 2.0f;
                    }
                    else if (gold >= 3000)
                    {
                        totalBuffATk *= 1.5f;
                        totalBuffDef *= 1.5f;
                    }
                    player.TotalAtk += totalBuffATk;
                    player.TotalDef += (int)totalBuffDef;

                    buffAtkVelue += totalBuffATk;
                    buffDefVelue += (int)totalBuffDef;
                }
                Console.WriteLine("버프를 받으셨습니다.\n");
                return;
            }            
            else if (input == 2)
            {
                Buff(player);
            }
            else
            {
                Console.WriteLine("잘못 입력했습니다, 다시 확인해주세요");
            }
        }
        public float BuffAtk(Player player)
        {
             float buffATk = 0.0f;
            if(player.buff)
            {
                buffATk = buffAtkVelue;
            }
            return buffATk;
        }
        public int BuffDef(Player player)
        {
            int buffDef = 0;
            if (player.buff)
            {
                buffDef = buffDefVelue;
            }
            return buffDef;
        }
    }
}
