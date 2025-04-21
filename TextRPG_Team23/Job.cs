using System;
using TextRPG_Team_private;

// 직업 기본 클래스
public abstract class Job
{
    public abstract string JobName { get; }
    public abstract float BaseAtkDmg { get; }
    public abstract int BaseDefence { get; }
    public abstract void PrintSkillInfo();

    public abstract void SkillA(GreenMonster enemy);
    public abstract void SkillB(List<GreenMonster> enemy);

}

public class Warrior : Job
{
    public override string JobName => "전사";
    public override float BaseAtkDmg => 1.0f;
    public override int BaseDefence => 1;

    public override void PrintSkillInfo()
    {
        // 1, 2 스킬설명 출력
    }

    public override void SkillA(GreenMonster enemy)
    {
        
    }
    public override void SkillB(List<GreenMonster> enemy)
    { 
        
    }

}

public class Magician : Job
{
    public override string JobName => "마법사";
    public override float BaseAtkDmg => 2.0f;
    public override int BaseDefence => 0;

    public override void PrintSkillInfo()
    {

    }

    public override void SkillA(GreenMonster enemy)
    {
    }
    public override void SkillB(List<GreenMonster> enemy)
    {
    }
}