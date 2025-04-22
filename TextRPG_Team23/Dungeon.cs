using System.ComponentModel;

/*namespace Team23_Dungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Player player = new Player();

            DungeonMaganer dungeon = new DungeonMaganer();

            dungeon.Battle(player);
        }
    }

    class Player
    {

        int att;
        int def;
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
*/

namespace Team23_Dungeon
{

    class DungeonMaganer
    {

        public static Random random = new Random();

        public static List<GreenMonster> monsterBox = new List<GreenMonster>();

        public void Battle(Player player)
        {

            Console.Clear();
            Console.Write("몬스터가 등장했다!\n\n" +
                          "전투를 시작하려면 아무키나 누르세요.\n" +
                          ">>>");

            Console.ReadKey();

            int spawnCount = random.Next(2,5);

            for (int i = 0; i < spawnCount; i++)
            {
                monsterBox.Add(new GreenMonster());
            }
            GreenMonster.nextCode = 0;

            while (monsterBox.Count > 0 && player.hp > 0)
            {
                Console.Clear();


                foreach (GreenMonster monster in monsterBox)
                {
                    monster.MobInfo(false);
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


                foreach (GreenMonster monster in monsterBox)
                {
                    CumputerTurn(monster, player);
                }
                
            }

        }

        public void CumputerTurn(GreenMonster monster, Player player)
        {
            int cumputerInput = random.Next(0, 8);

            switch (cumputerInput)
            {
                case int num when (cumputerInput >= 0 && cumputerInput <= 3):
                    monster.pattern.AttackPattern(monster, player);
                    break;

                case int num when (cumputerInput > 3 && cumputerInput <= 5):
                    monster.pattern.DefencePattern(monster, player);
                    break;

                case int num when (cumputerInput > 5 && cumputerInput <= 7):
                    monster.pattern.RoarPattern(monster, player);
                    break;
            }

        }





    }
    enum GreenMob
    {
        NeedlebackCrow,
        Boghowler,
        RockjawLizard,
        GhostMousse,
        BlueskinBeast,
        Vinebear

    }
    enum GreenMobBoss
    {
        CreusFangOfTheDark,
        NamelessGravedigger,
        Rotaruck
    }

    class GreenMonster
    {
        public string name;
        public int level;
        public int att;
        public int def;
        private int maxHp; //최대체력
        private int nowHp; //현제체력

        private int mobCode;
        public static int nextCode;

        public int hp
        {
            get {  return nowHp; }
            set
            {
                if (value < 0) { nowHp = 0; }

                else if (value > maxHp) { nowHp = maxHp; }

                else { nowHp = value; }  

            }
        }

        int patternCount = 0;

        public GreenMobPattern pattern;

        public GreenMonster() //몬스터 생성시 초기화를 위한 생성자
        {
            mobCode = nextCode;
            nextCode++;

            GreenMob greenmob;
            greenmob = (GreenMob)DungeonMaganer.random.Next(0, 6);
            level = DungeonMaganer.random.Next(1,5);

            string[] monsterName = { "바늘등까마귀", "습지울음꾼", "바위턱도마뱀", "유령무스", "시퍼런털짐승", "덩굴곰" };
            name = monsterName[(int)greenmob];

            MonsterStat(greenmob); //몹 생성시 초기 능력치 결정

            pattern = new GreenMobPattern();

        }

        public void MobInfo(bool isAction)
        {
            if (!isAction)
            {
                Console.WriteLine($"Lv: {level}  {name} \t[Att: {att}]  [Def: {def}]  [Hp: {hp}]");
            }
            else
            {
                Console.WriteLine($"{mobCode}번 Lv: {level}  {name} \t[Att: {att}]  [Def: {def}]  [Hp: {hp}]");
            }
        }


        private void MonsterStat(GreenMob m) // 몹종류에 따라 능력치 결정
        {

            switch (m)
            {
                case GreenMob.NeedlebackCrow:
                    att = 9 + (level);
                    def = 3 + (level);
                    maxHp = 30 + (level);
                    nowHp = 30 + (level);
                    break;

                case GreenMob.Boghowler:
                    att = 12 + (level);
                    def = 7 + (level);
                    maxHp = 42 + (level);
                    nowHp = 42 + (level);
                    break;

                case GreenMob.RockjawLizard:
                    att = 6 + (level);
                    def = 12 + (level);
                    maxHp = 37 + (level);
                    nowHp = 37 + (level);
                    break;

                case GreenMob.GhostMousse:
                    att = 6 + (level);
                    def = 0;
                    maxHp = 1 ;
                    nowHp = 1 ;
                    break;

                case GreenMob.BlueskinBeast:
                    att = 10 + (level);
                    def = 10 + (level);
                    maxHp = 50 + (level);
                    nowHp = 50 + (level);
                    break;

                case GreenMob.Vinebear:
                    att = 14 + (level);
                    def = 22 + (level);
                    maxHp = 80 + (level);
                    nowHp = 80 + (level);
                    break;
            }
        }
    }

    class GreenMobPattern
    {
        public void RoarPattern(GreenMonster monster, Player player)
        {
            Console.WriteLine($"{monster.name}의 흉표한 포효 {player}는 순간 얼어붙는다.");
            Console.ReadKey();
            //턴에 영향을 끼침
        }

        public void AttackPattern(GreenMonster monster, Player player)
        {
            Console.WriteLine($"{monster.name}의 기습!");
            player.AddDamage(monster.att);
            Console.ReadKey();
        }

        public void DefencePattern(GreenMonster monster, Player player)
        {
            Console.WriteLine($"{monster.name}은(는) 방어태세에 돌입했다.");
            Console.ReadKey();
            //턴에 영향을 끼침

        }
    }


    class GreenBoss
    {

    }



    class Monster
    {
        string name { get; set; }
        int level { get; set; }
        int att { get; set; }
        int def { get; set; }

        //int dodg { get; set; } 나중을 위한 회피스탯  

        private int maxHp; //최대체력
        private int nowHp; //현제체력

        private int mobCode;
        public static int nextCode;
    }
}
