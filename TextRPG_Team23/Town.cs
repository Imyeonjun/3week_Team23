using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class Town
    {
        // 인스턴스 생성
        private BranchManager menu = new BranchManager();
        private GameManager gameManager;

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
}
