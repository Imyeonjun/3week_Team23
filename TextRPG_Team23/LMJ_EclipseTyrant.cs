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
        bool SuperAttack;
        bool StrengthOfSun;
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
            SuperAttack = false;
        }

        public override void UseSkill(int Turn)
        {

            if (SuperAttack) //체력이 100일때 존나 쎈 데미지 한번
            {
                SuperAttack = false;
                condition.IgnoreAttack(90);
                condition.ui.SpecialMonsterLog = $"\n※{Name}: 난 아직 죽을 수 없다!\n" +
                                                 $"▶엄청난 폭발이 일어납니다!◀\n" +
                                                 $"< 받은 방어무시 데미지: ({90}) >";
            }

            switch (Turn)  
            {
                case 1: //두번 촤촥 때리기 공격력 강화가 걸려있으면 때리고 해제
                    SuperArmour = true;
                    BlazingCleave();
                    break;

                case 2: //별 떨구기 별이 몬스터리스트에 추가
                    StarfallSear();
                    break;

                case 3: // 개구라 패턴 
                    SolarBetrayal();
                    break;

                case 4: //이때 별폭발 패턴 && 만약 폭발 안하면 자신의 공격력 강화
                    SolarCoronation();
                    break;

                case 5: //이때 거대한 눈 소환 죽이지 않으면 필드에 남아있고 눈은 짝수 턴 마다 플레이어에게 검을 떨굼
                    CallingWatcher();
                    break;

                case 6: //Dawnbreaker Slam 크게 한방때리기
                    DawnbreakerSlam();
                    break;
                case 0:
                    YouCanHit();
                    break;
            }

        }
        void BlazingCleave() 
        {
            if (StrengthOfSun)
            {
                realDamage = 37;
            }
            else
            {
                realDamage = 30;
            }    
            condition.Attack(realDamage);
            condition.Attack(realDamage);
            condition.ui.MonsterLog = $"\n※{Name}: 내게 대적 할 수는 없다. 어리석은 너의 머리통을 산산조각 내주마!\n" +
                                      $"▶{Name}은 {condition.player.Name}에게 대검을 크게 휘둘렀습니다!◀\n" +
                                      $"< 받은 피해: ({realDamage} x 2회) >";
            StrengthOfSun = false;
        }

        void StarfallSear()
        {
            condition.SpawnStar();
            condition.ui.MonsterLog = $"\n※{Name}: 우주적 공포를 느끼게 해주마.\n" +
                                      $"▶{Name}은 별을 낙하시킵니다.◀\n" +
                                      $"< 별 낙하까지 두턴 >";
        }

        void SolarBetrayal() //때리면 감지해서 문자출력시키게 하면 됨
        {
            condition.ui.MonsterLog = $"\n※{Name}: 이 정도 거리라면 별을 폭발시켰을 때 네놈이 죽을 수도 있겠군\n" +
                                      $"▶{Name}은 즉시 별을 폭발시키려 합니다.◀\n" +
                                      $"< 공격하여 저지하거나 별의 폭발을 견디십시오 >";
        }

        void SolarCoronation()
        {
            //별 탐색하고 조건 추가
            StrengthOfSun = true;   
            
            condition.ui.MonsterLog = $"\n※{Name}: 허... 설마 별을 파괴할 줄이야... 하지만 그 정도로 나를 막을 수 있으리라 생각하지마라\n" +
                                      $"▶{Name}은 태양의 왕좌에서 힘을 끌어옵니다.◀\n" +
                                      $"< 추가된 공격력: (+{7}) >";
            
        }





        void CallingWatcher()
        {
            condition.ui.MonsterLog = $"\n※{Name}: 열기를 품은 눈이 네놈을 통구이로 만들 것이다.\n" +
                                      $"▶{Name}은 일식의 눈을 소환합니다.◀\n" +
                                      $"< 일식의 눈은 매 2턴마다 당신을 공격합니다. >";
        }

        void DawnbreakerSlam()
        {
            realDamage = 68;
            condition.Attack(realDamage);
            condition.ui.MonsterLog = $"\n※{Name}: 이젠 죽을 때도 되었지않나?.\n" +
                                      $"▶{Name}은 막대한 질량을 품은 열기를 {condition.player.Name}에게 방출합니다!◀\n" +
                                      $"< 받은 피해: ({realDamage}) >";
        }


        public void YouCanHit()
        {
            SuperArmour = false;    
            condition.ui.SpecialMonsterLog = $"\n※일식의 폭군이 잠시동안 피격 가능한 상태가 되었습니다!\n" +
                                             $"< 이번 턴에 가하는 공격은 반드시 {Name}에게 큰 피해를 줍니다! >";
        }

        public void CantHit()
        {
            condition.IgnoreAttack(15);
            condition.ui.SpecialMonsterLog = $"\n※{Name}: 그 어떠한 공격도 통하지 않는다.\n" +
                                             $"▶공격을 시도한 {condition.player.Name}은 뜨거운 열기에 되려 피해를 입습니다.◀\n" +
                                             $"< 받은 방어무시 데미지: ({15}) >";
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
