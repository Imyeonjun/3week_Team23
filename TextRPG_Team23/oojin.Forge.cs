using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextRPG_Team23
{
    public class Forge
    {
        public Player player;
        public Forge(Player player) { this.player = player; }

        public void Selection(Forge forge)
        {
            UpGrade upGrade = new UpGrade(player);

            Console.Clear();
            Console.WriteLine("\n == @@ 대장간에 어서오세요 ==");
            Console.Write("\n1. 강화\n2. 수리\n0. 나가기\n\n선택 >> ");
            int.TryParse(Console.ReadLine(), out int input);

            if (input > 0 && input <= 2)
            {
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        upGrade.ItemSelection(forge);
                        break;
                    case 2:
                        bool needGold = true;
                        Repair(player, needGold);
                        break;
                }
            }
            else if (input == 0)
            {
                Console.WriteLine("\n마을로 돌아갑니다.");
                Console.WriteLine("Enter키를 눌러주세요");
            }
            else
            {
                Console.WriteLine("\n잘못 입력했습니다, 다시 입력해 주세요");
                Selection(forge);
            }
        }
        public static void Repair(Player player, bool needGold)
        {
            Console.Clear();
            
            if (player.Inventory.CheckAllDurabilityIsFull())
            {
                Console.WriteLine("\n수리할 아이템이 없습니다.\nEnter키를 눌러주세요.");
                if (!needGold)
                {
                    Console.WriteLine("하지만 수리요정은 출장권을 환불해주지않았습니다.");
                }
                return;
            }

            Console.WriteLine("\n == 수리할 아이템을 선택하세요 == \n");

            int i = 0;
            var items = player.Inventory.Items.Where(i => i.Item is Weapon || i.Item is Clothes).ToList();
            foreach ( var itemList in items )
            {
                bool isItme = Array.Exists(player.Inventory.Slots, slots => slots == itemList.Item);
                string Mounting = isItme ? "[E] " : "";

                Console.WriteLine($"{i + 1}. {Mounting}{itemList}");
                i++;
            }
            Console.Write("0. 나가기 \n\n선택 >> ");
            int.TryParse(Console.ReadLine(), out int index);
            if (index >= 0 && index <= items.Count)
            {
                if (index == 0)
                {
                    Console.WriteLine("Enter키를 눌러주세요");
                    return;
                }
                var selectedItem = items[index - 1].Item;
                int cost = (selectedItem.MaxDurability - selectedItem.Durability) * 10;
                if (needGold)
                {
                    if (player.Gold >= cost)
                    {
                        player.Gold -= cost;
                        selectedItem.RepairMax();
                        Console.WriteLine($"수리가 완료되었습니다. {cost}G 소모");
                        Console.WriteLine("Enter키를 눌러 나가기");
                    }
                    else
                    {
                        Console.WriteLine("골드가 부족해 수리할수 없습니다.");
                        return;
                    }
                }
                else
                {
                    Console.Clear();
                    selectedItem.RepairMax();  // 여기서 호출!
                    Console.WriteLine("Enter키를 눌러주세요");
                    return;
                }

                
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
