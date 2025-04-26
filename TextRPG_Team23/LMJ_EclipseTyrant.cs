using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    class EclipseTyrant : Monster
    {
        bool SuperArmour;
        int realDamage;

        Battlecondition condition;

        public EclipseTyrant(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "일식의 폭군";
            Atk = 1000;
            Def = 1000;
            MaxHp = 300;
            CurrentHp = 300;
            IsDead = false;
            SuperArmour = true;
        }

        public override void UseSkill(int Turn)
        {

            //체력이 100일때 존나 쎈 데미지 한번
            //

            switch (Turn)  
            {
                case 1: //두번 촤촥 때리기 공격력 강화가 걸려있으면 때리고 해제
                    SuperArmour = true;
                    BlazingCleave();
                    break;

                case 2: //별 떨구기 별이 몬스터리스트에 추가 못 죽이면
                    StarfallSear();
                    break;

                case 3:

                    break;

                case 4: //이때 별폭발 패턴 && 만약 폭발 안하면 자신의 공격력 강화

                    break;

                case 5: //이때 거대한 눈 소환 죽이지 않으면 필드에 남아있고 눈은 짝수 턴 마다 플레이어에게 검을 떨굼

                    break;

                case 6: //Dawnbreaker Slam 크게 한방때리기

                    break;
                case 0:
                    YouCanHit();
                    break;
            }

        }

        void StarfallSear()
        {
            condition.ui.MonsterLog = $"\n※{Name}이 별을 떨어트립니다.\n" +
                                      $"< 별 낙하까지 두턴 >";
        }
        void BlazingCleave()
        {
            realDamage = 30;
            condition.Attack(realDamage);
            condition.Attack(realDamage);
            condition.ui.MonsterLog = $"\n※{Name}: 내게 대적 할 수는 없다. 어리석은 너의 머리통을 산산조각 내주마!\n" +
                                      $"▶{Name}은 {condition.player.Name}에게 대검을 크게 휘둘렀습니다!◀\n" +
                                      $"< 받은 피해: ({realDamage} x 2회) >";
        }



        public void YouCanHit()
        {
            SuperArmour = false;    
            condition.ui.SpecialMonsterLog = $"\n※일식의 폭군이 잠시동안 피격 가능한 상태가 되었습니다!";
        }

        public void CantHit()
        {
            condition.Attack(5);
            condition.ui.SpecialMonsterLog = $"\n※{Name}: 그 어떠한 공격도 통하지 않는다.";
        }

        public void TakeDamage(int Damage)
        {
            int realDamage = 0;
            if (Damage > 1 && !SuperArmour)
            {
                realDamage = 100;
                Hp -= realDamage;
            }
            else if (SuperArmour)
            {
                realDamage = 0;
                CantHit();
            }
        }


    }

    class Star : Monster, TakeDamage
    {
        Battlecondition condition;
        int ExplosionDmg;

        public Star(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "낙하하는 별";
            Atk = 0;
            Def = 0;
            MaxHp = 50;
            CurrentHp = 50;
            IsDead = false;
            ExplosionDmg = 30;
        }

        public override void UseSkill(int Turn)
        {

            switch (Turn)
            {
                case 2:
                    condition.ui.MonsterLog = $"\n▶{Name}이 {condition.player.Name}에게 가까워지기 시작합니다.\n" +
                                              $"< 낙하까지 2턴 >";
                    break;

                case 3:
                    condition.ui.MonsterLog = $"\n▶{Name}은 걷잡을 수 없는 속도가 되어갑니다.\n" +
                                              $"< 낙하까지 1턴 >";
                    break;

                case 4:
                    Name = "폭발하는 별";
                    condition.IgnoreAttack(ExplosionDmg);
                    condition.ui.MonsterLog = $"\n▶{Name}은 엄청난 충격을 만들어냅니다.\n" +
                                              $"< 받은 방어무시 데미지 ({ExplosionDmg}) >";
                    break;
            }

        }


        public void TakeDamage(int Damage)
        {
            Hp -= Damage;

        }
    }
}
