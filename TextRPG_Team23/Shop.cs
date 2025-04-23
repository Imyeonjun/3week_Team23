using System;
using System.Numerics;

namespace TextRPG_Team23
{
    public class Shop
    {
        private Player player;

        public Shop(Player player)
        {
            this.player = player;
        }

        public void ShopPhase()
        {
            while (true)
            {
                Console.WriteLine("=== 상점 ===");
                Console.WriteLine($"[보유 골드] {player.Gold}G\n");
                Console.WriteLine("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기");
                Console.Write("원하시는 행동을 입력해주세요: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        {
                            BuyItem();
                            break;
                        }
                    case "2":
                        {
                            SellItem();
                            break;
                        }
                    case "0":
                        return;
                    default:
                        Console.WriteLine("잘못된 입력입니다.");
                        break;

                }
            }
        }
        private void BuyItem()
        {
            while (true)
            {
                Console.WriteLine($"\n[보유 골드] {player.Gold} G\n");
                Console.WriteLine("[아이템 구매 목록]");
                for (int i = 0; i < ItemDB.Items.Count; i++)
                {
                    Item item = ItemDB.Items[i];
                    bool alreadyOwned = !(item is Consumable) &&
                        player.Inventory.Items.Any(stack => stack.Item.Name == item.Name);

                    if (alreadyOwned)
                    {
                        Console.WriteLine($"{i + 1}. {item.Name} | {item.Description} | 구매완료");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {item}");
                    }
                }

                Console.WriteLine("0. 나가기");
                Console.Write("구매할 아이템 번호를 입력하세요: ");
                string selectedInput = Console.ReadLine();

                if (int.TryParse(selectedInput, out int selected))
                {
                    if (selected == 0)
                    {
                        Console.WriteLine("상점 구매 메뉴를 종료합니다.");
                        return;
                    }

                    if (selected > 0 && selected <= ItemDB.Items.Count)
                    {
                        Item item = ItemDB.Items[selected - 1];
                        bool alreadyOwned = !(item is Consumable) && player.Inventory.Items.Any(stack => stack.Item.Name == item.Name);

                        if (alreadyOwned)
                        {
                            Console.WriteLine($"이미 보유 중인 아이템입니다: {item.Name}");
                        }
                        else if (player.Gold >= item.Price)
                        {
                            player.Gold -= item.Price;
                            player.Inventory.AddItem(item);
                            Console.WriteLine($"{item.Name}을 구매했습니다!");
                        }
                        else
                        {
                            Console.WriteLine("골드가 부족합니다.");
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

        private void SellItem()
        {
            while (true)
            {
                var items = player.Inventory.Items;
                if (items.Count == 0)
                {
                    Console.WriteLine("판매할 아이템이 없습니다.");
                    return;
                }

                Console.WriteLine($"\n[보유 골드] {player.Gold} G\n");
                Console.WriteLine("[아이템 판매 목록]");
                for (int i = 0; i < items.Count; i++)
                {
                    var stack = items[i];
                    bool isEquipped = player.Inventory.Slots.Contains(stack.Item);
                    string prefix = isEquipped ? "[E] " : "";
                    Console.WriteLine($"{i + 1}. {prefix}{stack}");
                }
                Console.WriteLine("0. 나가기");
                Console.Write("판매할 아이템 번호를 입력하세요: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int selected))
                {
                    if (selected == 0)
                    {
                        Console.WriteLine("상점 판매 메뉴를 종료합니다.");
                        return;
                    }
                    else if (selected > 0 && selected <= items.Count)
                    {
                        // 유효한 번호 → 아이템 판매
                        var stack = items[selected - 1];
                        var item = stack.Item;
                        if (player.Inventory.Slots.Contains(stack.Item))// 장비중인 아이템 이면
                        {
                            Console.WriteLine("장비중인 아이템 입니다. 판매할 수 없습니다.");
                        }
                        else
                        {
                            int sellPrice = (int)(item.Price * 0.85f);
                            player.Gold += sellPrice;
                            Console.WriteLine($"{item.Name}을 판매했습니다. (+{sellPrice}G)");
                            stack.Decrease();
                            if (stack.Quantity <= 0)
                            {
                                items.Remove(stack);
                            }

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
}
