using System;
using System.Threading;
using TextRPG_Team23;

// 직업 기본 클래스
public abstract class Job
{
    public abstract string JobName { get; }
    public abstract float BaseAtkDmg { get; }
    public abstract int BaseDefence { get; }
    public abstract void PrintSkillInfo();

    public abstract void Attack(Monster enemy, float atkDmg, Player player);
    public abstract void SkillA(Monster enemy, float atkDmg, Player player);
    public abstract void SkillB(List<Monster> enemy, float atkDmg, Player player);
    public void DisplayAttackResult(Player player, Monster enemy, int originalHp, int finalDamage, bool isCritical, bool isMiss)
    {
        Console.WriteLine($"\n{player.Name} 의 공격!");

        if (isMiss)
        {
            Console.WriteLine($"{enemy.Name} 을(를) 맞추지 못했습니다. [Miss!]");
        }
        else
        {
            string critText = isCritical ? " [크리티컬!]" : "";
            Console.WriteLine($"{enemy.Name} 을(를) 맞췄습니다.{critText} [데미지 : {finalDamage}]");
        }

        Console.WriteLine($"\n{enemy.Name}");
        Console.WriteLine($"HP {originalHp} -> {(enemy.CurrentHp <= 0 ? "Dead" : enemy.CurrentHp.ToString())}");

        Console.WriteLine("\n엔터를 눌러 진행\n");
        Console.Write(">> ");
    }
    public void DisplayAttackResult(Player player, Monster enemy, int originalHp, int finalDamage, string skillName)
    {
        Console.WriteLine($"\n{player.Name} 의 {skillName}!");

        Console.WriteLine($"{enemy.Name} 을(를) 맞췄습니다. [데미지 : {finalDamage}]");
        Console.WriteLine($"\n{enemy.Name}");
        Console.WriteLine($"HP {originalHp} -> {(enemy.CurrentHp <= 0 ? "Dead" : enemy.CurrentHp.ToString())}");

        Console.WriteLine("\n엔터를 눌러 진행\n");
        Console.Write(">> ");
    }
    public void DisplayAttackResult(Player player, List<Monster> enemies, List<int> originalHps, List<int> damages, string skillName)
    {
        Console.WriteLine($"\n{player.Name} 의 {skillName}!");

        for (int i = 0; i < enemies.Count; i++)
        {
            Monster mon = enemies[i];
            int originalHp = originalHps[i];
            int damage = damages[i];

            Console.WriteLine($"{mon.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.WriteLine($"\n{mon.Name}");
            Console.WriteLine($"HP {originalHp} -> {(mon.CurrentHp <= 0 ? "Dead" : mon.CurrentHp.ToString())}");
        }

        Console.WriteLine("\n엔터를 눌러 진행\n");
        Console.Write(">> ");
    }

    public void CheckKillQuest(int hp, Player player)
    {
        if(hp <= 0)
        {
            player.PlusKillMonsterCnt();
        }
    }
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

    public override void Attack(Monster enemy, float atkDmg, Player player)
    {
        Random rand = new Random();
        int critical = rand.Next(0, 100);
        int miss = rand.Next(0, 100);
        float finalAtkDmg;

        // 오차 계산
        float offset = (float)Math.Ceiling(atkDmg * 0.1f); // 오차 값 계산
        int variation = rand.Next(-(int)offset, (int)offset + 1); // -오차 ~ +오차 사이 랜덤

        finalAtkDmg = atkDmg + variation;

        // 크리티컬
        bool isCritical = false;
        if (critical < 15)
        {
            finalAtkDmg *= 1.6f;
            isCritical = true;
            Console.WriteLine("크리티컬 히트!");
        }

        // 미스
        bool isMiss = false;
        if (miss < 10)
        {
            finalAtkDmg = 0;
            isMiss = true;
            Console.WriteLine("공격이 빗나갔다!");
        }

        if(!isMiss)
        {
            player.Inventory.CheckWeaponDurability(player);
        }
        int finalDamage = (int)finalAtkDmg;
        int originalHp = enemy.CurrentHp;
        //enemy.CurrentHp -= finalDamage;
        if(enemy is TakeDamage a)
        {
            a.TakeDamage(finalDamage + player.BuffAtk);
        }
        CheckKillQuest(enemy.CurrentHp, player);
        DisplayAttackResult(player, enemy, originalHp, finalDamage, isCritical, isMiss);
    }

    public override void SkillA(Monster enemy, float atkDmg, Player player)
    {
        if (player.CurrentMp < 10)
        {
            Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
            return;
        }

        player.CurrentMp -= 10;

        Random rand = new Random();
        float offset = (float)Math.Ceiling(atkDmg * 0.1f);
        int variation = rand.Next(-(int)offset, (int)offset + 1);
        float finalAtkDmg = (atkDmg + variation) * 2.0f;

        int finalDamage = (int)finalAtkDmg;
        int originalHp = enemy.CurrentHp;
        //enemy.CurrentHp -= finalDamage;
        if (enemy is TakeDamage a)
        {
            a.TakeDamage(finalDamage);
        }
        CheckKillQuest(enemy.CurrentHp, player);

        DisplayAttackResult(player, enemy, originalHp, finalDamage, "알파 스트라이크");
    }

    public override void SkillB(List<Monster> enemies, float atkDmg, Player player)
    {
        if (player.CurrentMp < 15)
        {
            Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
            return;
        }

        player.CurrentMp -= 15;

        Random rand = new Random();
        List<Monster> targets;

        if (enemies.Count >= 2)
        {
            // 2마리 랜덤 공격
            targets = enemies.OrderBy(x => rand.Next()).Take(2).ToList();
        }
        else if (enemies.Count == 1)
        {
            // 한 마리밖에 없으면 그 몬스터만 공격
            targets = new List<Monster> { enemies[0] };
            Console.WriteLine("공격 대상이 1명뿐입니다. 단일 대상에게 더블 스트라이크를 사용합니다.");
        }
        else
        {
            Console.WriteLine("공격할 대상이 없습니다.");
            return;
        }


        List<int> originalHps = new List<int>();
        List<int> damages = new List<int>();

        foreach (Monster enemy in targets)
        {
            float offset = (float)Math.Ceiling(atkDmg * 0.1f);
            int variation = rand.Next(-(int)offset, (int)offset + 1);
            float finalAtkDmg = (atkDmg + variation) * 1.5f;

            int finalDamage = (int)finalAtkDmg;
            int originalHp = enemy.CurrentHp;
            //enemy.CurrentHp -= finalDamage;
            if (enemy is TakeDamage a)
            {
                a.TakeDamage(finalDamage);
            }
            CheckKillQuest(enemy.CurrentHp, player);

            originalHps.Add(originalHp);
            damages.Add(finalDamage);
        }

        DisplayAttackResult(player, targets, originalHps, damages, "더블 스트라이크");

    }




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

    public override void Attack(Monster enemy, float atkDmg, Player player)
    {
        Random rand = new Random();
        int critical = rand.Next(0, 100);
        int miss = rand.Next(0, 100);
        float finalAtkDmg;

        // 오차 계산
        float offset = (float)Math.Ceiling(atkDmg * 0.1f); // 오차 값 계산
        int variation = rand.Next(-(int)offset, (int)offset + 1); // -오차 ~ +오차 사이 랜덤

        finalAtkDmg = atkDmg + variation;

        // 크리티컬
        bool isCritical = false;
        if (critical < 15)
        {
            finalAtkDmg *= 1.6f;
            isCritical = true;
            Console.WriteLine("크리티컬 히트!");
        }

        // 미스
        bool isMiss = false;
        if (miss < 10)
        {
            finalAtkDmg = 0;
            isMiss = true;
            Console.WriteLine("공격이 빗나갔다!");
        }
        if (!isMiss)
        {
            player.Inventory.CheckWeaponDurability(player);
        }
        int finalDamage = (int)finalAtkDmg;
        int originalHp = enemy.CurrentHp;
        //enemy.CurrentHp -= finalDamage;
        if (enemy is TakeDamage a)
        {
            a.TakeDamage(finalDamage);
        }
        CheckKillQuest(enemy.CurrentHp, player);
        DisplayAttackResult(player, enemy, originalHp, finalDamage, isCritical, isMiss);
    }
    public override void SkillA(Monster enemy, float atkDmg, Player player)
    {
        if (player.CurrentMp < 10)
        {
            Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
            return;
        }

        player.CurrentMp -= 10;

        Random rand = new Random();
        float offset = (float)Math.Ceiling(atkDmg * 0.1f);
        int variation = rand.Next(-(int)offset, (int)offset + 1);
        float finalAtkDmg = (atkDmg + variation) * 2.0f;

        int finalDamage = (int)finalAtkDmg;
        int originalHp = enemy.CurrentHp;
        enemy.CurrentHp -= finalDamage;
        if(enemy is TakeDamage a)
        {
            a.TakeDamage(finalDamage);
        }
        if (enemy.CurrentHp < 0) enemy.CurrentHp = 0;
        CheckKillQuest(enemy.CurrentHp, player);

        DisplayAttackResult(player, enemy, originalHp, finalDamage, "알파 스트라이크");
    }
    public override void SkillB(List<Monster> enemies, float atkDmg, Player player)
    {
        if (player.CurrentMp < 15)
        {
            Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
            return;
        }

        player.CurrentMp -= 15;

        Random rand = new Random();
        List<Monster> targets;

        if (enemies.Count >= 2)
        {
            // 2마리 랜덤 공격
            targets = enemies.OrderBy(x => rand.Next()).Take(2).ToList();
        }
        else if (enemies.Count == 1)
        {
            // 한 마리밖에 없으면 그 몬스터만 공격
            targets = new List<Monster> { enemies[0] };
            Console.WriteLine("공격 대상이 1명뿐입니다. 단일 대상에게 더블 스트라이크를 사용합니다.");
        }
        else
        {
            Console.WriteLine("공격할 대상이 없습니다.");
            return;
        }


        List<int> originalHps = new List<int>();
        List<int> damages = new List<int>();

        foreach (Monster enemy in targets)
        {
            float offset = (float)Math.Ceiling(atkDmg * 0.1f);
            int variation = rand.Next(-(int)offset, (int)offset + 1);
            float finalAtkDmg = (atkDmg + variation) * 1.5f;

            int finalDamage = (int)finalAtkDmg;
            int originalHp = enemy.CurrentHp;
            //enemy.CurrentHp -= finalDamage;
            if (enemy is TakeDamage a)
            {
                a.TakeDamage(finalDamage);
            }
            CheckKillQuest(enemy.CurrentHp, player);
            originalHps.Add(originalHp);
            damages.Add(finalDamage);
        }

        DisplayAttackResult(player, targets, originalHps, damages, "더블 스트라이크");

    }




}
