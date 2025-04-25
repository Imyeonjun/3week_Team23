using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TextRPG_Team23
{
    internal class UpGrade
    {
        private Player player;
        private Dictionary<int, int> 
            UpradeChance()
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
                { 9, 100 },
            };
        }
        public UpGrade(Player player)
        {
            this.player = player;
        }

        private Item selectedItem;
        public void ItemSelection(Forge forge)
        {
            while (true)
            {
                //Console.Clear();

                var equipmentItems = player.Inventory.Items.Where(i => i.Item is Weapon || i.Item is Clothes).ToList(); // Weapon, Clothes 아이템이 equipmentitems에 저장
                if (equipmentItems.Count > 0) // 아이템이 하나 이상 들어있다면
                {

                    Console.WriteLine("\n == 강화 할 장비를 (번호)선택하세요 ==");
                    int i = 0;

                    foreach (var itemList in equipmentItems)
                    {
                        bool isEquipped = Array.Exists(player.Inventory.Slots, slots => slots == itemList.Item); // 배열을 돌면서 slots변수에 들어있는 아이템이 있는지 검사해서 bool값을 반환
                        string prefix = isEquipped ? "[E] " : ""; // true라면 들어있으면 [E]를 넣어줌

                        Console.WriteLine($"{i + 1}. {prefix}{itemList}");
                        i++;
                    }
                    Console.Write("0. 나가기\n>>> ");
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
                    if (input >= 0 && input <= equipmentItems.Count)
                    {
                        if (input == i + 1) // 입력한 숫자와 i값이 같다면
                        {
                            selectedItem = equipmentItems[i].Item; // 입력한 숫자와 같은 인덱스에 있는 아이템을 selecteditem에 저장
                            Console.WriteLine($"아이템 \"{selectedItem.Name}\" 이(가) 선택되었습니다.\n");
                            if (selectedItem.Upgrade == 10)
                            {
                                Console.WriteLine("더 이상 강화할 수 없습니다, 다른 아이템을 골라 주세요\n");
                                break;
                            }
                            //Console.Clear();
                            ItemUpgrade(forge);
                            break;
                        }
                        else if (input == 0)
                        {
                            Console.Write('\n');
                            return;
                        }
                    }
                    else
                    {
                        BranchManager.ErrorMessage("잘못된 번호입니다,  Enter를 누른 후 다시 입력해주세요.");
                        //Console.WriteLine("잘못된 번호입니다");
                        break;
                    }
                }
            }
        }
        private void ItemUpgrade(Forge forge)
        {
            while (true)
            {
                //Console.Clear();
                if(selectedItem == null)
                {
                    return;
                }
                if (selectedItem.Upgrade == 10)
                {
                    return;
                }
                Console.WriteLine($" == 아이템\"{selectedItem.Name}\" 을(를) 강화하시겠습니까? == ");
                Console.Write(" 1. [YES] 2. [NO] \n>>> ");

                int.TryParse(Console.ReadLine(), out int input);
                if (input > 0 && input <= 2)
                {
                    switch (input)
                    {
                        case 1:
                            ItemGamblig(forge);
                            break;
                        case 2:
                            return;
                    }
                }
                else
                {
                    BranchManager.ErrorMessage("잘못 입력했습니다, Enter를 누른 후 다시 입력해주세요");
                    //Console.WriteLine("잘못 입력했습니다.");
                }
            }
        }
        private void ItemGamblig(Forge forge)
        {

            Dictionary<int, int> upGradeChance = UpradeChance();

            Random rand = new Random();

            int roll = rand.Next(1, 101);

            if (roll <= upGradeChance[selectedItem.Upgrade]) // 강화에 성공
            {
                selectedItem.Upgrade++;
                Console.WriteLine(" == Success == ");
                Console.WriteLine($"아이템 \"{selectedItem.Name}\" 이(가) {selectedItem.Upgrade} + {selectedItem.Name} 되었습니다.\n");
            }
            else // 강화에 실패
            {
                Console.WriteLine(" == failure == ");
                Console.WriteLine($"강화에 실패 했습니다.\n");
                if (selectedItem.Upgrade >= 6 && selectedItem.Upgrade <= 8)
                {
                    //selectedItem.Upgrade--;
                    Console.WriteLine($"아이템 \"{selectedItem.Name}\" 이(가) \"+ {selectedItem.Upgrade}\" 되었습니다.\n");
                }
                else if (selectedItem.Upgrade == 9)
                {
                    
                    Console.WriteLine($"아이템 \"{selectedItem.Name}\"이(가) 파괴되었습니다.\n");
                    player.Inventory.RemoveItem(selectedItem);
                    selectedItem = null;

                    return;
                }
            }
        }
    }
}
