using System;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Battle_Results_Screen
{


    public class BattleResult
    {
        public void BattleResultUI(Player player, bool Victory, int dungeonMonsterCount, int dungeonExp, List<Item> droppedItems)
        {
            // 적용 전 
            int oldLevel = player.Level;
            int oldExp = player.Exp;
            int totalExp = oldExp + dungeonExp;

            if (Victory)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("Victory!");



                // 새 레벨, 경험치 계산
                int newLevel = LevelUp.GetNewLevel(oldLevel, totalExp, out int remainingExp);

                //플레이어 정보 업데이트
                player.Level = newLevel;
                player.Exp = remainingExp;


                Console.WriteLine($"던전에서 몬스터 {dungeonMonsterCount}마리를 잡았습니다.");

                Console.WriteLine("\n[캐릭터 정보]");

                Console.WriteLine($"Lv. {oldLevel} {player.Name} → Lv. {newLevel} {player.Name}");

                Console.WriteLine($"EXP: {oldExp} → {remainingExp}");

                Console.WriteLine($"HP: {player.MaxHp} → {player.CurrentHp}");


                Console.WriteLine("[획득 아이템]");





                foreach (var item in droppedItems)
                {
                    Console.WriteLine($"{item.Name} -{item.Value}");
                    
                }


                Console.WriteLine("\n0. 다음");
                Console.Write(">> ");

                int choiceNumber = CheckInput(0, 0);
                switch (choiceNumber)
                {
                    case 1:

                        break;


                }
            }
            else 
            {
                Console.Clear();
                Console.WriteLine("You Lose");

                
                Console.WriteLine($"Lv. {oldLevel} {player.Name}");
                Console.WriteLine($"{player.MaxHp} → 0");

                Console.WriteLine("0. 다음");                        // 엔딩 크레딧으로 연결
                Console.Write(">> ");
                
                int choiceNumber = CheckInput(0, 0);
                switch (choiceNumber)
                {
                    case 1:

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
        public static int GetNewLevel(int currentLevel, int totalExp, out int remainingExp)
        {
            int exp = totalExp;
            int level = currentLevel;

            while (exp >= GetRequiredExp(level))
            {
                exp -= GetRequiredExp(level);
                level++;
            }

            remainingExp = exp;
            return level;
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



    internal class Program
    {
        static void Main(string[] args)
        {
            BattleResult.BattleResultUI();
        }
    }








}









