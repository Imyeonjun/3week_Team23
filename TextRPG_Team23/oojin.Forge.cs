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

        public void Selection()
        {
            UpGrade upGrade = new UpGrade(player);
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("- @@대장간에 어서오세요 - \n\n1. 강화   2. 제작   3. 수리  0. 나가기");
                int.TryParse(Console.ReadLine(), out int input);

                if (input > 0 && input <= 2)
                {
                    switch (input)
                    {
                        case 1:
                            //Console.Clear();
                            upGrade.ItemSelection();
                            return;
                            break;
                        case 2:
                            break;
                        case 3:
                            Repair();
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
        private void Repair()
        {
            bool re = false;
            //Console.Clear();
            Console.WriteLine(" == 수리할 아이템 선택 == \n");

            var items = player.Inventory.Items;

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].Item.Name} (내구도: {items[i].Item.Durability}/{items[i].Item.MaxDurability})\n");
            }

            do
            {
                Console.WriteLine(" == 수리할 아이템 번호를 입력하세요 == ");
                int.TryParse(Console.ReadLine(), out int index);
                if (index > 0 && index <= items.Count)
                {
                    items[index - 1].Item.RepairMax();  // 여기서 호출!
                    return;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
            while (!re);
           
        }
    }
}
