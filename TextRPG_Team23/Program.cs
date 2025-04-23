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
}