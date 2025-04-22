using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team23_Dungeon
{
    class Monster
    {
        public static int NextCode;
        public int MobCode { get; set; } //고유번호
        public string Name { get; set; }
        public int Level { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int TotalAtk { get; set; }
        public int TotalDef { get; set; }
        public int SkillCount { get; set; }

        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }

        public void MonsterInfo(bool isAction, int number)
        {
            if (!isAction) //플레이어가 타겟으로 지정하지 않을 때 / 번호출력x
            {
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

    }
}
