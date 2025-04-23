using System;
<<<<<<< HEAD
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
            
            MonsterFactory factory = new MonsterFactory();

            factory.MakeMonster(4);
            int number = 1;
            foreach (var m in DungeonMaganer.monsterBox)
            {

                m.MonsterInfo(true, number);
                number++;
            }
            

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

        


    }

    class Battle
    {
        public void BattleStart()
        {
            Console.Clear();
            Console.Write("몬스터가 등장했다!\n\n" +
              "전투를 시작하려면 아무키나 누르세요.\n" +
              ">>>");

            Console.ReadKey();
            int spawnCount = DungeonMaganer.random.Next(2, 5);
        }


        public void BattleAi()
        {

        }

    }

    

=======
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    class DungeonMaganer
    {

        List<Monster> monsterBox;
        BattleUi ui;
        Battle battle;
        Battlecondition condition;
        MonsterFactory factory;


        public DungeonMaganer(List<Monster> monsterBox, BattleUi ui, Battle battle, Battlecondition condition, MonsterFactory factory)
        {

            this.monsterBox = monsterBox;
            this.ui = ui;
            this.battle = battle;
            this.condition = condition;
            this.factory = factory;
        }

        public void Start()
        {
            battle.EnterBattle();
        }
        public void WorkFactory()
        {
            int spawnCount = Program.random.Next(2, 5);
            factory.MakeMonster(spawnCount);
        }
    }
>>>>>>> TestCombine
}

