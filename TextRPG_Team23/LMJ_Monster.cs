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

        public bool IsDead { get; set; }

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
                Console.WriteLine($"{number}번 Lv: {Level}  {Name} \t[Att: {Atk} (+{BuffAtk})]  [Def: {Def} (+{BuffDef})]  [Hp: {CurrentHp}]");
            }
        }

        public virtual void UseSkill(int Turn)
        {

        }

    }

    class Gravemoss : Monster, TakeDamage
    {
        int Shield { get; set; }

        Battlecondition condition;

        public Gravemoss(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "모혈의 이끼거북";
            Atk = 2;
            Def = 12;
            MaxHp = 34;
            CurrentHp = 34;
            IsDead = false;
        }

        public override void UseSkill(int Turn)
        {

            if (BuffDef > 0){BuffDef--;}

            if ((Turn % 2) == 0)
            {
                Shield += 5;
                Hp += Shield;
                Console.Write($"\n{Name}의 등껍질이 자라나기 시작한다.\n" +
                              $"Shield: +({Shield})\n");
            }

            if((Turn % 2) != 0)
            {
                condition.ui.MonsterLog = $"{Name}은 {condition.player.Name}을 등껍질로 후려친다!";
                condition.Attack(Atk + (Shield / 2));
            }

            if ((Turn % 8 == 0))
            {
                Shield = 0;

                Console.Write($"\n너무 커져버린 등껍질이 바스라지고 말았다.\n" +
                              $"<재생 보호막 파괴 ({Shield})>\n");
            }
        }


        public void TakeDamage(int Damage)
        {
            if (BuffDef <= 0)
            {
                Hp -= (Damage - (Def));
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                Hp -= (Damage - (Def * 2));
            }
        }
    }

    class Roothelm : Monster, TakeDamage
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
            if (BuffDef > 0) {BuffDef--;}

            if((Turn % 2) == 0)
            {
                BuffDef++;
                condition.BuffDef(1);
                condition.ui.MonsterLog = $"{Name}는 아군 전체를 축복합니다.";
            }
            if((Turn % 2) != 0)
            {
                condition.Attack(Atk + BuffDef);
                condition.ui.MonsterLog = $"{Name}는 거대한 줄기로 {condition.player.Name}을 강타합니다.";
            }
        }


        public void TakeDamage(int Damage)
        {
            if (BuffDef <= 0)
            {
                Hp -= (Damage - (Def));
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                Hp -= (Damage - (Def * 2));
            }
        }
    }

    class Blightmaw : Monster, TakeDamage
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
            if (BuffDef > 0) {  BuffDef--; }

            if ((Turn % 2) == 0)
            {

            }
            if ((Turn % 2) != 0)
            {

            }
        }

        public void TakeDamage(int Damage)
        {
            if (BuffDef <= 0)
            {
                Hp -= (Damage - (Def));
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                Hp -= (Damage - (Def * 2));
            }
        }
    }

    class Duskrend : Monster, TakeDamage
    {

        Battlecondition condition;

        int Luck;
        bool hide;

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
            if (BuffDef > 0) { BuffDef--; }

            int realDamage = 15;

            if ((Turn % 2) != 0)
            {
                Def = 50;
                hide = true;
                condition.ui.MonsterLog = $"{Name}은 그림자에 몸을 숨겼다.";
            }
            else if (hide)
            {
                Luck = Program.random.Next(1, 100);
                Def = 0;
                hide = false;
                if (Luck > 70)
                {
                    condition.Attack(realDamage + Atk);
                    condition.ui.MonsterLog = $"{Name}의 급소공략!";
                }
                else if (Luck < 40)
                {
                    condition.Attack(Atk - realDamage);
                    condition.ui.MonsterLog = $"{Name}은 돌뿌리에 걸려 볼품없이 넘어져 {condition.player.Name}을 타격했다.";
                }
                else
                {
                    condition.Attack(realDamage);  
                    condition.ui.MonsterLog = $"{Name}의 비열한 공격";
                }
            }
            
        }


        public void TakeDamage(int Damage)
        {
            if (BuffDef <= 0)
            {
                Hp -= (Damage - (Def));
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                Hp -= (Damage - (Def * 2));
            }
        }
    }

    class Gloomseer : Monster,TakeDamage
    {

        Battlecondition condition;

        int countProphecy;
        bool doAttack = false;

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
            countProphecy = 0;
        }

        public override void UseSkill(int Turn)
        {
            if (BuffDef > 0) {BuffDef--;}

            if (Turn % 2 != 0)
            {
                doAttack = true;
                countProphecy = Program.random.Next(0,5);
                condition.ui.MonsterLog = $"{Name}가 미래를 보기 시작합니다.\n" +
                                          $"다음 턴에 {countProphecy}번의 공격이 적중합니다!";
            }

            if (Turn % 2 == 0 && doAttack == true)
            {
                doAttack = false;
                for (int i = 0; i < countProphecy; i++)
                {
                    condition.Attack(Atk);
                }
                condition.ui.MonsterLog = $"{Name}는 신비한 힘으로 {condition.player.Name}의 정신을 공격합니다!\n" +
                                          $"적중한 공격 횟수: {countProphecy} 피해량: {countProphecy * Atk}";
            }

        }


        public void TakeDamage(int Damage)
        {
            if (BuffDef <= 0)
            {
                Hp -= (Damage - (Def));
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                Hp -= (Damage - (Def * 2));
            }
        }
    }

    class Rainwisp : Monster, TakeDamage
    {

        Battlecondition condition;
        int HealPoint;
        bool isDanger;
        public Rainwisp(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "비안개 정령";
            Atk = 0;
            Def = 7;
            MaxHp = 26;
            CurrentHp = 26;
            isDanger = false;

        }

        public override void UseSkill(int Turn)
        {
            if (BuffDef > 0) { BuffDef--; }

            if (Turn % 2 != 0 && !isDanger)
            {
                int code = Program.random.Next(1, (condition.monsterBox.Count));
                HealPoint = 12;
                condition.HealMonster(HealPoint,code);

                if (CurrentHp < 14)
                {
                    isDanger = true;
                }
                condition.ui.MonsterLog = $"{Name}의 안개가 아군을 감싸안습니다.";
            }
            else if (Turn % 2 != 0 && isDanger)
            {
                condition.ui.MonsterLog = $"{Name}이 안개를 준비하고 있습니다.";
            }


            if (Turn % 2 == 0 && !isDanger)
            {
                condition.ui.MonsterLog = $"{Name}이 안개를 준비하고 있습니다.";
            }
            else if (Turn % 2 == 0 && isDanger)
            {
                condition.HealAllMonster((HealPoint = Program.random.Next(5, 9)));
                condition.ui.MonsterLog = $"자욱하게 퍼진 안개는 몬스터들을 회복시켰습니다.";
            }

        }


        public void TakeDamage(int Damage)
        {
            if (BuffDef <= 0)
            {
                Hp -= (Damage - (Def));
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                Hp -= (Damage - (Def * 2));
            }
        }
    }
}
