﻿
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


        public static void BattleResultUI(Player player, List<Monster> monsters, int hp, int step,bool isClear)
        {
            // 적용 전 
            int oldLevel = player.Level;
            int oldExp = player.Exp;

            int monsterLevelSum = 0;

            for (int i = 0; i < monsters.Count; i++)
            {
                monsterLevelSum += monsters[i].Level;
            }


            player.Exp += monsterLevelSum;


            bool isVictory = player.CurrentHp > 0;

            if (isVictory)
            {
                if (monsters[0].Name == "일식의 폭군")
                {
                    Console.Clear();
                    Console.WriteLine("Battle!! - Result");
                    Console.WriteLine("");
                    Console.WriteLine("Victory!");
                    Console.WriteLine("");

                    // 레벨, 경험치 
                    int newLevel = LevelUp.NewLevel(oldLevel, player.Exp, player);

                    //플레이어 정보 업데이트
                    player.Level = newLevel;


                   /* Console.WriteLine("[캐릭터 정보]");
                    Console.WriteLine($"Lv. {oldLevel} {player.Name} → Lv. {newLevel} {player.Name}");
                    Console.WriteLine($"EXP: {oldExp} → {player.Exp}");
                    Console.WriteLine($"HP: {hp} → {player.CurrentHp}"); // 던전을 들어가기전 hp가 있어야한다. {player.CurrentHp} 현재 체력
                    Console.WriteLine("");*/
                    player.PrintEndingStatus();

                    //Console.WriteLine("\n>>>");
                    Console.ReadLine();
                    End.Eending();
                }
                else
                {
                    switch (step)
                    {
                        case 1:
                            if (isClear)
                            {
                                DungeonMaganer.isClearStep1 = true;
                            }
                            break;

                        case 2:
                            if (isClear)
                            {
                                DungeonMaganer.isClearStep2 = true;
                            }
                            break;

                        case 3:
                            if (isClear)
                            {
                                DungeonMaganer.isClearStep3 = true;
                            }
                            break;
                    }


                    Console.Clear();
                    Console.WriteLine("Battle!! - Result");
                    Console.WriteLine("");
                    Console.WriteLine("Victory!");
                    Console.WriteLine("");



                    // 레벨, 경험치 
                    int newLevel = LevelUp.NewLevel(oldLevel, player.Exp, player);

                    //플레이어 정보 업데이트
                    player.Level = newLevel;



                    Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.");
                    Console.WriteLine("");

                    Console.WriteLine("[캐릭터 정보]");

                    Console.WriteLine($"Lv. {oldLevel} {player.Name} → Lv. {newLevel} {player.Name}");

                    Console.WriteLine($"EXP: {oldExp} → {player.Exp}");

                    Console.WriteLine($"HP: {hp} → {player.CurrentHp}"); // 던전을 들어가기전 hp가 있어야한다. {player.CurrentHp} 현재 체력
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

                    if (step == 2 && isClear)
                    {
                        Item hiddenItem = ItemDB.Items.FirstOrDefault(x => x.Name == "보스몬스터 완벽 공략집");
                        if (hiddenItem != null && hiddenItem.IsHidden)
                        {
                            hiddenItem.IsHidden = false;
                            Console.WriteLine("\n[상점 신규 입고]");
                            Console.WriteLine($"=> '보스몬스터 완벽 공략집'이 상점에 입고되었습니다!");
                        }
                    }

                    Console.WriteLine("\n 아무 키나 누르세요");
                    Console.ReadLine();
                    return;
                }
                    
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine("");
                Console.WriteLine("You Lose");
                Console.WriteLine("");
                Console.WriteLine("[캐릭터 정보]");
                Console.WriteLine($"Lv. {oldLevel}  {player.Name}");
                Console.WriteLine($"HP {player.MaxHp} → 0");
                Console.WriteLine("");


                Console.WriteLine("\n 마을로 돌아가려면 아무 키나 누르세요");
                Console.ReadLine();
                //End.Eending();
                return;


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


        public static int NewLevel(int currentLevel, int totalExp, Player player)
        {
            int exp = totalExp;
            int level = currentLevel;

            while (exp >= MaxExp(level))
            {
                exp -= MaxExp(level);
                level++;
            }
            if (level != currentLevel)
            {
                player.LevelUp();
            }


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
            Console.WriteLine("메인 메뉴, 세이브 로드                              : 임연준 (팀장)");
            Console.WriteLine("던전, 몬스터, 전투시스템                            : 이명준");
            Console.WriteLine("Player 클래스, 상태보기, Quest 클래스, 세이브 로드  : 문승준");
            Console.WriteLine("아이템, 인벤토리, 상점                              : 박지환");
            Console.WriteLine("아이템 강화, 여관, 신전                             : 차우진");
            Console.WriteLine("레벨업기능, 보상추가, 전투결과                      : 이창선");

            //Console.WriteLine("\n\n" + "게임을 계속하려면 엔터를 눌러주세요.");
            Console.ReadKey();
            Environment.Exit(0); // 프로그램 즉시 종료

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
                        drops.Add(ItemDB.Items[1]);
                        drops.Add(ItemDB.Items[2]);
                        break;
                    case "부패의 턱":
                        drops.Add(ItemDB.Items[3]);
                        drops.Add(ItemDB.Items[9]);
                        break;
                    case "뿌리투구 수호자":
                        drops.Add(ItemDB.Items[11]);
                        drops.Add(ItemDB.Items[11]);
                        break;
                    case "일몰 좀도둑":
                        drops.Add(ItemDB.Items[13]);
                        drops.Add(ItemDB.Items[13]);
                        break;
                    case "어스름 예언자":
                        drops.Add(ItemDB.Items[9]);
                        drops.Add(ItemDB.Items[9]);
                        break;
                    case "비안개 정령":
                        drops.Add(ItemDB.Items[11]);
                        drops.Add(ItemDB.Items[11]);
                        break;
                    case "그림자 짐승":
                        drops.Add(ItemDB.Items[12]);
                        drops.Add(ItemDB.Items[12]);
                        break;
                    case "공허를 부르는 자":
                        drops.Add(ItemDB.Items[10]);
                        drops.Add(ItemDB.Items[10]);
                        break;
                    case "흡혈 파수병":
                        drops.Add(ItemDB.Items[10]);
                        drops.Add(ItemDB.Items[12]);
                        break;
                    case "공허 송곳니 추적자":
                        drops.Add(ItemDB.Items[14]);
                        drops.Add(ItemDB.Items[14]);
                        break;
                }
            }

          
            List<Weapon> weapons = drops.OfType<Weapon>().ToList();
            List<Clothes> armors = drops.OfType<Clothes>().ToList();
            List<Consumable> consumable = drops.OfType<Consumable>().ToList();

            
            var uniqueWeapons = new HashSet<Weapon>(weapons);
            var uniqueArmors = new HashSet<Clothes>(armors);


            
            var existingNames = new HashSet<string>(
                player.Inventory.Items.Select(stack => stack.Item.Name)
            );

            
            var rewardNames = new List<string>();

           
            foreach (var armor in uniqueArmors)
            {
                if (!existingNames.Contains(armor.Name))
                {
                    player.Inventory.AddItem(armor.Clone());
                    rewardNames.Add(armor.Name);
                    existingNames.Add(armor.Name);
                }
            }

            
            foreach (var weapon in uniqueWeapons)
            {
                if (!existingNames.Contains(weapon.Name))
                {
                    player.Inventory.AddItem(weapon.Clone());
                    rewardNames.Add(weapon.Name);
                    existingNames.Add(weapon.Name);
                }
            }

            
            foreach (var con in consumable)
            {


                player.Inventory.AddItem(con.Clone());
                rewardNames.Add(con.Name);
                existingNames.Add(con.Name);

            }

            
            return rewardNames.ToArray();


        }
    }









    internal class Battle_Results_Screen
    {

    }








}
