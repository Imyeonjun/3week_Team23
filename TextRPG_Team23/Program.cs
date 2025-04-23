using System.Numerics;
using System;

namespace TextRPG_Team23
{
    internal class Program
    {
        public static Random random = new Random();

        static void Main(string[] args)
        {
            /*List<Monster> monsterBox = new List<Monster>();

            BattleUi ui = new BattleUi();

            Battle battle = new Battle();

            Battlecondition condition = new Battlecondition();

            MonsterFactory factory = new MonsterFactory(monsterBox, condition);

            DungeonMaganer dungeon = new DungeonMaganer(monsterBox, ui, battle, condition, factory);

            condition.BattleConnect(monsterBox, ui, battle);*/

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