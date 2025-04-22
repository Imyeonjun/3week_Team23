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
                Console.WriteLine($"퀘스트 완료! 보상으로 {RewardGold} 골드를 획득했습니다.");
                player.Gold += RewardGold;
                if (Item != null)
                {
                    // 플레이어 인벤토리에 아이템 추가 
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
            if(IsActive)
            {
                Console.WriteLine("이미 수령한 퀘스트 입니다.");

            } else
            {
                Console.WriteLine("1. 수락");
            }
            Console.WriteLine("0. 나가기");
        }
    }

    public class QuestMenu
    {
        private List<Quest> allQuests;

        public QuestMenu()
        {
            allQuests = new List<Quest>
        {
            new Quest("마을을 위협하는 몬스터 처치", "몬스터 5마리를 처치하세요.", 300, null/*(player)=>{player.~~~}*/),
            new Quest("장비를 장착해보자", "인벤토리에서 장비를 장착해보세요.", 150, null/*(player)=>{player.~~~}*/),
            new Quest("더욱 강해지기", "5레벨을 달성하세요.", 500, null/*(player)=>{player.~~~}*/),
        };
        }

        // 플레이어에게 퀘스트 제공
        public List<Quest> GetAvailableQuests()
        {
            return new List<Quest>(allQuests);
        }

        public void ShowAllQuests()
        {
            Console.WriteLine("\n==== [전체 퀘스트 목록] ====");
            for (int i = 0; i < allQuests.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {allQuests[i].Title}");
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
