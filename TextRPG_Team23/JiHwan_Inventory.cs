﻿

namespace TextRPG_Team23
{
    public class ItemStack //아이템 수량정보
    {
        public Item Item { get; private set; }
        public int Quantity { get; private set; }

        public ItemStack(Item item, int quantity = 1)
        {
            Item = item;
            Quantity = quantity;
        }

        public void Add(int amount = 1) => Quantity += amount;
        public void Decrease(int amount = 1)
        {
            Quantity = Math.Max(Quantity - amount, 0);
        }
        public bool Use(Player player)
        {
            if (Item.Use(player)) //아이템 사용 시도 성공 시
            {
                if (Item is Consumable) //사용하는 아이템이 소모품일시
                {
                    Quantity--;

                }
                return Quantity > 0;

            }
            Console.WriteLine($"{Item.Name}은 사용할수 없는 아이템입니다.");
            return true;
        }

        public override string ToString()
        {
            string info = Item.ToString();
            return (Item is Consumable && Quantity > 1) ? $"{info} x{Quantity}" : info;
        }
    }

    public class Inventory //아이템 목록, 장비슬롯
    {
        public List<ItemStack> Items { get; private set; } = new List<ItemStack>();
        public Item[] Slots = new Item[2]; // 무기, 방어구
        public List<ItemStack> GetAllItems() => Items;
        public Inventory()
        {

        }

        public Inventory(List<ItemStack> newItems)
        {
            Items = newItems;
        }

        public void AddItem(Item newItem) //아이템 추가 로직
        {
            var existing = Items.FirstOrDefault(i => i.Item.Name == newItem.Name);
            if (existing != null)
                existing.Add();
            else
                Items.Add(new ItemStack(newItem));
        }

        public void RemoveItem(Item item) //아이템 삭제 로직
        {
            var target = Items.FirstOrDefault(i => i.Item == item);
            if (target != null)
                Items.Remove(target);
        }

        public void PrintInventory(Player player, bool limitedUse, ref bool alreadyUse) //인벤토리 UI 출력
        {
            while (true)
            {
                //Console.Clear();
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

                Console.WriteLine("\n1. 장착 관리");               
                Console.WriteLine("2. 아이템 사용");
                Console.WriteLine("0. 나가기");
                Console.WriteLine("\n원하는 행동의 숫자를 입력해주세요");
                Console.Write("\n>>>");
                string input = Console.ReadLine();

                if (input == "1")
                    ManageEquipment(player, sortedItems);
                else if (input == "2")
                {
                    bool used = UseItemPhase(player, limitedUse, alreadyUse);
                    if (used) alreadyUse = true;
                }

                else if (input == "0")
                {
                    Console.WriteLine("인벤토리를 닫습니다.");
                    return;
                }
                else
                    Console.WriteLine("잘못 입력하셨습니다.");
            }
        }

        public void ManageEquipment(Player player, List<ItemStack> sortedItems)//장비 관리 UI 및 시스템
        {
            while (true)
            {
                //Console.Clear();
                List<IEquipable> equipables = new List<IEquipable>();
                foreach (var invItem in sortedItems)
                {
                    if (invItem.Item is IEquipable eq)
                        equipables.Add(eq);
                }

                if (equipables.Count == 0)
                {
                    Console.WriteLine("장착 가능한 아이템이 없습니다.");
                    //Console.ReadKey();
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
                Console.Write("\n장착하거나 해제할 아이템 번호를 입력하세요.");
                Console.Write("\n>>>");
                string sel = Console.ReadLine();

                if (int.TryParse(sel, out int selected))
                {
                    if (selected == 0)
                    {
                        Console.WriteLine("장착 관리를 종료합니다.");
                        return;
                    }
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
                        player.RecalculateStats();
                        //Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("해당 번호의 아이템은 존재하지 않습니다.");
                        //Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    //Console.ReadKey();
                }
            }
        }
        public bool UseItemPhase(Player player, bool limitedUse, bool alreadyUse)//아이템 사용 페이즈
        {
            while (true)
            {
                //Console.Clear();
                if (alreadyUse)
                {
                    Console.WriteLine("이미 아이템을 사용했습니다. 이번 턴에는 사용할 수 없습니다.");
                    return true;
                }

                var usableItems = Items.Where(i => i.Item is Consumable).ToList();

                if (usableItems.Count == 0)
                {
                    Console.WriteLine("사용 가능한 소비 아이템이 없습니다.");
                    if (limitedUse) return true;
                    return false;
                }
                Console.WriteLine("사용 가능한 아이템 목록:");
                for (int i = 0; i < usableItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {usableItems[i]}");
                }
                Console.WriteLine("0: 나가기");
                Console.WriteLine("\n원하는 행동의 숫자를 입력해주세요");
                Console.Write("\n>>>");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int selected))
                {
                    if (selected == 0) 
                    {
                        Console.WriteLine("아이템 사용창을 닫습니다.");
                        return false;
                    }
                    if (selected > 0 && selected <= usableItems.Count)
                    {
                        var selectedItem = usableItems[selected - 1];
                        bool isExists = selectedItem.Use(player); //실제 아이템 사용

                        if (!isExists)
                        {
                            Items.Remove(selectedItem);
                            Console.WriteLine($"{selectedItem.Item.Name}을 모두 사용했습니다.");

                        }
                        if (limitedUse) return true;
                        return false;

                    }
                    else
                    {
                        Console.WriteLine("해당 번호의 아이템은 존재하지 않습니다.");
                    }
                
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    
                }
                
            }
        }
        public bool CheckAllDurabilityIsFull()
        {
            foreach (var stack in Items)
            {
                if (stack.Item is Weapon || stack.Item is Clothes)
                {
                    if (stack.Item.Durability != stack.Item.MaxDurability)
                    {
                        // 내구도가 깎인 장비가 있다
                        return false;
                    }
                }
            }
            // 모든 장비가 풀내구도다
            return true;
        }



        //public void CheckEquipmentDurability(Player player)//전투시에 장비중인 아이템 내구 감소 및 내구0일때 장착해제 기능 현재는 안씀
        //{
        //    for (int i = 0; i < Slots.Length; i++)
        //    {
        //        if (Slots[i] is Item equiped && equiped.Durability > 0)
        //        {
        //            equiped.ReduceDurability();

        //            if (equiped.IsBroken)
        //            {
        //                Console.WriteLine($"{equiped.Name}의 내구도가 0이되어 장비가 망가졌습니다.");
        //                Slots[i] = null;
        //                player.RecalculateStats();
        //                Console.ReadKey();
        //            }
        //        }
        //    }
        //}

        public void CheckWeaponDurability(Player player)//장착중인 무기 내구감소 및 자동해제
        {
            int i = (int)EquipSlot.Weapon;

            if (Slots[i] is Item equiped && equiped.Durability > 0)
            {
                equiped.ReduceDurability();

                if (equiped.IsBroken)
                {
                    Console.WriteLine($"{equiped.Name}의 내구도가 0이되어 장비가 망가졌습니다.");
                    Slots[i] = null;
                    player.RecalculateStats();
                }
            }

        }
        public void CheckClothesDurability(Player player) //장착중인 방어구 내구감소 및 자동해제
        {
            int i = (int)EquipSlot.Clothes;

            if (Slots[i] is Item equiped && equiped.Durability > 0)
            {
                equiped.ReduceDurability();

                if (equiped.IsBroken)
                {
                    Console.WriteLine($"{equiped.Name}의 내구도가 0이되어 장비가 망가졌습니다.");
                    Slots[i] = null;
                    player.RecalculateStats();
                }
            }

        }
    }
}