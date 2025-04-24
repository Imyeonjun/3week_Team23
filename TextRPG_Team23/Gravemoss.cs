using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
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

}
