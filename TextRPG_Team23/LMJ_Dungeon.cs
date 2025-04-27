using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class DungeonMaganer
    {

        public List<Monster> monsterBox;
        public BattleUi ui;
        public Battle battle;
        public Battlecondition condition;
        public MonsterFactory factory;
        public static bool isClearStep1;
        public static bool isClearStep2;
        public static bool isClearStep3;

        public DungeonMaganer(List<Monster> monsterBox, BattleUi ui, Battle battle, Battlecondition condition, MonsterFactory factory)
        {

            this.monsterBox = monsterBox;
            this.ui = ui;
            this.battle = battle;
            this.condition = condition;
            this.factory = factory;
            isClearStep1 = false;
            isClearStep2 = false;
            isClearStep3 = false;
        }

        public void StartDungeonStep1()
        {
            battle.EnterBattle(true,1);
        }
        public void StartDungeonStep2()
        {
            battle.EnterBattle(true, 2);
        }
        public void WorkFactory(string where)
        {
            factory.MakeMonster(where);
        }

        public void BossBattleStart()
        {
            battle.EnterBoss(true,3);
        }

    }
}
