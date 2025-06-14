﻿using System;
using System.Numerics;
using System.Threading;

namespace TextRPG_Team23
{
    public class Player
    {
        private int level;
        private int exp;
        private string name;
        private float atkDmg;
        private int defence;
        private int currentHp;
        private int maxHp = 100;
        private int currentMp;
        private int maxMp = 50;
        private int gld;
        public Job job;
        private string jobName;
        private int killingMonsterCnt = 0;
        public int killCntForEnding = 0;


        public int Level { get => level; set => level = value; }
        public int Exp { get => exp; set => exp = value; }
        public string Name { get => name; set => name = value; }
        public float Atk { get => atkDmg; set => atkDmg = value; }
        public int Def { get => defence; set => defence = value; }
        public float TotalAtk { get; set; }
        public int TotalDef { get; set; }
        public int CurrentHp { get => currentHp; set => currentHp = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int CurrentMp { get => currentMp; set => currentMp = value; }
        public int MaxMp { get => maxMp; set => maxMp = value; }
        public int Gold { get => gld; set => gld = value; }
        public int KillMon { get => killingMonsterCnt; set => killingMonsterCnt = value; }
        public bool MonsterQuest { get; set; }
        public int BuffAtk { get; set; }
        public int BuffDef { get; set; }
        public Inventory Inventory { get; set; }
        public List<Quest>? Quests { get; set; }


        public Player()
        {
            Inventory = new Inventory();
            RecalculateStats();
        }

        public Player(string name, Job job) // 인벤토리 구현되면 추가예정
        {
            this.name = name;
            this.job = job;

            this.jobName = job.JobName;
            this.atkDmg = job.BaseAtkDmg;
            this.defence = job.BaseDefence;

            this.level = 1;
            this.exp = 1;
            this.currentHp = maxHp;
            this.currentMp = maxMp;
            this.gld = 5000;

            Inventory = new Inventory();
            Quests = new List<Quest>();

            RecalculateStats();
        }

        public Player(string name, Job job, int level, int exp, int Hp, int Mp, int gld) // 인벤토리 구현되면 추가예정
        {
            this.name = name;
            this.job = job;

            this.jobName = job.JobName;
            this.atkDmg = job.BaseAtkDmg;
            this.defence = job.BaseDefence;

            this.level = level;
            this.exp = exp;
            this.currentHp = Hp;
            this.currentMp = Mp;
            this.gld = gld;

            Inventory = new Inventory();
            Quests = new List<Quest>();

            RecalculateStats();
        }

        public void PrintStatus()
        {
            Console.WriteLine("\n===== 캐릭터 상태 =====");
            Console.WriteLine($"이름: {name}");
            Console.WriteLine($"직업: {jobName}");
            Console.WriteLine($"레벨: {level}");
            Console.WriteLine($"체력: {currentHp}");
            Console.WriteLine($"공격력: {TotalAtk} | 버프 공격력: {BuffAtk}");
            Console.WriteLine($"방어력: {TotalDef} | 버프 방어력: {BuffDef}");
            Console.WriteLine($"소지 골드: {gld} G");
            Console.WriteLine("======================\n");
            Console.WriteLine("\n메인 메뉴로 돌아가려면 아무키나 입력하세요.");
            Console.Write("\n>>>");


        }

        public void PrintEndingStatus()
        {
            Console.WriteLine("\n===== 최종 캐릭터 상태 =====");
            Console.WriteLine($"이름: {name}");
            Console.WriteLine($"직업: {jobName}");
            Console.WriteLine($"레벨: {level}");
            Console.WriteLine($"체력: {currentHp}");
            Console.WriteLine($"공격력: {TotalAtk} | 버프 공격력: {BuffAtk}");
            Console.WriteLine($"방어력: {TotalDef} | 버프 방어력: {BuffDef}");
            Console.WriteLine($"소지 골드: {gld} G");
            Console.WriteLine($"몬스터 처치 수: {killCntForEnding} 마리 처치");
            Console.WriteLine("======================\n");
            Console.Write("\n>>>");

        }


        private void PrintStatusInDungeon()
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"LV.{level}   {name}  ({jobName})");
            Console.WriteLine($"HP : {currentHp}/{maxHp}");
            Console.WriteLine($"MP : {currentMp}/{maxMp}");
            Console.WriteLine($"ATK : {TotalAtk} | BuffAtk: {BuffAtk}");
            Console.WriteLine($"DEF : {TotalDef} | BuffDef: {BuffDef}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("3. 인벤토리");
            Console.Write("\n>>>");

        }

        private void PrintSkillStatus()
        {
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"LV.{level}   {name}  ({jobName})");
            Console.WriteLine($"HP {currentHp}/{maxHp}");
            Console.WriteLine($"MP {currentMp}/{maxMp}");
            Console.WriteLine($"ATK : {TotalAtk} | BuffAtk: {BuffAtk}");
            Console.WriteLine($"DEF : {TotalDef} | BuffDef: {BuffDef}");
            Console.WriteLine();
            job.PrintSkillInfo();
        }

        public void PlayerDoing(List<Monster> monBox, Player player, BattleUi ui)
        {
            //foreach (Monster mon in monBox)
            //{
            //    //mon.MonsterInfo(false, mon.MobCode);
            //    ui.PrintMonster(true);
            //}
            //ui.PrintMonster(true);
            PrintStatusInDungeon();
            string input = Console.ReadLine();
            Console.Clear();
            switch (input)
            {
                case "1":
                    // 몬스터 목록 출력
                    //foreach (Monster mon in monBox)
                    //{
                    //    //mon.MonsterInfo(true, mon.MobCode);
                    //    ui.PrintMonster(true);
                    //}
                    ui.PrintMonster(true);

                    Console.Write("\n공격할 몬스터 번호를 선택하세요 >>> ");
                    if (int.TryParse(Console.ReadLine(), out int targetIndex) && targetIndex >= 1 && targetIndex <= monBox.Count)
                    {
                        if (monBox[targetIndex - 1].IsDead)
                        {
                            Console.WriteLine("이미 죽은 몬스터 입니다.");
                            PlayerDoing(monBox, player, ui);
                        }
                        //공격 로직 작성
                        job.Attack(monBox[targetIndex - 1], TotalAtk, player);
                    }
                    else
                    {
                        //PlayerDoing(monBox, player, ui);
                        Console.WriteLine("잘못된 입력입니다.");
                    }
                    break;

                case "2":
                    // 몬스터 목록 출력
                    /*foreach (Monster mon in monBox)
                    {
                        mon.MonsterInfo(false, mon.MobCode);

                    }*/
                    ui.PrintMonster(false);

                    PrintSkillStatus();
                    Console.Write(">>> ");
                    string skillInput = Console.ReadLine();

                    if (skillInput == "1")
                    {
                        /*foreach (Monster mon in monBox)
                        {
                            mon.MonsterInfo(true, mon.MobCode);
                        }*/
                        ui.PrintMonster(true);

                        if (int.TryParse(Console.ReadLine(), out int tgIndex) && tgIndex >= 1 && tgIndex <= monBox.Count)
                        {
                            if (monBox[tgIndex - 1].IsDead)
                            {
                                Console.WriteLine("이미 죽은 몬스터 입니다.");
                                PlayerDoing(monBox, player, ui);
                            }
                            //공격 로직 작성(단일딜)
                            job.SkillA(monBox[tgIndex - 1], TotalAtk, player);
                        }
                        else
                        {
                            //PlayerDoing(monBox, player, ui);
                            Console.WriteLine("잘못된 입력입니다.");
                        }
                    }
                    else if (skillInput == "2")
                    {
                        //살아있는 몬스터만 필터링
                        List<Monster> aliveMonsters = monBox.FindAll(mon => mon.IsDead == false);

                        //광역 공격
                        job.SkillB(aliveMonsters, TotalAtk, player);
                    }
                    else
                    {
                        //PlayerDoing(monBox, player, ui);
                        Console.WriteLine("잘못된 스킬 선택입니다.");
                    }
                    break;

                case "3":
                    bool limitedUse = true;
                    bool alreadyUse = false;

                    player.Inventory.PrintInventory(player, limitedUse, ref alreadyUse);
                    break;
                default:
                    //PlayerDoing(monBox, player, ui);
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

        } // 던전 행동 로직

        public int ItemAttack()
        {
            int equipAtk = 0;
            if (Inventory.Slots[(int)EquipSlot.Weapon] is Weapon weapon)
            {
                equipAtk = weapon.Atk;
            }
            return equipAtk;
        }

        public int ItemDefense()
        {

            int equipDef = 0;
            if (Inventory.Slots[(int)EquipSlot.Clothes] is Clothes clothes)
            {
                equipDef = clothes.Def;
            }
            return equipDef;
        }


        public void RecalculateStats()
        {
            TotalAtk = atkDmg + ItemAttack();
            TotalDef = defence + ItemDefense();

        }

        public void PlayerTakeDamage(int dmg)
        {
            int realDamage = 0;

            if (TotalDef + BuffDef > dmg)
            {
                realDamage = 0;
            }
            else
            {
                realDamage = dmg - (TotalDef + BuffDef);
            }

            this.Inventory.CheckClothesDurability(this);

            if (CurrentHp > realDamage)
            {
                CurrentHp -= realDamage;
            }
            else
            {
                CurrentHp = 0;
                // Die 로직실행
            }
        }

        public void IgnoreDefenseDamage(int dmg)
        {
            if (CurrentHp > dmg)
            {
                CurrentHp -= dmg;
            }
            else
            {
                CurrentHp = 0;
            }
        }

        //public void AddDungeonClear()
        //{

        //    UpdateLevel();

        //}

        /*  public void UpdateLevel()
          {
              switch (Level)
              {
                  case 1:
                      if (exp >= 10)
                      {
                          Level++;
                          atkDmg += 0.5f;
                          defence += 1;
                      }
                      break;
                  case 2:
                      if (exp >= 35)
                      {
                          Level++;
                          atkDmg += 0.5f;
                          defence += 1;
                      }
                      break;
                  case 3:
                      if (exp >= 65)
                      {
                          Level++;
                          atkDmg += 0.5f;
                          defence += 1;
                      }
                      break;
                  case 4:
                      if (exp >= 100)
                      {
                          Level++;
                          atkDmg += 0.5f;
                          defence += 1;
                      }
                      break;
                  default:
                      break;
              }

              RecalculateStats();
          }*/

        public void LevelUp()
        {
            atkDmg += 0.5f;
            defence += 1;
            CurrentMp = MaxHp;
            RecalculateStats();
        }

        public bool QuestCheck(Quest q)
        {
            if (Quests != null)
            {
                foreach (Quest quest in Quests)
                {
                    if (quest.Title == q.Title)
                    {
                        Console.WriteLine($"플레이어가 수락한 '{q.Title}' 퀘스트 출력");
                        return true;
                    }
                }
            }

            return false;
        }

        public Quest? ReturnQuest(Quest q)
        {
            if (Quests != null)
            {
                foreach (Quest quest in Quests)
                {
                    if (quest.Title == q.Title)
                        return quest;
                }
            }

            return null;
        }


        public void AddQuest(Quest q)
        {
            if (Quests != null)
            {
                foreach (Quest quest in Quests)
                {
                    if (quest.Title == q.Title)
                    {
                        Console.WriteLine($"이미 '{q.Title}' 퀘스트를 수락했습니다.");
                        return;
                    }
                }
            }


            q.IsActive = true;
            Quests.Add(q);
            Console.WriteLine($"'{q.Title}' 퀘스트를 수락했습니다!");
        }

        public void CheckAllQuests()
        {
            if (Quests == null || Quests.Count == 0)
            {
                return;
            }

            foreach (var quest in Quests)
            {
                quest.CheckCompletion(this);
            }
        }

        public bool HasEquippedAnyItem()
        {
            if (Inventory.Slots[(int)EquipSlot.Weapon] is Weapon weapon)
            {
                return true;
            }
            else if (Inventory.Slots[(int)EquipSlot.Clothes] is Clothes clothes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasLevel5()
        {
            return Level >= 5;
        }
        public bool HasKillMonster()
        {
            return KillMon >= 5;
        }

        public void PlusKillMonsterCnt()
        {
            if (MonsterQuest)
            {
                KillMon++;
            }
        }
    }

}