﻿using System;
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
        public int Gold { get { return gold; } set { gold = value; } }

        float buffAtkVelue = 0.0f;
        int buffDefVelue = 0;

        public static bool isBuff = false;


        public void Selection(Player player)
        {
            // 기능 추가 수정
           
            while (true)
            {
                Console.Clear();
                Console.WriteLine(" == @@ 신전에 어서오세요 ==");
                Console.Write("\n1. 헌금\n2. 버프\n0. 나가기 \n\n선택 >> ");

                int.TryParse(Console.ReadLine(), out int input);// 헌금만 해서 돈이 싸이면 버프의 효과가 상승 세례(버프) 축복(상태이상해제)
                if (input > 0 && input <= 3)
                {
                    switch (input)
                    {
                        case 1:
                            Console.Clear();
                            Offering(player);
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.Clear();
                            Buff(player);
                            Console.ReadKey();
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
                    Console.WriteLine("잘못된 번호입니다, 다시 입력해주세요.");
                    break;
                }
            }
        }
        private void Offering(Player player)
        {
            while (true)
            {
                Console.WriteLine(" == 헌금을 하시겠습니까? ==");
                Console.WriteLine($"\"현재 가지고 있는 금액 :{player.Gold}G\n");
                Console.WriteLine("헌금 했을 시 얻는 해택 정보");
                Console.WriteLine("3000G : 공격력 / 방어력 15↑\n5000G : 공격력 / 방어력 20↑\n8000G : 공격력 / 방어력 25↑\n12000G : 공격력 / 방어력 35↑\n16000G : 공격력 / 방어력 50↑");
                Console.Write("1. YES\n2. NO \n\n선택 >> ");

                int.TryParse(Console.ReadLine(), out int input);
                if (input > 0 && input <= 2)
                {
                    if (input == 1)
                    {
                        Console.Clear();
                        Console.Write($" == 헌금 할 금액을 입력해주세요 ==\n>> ");
                        int.TryParse(Console.ReadLine(), out int goldInput);

                        player.Gold -= goldInput;

                        gold += goldInput;
                        Console.WriteLine($"기부된 현재 금액 : {gold}");
                    }
                    else if(input == 2)
                    {
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("\n잘못 입력했습니다, 다시 입력해주세요\n");
                }
            }
           
        }
        private void Buff(Player player)
        {
            Console.WriteLine("\n버프를 받으시겠습니까?");
            Console.WriteLine($"현재 남아있는 골드 : {player.Gold} | 차감되는 골드 : 1000G");
            Console.Write("1. YES\n2. NO \n\n선택 >> ");

            int.TryParse(Console.ReadLine(), out int input);
            if (input == 1)
            {
                if (player.Gold < 1000 || isBuff == true)
                {
                    Console.WriteLine("돈이 부족하거나 이미 버프가 있으셔서 버프를 받으실 수 없습니다.\n");
                    return;
                }    
                else
                {
                    player.Gold -= 1000;
                    if (gold >= 16000)
                    {
                        player.BuffAtk = 50;
                        player.BuffDef = 40;
                        isBuff = true;
                    }
                    else if (gold >= 12000)
                    {
                        player.BuffAtk = 35;
                        player.BuffDef = 28;
                        isBuff = true;
                    }
                    else if (gold >= 8000)
                    {
                        player.BuffAtk = 25;
                        player.BuffDef = 18;
                        isBuff = true;
                    }
                    else if (gold >= 5000)
                    {
                        player.BuffAtk = 20;
                        player.BuffDef = 13;
                        isBuff = true;
                    }
                    else if (gold >= 3000)
                    {
                        player.BuffAtk = 15;
                        player.BuffDef = 10;
                        isBuff = true;
                    }
                    else if(gold < 3000 && gold >= 0)
                    {
                        player.BuffAtk = 10;
                        player.BuffDef = 5;
                        isBuff = true;
                    }
                }
                Console.WriteLine("\n버프를 받았습니다.");
                return;
            }            
            else if (input == 2)
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못 입력했습니다, 다시 입력해주세요");
            }
        }
    }
}
