using Newtonsoft.Json;
using System;

namespace TextRPG_Team23
{
    public class Quest
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int RewardGold { get; private set; }
        public bool IsCompleted { get; private set; }
        public Item? Item { get; private set; }
        public bool IsActive { get; set; }

        private Func<Player, bool> comepleteCheck;

        public Quest()
        {

        }
        public Quest(string title, string description, int rewardGold, Func<Player, bool> comepleteCheck)
        {
            Title = title;
            Description = description;
            RewardGold = rewardGold;
            IsCompleted = false;
            this.comepleteCheck = comepleteCheck;
        }

        public Quest(string title, string description, int rewardGold, Item item, Func<Player, bool> comepleteCheck)
        {
            Title = title;
            Description = description;
            RewardGold = rewardGold;
            IsCompleted = false;
            Item = item;
            IsActive = false;
            this.comepleteCheck = comepleteCheck;
        }


        public void CheckCompletion(Player player)
        {
            if (!IsCompleted && comepleteCheck(player))
            {
                IsCompleted = true;
                Console.WriteLine($"{Title} 퀘스트 완료! 보상으로 {RewardGold} 골드를 획득했습니다.");
                player.Gold += RewardGold;
                if (Item != null)
                {
                    // 플레이어 인벤토리에 아이템 추가 
                    player.Inventory.AddItem(Item);
                }
            }
        }

        public void ShowQuestInfo()
        {
            Console.WriteLine($"[퀘스트] {Title}");
            Console.WriteLine($"- 설명: {Description}");
            Console.WriteLine($"- 보상: {RewardGold} G");
            Console.WriteLine($"- 상태: {(IsCompleted ? "완료" : "미완료")}");
            Console.WriteLine();
            if (IsActive)
            {
                Console.WriteLine("이미 수령한 퀘스트 입니다.");
            }
            else
            {
                Console.WriteLine("1. 수락");
            }
            Console.WriteLine("0. 나가기");
        }
    }

    public class QuestMenu // 게임 시작시 퀘스트메뉴 초기화 해야됨
    {
        private List<Quest> allQuests;

        public QuestMenu()
        {
            allQuests = new List<Quest>
            {
            new Quest("마을을 위협하는 몬스터 처치", "몬스터 5마리를 처치하세요.", 300, (player)=>player.HasKillMonster()),
            new Quest("장비를 장착해보자", "인벤토리에서 장비를 장착해보세요.", 150, (player)=>player.HasEquippedAnyItem() ),
            new Quest("더욱 강해지기", "5레벨을 달성하세요.", 500, (player)=>player.HasLevel5()),
            };
        }

        // 플레이어에게 퀘스트 제공
        public List<Quest> GetAvailableQuests()
        {
            return new List<Quest>(allQuests);
        }

        public void ShowAllQuests(Player player)
        {
            Console.WriteLine("\n==== [전체 퀘스트 목록] ====");
            for (int i = 0; i < allQuests.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {allQuests[i].Title}");
            }

            Console.Write("\n자세히 볼 퀘스트 번호를 입력하세요: ");
            Console.WriteLine("\n0. 나가기");
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (input == 0)
                {
                    Console.WriteLine("취소되었습니다.");
                    return;
                }

                if (input >= 1 && input <= allQuests.Count)
                {
                    var selectedQuest = allQuests[input - 1];
                    if(player.QuestCheck(selectedQuest))
                    {
                        player.CheckAllQuests();
                        player.ReturnQuest(selectedQuest).ShowQuestInfo();
                    }
                    else
                    {
                        selectedQuest.ShowQuestInfo();
                    }

                    // 입력 없을 때 예외처리 수정
                    try
                    {
                        Console.WriteLine("숫자를 입력하세요:");
                        int answer = int.Parse(Console.ReadLine());

                        switch (answer)
                        {
                            case 0:
                                Console.WriteLine("나가기 입력");
                                break;
                            case 1:
                                if (selectedQuest.Title == "마을을 위협하는 몬스터 처치")
                                {
                                    player.MonsterQuest = true;
                                    Console.WriteLine("나이스\n");
                                }
                                player.AddQuest(selectedQuest); // 이미 있는지 확인하는 검사가 포함된 AddQuest 사용
                                break;
                            default:
                                Console.WriteLine("잘못된 번호입니다.");
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("유효한 숫자를 입력해야 합니다.");
                        ShowAllQuests(player);
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("입력이 비어있습니다. 숫자를 입력하세요.");
                        ShowAllQuests(player);

                    }


                }
                else
                {
                    Console.WriteLine("잘못된 번호입니다.");
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
            }
        }


        public Quest GetQuestByIndex(int index)
        {
            if (index < 0 || index >= allQuests.Count)
            {
                Console.WriteLine("해당 퀘스트는 존재하지 않습니다.");
                return null;
            }
            return allQuests[index];
        }
    }
}
