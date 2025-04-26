using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    class HollowshadeBeast : Monster, TakeDamage
    {
        int Shield { get; set; }

        Battlecondition condition;

        public HollowshadeBeast(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "그림자 짐승";
            Atk = 4;
            Def = 10;
            MaxHp = 34;
            CurrentHp = 34;
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
                condition.ui.MonsterLog = $"\n▷{Name}의 덩껍질이 바스라졌다 \n" +
                                          $"< 재생 보호막 파괴 ({Shield}) >";

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
}
