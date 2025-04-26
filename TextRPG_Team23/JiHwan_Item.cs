

namespace TextRPG_Team23
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public int Price { get; protected set; }
        public string Description { get; protected set; }

        public int Durability { get; protected set; } = -1;
        public int MaxDurability { get; protected set; } = -1;

        public int Upgrade { get; set; } = 0;

        public bool IsHidden { get; set; } = false;

        protected Item(int upgrade, string name, int price, string description, int durability = -1)//장비아이템 용 생성자
        {
            Upgrade = upgrade;
            Name = name;
            Price = price;
            Description = description;
            Durability = durability;
            MaxDurability = durability;
        }

        protected Item(string name, int price, string description) //소모품 용 생성자
        {
            Name = name;
            Price = price;
            Description = description;
        }

        public virtual void ReduceDurability() //전투시 내구도 감소 로직
        {
            if (Durability > 0)
                Durability--;
        }
        public void RepairMax() // 내구도 전체수리 로직 외부에서 불러다쓰기
        {
            Durability = MaxDurability;
            Console.WriteLine($"{Name}의 내구도가 {Durability}/{MaxDurability}로 복구되었습니다.");
        }

        public bool IsBroken => Durability == 0;

        public virtual bool Use (Player player) => false;

        public abstract Item Clone();
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
        public int BaseAtk { get; private set; }
        public int Atk => BaseAtk + (Upgrade * 2); 

        public Weapon(int upgrade, string name, int price, string description, int baseAtk, int durability = -1)
            : base(upgrade, name, price, description, durability)
        {
            BaseAtk = baseAtk;
        }

        public EquipSlot SlotType => EquipSlot.Weapon;

        public void Equip(Player player)
        {
           if (IsBroken)
            {
                Console.WriteLine($"{Name}의 내구도가 0이라 착용할 수 없습니다.");
                return;
            }
            player.Inventory.Slots[(int)SlotType] = this;
            player.RecalculateStats();
            Console.WriteLine($"{Name}을 장착했습니다. (공격력 +{Atk})");
            
        }

        public override string ToString()
        {
            string upgradeText = Upgrade > 0 ? $"+{Upgrade} " : "";
            string durText = (Durability >= 0) ? $" | 내구도 {Durability}/{MaxDurability}" : "";
            string brokenText = IsBroken ? " (망가짐)" : "";
            return $"{upgradeText}{Name}{brokenText} | 공격력 +{Atk} | {Description}{durText}";
        }
        public override Item Clone()
        {
            return new Weapon(Upgrade, Name, Price, Description, BaseAtk, Durability);
        }
    }

    public class Clothes : Item, IEquipable
    {
        public int BaseDef { get; private set; }
        public int Def => BaseDef + (Upgrade * 2);

        public Clothes(int upgrade, string name, int price, string description, int baseDef, int durability = -1)
            : base(upgrade, name, price, description, durability)
        {
            BaseDef = baseDef;
        }

        public EquipSlot SlotType => EquipSlot.Clothes;

        public void Equip(Player player)
        {
            if (IsBroken)
            {
                Console.WriteLine($"{Name}의 내구도가 0이라 착용할 수 없습니다.");
                return;
            }
            player.Inventory.Slots[(int)SlotType] = this;
            player.RecalculateStats();
            Console.WriteLine($"{Name}을 장착했습니다. (방어력 +{Def})");
        }

        public override string ToString()
        {
            string upgradeText = Upgrade > 0 ? $"+{Upgrade} " : "";
            string durText = (Durability >= 0) ? $" | 내구도 {Durability}/{MaxDurability}" : "";
            string brokenText = IsBroken ? " (망가짐)" : "";
            return $"{upgradeText}{Name}{brokenText} | 방어력 +{Def} | {Description}{durText}";
        }
        public override Item Clone()
        {
            return new Clothes(Upgrade, Name, Price, Description, BaseDef, Durability);
        }
    }

    public class Consumable : Item
    {
        private Action<Player> effect;

        public Consumable(string name, int price, string description, Action<Player> effect)
            : base(name, price, description)
        {
            this.effect = effect;
        }

        public override bool Use(Player player) //소비 아이템 
        {
            effect?.Invoke(player); 
            Console.WriteLine($"{Name}을 사용했습니다. {Description}");
            return true;
        }

        public override string ToString()
        {
            return $"{Name} | {Description}";
        }
        public override Item Clone()
        {
            return new Consumable(Name, Price, Description, effect);
        }
    }

    public static class ItemDB
    {

        public static List<Item> Items = new List<Item>()
        {
            new Weapon(0, "철검", 100, "단순한 철검이다.", 10, 20),
            new Weapon(0, "은검", 200, "은으로 제작된 검이다.", 20, 30),
            new Weapon(0, "금검", 300, "금으로 제작된 검이다.", 30, 40),
            new Weapon(0, "다이아검", 500, "다이아몬드로 제작된 검이다.", 50, 60),
            new Weapon(0, "집행검", 99999, "모든걸 다 파괴하는 전설의 검", 999, 100),
            new Clothes(0, "낡은 옷", 50, "낡은 방어구입니다.", 5, 30),
            new Clothes(0, "가죽 갑옷", 150, "튼튼한 가죽 방어구입니다.", 10, 50),
            new Clothes(0, "판금 갑옷", 300, "매우 튼튼한 방어구입니다.", 20, 80),
            new Consumable("체력 포션", 50, "체력을 30 회복합니다.", p => p.CurrentHp = Math.Min(p.MaxHp, p.CurrentHp + 30)),
            new Consumable("상급 체력 포션", 100, "체력을 60 회복합니다.", p => p.CurrentHp = Math.Min(p.MaxHp, p.CurrentHp + 60)),
            new Consumable("마나 포션", 50, "마나를 30 회복합니다.", p => p.CurrentMp = Math.Min(p.MaxMp, p.CurrentMp + 30)),
            new Consumable("상급 마나 포션", 100, "마나를 60 회복합니다.", p => p.CurrentMp = Math.Min(p.MaxMp, p.CurrentMp + 60)),
            new Consumable("수리 요정 출장권(전체 수리)", 80, "보유중인 모든 아이템을 수리합니다.", RepairItem.RepairAll),
            new Consumable("수리 요정 출장권(단일 수리)", 40, "하나의 장비를 수리합니다.", p => Forge.Repair(p ,false)),
            new Consumable("보스몬스터 완벽 공략집", 1000, "보스몬스터의 약점을 드러냅니다.", RepairItem.RepairAll) {IsHidden = true }//숨겨진 아이템
        };
    }
}