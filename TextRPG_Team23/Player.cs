using System;
using System.Numerics;

namespace TextRPG_Team23
{
    public class Player
    {
        private int level;
        private string name;
        private float atkDmg;
        private int defence;
        private int currentHp;
        private int maxHp = 100;
        private int currentMp;
        private int maxMp = 50;
        private int gld;
        private Job job;
        private string jobName;

        public int Level { get => level; set => level = value; }
        public string Name { get => name; set => name = value; }
        public float AtkDmg { get => atkDmg; set => atkDmg = value; }
        public int Defence { get => defence; set => defence = value; }
        public float TotalAtk { get; set; }
        public int TotalDef { get; set; }
        public int CurrentHp { get => currentHp; set => currentHp = value; }
        public int MaxHp { get => maxHp; set => maxHp = value; }
        public int CurrentMP { get => currentMp; set => currentMp = value; }
        public int MaxMp { get => maxMp; set => maxMp = value; }
        public int Gold { get => gld; set => gld = value; }
        //public Inventory Inventory { get; private set; }

        //public Item[] Slots = new Item[2]; // 추후 변경


        public Player(string name, Job job) // 인벤토리 구현되면 추가예정
        {
            this.name = name;
            this.job = job;

            this.jobName = job.JobName;
            this.atkDmg = job.BaseAtkDmg;
            this.defence = job.BaseDefence;

            this.level = 1;
            this.currentHp = maxHp;
            this.currentMp = maxMp;
            this.gld = 500;

            //Inventory = new Inventory();

            //RecalculateStats();
        }

        public void PrintStatus()
        {
            Console.WriteLine("===== 캐릭터 상태 =====");
            Console.WriteLine($"이름: {name}");
            Console.WriteLine($"직업: {jobName}");
            Console.WriteLine($"레벨: {level}");
            Console.WriteLine($"체력: {currentHp}");
            //Console.WriteLine($"공격력: {TotalAtk} (+{ItemAttack()})");
            //Console.WriteLine($"방어력: {TotalDef} (+{ItemDefense()})");
            Console.WriteLine($"소지 골드: {gld} G");
            Console.WriteLine("======================\n");

        }

        private void PrintStatusInDungeon()
        {
            Console.WriteLine("[내정보]");
            Console.WriteLine($"LV.{level}   {name}  ({jobName})");
            Console.WriteLine($"HP {currentHp}/{maxHp}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("\n>>>");

        }

        private void PrintSkillStatus()
        {
            Console.WriteLine("[내정보]");
            Console.WriteLine($"LV.{level}   {name}  ({jobName})");
            Console.WriteLine($"HP {currentHp}/{maxHp}");
            Console.WriteLine();
            job.PrintSkillInfo();
        }

         public void PlayerDoing(List<Monster> monBox)
         {
             foreach (Monster mon in monBox)
             {
                 mon.MonsterInfo(false);
             }
             PrintStatusInDungeon();
             string input = Console.ReadLine();

             switch (input)
             {
                 case "1":
                     // 몬스터 목록 출력
                     foreach (Monster mon in monBox)
                     {
                         mon.MonsterInfo(true);
                     }

                     Console.Write("\n공격할 몬스터 번호를 선택하세요 >>> ");
                     if (int.TryParse(Console.ReadLine(), out int targetIndex) && targetIndex >= 1 && targetIndex <= monBox.Count)
                     {
                         //공격 로직 작성
                         job.Attack(monBox[targetIndex - 1], TotalAtk);
                     }
                     else
                     {
                         PlayerDoing(monBox);
                         Console.WriteLine("잘못된 입력입니다.");
                     }
                     break;

                 case "2":
                     // 몬스터 목록 출력
                     foreach (Monster mon in monBox)
                     {
                         mon.MonsterInfo(false);
                     }
                     PrintSkillStatus();
                     Console.Write(">>> ");
                     string skillInput = Console.ReadLine();

                     if (skillInput == "1")
                     {
                         foreach (Monster mon in monBox)
                         {
                             mon.MonsterInfo(true);
                         }
                         if (int.TryParse(Console.ReadLine(), out int tgIndex) && tgIndex >= 1 && tgIndex <= monBox.Count)
                         {
                             //공격 로직 작성(단일딜)
                             job.SkillA(monBox[tgIndex - 1], TotalAtk);
                         }
                         else
                         {
                             PlayerDoing(monBox);
                             Console.WriteLine("잘못된 입력입니다.");
                         }
                     }
                     else if (skillInput == "2")
                     {
                         // 공격 로직 작성 (광역딜)
                         job.SkillB(monBox, TotalAtk);
                     }
                     else
                     {
                         PlayerDoing(monBox);
                         Console.WriteLine("잘못된 스킬 선택입니다.");
                     }
                     break;

                 default:
                     PlayerDoing(monBox);
                     Console.WriteLine("잘못된 입력입니다.");
                     break;
             }

         } // 던전 행동 로직

        //public int ItemAttack()
        //{

        //    int equipAtk = 0;
        //    if (Slots[(int)EquipSlot.Weapon] is Weapon weapon)
        //    {
        //        equipAtk = weapon.Atk;
        //    }
        //    return equipAtk;
        //}

        //public int ItemDefense()
        //{

        //    int equipDef = 0;
        //    if (Slots[(int)EquipSlot.Clothes] is Clothes clothes)
        //    {
        //        equipDef = clothes.Def;
        //    }
        //    return equipDef;
        //} 


        //public void RecalculateStats()
        //{
        //    TotalAtk = atkDmg + ItemAttack();
        //    TotalDef = defence + ItemDefense();
        //}


        //public void AddDungeonClear()
        //{

        //    UpdateLevel();

        //}

        //void UpdateLevel()
        //{
        //    Level++;
        //    AtkDmg += 0.5f;
        //    Defence += 1;

        //    RecalculateStats();
        //}
    }


}