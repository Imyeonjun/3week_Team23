using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace TextRPG_Team23
{
    public class InventoryItem
    {
        public Item Item { get; private set; }
        public int Quantity { get; private set; }

        public InventoryItem(Item item, int quantity = 1)
        {
            Item = item;
            Quantity = quantity;
        }

        public void Add(int amount = 1) => Quantity += amount;

        public bool Use(Player player)
        {
            if (Item is Consumable consumable)
            {
                consumable.Use(player);
                Quantity--;
                return Quantity > 0;
            }
            return true;
        }

        public override string ToString()
        {
            string info = Item.ToString();
            return (Item is Consumable && Quantity > 1) ? $"{info} x{Quantity}" : info;
        }
    }

    public class Inventory
    {
        public List<InventoryItem> Items { get; private set; } = new List<InventoryItem>();
        public Item[] Slots = new Item[2]; // 무기, 방어구

        public void AddItem(Item newItem)
        {
            var existing = Items.FirstOrDefault(i => i.Item.Name == newItem.Name);
            if (existing != null)
                existing.Add();
            else
                Items.Add(new InventoryItem(newItem));
        }

        public void RemoveItem(Item item)
        {
            var target = Items.FirstOrDefault(i => i.Item == item);
            if (target != null)
                Items.Remove(target);
        }

        public void PrintInventory(Player player) //인벤토리 화면 출력
        {
            while (true)
            {
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템 목록:");

                var sortedItems = Items.OrderBy(i =>
                {
                    if (i.Item is Weapon) return 0;
                    if (i.Item is Clothes) return 1;
                    if (i.Item is Consumable) return 2;
                    return 3;
                }).ToList();

                foreach (var invItem in sortedItems)
                {
                    bool isEquipped = Array.Exists(Slots, slot => slot == invItem.Item);
                    string prefix = isEquipped ? "[E] " : "";
                    Console.WriteLine($"{prefix}{invItem}");
                }

                Console.WriteLine("\n1. 장착 관리\n0. 나가기");
                string input = Console.ReadLine();

                if (input == "1")
                    ManageEquipment(player, sortedItems);
                else if (input == "0")
                    break;
                else
                    Console.WriteLine("잘못 입력하셨습니다.");
            }
        }

        public void ManageEquipment(Player player, List<InventoryItem> sortedItems)//장비 관리 화면 및 시스템
        {
            List<IEquipable> equipables = new List<IEquipable>();
            foreach (var invItem in sortedItems)
            {
                if (invItem.Item is IEquipable eq)
                    equipables.Add(eq);
            }

            if (equipables.Count == 0)
            {
                Console.WriteLine("장착 가능한 아이템이 없습니다.");
                return;
            }

            Console.WriteLine("\n[장착 가능한 아이템 목록]");
            for (int i = 0; i < equipables.Count; i++)
            {
                var item = (Item)equipables[i];
                var slotType = equipables[i].SlotType;
                bool isEquipped = Slots[(int)slotType] == item;
                string prefix = isEquipped ? "[E] " : "";
                Console.WriteLine($"{i + 1}. {prefix}{item}");
            }

            Console.WriteLine("0. 나가기");
            Console.Write("장착하거나 해제할 아이템 번호를 입력하세요: ");
            string sel = Console.ReadLine();

            if (int.TryParse(sel, out int selected))
            {
                if (selected == 0) return;
                if (selected > 0 && selected <= equipables.Count)
                {
                    IEquipable selectedItem = equipables[selected - 1];
                    var slotIndex = (int)selectedItem.SlotType;

                    if (Slots[slotIndex] == selectedItem)
                    {
                        Slots[slotIndex] = null;
                        Console.WriteLine($"{((Item)selectedItem).Name}을(를) 해제했습니다.");
                    }
                    else
                    {
                        selectedItem.Equip(player);
                    }
                }
                else
                {
                    Console.WriteLine("해당 번호의 아이템은 존재하지 않습니다.");
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
            }
        }
    }
}