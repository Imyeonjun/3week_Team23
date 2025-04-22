using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Battle_Results_Screen
{


    public static class BattleResult

    {
        public static List<string> monster = new List<string>();



         public static void BattleResultUI()
        {

            bool bol = true;
            int dungeonHp = 50;
            int playerHp = 100;
            string playerName = "chan";

            int[] dungeonMonster = { 1, 2, 3, 4, 5, 6, 7, 8 };

            int[] monsterLevels = { 2, 3, 5 };


            int sumMonsterLevel = monsterLevels.Sum();
            
            int dungeonExp = sumMonsterLevel;

            int playerLevel = 1;
            int playerExp = 8;
            int totalExp = playerExp + dungeonExp;
            
            int remaining;



            if (bol)
            {
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("Victory");




                Console.WriteLine($"던전에서 몬스터 {dungeonMonster.Length}마리를 잡았습니다.");  //던전의 몬스터 정보, 던전 몬스터의 정보를 세어서 적용


                Console.WriteLine("[캐릭터 정보]");

                



                Console.WriteLine($"Lv. {playerLevel}. {playerName} ->  {LevelUp.GetNewLevel(playerLevel,totalExp, out remaining)}. {playerName}");   // 플레이어 네임 필요, 레벨 클래스 만들기  LevelUp
                playerExp = remaining;
                Console.WriteLine(playerExp);
                playerLevel = LevelUp.GetNewLevel(playerLevel, totalExp, out remaining);
                Console.WriteLine(playerLevel);




                Console.WriteLine($"HP {playerHp} -> {dungeonHp}");     // 현재 hp 를 보여주고 던전 이후 hp 를 보여준다. 
                playerHp = dungeonHp;
                Console.WriteLine(playerHp);




                Console.WriteLine($"exp {playerExp - dungeonExp} -> {playerExp} ");   // 몬스터 레벨에 대한 정보가 필료하고, 현재 플레이어 exp 와 던전 이후 플레이어 exp가 필요 하다. 


                Console.WriteLine("\n[획득 아이템]");

                Console.WriteLine("500 Gold");                                                // 몬스터의 드롭 아이템에 대한 정보가 필요 하거나 던전 드롭 아이템에 대한 정보 필요 
                Console.WriteLine("포션 - 1");
                Console.WriteLine("낡은검 - 1");

                Console.WriteLine("0. 다음");                                                 // 메인 스크린으로 연결한다. 
                Console.WriteLine(">>");
                Console.ReadLine();
            }



            else
            {

                Console.WriteLine("You Lose\n");
                Console.WriteLine($"{playerLevel} .{playerName}");
                Console.WriteLine($"HP 100 -> 0");
                Console.WriteLine("\n0. 다음");                           // 엔딩 크레딧 으로  연결 
                Console.Write(">> ");
                Console.ReadLine();

            }


        }
    }


    public static class LevelUp
    {

        public static int GetRequiredExp(int level)
        {
            if (level == 1)
            {
                return 10;
            }
            else if (level == 2)
            {
                return 35;
            }
            else if (level == 3)
            {
                return 65;
            }
            else if (level == 4)
            {
                return 100;
            }
            else
            {
                return int.MaxValue;
            }
        }

        // 레벨업 계산: 새 레벨과 남은 경험치를 반환     
        public static int GetNewLevel(int currentLevel, int totalExp, out int aExp)  //totalExp =현재 경험치+ 던전에서 받은 경험치 
        {
            while (totalExp >= GetRequiredExp(currentLevel))
            {
                
                currentLevel++;
            }

            aExp = totalExp;
            return currentLevel;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            BattleResult.BattleResultUI();
        }
    }



}
