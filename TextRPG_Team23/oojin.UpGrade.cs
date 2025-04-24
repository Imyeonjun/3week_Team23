using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TextRPG_Team23
{

    internal class UpGrade : Inventory
    {
        private Dictionary<int, int> UpradeChance()
        {
            return new Dictionary<int, int>
            {
                { 0, 100 },
                { 1, 90 },
                { 2, 80 },
                { 3, 70 },
                { 4, 60 },
                { 5, 50 },
                { 6, 40 },
                { 7, 30 },
                { 8, 20 },
                { 9, 10 },
            };
        }
        
        private Item selectedItem;
        public void ItemSelection()
        {
            while (true)
            {
                //Console.Clear();

                Console.WriteLine(" - 강화 할 장비를 (번호)선택하세요 - ");
                
                var equipmentItems = Items.Where(i => i.Item is Weapon || i.Item is Clothes).ToList(); // Weapon, Clothes 아이템이 equipmentitems에 저장
                if (equipmentItems.Count > 0) // 아이템이 하나 이상 들어있다면
                {
                    foreach (var itemList in equipmentItems) 
                    {
                        bool isEquipped = Array.Exists(Slots, slots => slots == itemList.Item); // 배열을 돌면서 slots변수에 들어있는 아이템이 있는지 검사해서 bool값을 반환
                        string prefix = isEquipped ? "[E] " : ""; // true라면 들어있으면 [E]를 넣어줌

                        Console.WriteLine($"{prefix}{itemList}");
                    }
                }
                else
                {
                    Console.WriteLine("강화할 장비 아이템이 없습니다.");
                    Console.WriteLine("아무키를 누르고 Enter로 나가기");
                    return;
                }
                
                int.TryParse(Console.ReadLine(), out int input);
                for (int i = 0; i < equipmentItems.Count; i++) 
                {
                    
                    if (input >= 1 && input <= equipmentItems.Count)
                    {
                        if (input == i  + 1) // 입력한 숫자와 i값이 같다면
                        {
                            Console.WriteLine();

                            selectedItem = equipmentItems[i].Item; // 입력한 숫자와 같은 인덱스에 있는 아이템을 selecteditem에 저장
                            Console.Clear();
                            ItemUpgrade();
                            return;
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 번호입니다");
                        break;
                    }
                }
            }
        }
        private void ItemUpgrade()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"아이템 '{selectedItem}' 이(가) 선택되었습니다.\n");

                Console.WriteLine(" - 아이템을 강화하시겠습니까? - ");
                Console.WriteLine(" 1. [YES] 2. [NO]");

                int.TryParse(Console.ReadLine(), out int input);
                switch (input)
                {
                    case 1:
                        ItemGamblig();
                        break;
                    case 2:
                        return;
                    default:
                        Console.WriteLine("잘못 입력 했습니다.");
                        break;
                }
            }
        }
        private void ItemGamblig()
        {

            Dictionary<int, int> upGradeChance = UpradeChance();

            Random rand = new Random();

            int roll = rand.Next(1, 101);

            if (roll <= upGradeChance[selectedItem.Upgrade])
            {
                selectedItem.UpUpgrade();
                Console.WriteLine(" - Success - ");
                Console.WriteLine($"아이템 '{selectedItem}' 이(가) + {selectedItem.Upgrade} 되었습니다.\n");
                Console.WriteLine("Enter를 눌러서 계속...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine(" - failure - ");
                Console.WriteLine(" 강화에 실패...\n");
                if (selectedItem.Upgrade >= 6 && selectedItem.Upgrade <= 8)
                {
                    selectedItem.DonwUpgrade();
                    Console.WriteLine($"아이템 '{selectedItem}' 이(가) + {selectedItem.Upgrade} 되었습니다.\n");
                }
                else if (selectedItem.Upgrade == 9)
                {
                    Console.WriteLine($"아이템 {selectedItem}이(가) 파괴되었습니다.");
                    RemoveItem(selectedItem);
                 }
                else
                {
                    Console.WriteLine("더 이상 강화할 수 없습니다.");
                }
                Console.WriteLine("Enter를 눌러서 계속...");
                Console.ReadLine();
            }
        }
    }
}
