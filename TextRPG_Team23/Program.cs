using System.Numerics;
using System;

namespace TextRPG_Team23
{
    internal class Program
    {
<<<<<<< HEAD
        //게임 종료를 위한 변수
        static private bool isRunning = true;
        static Player player;
        static void Main(string[] args)
        {

            CreateCharacter();
            


            //여기서 인트로 (또는 캐릭터 생성) 호출
            ShowMainMenu();

            //isRunning = false;가 되면 while 조건에 벗어나면서 프로그램 중단 = 게임 종료
            while (isRunning == true)
            {
                Console.Write("메뉴 선택 > ");
                string input = Console.ReadLine();
                MenuSelect(input);
            }
        }

        static void CreateCharacter()
        {
            Console.WriteLine("이름을 입력하세요.");
            string name = Console.ReadLine();
            Console.WriteLine("직업을 선택하세요. (0: 전사, 1: 마법사)");
            int jobChoice = int.Parse(Console.ReadLine());
            Job job;

            switch (jobChoice)
            {
                case 0:
                    job = new Warrior();
                    break;
                case 1:
                    job = new Magician();
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 기본 직업으로 전사를 선택합니다.");
                    job = new Warrior();
                    break;
            }

            player = new Player(name, job);

        }


        static private void ShowMainMenu()
        {
            Console.Clear();

            Console.WriteLine
                ("=== 현재 위치 : 마을 ===" +
                "\n" +
                "\n1. 스테이터스" +
                "\n2. 인벤토리" +
                "\n3. 상점" +
                "\n4. 던전" +
                "\n5. 휴식 (여관)" +
                "\n" +
                "\n0. 게임 저장");
=======
        public static Random random = new Random();
         

        static void Main(string[] args)
        {
            Player player;
            Intro intro = new Intro();
            intro.CreateCharacter(out player);

            GameManager gameManager = new GameManager(player);
            gameManager.StartGame();
>>>>>>> TestCombine
        }

    }

    public class GameManager
    {
        private bool isRunning = true;
        private Player _player;
        private QuestMenu _menu;
        private Town town;

        public GameManager(Player player)
        {
            _player = player;
            
            town = new Town(this);

            List<Monster> monsterBox = new List<Monster>();

            Battlecondition condition = new Battlecondition();

            BattleUi ui = new BattleUi(condition);

            Battle battle = new Battle(condition);

            MonsterFactory factory = new MonsterFactory(monsterBox, condition);

            DungeonMaganer dungeon = new DungeonMaganer(monsterBox, ui, battle, condition, factory);

            condition.BattleConnect(_player, monsterBox, ui, battle);


            _menu = new QuestMenu();

            dungeon.WorkFactory();
            dungeon.Start();


        }

        public void StartGame()
        {

            while (isRunning)
            {
<<<<<<< HEAD
                case "1": // 상태 보기

                    break;
                case "2": // 인벤토리

                    break;
                case "3": // 상점

                    break;
                case "4": // 던전
                    DungeonMaganer d = new DungeonMaganer();
                    d.BattleStart(player);
                    break;
                case "5": // 휴식 or 여관

                    break;
                case "0":
                    Console.WriteLine
                        ("== 저장 메뉴 ==" +
                        "\n" +
                        "\n1. 저장" +
                        "\n2. 저장 후 종료" +
                        "\n" +
                        "\n0. 돌아가기");
                    string saveInput = Console.ReadLine();

                    switch (saveInput)
                    {
                        case "1":
                            Console.WriteLine("게임을 저장합니다.");
                            //게임 저장 호출
                            ShowMainMenu();
                            break;
                        case "2":
                            Console.WriteLine("게임을 저장합니다.");
                            //게임 저장 호출
                            isRunning = false;
                            break;
                        case "0":
                            break;
                    }
                    break;

                default:
                    Console.WriteLine("\n잘못된 입력입니다.");
                    break;
=======
                town.MainMenu(_player);
>>>>>>> TestCombine
            }
        }

        public void StopGame()
        {
            isRunning = false;
        }
    }

    public class DungeonTest // 해당 클래스 or 메서드는 던전 관련 클래스로 이동 예정.
    {

        public string[] gateOptions = {
                "하급 던전",
                "중급 던전",
                "상급 던전",
            };

        public void Gate()
        {
            Console.WriteLine("== 던전 선택 ==");
            int selected = BranchManager.ReturnSelect(gateOptions, true, "돌아가기");

            switch (selected)
            {
                case 1:
                    Console.WriteLine("디버그 : 하급 던전 입장");
                    Console.ReadKey();
                    

                    break;
                case 2:
                    Console.WriteLine("디버그 : 중급 던전 입장");
                    Console.ReadKey();
                    break;
                case 3:
                    Console.WriteLine("디버그 : 상급 던전 입장");
                    Console.ReadKey();
                    break;
                case 0:
                    Console.WriteLine("마을로 돌아갑니다.");
                    Console.ReadKey();
                    break;
                case -1:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}