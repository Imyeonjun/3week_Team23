using System.Numerics;
using System;

namespace TextRPG_Team23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }

    public class GameManager
    {
        private bool isRunning = true;
        private Player player;

        private Intro intro = new Intro();
        private Town town;

        public GameManager()
        {
            town = new Town(this);
        }

        public void StartGame()
        {
            intro.CreateCharacter(out player);

            while (isRunning)
            {
                town.MainMenu(player);
            }
        }

        public void StopGame()
        {
            isRunning = false;
        }
    }

    public class Intro
    {
        static Player player;

        private BranchManager selectJob = new BranchManager();

        public string[] jobOptions = {
                "전사",
                "마법사"
            };

        public void CreateCharacter(out Player player)
        {
            Console.WriteLine("이름을 입력하세요.");
            string name = Console.ReadLine();

            Job job = null;
            bool selected = false;

            while (!selected)
            {
                Console.WriteLine("\n직업을 선택하세요.");
                int result = selectJob.ReturnSelect(jobOptions, false, " ");


                switch (result)
                {
                    case 1:
                        job = new Warrior();
                        selected = true;
                        break;
                    case 2:
                        job = new Magician();
                        selected = true;
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
            player = new Player(name, job);
        }
    }

    public class Town
    {
        // 인스턴스 생성
        private BranchManager menu = new BranchManager();
        private GameManager gameManager;

        Dungeon gate = new Dungeon();

        public Town(GameManager gm)
        {
            gameManager = gm;
        }

        // 선택지 종류
        public string[] mainMenuOptions = {
                "스테이터스 보기",
                "인벤토리",
                "상점",
                "던전",
                "여관"
            };

        public void MainMenu(Player player)
        {
            Console.Clear();
            Console.WriteLine("== 메인 메뉴 ==");
            int selected = menu.ReturnSelect(mainMenuOptions, true, "게임 종료");

            switch (selected)
            {
                case 1:
                    Console.WriteLine("디버그 : 상태창 출력");
                    Console.ReadKey();
                    break;
                case 2:
                    player.Inventory.PrintInventory(player);
                    Console.ReadKey();
                    break;
                case 3:
                    new Shop(player).ShopPhase();
                    Console.ReadKey();
                    break;
                case 4:
                    Console.WriteLine("디버그 : 던전 출력");
                    Console.ReadKey();
                    gate.Gate();
                    break;
                case 5:
                    Console.WriteLine("디버그 : 여관 출력");
                    Console.ReadKey();
                    break;
                case 0:
                    Console.WriteLine("게임을 종료합니다.");
                    Console.ReadKey();
                    gameManager.StopGame();
                    break;
                case -1:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public class Dungeon
    {
        private BranchManager choice = new BranchManager();

        public string[] gateOptions = {
                "하급 던전",
                "중급 던전",
                "상급 던전",
            };

        public void Gate()
        {
            Console.WriteLine("== 던전 선택 ==");
            int selected = choice.ReturnSelect(gateOptions, true, "돌아가기");

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

    public class BranchManager //선택지 생성기
    {
        //Branch 함수는 ReturnSelect를 위한 보조 함수. 호출은 ReturnSelect 권장.
        public void Branch(string[] options, bool isCancel, string cancelText) // 선택지 내용, 취소문 유무, 취소문 텍스트
        {
            for (int i = 0; i < options.Length; i++)
            {
                // i를 번호로 지정하고, 뒤쪽에 가져온 선택지 불러오기
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            if (isCancel)
            {
                Console.WriteLine("\n0. " + cancelText);
            }
        }

        public int ReturnSelect(string[] options, bool isCancel, string cancelTex)
        {
            //선택지 출력
            Branch(options, isCancel, cancelTex);

            //사용자 입력 받기
            Console.Write("\n선택 >> ");
            string input = Console.ReadLine();

            //입력값 판단
            if (int.TryParse(input, out int result))
            {
                int maxOptions = options.Length;

                if (isCancel && result == 0)
                    return 0;

                else if (result >= 1 && result <= maxOptions)
                    return result;

                return -1;
            }
            return -1;
        }
    }
}