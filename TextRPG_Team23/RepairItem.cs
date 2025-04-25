using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public static class RepairItem
    {
        public static void RepairAll(Player player) //전체수리 메서드
        {
            foreach (var slot in player.Inventory.Slots)//슬롯에있는 장비템 획인 및 수리
            {
                if(slot != null && (slot.Durability < slot.MaxDurability))
                {
                    slot.RepairMax();
                }
            }

            foreach (var stack in player.Inventory.Items) // 인벤토리에 있는 아이템 확인 및 수리
            {
                if ((stack.Item is Weapon || stack.Item is Clothes) && (stack.Item.Durability < stack.Item.MaxDurability))
                {
                    stack.Item.RepairMax();
                }

            }

        }
        //public static void RepairPartial(Player player) //내구도 일정수치를 회복시켜주는 로직
        //{

        //}
    }
}
