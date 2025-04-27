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
                "대장간",
                "신전",
                "저장하기",
                "불러오기"
            };

        public void MainMenu(Player player, QuestMenu quest, Inn inn, Forge forge, Temple temple, DungeonMaganer dungeon)
        {
            Console.Clear();
            Console.WriteLine("== 메인 메뉴 ==");
            int selected = BranchManager.ReturnSelect(mainMenuOptions, true, "게임 종료");

            switch (selected)
            {
                case 1:
                    Console.Clear();
                    player.PrintStatus();
                    Console.ReadLine();
                    break;
                case 2:
                    bool limitedUse = false;
                    bool alreadyUse = false;
                    Console.Clear();
                    player.Inventory.PrintInventory(player, limitedUse, ref alreadyUse);
                    Console.ReadLine();
                    break;
                case 3:
                    Console.Clear();
                    new Shop(player).ShopPhase();
                    Console.ReadLine();
                    break;
                case 4:
                    // 던전 입장 코드 수정
                    Console.Clear();
                    gate.Gate(dungeon,player);
                    break;
                case 5:
                    Console.Clear();
                    inn.Selection(player, quest);
                    Console.ReadLine();
                    break;
                case 6:
                    Console.Clear();
                    forge.Selection(forge);
                    Console.ReadLine();
                    break;
                case 7:
                    Console.Clear();
                    temple.Selection(player);
                    Console.ReadLine();
                    break;
                case 8: // 게임 저장
                    Console.Clear();
                    gameManager.SaveGame(player, player.Inventory);
                    Console.ReadLine();
                    break;
                case 9: // 게임 불러오기
                    Console.Clear();
                    gameManager.LoadGame();
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
