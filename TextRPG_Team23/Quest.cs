//using System;

//namespace TextRPG_Team23
//{
//    public class Quest
//    {
//        public string Title { get; private set; }
//        public string Description { get; private set; }
//        public int RewardGold { get; private set; }
//        public bool IsCompleted { get; private set; }
//        public Item Item { get; private set; }

//        public Quest(string title, string description, int rewardGold)
//        {
//            Title = title;
//            Description = description;
//            RewardGold = rewardGold;
//            IsCompleted = false;
//        }

//        public Quest(string title, string description, int rewardGold, Item item)
//        {
//            Title = title;
//            Description = description;
//            RewardGold = rewardGold;
//            IsCompleted = false;
//            Item = item;
//        }

//        public void CompleteQuest(Player player)
//        {
//            IsCompleted = true;
//            Console.WriteLine($"퀘스트 완료! 보상으로 {RewardGold} 골드를 획득했습니다.");
//            player.Gold += RewardGold;
//            if(Item != null)
//            {
//                // 플레이어 인벤토리에 아이템 추가 
//            }
//        }

//        public void ShowQuestInfo()
//        {
//            Console.WriteLine($"[퀘스트] {Title}");
//            Console.WriteLine($"- 설명: {Description}");
//            Console.WriteLine($"- 보상: {RewardGold} G");
//            Console.WriteLine($"- 상태: {(IsCompleted ? "완료" : "미완료")}");
//        }
//    }

//}
