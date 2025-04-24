using System.Numerics;
using System;

namespace TextRPG_Team23
{
    internal class Program
    {

        public static Random random = new Random();
         

        static void Main(string[] args)
        {
            QuestMenu questMenu = new QuestMenu();
            Player player;
            Intro intro = new Intro();
            intro.CreateCharacter(out player);

            GameManager gameManager = new GameManager(player);
            gameManager.StartGame();

            Inn inn = new Inn();
            inn.Selection(player, questMenu);
        }

    }

    public class GameManager
    {
        private bool isRunning = true;
        private Player _player;
        private QuestMenu _menu;
        private Town town;
        private Forge Forge;
        private Inn Inn;
        private Temple Temple;


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


            Inn = new Inn();
            Forge = new Forge();
            Temple = new Temple();
            _menu = new QuestMenu();

            //dungeon.WorkFactory();
            //dungeon.Start();
        }

        public void StartGame()
        {

            while (isRunning)
            {
                town.MainMenu(_player, _menu, Inn, Forge, Temple);
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