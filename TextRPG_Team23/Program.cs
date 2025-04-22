using System.Numerics;
using TextRPG_Team_private;

namespace TextRPG_Team23
{
    internal class Program
    {
        static private bool isRunning = true;

        static void Main(string[] args)
        {
            Intro player = new Intro();
            player.CreateCharacter();
            ShowMainMenu();

            while (isRunning)
            {
                Console.Write("\n메뉴 선택 > ");
                string input = Console.ReadLine();
                MenuSelect(input);
            }
        }

        static private void ShowMainMenu()
        {
            var mainMenu = BranchManager._instance.PlaceInfo["MainMenu"];
            mainMenu.ShowOptions();
        }

        static private void MenuSelect(string input)
        {
            switch (input)
            {
                case "1": // 스테이터스
                    Console.WriteLine("스테이터스를 출력합니다.");
                    break;
                case "2": // 인벤토리
                    Console.WriteLine("인벤토리를 출력합니다.");
                    break;
                case "3": // 상점
                    Console.WriteLine("상점을 이용합니다.");
                    break;
                case "4": // 던전
                    Console.WriteLine("던전에 입장합니다.");
                    break;
                case "5": // 여관
                    Console.WriteLine("여관에서 휴식을 취합니다.");
                    break;
                case "0": // 게임 저장
                    var saveMenu = BranchManager._instance.PlaceInfo["SaveMenu"];
                    saveMenu.ShowOptions();
                    Console.Write("\n선택 > ");
                    string saveInput = Console.ReadLine();

                    switch (saveInput)
                    {
                        case "1":
                            Console.WriteLine("게임을 저장합니다.");
                            break;
                        case "2":
                            Console.WriteLine("게임을 저장하고 종료합니다.");
                            isRunning = false;
                            break;
                        case "0":
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("\n아무 키나 누르면 계속...");
                Console.ReadKey();
                ShowMainMenu();
            }
        }
    }
}

public class Place
{
    public string Name { get; set; }
    public List<string> Options { get; set; }

    public Place(string name, List<string> options)
    {
        Name = name;
        Options = options;
    }

    public void ShowOptions()
    {
        Console.Clear();
        Console.WriteLine($"=== 현재 위치 : {Name} ===\n");
        foreach (var option in Options)
        {
            Console.WriteLine(option);
        }
    }
}

public class BranchManager
{
    private static BranchManager instance;

    public static BranchManager _instance
    {
        get
        {
            if (instance == null)
                instance = new BranchManager();
            return instance;
        }
    }

    public Dictionary<string, Place> PlaceInfo { get; set; }

    private BranchManager()
    {
        PlaceInfo = new Dictionary<string, Place>
            {
                {
                    "MainMenu", new Place("마을", new List<string>
                    {
                        "1. 스테이터스",
                        "2. 인벤토리",
                        "3. 상점",
                        "4. 던전",
                        "5. 휴식 (여관)",
                        "0. 게임 저장"
                    })
                },
                {
                    "SaveMenu", new Place("저장 메뉴", new List<string>
                    {
                        "1. 저장",
                        "2. 저장 후 종료",
                        "0. 돌아가기"
                    })
                }
            };
    }
}

internal class Intro {
    public void CreateCharacter()
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
        //Player player = new Player(name, job);
    }
}



