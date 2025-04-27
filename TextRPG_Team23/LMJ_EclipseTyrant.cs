using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    class EclipseTyrant : Monster, TakeDamage
    {
        bool SuperArmour;
        bool SuperAttack;
        bool StrengthOfSun;
        int realDamage;
        bool isArmourRemove;

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
            SuperAttack = true;
        }

        public override void UseSkill(int Turn)
        {


            switch (Turn)  
            {
                case 1: //두번 촤촥 때리기 공격력 강화가 걸려있으면 때리고 해제 완성
                    SuperArmour = true;
                    BlazingCleave();
                    break;

                case 2: //별 떨구기 별이 몬스터리스트에 추가 완성
                    SuperArmour = true;
                    StarfallSear();
                    break;

                case 3: // 딜 타임 패턴
                    SolarBetrayal();
                    LethalAttack(); //체력이 100일때 존나 쎈 데미지 한번 완성
                    break;

                case 4: //이때 별폭발 패턴 && 만약 폭발 안하면 자신의 공격력 강화 완성
                    if (!SuperArmour)
                    {
                        SuperArmour = true;
                        condition.ui.SpecialMonsterLog = $"\n※ {Name}: 갑옷 따위 얼마든 다시 만들면 그만이다..\n" +
                                                         $"▶ {Name}은 다시 무적상태에 들어섭니다. ◀\n" +
                                                         $"< 무적 활성화: ({SuperArmour}) >";
                        condition.ui.PrintBossMonsterLog();
                    }

                    SolarCoronation();
                    break;

                case 5: //이때 거대한 눈 2개 소환 죽이지 않으면 필드에 남아있고 눈은 매턴 마다 플레이어에게 피해를 줌 완성
                    SuperArmour = true;
                    CallingWatcher();
                    break;

                case 6: //Dawnbreaker Slam 크게 한방때리기 완성
                    SuperArmour = true;
                    DawnbreakerSlam();
                    break;
            }

        }

        void LethalAttack()
        {
            if (SuperAttack && Hp == 100) //체력이 100일때 존나 쎈 데미지 한번 미완
            {
                SuperAttack = false;
                condition.IgnoreAttack(70);
                condition.ui.SpecialMonsterLog = $"\n※ {Name}: 난 아직 죽을 수 없다!\n" +
                                                 $"▶ 죽음의 위기에 임박한 {Name}은 엄청난 폭발을 일으킵니다! ◀\n" +
                                                 $"< 받은 방어무시 데미지: ({70}) >";
                condition.ui.PrintSpecialMonsterLog();
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
            condition.ui.MonsterLog = $"\n※ {Name}: 내게 대적 할 수는 없다. 어리석은 너의 머리통을 산산조각 내주마!\n" +
                                      $"▶ {Name}은 {condition.player.Name}에게 대검을 크게 휘둘렀습니다! ◀\n" +
                                      $"< 받은 피해: ({realDamage} x 2회) >";
            StrengthOfSun = false;
        }

        void StarfallSear()
        {
            condition.SpawnStar();
            condition.ui.MonsterLog = $"\n※ {Name}: 우주적 공포를 느끼게 해주마.\n" +
                                      $"▶ {Name}은 별을 낙하시킵니다. ◀\n" +
                                      $"< 별 낙하까지 두턴 >";
        }

        void SolarBetrayal() //때리면 감지해서 문자출력시키게 하면 됨
        {
            if (isArmourRemove = condition.HasBossGuideItem()) //보스몹 무적 기믹 해제 판별
            {
                YouCanHit(); // 슈퍼아머 해제
            }
            else
            {
                condition.ui.MonsterLog = $"\n※ {Name}: 그 어떠한 공격도 통하지 않는다.\n" +
                                          $"▶ 공격을 시도한다면 뜨거운 열기에 되려 피해를 입습니다. ◀\n" +
                                          $"< 공격 시 받게 될 방어무시 데미지: ({15}) >";
            }
        }

        void SolarCoronation()
        {
            //별 탐색하고 조건 추가
            bool isStarHere = condition.CheckStar();

            if (isStarHere) //별이 살아있다면 실행
            {
                condition.ui.MonsterLog = $"\n※ {Name}: 고작 이 정도 시련도 이겨내지 못하는가\n" +
                                          $"▶ {Name}은 당신을 비웃습니다. ◀\n" +
                                          $"< 별의 폭발이 시작됩니다. >";

                condition.StarExplode(isStarHere); //리스트에서 별 삭제
            }
            else //별이 없으면 실행
            {
                StrengthOfSun = true;

                condition.ui.MonsterLog = $"\n※ {Name}: 허... 설마 별을 파괴할 줄이야... 하지만 그 정도로 나를 막을 수 있으리라 생각하지마라\n" +
                                          $"▶ {Name}은 태양의 왕좌에서 힘을 끌어옵니다. ◀\n" +
                                          $"< 추가된 공격력: (+{7}) >";
                condition.StarExplode(isStarHere);
            }
            
        }

        void CallingWatcher()
        {
            condition.SpawnWatcher();
            condition.ui.MonsterLog = $"\n※ {Name}: 열기를 품은 눈이 네놈을 통구이로 만들 것이다.\n" +
                                      $"▶ {Name}은 일식의 눈을 2개 소환합니다. ◀\n" +
                                      $"< 일식의 눈은 매턴마다 당신을 공격합니다. >";
        }

        void DawnbreakerSlam()
        {
            realDamage = 60;
            condition.Attack(realDamage);
            condition.ui.MonsterLog = $"\n※ {Name}: 이젠 죽을 때도 되었지않나?.\n" +
                                      $"▶ {Name}은 막대한 질량을 품은 열기를 {condition.player.Name}에게 방출합니다! ◀\n" +
                                      $"< 받은 피해: ({realDamage}) >";
        }


        public void YouCanHit()
        {
            SuperArmour = false;    
            condition.ui.SpecialMonsterLog = $"\n※ {Name}: 이런 망할놈이!!! 대체 언제 그 물건을 손에 넣은거냐!\n" +
                                             $"▶ 공략집에 의해 일식의 폭군이 잠시동안 피격 가능한 상태가 되었습니다!! ◀\n" +
                                             $"< 이번 턴에 가하는 공격은 반드시 {Name}에게 큰 피해를 줍니다! >";
            condition.ui.PrintSpecialMonsterLog();
        }

        public void CantHit()
        {
            condition.IgnoreAttack(15);
            condition.ui.MonsterLog = $"\n※ {Name}: 이 갑옷이 있는 한 난 그 무엇에도 죽지않는다!\n" +
                                      $"▶ {condition.player.Name}의 공격은 반사되었습니다. ◀\n" +
                                      $"< 받은 방어무시 데미지: ({15}) >";
            condition.ui.PrintBossMonsterLog();
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
        public bool isExplode;

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
            isExplode = false;
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
                    isExplode = true;

                    break;
            }

        }


        public void TakeDamage(int Damage)
        {
            Hp -= Damage;

        }
    }

    class Watcher : Monster, TakeDamage
    {
        Battlecondition condition;
        public int entityDamage;

        public Watcher(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "일식의 눈";
            Atk = 0;
            Def = 0;
            MaxHp = 20;
            CurrentHp = 20;
            IsDead = false;
            entityDamage = 5;
        }

        public override void UseSkill(int Turn)
        {
            
            condition.IgnoreAttack(entityDamage);
            condition.ui.MonsterLog = $"▶{Name}이 {condition.player.Name}의 신체 내부에 열기를 터트립니다." +
                                      $"< 받은 방어무시 데미지: ({entityDamage}) >";

        }


        public void TakeDamage(int Damage)
        {
            Hp -= Damage;
        }
    }
}
