
using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Team23_Dungeon;


namespace Team23_Dungeon
{

    

    class BattleResult
    {


        public static void BattleResultUI(Player player, int dungeonMonsterCount, List<Monster> monsters)  // int dungeonExp 받아와야함(1)
        {
            // 적용 전 
            int oldLevel = player.Level;
            int oldExp = player.Exp;
            int monsterLevelSum = 0;

            for (int i = 0; i < monsters.Count; i++)
            {
                monsterLevelSum += monsters[i].Level;
            }

            int totalExp = monsterLevelSum + player.Exp;
            int plyerGold = player.Gold;

            bool isVictory = player.CurrentHp > 0;

            if (isVictory)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("Victory!");



                // 레벨, 경험치 
                int newLevel = LevelUp.NewLevel(oldLevel, totalExp, out int remainingExp);

                //플레이어 정보 업데이트
                player.Level = newLevel;
                player.Exp = remainingExp;


                Console.WriteLine($"던전에서 몬스터 {dungeonMonsterCount}마리를 잡았습니다.");

                Console.WriteLine("\n[캐릭터 정보]");

                Console.WriteLine($"Lv. {oldLevel} {player.Name} → Lv. {newLevel} {player.Name}");

                Console.WriteLine($"EXP: {oldExp} → {remainingExp}");

                Console.WriteLine($"HP: {player.MaxHp} → {player.CurrentHp}");






                Console.WriteLine("\n0. 다음");
                Console.Write(">> ");

                int choiceNumber = CheckInput(0, 0);
                switch (choiceNumber)
                {
                    case 0:
                        Program.ShowMainMenu();
                        break;


                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("");
                Console.WriteLine("You Lose");
                Console.WriteLine("");

                Console.WriteLine($"Lv. {oldLevel}  {player.Name}");
                Console.WriteLine($"HP {player.MaxHp} → 0");
                Console.WriteLine("");

                Console.WriteLine("0. 다음");                        // 엔딩 크레딧으로 연결
                Console.Write(">> ");

                int choiceNumber = CheckInput(0, 0);
                switch (choiceNumber)
                {
                    case 0:
                        End.Eending();
                        break;


                }


            }
        }

        static int CheckInput(int min, int max)
        {
            int result;
            while (true)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out result);
                if (isNumber)
                {
                    if (result >= min && result <= max)
                        return result;
                }
                Console.WriteLine("잘못된 입력입니다!!!!");
            }
        }
    }

    public static class LevelUp
    {

        public static int MaxExp(int level)
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

           
        public static int NewLevel(int currentLevel, int totalExp, out int remainingExp)
        {
            int exp = totalExp;
            int level = currentLevel;

            while (exp >= MaxExp(level))
            {
                exp -= MaxExp(level);
                level++;
            }

            remainingExp = exp;
            return level;
        }
    }

    public class End
    {

        public static void Eending()
        {


            Console.Clear();
            Console.WriteLine("               게임이 종료되었습니다. ");
            Console.WriteLine("");
            Console.WriteLine("                    팀   :  23조 ");
            Console.WriteLine("");
            Console.WriteLine("                       제작 ");
            Console.WriteLine("");
            Console.WriteLine("메인 메뉴                             : 임연준 (팀장)");
            Console.WriteLine("던전, 몬스터, 전투시스템              : 이명준");
            Console.WriteLine("Player 클래스, 상태보기, Quest 클래스 : 문승준");
            Console.WriteLine("아이템, 인벤토리, 상점                : 박지환");
            Console.WriteLine("아이템 강화, 여관, 신전               : 차우진");
            Console.WriteLine("레벨업기능, 보상추가, 전투결과        : 이창선");




        }

    }


    internal class Battle_Results_Screen
    {
        static void Main(string[] args)
        {
           
              
            
        }
    }

    
    //class LevelUp2
    //{
    //    public static int GetRequiredExp(int level)
    //    {
    //        if (level == 1)
    //        {
    //            return 10;
    //        }
    //        else if (level == 2)
    //        {
    //            return 35;
    //        }
    //        else if (level == 3)
    //        {
    //            return 65;
    //        }
    //        else if (level == 4)
    //        {
    //            return 100;
    //        }
    //        else
    //        {
    //            return int.MaxValue;
    //        }
    //    }
    //    public void firstSetting()
    //    {
    //        Level = 1;

    //    }
    //    public int Level { get; private set; }

    //    private int exp;

    //    public int Exp
    //    {
    //        get { return exp; }
    //        set
    //        {

    //            if (value > 0)
    //            {
    //                exp += value;

    //                if (Level < 10)
    //                {
    //                    while (exp >= GetRequiredExp(Level) && Level < 10 ) 
    //                    {

    //                        exp -= GetRequiredExp(Level);
    //                        Level++;

    //                    }
    //                    if (Level == 10)
    //                    {
    //                        Console.WriteLine("최대 레벨입니다.");
    //                        exp = 0;
    //                    }
    //                }
    //                else
    //                {
    //                    Console.WriteLine("최대 레벨입니다.");
    //                }
    //            }


    //        }
    //    }

    //}



    


}
