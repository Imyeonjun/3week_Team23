using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDungeon
{
    class DungeonMaganer
    {

        Person p;
        List<Monster> monsterBox;
        BattleUi ui;
        Battle battle;
        Battlecondition condition;
        MonsterFactory factory;


        public DungeonMaganer(Person p, List<Monster> monsterBox, BattleUi ui, Battle battle, Battlecondition condition, MonsterFactory factory)
        {
            this.p = p;
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
            int spawnCount = Program.random.Next(2, 5);
            factory.MakeMonster(spawnCount);
        }
    }
}
