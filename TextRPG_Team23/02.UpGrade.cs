using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TextRPG_Team23
{

    internal class UpGrade : Inventory
    {
        Obj obj = new Obj(1, 1, "▶");

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

            
            Dictionary<Item, int> itemList = new Dictionary<Item, int>();
            for (var i = 0; i < ItemDB.Items.Count; i++)
            {
                var item = ItemDB.Items[i];
                itemList.Add(item, 1);
            }
            foreach (var items in itemList)
            {
                Console.WriteLine($"{items.Key}, {items.Value}");
            }
            while (true)
            {
                Console.Clear();

                Console.WriteLine(" - 강화 할 무기를 선택하세요 - ");
                foreach (var items in itemList)
                {
                    Console.WriteLine($"{items.Key}, {items.Value}");
                }
                if (itemList.Count == 0)
                {
                    Console.WriteLine("강화 할 아이템이 없습니다.");
                    return;
                }

                obj.ObjPos(4, itemList.Count + 1, "나가기");
                obj.ObjPos(obj.x, obj.y, obj.button);

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (obj.y > 1)
                        {
                            obj.y--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (obj.y < itemList.Count + 1)
                        {
                            obj.y++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (obj.y == itemList.Count + 1)
                        {
                            return;
                        }
                        selectedItem = itemList.Keys.ElementAt(obj.y - 1);
                        ItemUpgrade();
                        break;
                }
            }

        }
        private void ItemUpgrade()
        {
            obj.y = 1;
            while (true)
            {
                Console.Clear();

                Console.WriteLine($"아이템 '{selectedItem}' 이(가) 선택되었습니다.\n");

                Console.WriteLine(" - 아이템을 강화하시겠습니까? - ");

                obj.ObjPos(3, 3, "YES");
                obj.ObjPos(3, 4, "NO");
                obj.ObjPos(obj.x, obj.y + 2, obj.button);

                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (obj.y > 1)
                        {
                            obj.y--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (obj.y < 2)
                        {
                            obj.y++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        if (obj.y == 1)
                        {
                            if(selectedItem.Upgrade > 9)
                            {
                                Console.WriteLine("\n [더 이상 강화할 수 없습니다.]");
                                Console.ReadLine();
                                return;
                            }
                            else
                            {
                                Console.Clear();
                                ItemGamblig();
                                // 돈 차감
                            }
                           
                            return;
                        }
                        else
                        {
                            return;
                        }
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
                //selectedItem.UpUpgrade();
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
                    //selectedItem.DonwUpgrade();
                    Console.WriteLine($"아이템 '{selectedItem}' 이(가) + {selectedItem.Upgrade} 되었습니다.\n");
                }
                else if (selectedItem.Upgrade == 9)
                {
                    // 파괴 로직
                    Console.WriteLine("장비파괴");
                }
                Console.WriteLine("Enter를 눌러서 계속...");
                Console.ReadLine();
            }
        }
    }
}
