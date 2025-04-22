using System;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;
using Team23_Dungeon;

namespace Team23_Dungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Player player = new Player();

            DungeonMaganer dungeon = new DungeonMaganer();

            dungeon.BattleStart(player);
        }
    }

    class Player
    {
        public string name;
        public int att;
        public int def;
        private int maxHp;
        private int nowHp;
        
        public int hp
        {
            get { return nowHp; }
            set
            {
                if (value < 0)
                {
                    nowHp = 0;
                }
                else if (value > maxHp)
                {
                    nowHp = maxHp;
                }
                else
                {
                    nowHp = value;
                }
            }
        }

        public Player()
        {
            name = "용사";
            att = 10;
            def = 5;
            maxHp = 100;
            nowHp = 100;
        }


        public void AddDamage(int damage)
        {
            hp -= damage;
        }

        public void HealHp(int heal)
        {
            hp += heal;
        }

        public void Attack(int damage)
        {
            string inputTarget = Console.ReadLine();
            switch (inputTarget)
            {
                case "1":

                    break;

                case "2":

                    break;

                case "3":
                    
                    break;

                case "4":   

                    break;

                default:
                    Console.WriteLine("올바른 키가 아닙니다.");
                    break;
            }
        }

        public void Defence(int defPoint)
        {

        }

    }
}


namespace Team23_Dungeon
{

    class DungeonMaganer
    {
        public static Random random = new Random();

        public static List<Monster> monsterBox = new List<Monster>();

        MonsterFactory facktory = new MonsterFactory();

        public void BattleStart(Player player)
        {

            Console.Clear();
            Console.Write("몬스터가 등장했다!\n\n" +
                          "전투를 시작하려면 아무키나 누르세요.\n" +
                          ">>>");

            Console.ReadKey();
            int spawnCount = random.Next(2, 5);

            facktory.MakeMonster(spawnCount);
            Monster.NextCode = 0;

            while (monsterBox.Count > 0 && player.hp > 0)
            {
                Console.Clear();
                foreach (Monster monster in monsterBox)
                {
                    monster.MonsterInfo(false);
                    monster.Attack(player);
                    Console.WriteLine();
                }

                Console.Write($"\n\n[플레이어의 턴]\n\n" +
                                  $"1.공격\n" +
                                  $"2.방어\n" +
                                  $">>>");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":


                        break;

                    case "2":


                        break;
                }

            }

        }


    }

    class Battle
    {
        
    }

    class MonsterFactory
    {
        List<Monster> monsterPool = new List<Monster>();

        public MonsterFactory()
        {
            int count = DataManager.DManager.MonsterInfo.Length;

            for (int i = 0;  i < count; i++)
            {
                SettingMonster(new Monster(), DataManager.DManager.MonsterInfo[i]);
            }
        }

        public void SettingMonster(Monster m, string mStat)
        {
            string[] stat = mStat.Split('/');

            m.MobCode = Monster.NextCode;
            Monster.NextCode++;

            m.Level = int.Parse(stat[0]);
            m.Name = stat[1];
            m.Att = int.Parse(stat[2]);
            m.Def = int.Parse(stat[3]);
            m.MaxHp = int.Parse(stat[4]);
            m.NowHp = m.MaxHp;

            monsterPool.Add(m);
        }

        public void MakeMonster(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int select = DungeonMaganer.random.Next(0,6);

                DungeonMaganer.monsterBox.Add(monsterPool[select]);
            }

        }
    }

    class Monster
    {
        public static int NextCode;
        public int MobCode { get; set; } //고유번호
        public string Name { get; set; }
        public int Level { get; set; }
        public int Att { get; set; }
        public int Def { get; set; }
        public int dodg { get; set; }

        public int MaxHp { get; set; }
        public int NowHp { get; set; }

        public void MonsterInfo(bool isAction)
        {
            if (!isAction) //플레이어가 타겟으로 지정하지 않을 때 / 번호출력x
            {
                Console.WriteLine($"Lv: {Level}  {Name} \t[Att: {Att}]  [Def: {Def}]  [Hp: {NowHp}]");
            }
            else //플레이어가 타겟으로 지정할 때 / 번호출력o
            {
                Console.WriteLine($"{MobCode}번 Lv: {Level}  {Name} \t[Att: {Att}]  [Def: {Def}]  [Hp: {NowHp}]");
            }
        }


        public void Attack(Player p)
        {
            int beforeHp = p.hp;
            p.AddDamage(Att);
            int afterHp = p.hp; 

            Console.WriteLine($"몬스터 공격 {beforeHp} -> {afterHp}");
        }
    }

    enum MonsterType
    {
        NeedlebackCrow,
        Boghowler,
        RockjawLizard,
        GhostMousse,
        BlueskinBeast,
        Vinebear

    }



}

