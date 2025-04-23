using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    class DungeonMaganer
    {

        List<Monster> monsterBox;
        BattleUi ui;
        Battle battle;
        Battlecondition condition;
        MonsterFactory factory;


        public DungeonMaganer(List<Monster> monsterBox, BattleUi ui, Battle battle, Battlecondition condition, MonsterFactory factory)
        {

            this.monsterBox = monsterBox;
            this.ui = ui;
            this.battle = battle;
            this.condition = condition;
            this.factory = factory;
        }

        public void Start()
        {
            battle.EnterBattle();
        }
        public void WorkFactory()
        {
            int spawnCount = Yeonjun_Program.random.Next(2, 5);
            factory.MakeMonster(spawnCount);
        }
    }
}
