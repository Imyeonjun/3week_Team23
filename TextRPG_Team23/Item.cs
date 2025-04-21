using System;
using System.Collections.Generic;
using System.Numerics;

namespace TextRPG_Team23
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public int Price { get; protected set; }
        public string Description { get; protected set; }

        public int Durability { get; protected set; } = -1;
        public int MaxDurability { get; protected set; } = -1;

        protected Item(string name, int price, string description, int durability = -1)
        {
            Name = name;
            Price = price;
            Description = description;
            Durability = durability;
            MaxDurability = durability;
        }

        public virtual void Use()
        {
            if (Durability > 0)
                Durability--;
        }

        public bool IsBroken => Durability == 0;
    }

    public interface IEquipable
    {
        EquipSlot SlotType { get; }
        void Equip(Player player);
    }

    public enum EquipSlot
    {
        Weapon,
        Clothes
    }

    public class Weapon : Item, IEquipable
    {
        public int Atk { get; private set; }

        public Weapon(string name, int price, string description, int atk, int durability = -1)
            : base(name, price, description, durability)
        {
            Atk = atk;
        }

        public EquipSlot SlotType => EquipSlot.Weapon;

        public void Equip(Player player)
        {
            player.Inventory.Slots[(int)SlotType] = this;
            Console.WriteLine($"{Name}을 장착했습니다. (공격력 +{Atk})");
        }

        public override string ToString()
        {
            string durText = (Durability >= 0) ? $" | 내구도 {Durability}/{MaxDurability}" : "";
            return $"{Name} | 공격력 +{Atk} | {Description}{durText} | 상점가 {Price}G";
        }
    }

    public class Clothes : Item, IEquipable
    {
        public int Def { get; private set; }

        public Clothes(string name, int price, string description, int def, int durability = -1)
            : base(name, price, description, durability)
        {
            Def = def;
        }

        public EquipSlot SlotType => EquipSlot.Clothes;

        public void Equip(Player player)
        {
            player.Inventory.Slots[(int)SlotType] = this;
            Console.WriteLine($"{Name}을 장착했습니다. (방어력 +{Def})");
        }

        public override string ToString()
        {
            string durText = (Durability >= 0) ? $" | 내구도 {Durability}/{MaxDurability}" : "";
            return $"{Name} | 방어력 +{Def} | {Description}{durText} | 상점가 {Price}G";
        }
    }

    public class Consumable : Item
    {
        private Action<Player> effect;

        public Consumable(string name, int price, string description, Action<Player> effect)
            : base(name, price, description, durability: 1)
        {
            this.effect = effect;
        }

        public override void Use()
        {
            base.Use();
            effect?.Invoke(Player._player); // 또는 외부에서 player 전달
            Console.WriteLine($"{Name}을 사용했습니다. {Description}");
        }

        public override string ToString()
        {
            return $"{Name} | {Description} | 상점가 {Price}G";
        }
    }

    public static class ItemDB
    {
        public static List<Item> Items = new List<Item>()
        {
            new Weapon("철검", 100, "단순한 철검이다.", 10, 10),
            new Weapon("은검", 200, "은으로 제작된 검이다.", 20, 15),
            new Clothes("낡은 옷", 50, "낡은 방어구입니다.", 5, 8),
            new Clothes("가죽 갑옷", 150, "튼튼한 가죽 방어구입니다.", 10, 12),
            new Consumable("체력 포션", 50, "체력을 30 회복합니다.", p => p.Hp = Math.Min(p.MaxHp, p.Hp + 30)),
            new Consumable("마나 포션", 50, "마나를 30 회복합니다.", p => p.Mp = Math.Min(p.MaxMp, p.Mp + 30))
        };
    }
}