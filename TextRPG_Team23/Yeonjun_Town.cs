using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class Town
    {
        private GameManager gameManager;
        private QuestMenu questMenu = new QuestMenu();

        DungeonTest gate = new DungeonTest();

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
                "여관",
                "대장간"
            };

        public void MainMenu(Player player)
        {
            //Console.Clear();
            Console.WriteLine("== 메인 메뉴 ==");
            int selected = BranchManager.ReturnSelect(mainMenuOptions, true, "게임 종료");

            switch (selected)
            {
                case 1:
                    //Console.WriteLine("디버그 : 상태창 출력");
                    player.PrintStatus();
                    Console.ReadLine();
                    break;
                case 2:
                    player.Inventory.PrintInventory(player);
                    Console.ReadLine();
                    break;
                case 3:
                    new Shop(player).ShopPhase();
                    Console.ReadLine();
                    break;
                case 4:
                    Console.WriteLine("디버그 : 던전 출력");
                    Console.ReadLine();
                    gate.Gate();
                    break;
                case 5:
                    new Inn().Selection(player, questMenu);
                    Console.ReadLine();
                    break;
                case 6:
                    new Forge().Selection();
                    Console.ReadLine();
                    
                    break;
                case 0:
                    Console.WriteLine("게임을 종료합니다.");
                    Console.ReadLine();
                    gameManager.StopGame();
                    break;
                case -1:
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
