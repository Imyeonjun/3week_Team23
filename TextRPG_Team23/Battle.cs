using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDungeon
{
    class Battlecondition
    {
        List<Monster> monsterBox;
        Person p;
        BattleUi ui;
        Battle battle;

        public void BattleConnect(Person p,List<Monster> monsterBox,BattleUi ui, Battle battle)
        {
            this.p = p;
            this.monsterBox = monsterBox;
            this.ui = ui;
            this.battle = battle;
        }


    }


    class Battle // 턴제루프만 맡는 녀석
    {
        private List<Monster> monsterBox;
        private BattleUi ui;
        private Person p;

        int turnCount;

        //public Battle(Person p, List<Monster> monsterBox, BattleUi ui)
        //{
        //    this.p = p;
        //    this.monsterBox = monsterBox;
        //    this.ui = ui;

        //}

        public void EnterBattle()
        {
            turnCount = 1;

            Console.Clear();
            Console.Write("몬스터가 등장했다!\n\n" +
              "전투를 시작하려면 아무키나 누르세요.\n" +
              ">>>");

            Console.ReadKey();

            while (monsterBox.Count > 0 && p.hp > 0)
            {
                Console.Clear();

                ui.PrintMonster(true);
                Console.ReadKey();
                StartMonsterTurn();
                Console.ReadKey();

                //monsterBox.Clear();
                //Console.WriteLine(string.Join(", ", monsterBox));
            }
        }

        public void StartMonsterTurn()
        {
            foreach (Monster m in monsterBox)
            {
                m.UseSkill(turnCount);
            }
            turnCount += 1;
        }


    }

    class BattleUi
    {
        private List<Monster> monsterBox;

        //public BattleUi(List<Monster> monsterBox)
        //{
        //    this.monsterBox = monsterBox;
        //}

        public void PrintMonster(bool playerTurn)
        {
            int num = 1;

            foreach (Monster m in monsterBox)
            {
                m.MonsterInfo(playerTurn, num);
                num++;
            }
            num = 1;
        }

        //public void PrintMonsterAction()
        //{
        //    foreach (Monster m in monsterBox)
        //    {
        //        //monster.UseSkill
        //    }
        //}
    }
}
