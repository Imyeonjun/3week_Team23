using System;
using TextRPG_Team23;

// 직업 기본 클래스
public abstract class Job
{
    public abstract string JobName { get; }
    public abstract float BaseAtkDmg { get; }
    public abstract int BaseDefence { get; }
    public abstract void PrintSkillInfo();

    //public abstract void Attack(GreenMonster enemy, float atkDmg);
    //public abstract void SkillA(GreenMonster enemy, float atkDmg);
    //public abstract void SkillB(List<GreenMonster> enemy, float atkDmg);

}

public class Warrior : Job
{
    public override string JobName => "전사";
    public override float BaseAtkDmg => 1.0f;
    public override int BaseDefence => 1;

    public override void PrintSkillInfo()
    {
        Console.WriteLine("1. 알파 스트라이크 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다.");
        Console.WriteLine("2. 더블 스트라이크 - MP 10\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다.");
    }

    //public override void Attack(GreenMonster enemy, float atkDmg)
    //{
    //치명타 or 회피 결정 
    //enemy에게 데미지 주기
    //결과창 출력
    //}
    //public override void SkillA(GreenMonster enemy, float atkDmg)
    //{
    //마나 계산
    //enemy에게 데미지 주기
    //결과창 출력
    //}
    //public override void SkillB(List<GreenMonster> enemy, float atkDmg)
    //{
    //마나 계산
    //enemy들중 랜덤한 두마리 선별 해서 데미지 주기
    //결과창 출력
    //}

}

public class Magician : Job
{
    public override string JobName => "마법사";
    public override float BaseAtkDmg => 2.0f;
    public override int BaseDefence => 0;

    public override void PrintSkillInfo()
    {
        Console.WriteLine("1. 알파 스트라이크 - MP 10\n공격력 * 2 로 하나의 적을 공격합니다."); // 추후 마법사에 맞게 변경
        Console.WriteLine("2. 더블 스트라이크 - MP 10\n공격력 * 1.5 로 2명의 적을 랜덤으로 공격합니다."); // 추후 마법사에 맞게 변경

    }

    //public override void Attack(GreenMonster enemy, float atkDmg)
    //{
    //치명타 or 회피 결정 
    //enemy에게 데미지 주기
    //결과창 출력
    //}
    //public override void SkillA(GreenMonster enemy, float atkDmg)
    //{
    //마나 계산
    //enemy에게 데미지 주기
    //결과창 출력
    //}
    //public override void SkillB(List<GreenMonster> enemy, float atkDmg)
    //{
    //마나 계산
    //enemy들중 랜덤한 두마리 선별 해서 데미지 주기
    //결과창 출력
    //}
}