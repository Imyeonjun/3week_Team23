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
            Console.WriteLine(" == @@ 대장간에 어서오세요 ==");
            while (true)
            {
                //Console.Clear();
               
                Console.Write("\n1. 강화  2. 제작  3. 수리 0. 나가기 \n>>> ");
                int.TryParse(Console.ReadLine(), out int input);

                if (input > 0 && input <= 3)
                {
                    switch (input)
                    {
                        case 1:
                            //Console.Clear();
                            upGrade.ItemSelection(forge);
                            break;
                        case 2:
                            Console.WriteLine("미구현");
                            Console.WriteLine("Enter키를 눌러주세요");
                            return;
                            break;
                        case 3:
                            Repair(player);
                            break;
                    }
                }
                else if (input ==  0)
                {
                    Console.WriteLine("마을로 돌아갑니다.");
                    Console.WriteLine("Enter키를 눌러주세요");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못 입력했습니다, 다시 입력해 주세요\n");
                }
            }
        }
        public static void Repair(Player player)
        {
            //Console.Clear();
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
            Console.Write("0. 나가기 \n>>> ");
            int.TryParse(Console.ReadLine(), out int index);
            if (index >= 0 && index <= items.Count)
            {
                if (index == 0)
                {
                    return;
                }
                items[index - 1].Item.RepairMax();  // 여기서 호출!
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
