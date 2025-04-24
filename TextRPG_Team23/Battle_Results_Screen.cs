
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using TextRPG_Team23;


namespace TextRPG_Team23
{



    class BattleResult
    {


        public static void BattleResultUI(Player player, int dungeonMonsterCount, List<Monster> monsters)  
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


            bool isVictory = player.CurrentHp > 0;

            if (isVictory)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("");
                Console.WriteLine("Victory!");
                Console.WriteLine("");



                // 레벨, 경험치 
                int newLevel = LevelUp.NewLevel(oldLevel, totalExp, out int remainingExp);

                //플레이어 정보 업데이트
                player.Level = newLevel;
                player.Exp = remainingExp;


                Console.WriteLine($"던전에서 몬스터 {dungeonMonsterCount}마리를 잡았습니다.");
                Console.WriteLine("");

                Console.WriteLine("\n[캐릭터 정보]");

                Console.WriteLine($"Lv. {oldLevel} {player.Name} → Lv. {newLevel} {player.Name}");

                Console.WriteLine($"EXP: {oldExp} → {remainingExp}");

                Console.WriteLine($"HP: {player.MaxHp} → {player.CurrentHp}");
                Console.WriteLine("");

                Console.WriteLine($"[획득 아이템]");
                int goldGained = BattleReward.GoldReward(player, monsters);
                Console.WriteLine($"획득 골드: {goldGained} G");


                string[] itemNames = BattleReward.ItemReward(player, monsters);
                List<string> itn = new List<string>();

                foreach (string name in itemNames)
                {

                    if (!itn.Contains(name))
                    {

                        int count = 0;
                        foreach (string n in itemNames)
                        {
                            if (n == name)
                                
                                count++;
                        }


                        Console.WriteLine($"{name} - {count}");


                        itn.Add(name);
                    }
                }
                Console.WriteLine("");






                Console.WriteLine("\n0. 다음");
                Console.Write(">> ");

                int choiceNumber = CheckInput(0, 0);
                switch (choiceNumber)
                {
                    case 0:
                        Town.MainMenu();
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
                Console.WriteLine("\n[캐릭터 정보]");
                Console.WriteLine($"Lv. {oldLevel}  {player.Name}");
                Console.WriteLine($"HP {player.MaxHp} → 0");
                Console.WriteLine("");

                Console.WriteLine("0. 다음");                       
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

    class End
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

            GameManager.StopGame();


        }

    }
    public class BattleReward
    {

        public static int GoldReward(Player player, List<Monster> monsters)
        {
            int totalGold = 0;
            foreach (Monster monsterlvl in monsters)
            {

                int reward = monsterlvl.Level * 20;
                totalGold += reward;
            }


            player.Gold += totalGold;
            return totalGold;
        }


        public static string[] ItemReward(Player player, List<Monster> monsters)
        {
            List<Item> drops = new List<Item>();

            foreach (Monster monsternam in monsters)
            {

                switch (monsternam.Name)
                {
                    case "모혈의 이끼거북":
                        drops.Add(ItemDB.Items[5]);
                        drops.Add(ItemDB.Items[4]);
                        break;
                    case "부패의 턱":
                        drops.Add(ItemDB.Items[5]);
                        drops.Add(ItemDB.Items[4]);
                        break;
                    case "뿌리투구 수호자":
                        drops.Add(ItemDB.Items[5]);
                        drops.Add(ItemDB.Items[4]);
                        break;
                    case "일몰 좀도둑":
                        drops.Add(ItemDB.Items[5]);
                        drops.Add(ItemDB.Items[4]);
                        break;
                    case "어스름 예언자":
                        drops.Add(ItemDB.Items[5]);
                        drops.Add(ItemDB.Items[4]);
                        break;
                    case "비안개 정령":
                        drops.Add(ItemDB.Items[5]);
                        drops.Add(ItemDB.Items[4]);
                        break;

                }
            }


            foreach (Item item in drops)
            {
                player.Inventory.AddItem(item);

            }


            var itemName = new string[drops.Count];

            for (int i = 0; i < drops.Count; i++)
            {
                itemName[i] = drops[i].Name;
            }
            return itemName;


        }
    }









    internal class Battle_Results_Screen
    {
      
    }

    






}
