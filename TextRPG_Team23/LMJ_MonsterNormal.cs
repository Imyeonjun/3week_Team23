namespace TextRPG_Team23
{
    class HollowshadeBeast : Monster, TakeDamage
    {
        int Anger;
        string newName = "틴달로스의 사냥개";

        Battlecondition condition;

        public HollowshadeBeast(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "그림자 짐승";
            Atk = 10;
            Def = 15;
            MaxHp = 7;
            CurrentHp = 7;
            IsDead = false;

            Anger = 0;
        }

        public override void UseSkill(int Turn)
        {

            if (BuffDef > 0) { BuffDef--; }

            if (Turn % 2 != 0)
            {
                if (Anger >= 0 && Anger < 6)
                {
                    AngerStack();
                }
                else
                {
                    condition.ui.MonsterLog = $"\n▷{newName}은 당신을 사냥 할 준비를 마쳤다.\n" +
                                              $"< 공격 준비 >";
                }

            }

            if (Turn % 2 == 0)
            {
                if (Anger < 3)
                {
                    condition.Attack(Atk);
                    condition.ui.MonsterLog = $"\n▶{Name}이 당신의 발목을 물어버렸다.\n" +
                                              $"< 받은 데미지: ({Atk}) >";
                }
                else if (Anger < 5)
                {
                    condition.Attack(Atk);
                    condition.ui.MonsterLog = $"\n▶{Name}은 당신의 급소를 정확히 물었다!\n" +
                                              $"< 받은 데미지: ({Atk}) >";
                }
                else if (Anger > 5)
                {
                    condition.Attack(Atk);
                    condition.ui.MonsterLog = $"\n▶{newName}는 사각지대에서 차원의 틈을 찢고 급습했다!\n" +
                                              $"< 받은 데미지: ({Atk}) >";
                }
            }


        }

        void AngerStack()
        {
            Anger++;
            if (Anger < 3)
            {
                condition.ui.MonsterLog = $"\n▷{Name}은 낮게 짖는다.\n" +
                                          $"< 누적된 분노: ({Anger}) >";
            }
            else if (Anger < 5 && Anger > 2)
            {
                Atk = 20;
                condition.ui.MonsterLog = $"\n▷{Name}의 눈이 붉게 물든다.\n" +
                                          $"< [진화 중] 누적된 분노: ({Anger}) >";
            }
            else
            {
                Atk = 30;
                string temp = Name;
                condition.ui.MonsterLog = $"\n▷{Name}은 또 다른 존재가 되었다.\n" +
                                          $"< [진화 성공] {temp} -> {newName} >";
            }

        }
        


        public void TakeDamage(int Damage)
        {
            Damage = 1;
            if (BuffDef <= 0)
            {
                Hp -= Damage;
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                Hp -= Damage;
            }
        }
    }

    class BloodrootSentinel : Monster, TakeDamage
    {

        bool isCounting;
        bool isHungry;
        int temp;
        Battlecondition condition;

        public BloodrootSentinel(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "흡혈 파수병";
            Atk = 0;
            Def = 30;
            MaxHp = 100;
            CurrentHp = 100;
            IsDead = false;
            isCounting = false;
            isHungry = false;
        }

        public override void UseSkill(int Turn)
        {

            if (BuffDef > 0) { BuffDef--; }

            if (Turn % 4 == 0)
            {
                isHungry = true;
                if (isHungry)
                {
                    HungryAttack(Def / 2);
                }    
            }

            if (Turn % 2 != 0 && Def <= 30)
            {
                isCounting = true;
                condition.ui.MonsterLog = $"\n▷{Name}의 눈에서 불길한 빛이 일렁인다!\n" +
                                          $"< 반격 활성화 >";
                if (Def >= 35)
                {
                    isCounting = true;
                    condition.ui.MonsterLog = $"\n▷피맛을 본 {Name}은 입고리를 올린다.\n" +
                                              $"< 반격 활성화 | 방어력 ({temp}) -> ({Def}) >";
                }

            }

            if (Turn % 2 == 0)
            {
                isCounting = false;
                condition.ui.MonsterLog = $"\n▷{Name}의 눈에서 빛이 사라졌다.\n" +
                                          $"< 반격 비활성화 >";
            }


        }

        public void HungryAttack(int dmg)
        {
            condition.Attack(dmg);
            condition.ui.SpecialMonsterLog = $"▶{Name}에게 충분한 피가 공급되지 않았습니다." +
                                             $"< [{Name}의 특수공격] | 받은 데미지: {dmg} >";
            temp = Def;
            Def += 5;
        }

        public void TakeItBack(int dmg)
        {
            isHungry = false;
            condition.Attack(dmg);
            Console.WriteLine($"▶{Name}의 반격!" +
                              $"< 받은 데미지: ({dmg}) | {Name}의 회복량 ({dmg}) >");
        }


        public void TakeDamage(int Damage)
        {
            int realDamage;

            if (BuffDef <= 0)
            {
                realDamage = Damage - Def;

                if (realDamage < 0)
                {
                    realDamage = 1;
                }

                if (isCounting)
                {
                    Hp += realDamage;
                    TakeItBack(realDamage);
                }
                else
                {
                    Hp -= realDamage;
                }

            }
            else if (BuffDef > 0)
            {
                BuffDef--;

                realDamage = Damage - (Def * 2);

                if (realDamage < 0)
                {
                    realDamage = 1;
                }

                if (isCounting)
                {
                    Hp += realDamage;
                    TakeItBack(realDamage);
                }
                else
                {
                    Hp -= realDamage;
                }
            }
        }
    }

/*    class Roothelm : Monster, TakeDamage
    {
        Battlecondition condition;

        public Roothelm(Battlecondition condition, int code, int level)
        {
            this.condition = condition;
            MobCode = code;
            Level = level;
            Name = "뿌리투구 수호자";
            Atk = 5;
            Def = 7;
            MaxHp = 40;
            CurrentHp = 40;
            IsDead = false;
        }

        public override void UseSkill(int Turn)
        {
            if (BuffDef > 0) { BuffDef--; }

            if ((Turn % 2) != 0)
            {
                condition.Attack(Atk + BuffDef);
                condition.ui.MonsterLog = $"\n▶{Name}는 거대한 줄기로 {condition.player.Name}을 강타합니다.\n" +
                                          $"< 받은 데미지: ({Atk + BuffDef}) >";
            }

            if ((Turn % 2) == 0)
            {
                BuffDef++;
                condition.BuffDef(1);
                condition.ui.MonsterLog = $"\n▷{Name}는 아군 전체를 축복합니다.\n" +
                                          $"< 아군 전체에게 방어력 2배 증가 버프 한턴 >";
            }
        }


        public void TakeDamage(int Damage)
        {
            int realDamage;
            if (BuffDef <= 0)
            {
                realDamage = Damage - Def;
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
            }
            else if (BuffDef > 0)
            {
                BuffDef--;
                realDamage = Damage - (Def * 2);
                if (realDamage < 0)
                {
                    realDamage = 1;
                }
                Hp -= realDamage;
            }
        }
    }*/
}
