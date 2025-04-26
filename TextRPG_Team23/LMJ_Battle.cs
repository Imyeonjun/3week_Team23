using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

        public List<Monster> deadMonsterBox { get; set; }
        
        public void BattleConnect(Player player, List<Monster> monsterBox, BattleUi ui, Battle battle)
        {
            this.player = player;
            this.monsterBox = monsterBox;
            this.ui = ui;
            this.battle = battle;
            deadMonsterBox = monsterBox;
        }


        public int svainghp;
        public string healTarget;




        public void Attack(int damage)
        {
            player.PlayerTakeDamage(damage);
        }

        public void IgnoreAttack(int damage) //방무뎀 넣기
        {
            player.IgnoreDefenseDamage(damage);
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
            foreach (Monster m in monsterBox)
            {
                if (!m.IsDead)
                {
                    m.Hp += heal;
                }
            }
        }
        public void HealMonster(int heal, int code)
        {
            foreach (Monster m in monsterBox)
            {
                if (code == m.MobCode && !m.IsDead) 
                {
                    healTarget = m.Name;
                    m.Hp += heal;
                }
            }
        }

        public void CheckWatcher()
        {
            int mobCount = 0;
            foreach(var m in monsterBox)
            {
                if(m is Watcher)
                {
                    mobCount++;
                }
            }
            if (mobCount > 0)
            {
                foreach(var m in monsterBox)
                {
                    if (m is Watcher w)
                        w.entityDamage += mobCount;
                }
            }
        }
        public bool CheckStar()
        {
            bool isStarExist = false;
            foreach (var m in monsterBox)
            {
                if (m is Star)
                {
                    isStarExist = true;
                }
            }
            return isStarExist;
        }
        public void StarExplode(bool Explode)
        {
            List<Monster> removelist = new List<Monster>();

            foreach (var m in monsterBox)
            {
                if (m is Star star && star.isExplode)
                {
                    removelist.Add(star);
                }
            }
            foreach (var m in removelist)
            {
                monsterBox.Remove(m);
            }
        }

        public void CheckSpawner()
        {
            foreach(Monster m in monsterBox)
            {
                if (m.Name == "공허를 부르는 자" && m.Hp <= 0)
                {
                    KillAllSpawnMob();
                }
            }
        }

        public void KillAllSpawnMob()
        {
            foreach (var mob in monsterBox)
            {
                if (mob is HollowFangstalker fang && !fang.IsDead)
                {
                    fang.MyLifeWithSpawner(false);
                    ui.PrintDestroySpawnMob();
                }
            }
        }

        public void SpawnHunter()
        {
            monsterBox.Add(new HollowFangstalker(this, 10, 1));
        }

        public void SpawnStar()
        {
            monsterBox.Add(new Star(this, 111, 10));
        }

        public void SpawnWatcher()
        {
            for (int i = 0; i < 2; i++)
            {
                monsterBox.Add(new Watcher(this, 222, 5));
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
            condition.svainghp = condition.player.CurrentHp;
            bool isBattle = isEnter;

            Console.Clear();
            Console.Write("몬스터가 등장했다!\n\n");

            //Console.ReadKey();
            condition.deadMonsterBox = condition.monsterBox.ToList();
            while (isBattle)
            {
                condition.CheckSpawner();
                CheckMonsterDead();

                if (condition.monsterBox.Count <= 0 || condition.player.CurrentHp <= 0)
                {
                    isBattle = false;
                    condition.monsterBox.Clear();
                    BattleResult.BattleResultUI(condition.player, condition.deadMonsterBox,condition.svainghp);
                    continue;
                }

                condition.ui.PrintMonster(false);

                StartMonsterTurn();
                turnCount++;

                condition.player.PlayerDoing(condition.monsterBox, condition.player, condition.ui);
                Console.ReadKey();
                Console.Clear();


            }
        }

        public void StartMonsterTurn()
        {

            foreach (Monster m in condition.monsterBox)
            {
                if (!m.IsDead)
                {
                    m.UseSkill(turnCount);
                    condition.ui.PrintMonsterLog();
                }
                else
                {
                    Console.WriteLine("죽은몬스터");
                }
            }
            
        }

        public void CheckMonsterDead()
        {
            int DeadCount = 0;
            foreach (Monster m in condition.monsterBox)
            {
                if (m.Hp <= 0)
                {
                    m.IsDead = true;
                    DeadCount++;
                }
            }
            if (DeadCount == condition.monsterBox.Count)
            {
                condition.monsterBox.Clear();
            }
        }
    }

    public class BattleUi
    {
        Battlecondition condition;

        public string MonsterLog;
        public string SpecialMonsterLog = "";

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
            if (!string.IsNullOrWhiteSpace(SpecialMonsterLog)) //만약 몬스터가 특수한 행동을 했을 경우에 출력
            {
                Console.WriteLine(SpecialMonsterLog);
                SpecialMonsterLog = "";
            }
            Console.WriteLine(MonsterLog);
        }

        public void PrintDestroySpawnMob()
        {
            Console.WriteLine(SpecialMonsterLog);
            SpecialMonsterLog = "";
        }


    }
}