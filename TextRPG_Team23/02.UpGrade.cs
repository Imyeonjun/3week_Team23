//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TextRPG_Team23;


//namespace TimeProject
//{
//    //dd
//    internal class UpGrade : Inventory
//    {
//        Obj obj = new Obj(1, 1, "▶");

//        private Weapon weapon;
//        ItemInfo itemInfo;
//        private Dictionary<int, int> UpradeChance()
//        {
//            return new Dictionary<int, int>
//            {
//                { 0, 100 },
//                { 1, 90 },
//                { 2, 80 },
//                { 3, 70 },
//                { 4, 60 },
//                { 5, 50 },
//                { 6, 40 },
//                { 7, 30 },
//                { 8, 20 },
//                { 9, 10 },
//            };
//        }
//        public void ItemSelection()
//        {
//            while (true)
//            {
//                Console.Clear();

//                Console.WriteLine(" - 강화 할 무기를 선택하세요 - ");
//                ItemList();

//                Console.SetCursorPosition(4, 4);
//                Console.WriteLine("나가기");

//                Console.SetCursorPosition(obj.x, obj.y);
//                Console.WriteLine(obj.button);

//                ConsoleKeyInfo key = Console.ReadKey(true);

//                switch (key.Key)
//                {
//                    case ConsoleKey.UpArrow:
//                        if (obj.y > 1)
//                        {
//                            obj.y--;
//                        }
//                        break;
//                    case ConsoleKey.DownArrow:
//                        if (obj.y < itemData.Count + 1)
//                        {
//                            obj.y++;
//                        }
//                        break;
//                    case ConsoleKey.Enter:
//                        if (obj.y == 4)
//                        {
//                            return;
//                        }
//                        weapon = itemData.Keys.ElementAt(obj.y - 1);
//                        itemInfo = itemData[weapon];
//                        ItemUpgrade();
//                        break;
//                }
//            }

//        }
//        private void ItemUpgrade()
//        {
//            obj.y = 1;
//            while (true)
//            {
//                Console.Clear();

//                Console.WriteLine($"아이템 '{weapon}' 이(가) 선택되었습니다.\n");

//                Console.WriteLine(" - 아이템을 강화하시겠습니까? - ");

//                Console.SetCursorPosition(3, 3);
//                Console.WriteLine("YES");

//                Console.SetCursorPosition(3, 4);
//                Console.WriteLine("NO");

//                Console.SetCursorPosition(obj.x, obj.y + 2);
//                Console.WriteLine(obj.button);

//                ConsoleKeyInfo key = Console.ReadKey();
//                switch (key.Key)
//                {
//                    case ConsoleKey.UpArrow:
//                        if (obj.y > 1)
//                        {
//                            obj.y--;
//                        }
//                        break;
//                    case ConsoleKey.DownArrow:
//                        if (obj.y < 2)
//                        {
//                            obj.y++;
//                        }
//                        break;
//                    case ConsoleKey.Enter:
//                        if (obj.y == 1)
//                        {
//                            Console.Clear();
//                            ItemGamblig();
//                            return;
//                        }
//                        else
//                        {
//                            return;
//                        }
//                }
//            }
//        }
//        private void ItemGamblig()
//        {

//            Dictionary<int, int> upGradeChance = UpradeChance();

//            Random rand = new Random();

//            int roll = rand.Next(1, 101);

//            if (roll <= upGradeChance[itemInfo.upgrade])
//            {
//                itemInfo.upgrade++;
//                Console.WriteLine(" - Success - ");
//                Console.WriteLine($"아이템 '{weapon}' 이(가) + {itemInfo.upgrade} 되었습니다.\n");
//                Console.WriteLine("Enter를 눌러서 계속...");
//                Console.ReadLine();
//            }
//            else
//            {
//                Console.WriteLine(" - failure - ");
//                Console.WriteLine(" 강화에 실패...\n");
//                if (itemInfo.upgrade >= 6 && itemInfo.upgrade <= 8)
//                {
//                    itemInfo.upgrade--;
//                    Console.WriteLine($"아이템 '{weapon}' 이(가) + {itemInfo.upgrade} 되었습니다.\n");
//                }
//                else if (itemInfo.upgrade == 9)
//                {
//                    Console.WriteLine("장비파괴");
//                }
//                Console.WriteLine("Enter를 눌러서 계속...");
//                Console.ReadLine();
//            }
//        }
//    }
//}
