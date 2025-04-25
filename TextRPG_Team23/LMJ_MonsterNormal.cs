using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    //class Rainwisp : Monster, TakeDamage
    //{

    //    Battlecondition condition;
    //    int HealPoint;
    //    bool isDanger;
    //    public Rainwisp(Battlecondition condition, int code, int level)
    //    {
    //        this.condition = condition;
    //        MobCode = code;
    //        Level = level;
    //        Name = "비안개 정령";
    //        Atk = 0;
    //        Def = 7;
    //        MaxHp = 26;
    //        CurrentHp = 26;
    //        isDanger = false;
    //        IsDead = false;

    //    }

    //    public override void UseSkill(int Turn)
    //    {
    //        if (BuffDef > 0) { BuffDef--; }

    //        if (Turn % 2 != 0 && !isDanger)
    //        {
    //            int code = Program.random.Next(1, (condition.monsterBox.Count));
    //            HealPoint = 12;
    //            condition.HealMonster(HealPoint, code);

    //            if (CurrentHp < 14)
    //            {
    //                isDanger = true;
    //            }
    //            condition.ui.MonsterLog = $"\n{Name}의 안개가 아군을 감싸안습니다.";
    //        }
    //        else if (Turn % 2 != 0 && isDanger)
    //        {
    //            condition.ui.MonsterLog = $"\n{Name}이 안개를 준비하고 있습니다.";
    //        }


    //        if (Turn % 2 == 0 && !isDanger)
    //        {
    //            condition.ui.MonsterLog = $"\n{Name}이 안개를 준비하고 있습니다.";
    //        }
    //        else if (Turn % 2 == 0 && isDanger)
    //        {
    //            condition.HealAllMonster((HealPoint = Program.random.Next(5, 9)));
    //            condition.ui.MonsterLog = $"\n자욱하게 퍼진 안개는 몬스터들을 회복시켰습니다.";
    //        }

    //    }


    //    public void TakeDamage(int Damage)
    //    {
    //        if (BuffDef <= 0)
    //        {
    //            Hp -= (Damage - (Def));
    //        }
    //        else if (BuffDef > 0)
    //        {
    //            BuffDef--;
    //            Hp -= (Damage - (Def * 2));
    //        }
    //    }
    //}
}
