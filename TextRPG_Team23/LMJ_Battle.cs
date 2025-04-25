using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class Battlecondition
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

        public void BuffDef(int BuffDef)
        {
            foreach (Monster m in monsterBox)
            {
                m.BuffDef = BuffDef;
            }
        }

        public void BleedingAttack(int damage)
        {
            player.PlayerTakeDamage(damage);
        }

        public void HealAllMonster(int heal)
        {
            foreach(Monster m in monsterBox)
            {
                m.Hp += heal;
            }
        }
        public void HealMonster(int heal,int code)
        {
            foreach(Monster m in monsterBox)
            {
                if(code == m.MobCode)
                {
                    m.Hp += heal;
                }
            }
        }

    }


    public class Battle // 턴제루프만 맡는 녀석
    {

        int turnCount;
        Battlecondition condition;
        public Battle(Battlecondition condition)
        {
            this.condition = condition;
        }


        public void EnterBattle(bool isEnter)
        {
            turnCount = 1;

            bool isBattle = isEnter;

            Console.Clear();
            Console.Write("몬스터가 등장했다!\n\n" +
              "전투를 시작하려면 아무키나 누르세요.\n" +
              ">>>");

            //Console.ReadKey();

            while (isBattle)
            {

                condition.ui.PrintMonster(false);
                //Console.ReadKey();
                StartMonsterTurn();
                //Console.ReadKey();

                condition.player.PlayerDoing(condition.monsterBox, condition.player, condition.ui);

                Console.WriteLine(condition.monsterBox.Count);

                if (condition.monsterBox.Count <= 0 || condition.player.CurrentHp <= 0)
                {
                    isBattle = false;
                }

            }
        }

        public void StartMonsterTurn()
        {
            MonsterDead();

            foreach (Monster m in condition.monsterBox)
            {
                m.UseSkill(turnCount);
                condition.ui.PrintMonsterLog();
                
            }

            turnCount += 1;
        }

        public void MonsterDead()
        {
            for (int i = condition.monsterBox.Count - 1; i >= 0; i--)
            {
                if (condition.monsterBox[i].Hp <= 0)
                {   
                    condition.monsterBox.RemoveAt(i);
                }
            }
        }
    }

    public class BattleUi
    {
        Battlecondition condition;

        public string MonsterLog;

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
        }
        public void PrintMonsterLog()
        {
            Console.WriteLine(MonsterLog);
        }


    }
}
