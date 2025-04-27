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

        public bool HasBossGuideItem() //인벤토리에 보스가디으 공략집이 있는지 체크 
        {
            foreach (var itemStack in player.Inventory.Items)
            {
                if (itemStack.Item.Name == "보스몬스터 완벽 공략집")
                {
                    return true;
                }
            }
            return false;
        }



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
        public void WatcherDead()
        {
            List<Monster> removelist = new List<Monster>();

            foreach (var m in monsterBox)
            {
                if (m is Watcher eye && eye.Hp <= 0)
                {
                    removelist.Add(eye);
                }
            }
            foreach (var m in removelist)
            {
                monsterBox.Remove(m);
            }
        }
        public bool CheckStar()
        {
            bool isStarAlive = false;
            foreach (var m in monsterBox)
            {
                if (m is Star star && !star.IsDead)
                {
                    isStarAlive = true;
                }
            }
            return isStarAlive;
        }
        public void StarExplode(bool Explode)
        {
            List<Monster> removelist = new List<Monster>();

            foreach (var m in monsterBox)
            {
                if (m is Star star)
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


        public void EnterBattle(bool isEnter,int step)
        {
            turnCount = 1;
            condition.svainghp = condition.player.CurrentHp;
            bool isBattle = isEnter;
            bool isClear = false;
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
                    if(condition.player.CurrentHp <= 0)
                    {
                        isClear = false;
                    }
                    if(condition.monsterBox.Count <= 0)
                    {
                        isClear = true;
                    }
                    isBattle = false;
                    condition.monsterBox.Clear();
                    BattleResult.BattleResultUI(condition.player, condition.deadMonsterBox,condition.svainghp,step,isClear);
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

        public void EnterBoss(bool isEnter, int step)
        {
            turnCount = 1;
            condition.svainghp = condition.player.CurrentHp;
            bool isBossBattle = isEnter;
            bool isClear = false;


            Console.Clear();
            Console.Write("당신은 태양이 가려진 왕성에 도달했다...\n\n" +
                          "왕좌에서 몸을 일으킨 폭군은 당신을 즉시 공격했다.\n\n");

            condition.deadMonsterBox = condition.monsterBox.ToList();

            while (isBossBattle)
            {
                CheckBossMonsterDead();
                CheckBossDead();

                if (condition.monsterBox.Count <= 0 || condition.player.CurrentHp <= 0)
                {
                    if (condition.player.CurrentHp <= 0)
                    {
                        isClear = false;
                    }
                    if (condition.monsterBox.Count <= 0)
                    {
                        isClear = true;
                    }
                    isBossBattle = false;
                    condition.monsterBox.Clear();
                    BattleResult.BattleResultUI(condition.player, condition.deadMonsterBox, condition.svainghp, step, isClear);
                    continue;
                }

                condition.ui.PrintMonster(false);

                StartBossTurn();
                turnCount++;
                condition.player.PlayerDoing(condition.monsterBox, condition.player, condition.ui);

                Console.ReadKey();
                Console.Clear();

                if (turnCount == 7)
                {
                    turnCount = 1;
                }

                condition.CheckWatcher();
                condition.WatcherDead();
            }
        }

        public void StartMonsterTurn()
        {
            foreach (Monster m in condition.monsterBox.ToList())
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
        public void StartBossTurn()
        {
            foreach (Monster m in condition.monsterBox.ToList())
            {
                if (!m.IsDead)
                {
                    m.UseSkill(turnCount);
                    condition.ui.PrintBossMonsterLog();
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
        
        public void CheckBossMonsterDead()
        {
            foreach(Monster m in condition.monsterBox)
            {
                if (m.Hp <= 0)
                {
                    m.IsDead= true;
                }
            }
        }

        public void CheckBossDead()
        {
            bool isOver = false;
            foreach(var m in condition.monsterBox)
            {
                if(m is EclipseTyrant boss && boss.Hp <= 0)
                {
                    m.IsDead = true;
                    isOver = true;
                }
            }
            if (isOver)
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



        public void PrintBossMonsterLog() //보스전을 위해 따로 준비한 출력장치
        {
            Console.WriteLine(MonsterLog);
        }
        public void PrintSpecialMonsterLog() //보스전을 위해 따로 준비한 출력장치
        {
            if (!string.IsNullOrWhiteSpace(SpecialMonsterLog))
            {
                Console.WriteLine(SpecialMonsterLog);
                SpecialMonsterLog = "";
            }
        }

    }
}