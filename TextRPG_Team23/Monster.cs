using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    interface Attack
    {
        void AttackPlayer();
    }

    interface TakeDamage
    {
        void TakeDamage(int Damage);
    }


    public class Monster
    {
        public static int NextCode;
        public int MobCode { get; set; } //고유번호
        public string Name { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int BuffAtk { get; set; }
        public int BuffDef { get; set; }

        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }

        public int Hp
        {
            get { return CurrentHp; }
            set
            {
                if (value < 0)
                {
                    CurrentHp = 0;
                }
                else if (value > MaxHp)
                {
                    CurrentHp = MaxHp;
                }
                else { CurrentHp = value; }
            }
        }

        public void MonsterInfo(bool isAction, int number)
        {
            if (!isAction) //플레이어가 타겟으로 지정하지 않을 때 / 번호출력x
            {
                Console.WriteLine($"Lv: {Level}  {Name} \t[Att: {Atk} (+{BuffAtk})]  [Def: {Def} (+{BuffDef})]  [Hp: {CurrentHp}]");
            }
            else //플레이어가 타겟으로 지정할 때 / 번호출력o
            {
                Console.WriteLine($"{MobCode}번 Lv: {Level}  {Name} \t[Att: {Atk} (+{BuffAtk})]  [Def: {Def} (+{BuffDef})]  [Hp: {CurrentHp}]");
            }
        }

        public virtual void UseSkill(int Turn)
        {

        }

    }

    class Gravemoss : Monster, Attack, TakeDamage
    {
        int Shield { get; set; }

        Battlecondition condition;
        public Gravemoss(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "모혈의 이끼거북";
            Atk = 1;
            Def = 12;
            MaxHp = 34;
            CurrentHp = 34;
        }

        public override void UseSkill(int Turn)
        {
            if ((Turn % 2) == 0)
            {
                Shield += 5;
                BuffDef += (Shield / 5);

                Console.Write($"\n{Name}의 등껍질이 자라나기 시작한다.\n" +
                              $"Def: +({BuffDef}) Shield: +({Shield})\n");
            }

            if ((Turn % 8 == 0))
            {
                Shield = 0;
                BuffDef = 0;

                Console.Write($"\n너무 커져버린 등껍질이 바스라지고 말았다.\n" +
                              $"<추가 방어력 제거 ({BuffDef}), 재생 보호막 파괴 ({Shield})>\n");
            }
        }

        public void AttackPlayer()
        {
            condition.Attack(Atk);
        }

        public void TakeDamage(int Damage)
        {
            int beforeHp = Hp;
            int beforeShield = Shield;

            Hp -= (Damage - ((Def + BuffDef) / 3));
            if ((Damage - ((Def + BuffDef) / 3)) > Shield)
            {
                Shield = 0;
                Console.Write($"<재생 보호막 파괴>");
            }
            Hp += Shield;

            Console.Write($"\n-{Name}- Hp: {beforeHp} -> {CurrentHp} Shield: {beforeShield} -> {Shield}\n" +
                          $"신비한 등껍질은 {Name}을 회복시킨다. Heal: {Shield}");
        }
    }

    class Roothelm : Monster, Attack, TakeDamage
    {
        Battlecondition condition;

        public Roothelm(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "뿌리투구 수호자";
            Atk = 4;
            Def = 10;
            MaxHp = 40;
            CurrentHp = 40;
        }

        public override void UseSkill(int Turn)
        {
            //아군 전체 일시적보호막
        }

        public void AttackPlayer()
        {
            condition.Attack(Atk);
        }

        public void TakeDamage(int Damage)
        {
            
        }
    }

    class Blightmaw : Monster, Attack, TakeDamage
    {

        Battlecondition condition;
        public Blightmaw(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "부패의 턱";
            Atk = 7;
            Def = 3;
            MaxHp = 27;
            CurrentHp = 27;

        }

        public override void UseSkill(int Turn)
        {
            //출혈,중독 
        }

        public void AttackPlayer()
        {
            condition.Attack(Atk);
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - Def);
        }
    }

    class Duskrend : Monster, Attack, TakeDamage
    {

        Battlecondition condition;
        public Duskrend(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "일몰 좀도둑";
            Atk = 15;
            Def = 0;
            MaxHp = 19;
            CurrentHp = 19;

        }

        public override void UseSkill(int Turn)
        {
            //은신 강력한 한방
        }

        public void AttackPlayer()
        {
            condition.Attack(Atk);
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - Def);
        }
    }

    class Gloomseer : Monster, Attack, TakeDamage
    {

        Battlecondition condition;
        public Gloomseer(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "어스름 예언자";
            Atk = 4;
            Def = 2;
            MaxHp = 24;
            CurrentHp = 24;

        }

        public override void UseSkill(int Turn)
        {
            //디버프 다중타격
        }

        public void AttackPlayer()
        {
            condition.Attack(Atk);
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - Def);
        }
    }

    class Rainwisp : Monster, Attack, TakeDamage
    {

        Battlecondition condition;
        public Rainwisp(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "비안개 정령";
            Atk = 0;
            Def = 9;
            MaxHp = 26;
            CurrentHp = 26;

        }

        public override void UseSkill(int Turn)
        {
            //지속회복   
        }

        public void AttackPlayer()
        {
            condition.Attack(Atk);
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - Def);
        }
    }
}
