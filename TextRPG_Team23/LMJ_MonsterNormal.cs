namespace TextRPG_Team23
{
    class HollowshadeBeast : Monster, TakeDamage
    {
        int Shield { get; set; }
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
            Def = 10;
            MaxHp = 6;
            CurrentHp = 6;
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
}
