using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    class Battlecondition
    {

        public Player player;
        public List<Monster> monsterBox { get; private set; }
        public BattleUi ui { get; private set; }
        public Battle battle { get; private set; }

        public void BattleConnect(Player player,List<Monster> monsterBox, BattleUi ui, Battle battle)
        {
            this.player = player;
            this.monsterBox = monsterBox;
            this.ui = ui;
            this.battle = battle;
        }

        public void Attack(int damage)
        {
            player.PlayerTakeDamage(damage);
        }

    }


    class Battle // 턴제루프만 맡는 녀석
    {

        int turnCount;
        Battlecondition condition;
        public Battle(Battlecondition condition)
        {
            this.condition = condition;
        }


        public void EnterBattle()
        {
            turnCount = 1;

            Console.Clear();
            Console.Write("몬스터가 등장했다!\n\n" +
              "전투를 시작하려면 아무키나 누르세요.\n" +
              ">>>");

            Console.ReadKey();

            while (condition.monsterBox.Count > 0 &&  condition.player.CurrentHp> 0)
            {
                Console.Clear();

                condition.ui.PrintMonster(true);
                Console.ReadKey();
                StartMonsterTurn();
                Console.ReadKey();



                condition.player.PlayerDoing(condition.monsterBox, condition.player);
            }
        }

        public void StartMonsterTurn()
        {
            foreach (Monster m in condition.monsterBox)
            {
                m.UseSkill(turnCount);
                
            }
            foreach (Monster m in condition.monsterBox)
            {
                if (m is Attack a)
                {
                    a.AttackPlayer();
                }
            }



            turnCount += 1;
        }


    }

    class BattleUi
    {
        Battlecondition condition;

        public BattleUi(Battlecondition condition)
        {
            this.condition = condition;
        }

        public void PrintMonster(bool playerTurn)
        {
            int num = 1;

            foreach (Monster m in condition.monsterBox)
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
