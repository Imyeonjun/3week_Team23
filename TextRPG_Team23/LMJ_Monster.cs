using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    interface TakeDamage
    {
        void TakeDamage(int Damage);
    }


    public class Monster
    {
        public static int NextCode;
        public int MobCode { get; set; } //고유번호
        public string Name { get; set; } //이름
        public int Level { get; set; } //레벨
        public int Atk { get; set; } //공격력
        public int Def { get; set; } //방어력
        public int BuffAtk { get; set; } //버프공격력
        public int BuffDef { get; set; } //버프방어력

        public int MaxHp { get; set; } //최대체력
        public int CurrentHp { get; set; } //현재체력

        public bool IsDead { get; set; } // 생,사 상태값

        public int Hp //체력 증,차감 판별프로퍼티
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
                Console.WriteLine($"Lv: {Level}  {Name} \t[Atk: {Atk} (+{BuffAtk})]  [Def: {Def} (+{BuffDef})]  [Hp: {CurrentHp}/{MaxHp}] {(IsDead ? "Dead" : "Alive")}");
            }
            else /*if (!IsDead)*///플레이어가 타겟으로 지정할 때 / 번호출력o
            {
                Console.WriteLine($"{number}번 Lv: {Level}  {Name} \t[Atk: {Atk} (+{BuffAtk})]  [Def: {Def} (+{BuffDef})]  [Hp: {CurrentHp}/{MaxHp}]  {(IsDead ? "Dead" : "Alive" )}");
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
            Atk = 4;
            Def = 10;
            MaxHp = 40;
            CurrentHp = 40;
            IsDead = false;
        }

        public override void UseSkill(int Turn)
        {

            if (BuffDef > 0) { BuffDef--; }

            if (Turn % 2 != 0)
            {
                condition.Attack(Atk + (Shield / 2));
                condition.ui.MonsterLog = $"\n▶{Name}은 {condition.player.Name}을 등껍질로 후려친다!\n" +
                                          $"< 받은 데미지: ({Atk + (Shield / 2)}) >";
            }

            if (Turn % 2 == 0)
            {
                Shield += 3;
                Hp += Shield;
                condition.ui.MonsterLog = $"\n▷{Name}의 등껍질이 자라나기 시작한다.\n" + 
                                          $"< 누적된 재생등껍질: ({Shield}) >";
            }

            if (Turn % 8 == 0)
            {
                Shield = 0;
                condition.ui.MonsterLog = $"\n▷{Name}의 덩껍질이 바스라졌다 \n"+
                                          $"< 재생 보호막 파괴 ({Shield}) >";

            }
        }


        public void TakeDamage(int Damage)
        {
            int realDamage;
            if (BuffDef <= 0)
            {
                realDamage = Damage - Def;
                if(realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                realDamage = Damage - (Def * 2);
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
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
            Atk = 5;
            Def = 7;
            MaxHp = 52;
            CurrentHp = 52;
            IsDead = false;
        }

        public override void UseSkill(int Turn)
        {
            if (BuffDef > 0) { BuffDef--; }

            if ((Turn % 2) != 0)
            {
                condition.Attack(Atk + BuffDef);
                condition.ui.MonsterLog = $"\n▶{Name}는 거대한 줄기로 {condition.player.Name}을 강타합니다.\n" +
                                          $"< 받은 데미지: ({Atk + BuffDef}) >";
            }

            if ((Turn % 2) == 0)
            {
                BuffDef++;
                condition.BuffDef(1);
                condition.ui.MonsterLog = $"\n▷{Name}는 아군 전체를 축복합니다.\n" +
                                          $"< 아군 전체에게 방어력 2배 증가 버프 한턴 >";
            }
        }


        public void TakeDamage(int Damage)
        {
            int realDamage;
            if (BuffDef <= 0)
            {
                realDamage = Damage - Def;
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                realDamage = Damage - (Def * 2);
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
            }
        }
    }

    class Blightmaw : Monster, TakeDamage
    {

        Battlecondition condition;

        int poisonCount;
        int poisonDamage;
        int poisonPower;
        bool isPowerUp;

        public Blightmaw(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "부패의 턱";
            Atk = 7;
            Def = 2;
            MaxHp = 38;
            CurrentHp = 38;
            IsDead = false;
            isPowerUp = false;
            poisonDamage = 3;
            poisonPower = 0;
            poisonCount = 0;

        }

        public override void UseSkill(int Turn)
        {
            if (BuffDef > 0) { BuffDef--; }

            if (poisonPower == 4)
            {
                isPowerUp = true;
                poisonDamage = 40;
                poisonCount = 1;
            }

            if ((Turn % 2) != 0)
            {

                if (isPowerUp)
                {
                    condition.ui.MonsterLog = $"\n▷{Name}은 아주 치명적인 맹독을 입에서 뚝 뚝 흘린다.\n" +
                                              $"다음 턴에 받게 될 데미지: {poisonDamage}";
                }
                else
                {
                    if (poisonCount >= 0 && poisonCount < 6)
                    {
                        poisonCount++;
                    }
                    condition.ui.MonsterLog = $"\n▷{Name}은 독을 내뿜을 준비를 한다.\n" +
                                              $"누적된 독수치: {poisonCount}  수치마다 받게 될 데미지{poisonDamage}";
                }

            }

            if ((Turn % 2) == 0)
            {
                if (isPowerUp)
                {
                    poisonCount = 1;
                    condition.Attack(poisonCount * poisonDamage);
                    condition.ui.MonsterLog = $"\n▶당신의 공격에 화가 난 {Name}은 치명적인 맹독을 분사했다!!!\n" +
                                              $"치명적인독 피해: {poisonCount * poisonDamage}";
                    isPowerUp = false;
                    poisonDamage = 3;
                    poisonPower = 0;
                }
                else
                {
                    condition.Attack(poisonCount * poisonDamage);
                    condition.ui.MonsterLog = $"\n▶{Name}의 독이 {condition.player.Name}에게 분사된다!\n" +
                                              $"독 피해: {poisonCount * poisonDamage}";
                }
            }

        }

        public void TakeDamage(int Damage)
        {
            int realDamage;
            if (BuffDef <= 0)
            {
                realDamage = Damage - Def;
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
                poisonPower++;
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                realDamage = Damage - (Def * 2);
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
                poisonPower++;
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
            MaxHp = 17;
            CurrentHp = 17;
            IsDead = false;
        }

        public override void UseSkill(int Turn)
        {
            if (BuffDef > 0) { BuffDef--; }

            int realDamage = 15;

            if ((Turn % 2) != 0)
            {
                Def = 50;
                hide = true;
                condition.ui.MonsterLog = $"\n▷{Name}은 그림자에 몸을 숨겼다.";
            }
            else if (hide)
            {
                Luck = Program.random.Next(1, 100);
                Def = 0;
                hide = false;
                if (Luck > 70)
                {
                    condition.Attack(realDamage + Atk);
                    condition.ui.MonsterLog = $"\n▶{Name}의 급소공략!\n" +
                                              $"받은 데미지: ({realDamage + Atk})";
                }
                else if (Luck < 40)
                {
                    condition.Attack(Atk - realDamage);
                    condition.ui.MonsterLog = $"\n▶{Name}은 돌뿌리에 걸려 볼품없이 넘어져 {condition.player.Name}을 타격했다.\n" +
                                              $"받은 데미지: ({Atk - realDamage})";
                }
                else
                {
                    condition.Attack(realDamage);
                    condition.ui.MonsterLog = $"\n▶{Name}의 비열한 공격\n" +
                                              $"받은 데미지: ({realDamage})"; ;
                }
            }

        }


        public void TakeDamage(int Damage)
        {
            int realDamage;
            if (BuffDef <= 0)
            {
                realDamage = Damage - Def;
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                realDamage = Damage - (Def * 2);
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
            }
        }
    }

    class Gloomseer : Monster, TakeDamage
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
            IsDead = false;
        }

        public override void UseSkill(int Turn)
        {
            if (BuffDef > 0) { BuffDef--; }

            if (Turn % 2 != 0)
            {
                doAttack = true;
                countProphecy = Program.random.Next(0, 5);
                condition.ui.MonsterLog = $"\n▷{Name}가 미래를 보기 시작합니다.\n" +
                                          $"다음 턴에 {countProphecy}번의 공격이 적중합니다!";
            }

            if (Turn % 2 == 0 && doAttack == true)
            {
                doAttack = false;
                for (int i = 0; i < countProphecy; i++)
                {
                    condition.Attack(Atk);
                }
                condition.ui.MonsterLog = $"\n▶{Name}는 신비한 힘으로 {condition.player.Name}의 정신을 공격합니다!\n" +
                                          $"적중한 공격 횟수: {countProphecy} 받은 피해량: {countProphecy * Atk}";
            }
        }

        public void TakeDamage(int Damage)
        {
            int realDamage;
            if (BuffDef <= 0)
            {
                realDamage = Damage - Def;
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                realDamage = Damage - (Def * 2);
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
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
            Def = 2;
            MaxHp = 30;
            CurrentHp = 30;
            isDanger = false;
            IsDead = false;
        }

        public override void UseSkill(int Turn)
        {
            if (BuffDef > 0) { BuffDef--; }

            if (Turn % 2 != 0 && !isDanger)
            {
                condition.ui.MonsterLog = $"\n▷{Name}이 안개를 준비하고 있습니다.";
            }
            else if (Turn % 2 != 0 && isDanger)
            {
                condition.ui.MonsterLog = $"\n▷{Name}이 안개를 준비하고 있습니다.";
            }


            if (Turn % 2 == 0 && !isDanger)
            {
                int myCode = MobCode;
                int target = FindFriendly();
                HealPoint = 8;
                condition.HealMonster(HealPoint, target);
                
                condition.ui.MonsterLog = $"\n▶{Name}의 안개가 아군을 회복시킵니다.\n" +
                                          $"< 회복시킨 아군: ({condition.healTarget}) | 회복량: ({HealPoint}) >";
            }
            else if (Turn % 2 == 0 && isDanger)
            {
                condition.HealAllMonster((HealPoint = Program.random.Next(3, 7)));
                condition.ui.MonsterLog = $"\n▶자욱하게 퍼진 안개는 몬스터들을 회복시켰습니다.\n" +
                                          $"< 회복량: ({HealPoint}) >";
            }
        }


        public void TakeDamage(int Damage)
        {
            int realDamage;
            if (BuffDef <= 0)
            {
                realDamage = Damage - Def;

                if (realDamage < 0)
                {
                    realDamage = 1;
                }

                Hp -= realDamage;

                if (CurrentHp < 14)
                {
                    isDanger = true;
                }
            }
            else if (BuffDef > 0)
            {
                BuffDef--;

                realDamage = Damage - (Def * 2);

                if (realDamage < 0)
                {
                    realDamage = 1;
                }

                Hp -= realDamage;

                if (CurrentHp < 14)
                {
                    isDanger = true;
                }
            }
        }

        int FindFriendly()
        {
            int targetCode = 0;
            bool isFind = false;
            while (!isFind)
            {
                int code = Program.random.Next(1, (condition.monsterBox.Count));

                if (code == MobCode)
                {
                    continue;
                }
                else
                {
                    targetCode = code;
                    isFind = true;
                }
            }
            return targetCode;
        }
    }
}
