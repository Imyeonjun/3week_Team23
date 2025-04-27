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

            //Inn inn = new Inn();
        
            //inn.Selection(player, questMenu);
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
        public DungeonMaganer dungeon;

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
            
            this.dungeon = dungeon;
            Inn = new Inn();
            Forge = new Forge(player);
            Temple = new Temple();
            _menu = new QuestMenu();

            //dungeon.WorkFactory();
            //dungeon.Start();    
        }

        public void StartGame()
        {

            while (isRunning)
            {
                town.MainMenu(_player, _menu, Inn, Forge, Temple, dungeon);
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
                $"1단계 던전 - 권장 능력치 Atk: ({12}) Def: ({8}) 이상",
                $"2단계 던전 - 권장 능력치 Atk: ({24}) Def: ({20}) 이상",
                $"보스 던전 - 권장 능력치 Atk: ({40}) Def: ({35}) 이상",
            };

        public void Gate(DungeonMaganer dungeon)
        {
            Console.WriteLine("== 던전 선택 ==");
            int selected = BranchManager.ReturnSelect(gateOptions, true, "돌아가기");

            switch (selected)
            {
                case 1:
                    Console.Write("\n당신은 어둠을 향해 몸을 던집니다.\n\n" +
                                  "1단계 던전을 진행하려면 아무키나 입력하세요.\n\n" +
                                  ">>>");
                    Console.ReadKey();
                    dungeon.WorkFactory("1단계던전");
                    dungeon.StartDungeonStep1();    
                    break;
                case 2:
                    if (!DungeonMaganer.isClearStep1)
                    {
                        Console.Write("\n1단계 던전을 클리어하지 못한자는 도전할 자격이 없다.\n\n" +
                                      "메인 메뉴로 돌아가려면 아무키나 입력하세요.\n\n" +
                                      ">>>");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        Console.Write("자격을 증명한 자에게 문이 열린다.\n" +
                                      "2단계 던전을 진행하려면 아무키나 입력하세요.\n\n" +
                                      ">>>");
                        Console.ReadKey();
                        dungeon.WorkFactory("2단계던전");
                        dungeon.StartDungeonStep2();
                        break;
                    }
                case 3:
                    if (!DungeonMaganer.isClearStep1 && !DungeonMaganer.isClearStep2)
                    {
                        Console.Write("\n2단계 던전을 클리어하지 못한자는 도전할 자격이 없다.\n\n" +
                                      "메인 메뉴로 돌아가려면 아무키나 입력하세요.\n\n" +
                                      ">>>");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        Console.Write("\n태양의 왕좌가 당신을 부른다.\n" +
                                      "보스전을 진행하려면 아무키나 입력하세요.\n\n" +
                                      ">>>");
                        Console.ReadKey();
                        dungeon.WorkFactory("보스던전");
                        dungeon.BossBattleStart();
                        break;
                    }
                case 0:
                    Console.WriteLine("\n마을로 돌아갑니다.");
                    Console.ReadKey();
                    break;
                case -1:
                    Console.WriteLine("\n잘못된 입력입니다. 다시 시도해주세요.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}