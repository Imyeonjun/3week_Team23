using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD
namespace Team23_Dungeon
{
    class Monster
=======
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
>>>>>>> TestCombine
    {
        public static int NextCode;
        public int MobCode { get; set; } //고유번호
        public string Name { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
<<<<<<< HEAD
        public int TotalAtk { get; set; }
        public int TotalDef { get; set; }
        public int SkillCount { get; set; }
=======
        public int BuffAtk { get; set; }
        public int BuffDef { get; set; }
>>>>>>> TestCombine

        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }

<<<<<<< HEAD
=======
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

>>>>>>> TestCombine
        public void MonsterInfo(bool isAction, int number)
        {
            if (!isAction) //플레이어가 타겟으로 지정하지 않을 때 / 번호출력x
            {
<<<<<<< HEAD
                Console.WriteLine($"Lv: {Level}  {Name} \t[Att: {Atk}]  [Def: {Def}]  [Hp: {CurrentHp}]");
            }
            else //플레이어가 타겟으로 지정할 때 / 번호출력o
            {
                Console.WriteLine($"{number}번 Lv: {Level}  {Name} \t[Att: {Atk}]  [Def: {Def}]  [Hp: {CurrentHp}]");
            }
        }


    }

    enum MonsterType
    {
        NeedlebackCrow,
        Boghowler,
        RockjawLizard,
        GhostMousse,
        BlueskinBeast,
        Vinebear

=======
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
            Console.WriteLine($"\n\n디버그: {Name}몬스터공격\n\n");
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - (Def / 3));
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
            Console.WriteLine($"\n\n디버그: {Name}몬스터공격\n\n");
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - (Def / 3));
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
            Console.WriteLine($"\n\n디버그: {Name}몬스터공격\n\n");
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - (Def / 3));
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
            Console.WriteLine($"\n\n디버그: {Name}몬스터공격\n\n");
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - (Def / 3));
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
            Console.WriteLine($"\n\n디버그: {Name}몬스터공격\n\n");
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - (Def / 3));
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
            Console.WriteLine($"\n\n디버그: {Name}몬스터공격\n\n");
        }

        public void TakeDamage(int Damage)
        {
            Hp -= (Damage - (Def / 3));
        }
>>>>>>> TestCombine
    }
}
