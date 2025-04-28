using System.Data;

namespace TextRPG_Team23
{
    public class MonsterFactory
    {
        //List<Monster> monsterPool = new List<Monster>();

        List<Monster> monsterBox;
        Battlecondition condition;

        public MonsterFactory(List<Monster> monsterBox, Battlecondition condition)
        {
            this.monsterBox = monsterBox;
            this.condition = condition;
        }

        public void MakeMonster(string where)
        {
            int giveCode = 1;

            if (where == "1단계던전")  //1단계 던전 생성
            {
                int mobPartyType = Program.random.Next(1, 4);
                int level = Program.random.Next(1, 6);
                switch (mobPartyType)
                {
                    case 1:
                        monsterBox.Add(new Gravemoss(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new Blightmaw(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new Duskrend(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new Rainwisp(condition, giveCode, level));
                        break;

                    case 2:
                        monsterBox.Add(new Roothelm(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new Gloomseer(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new Gloomseer(condition, giveCode, level));
                        break;

                    case 3:
                        monsterBox.Add(new Blightmaw(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new Blightmaw(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new Gloomseer(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new Rainwisp(condition, giveCode, level));
                        break;
                }
            }
            else if (where == "2단계던전") //2단계 던전 생성
            {
                int mobPartyType = Program.random.Next(1, 4);
                int level = Program.random.Next(6, 13);
                switch (mobPartyType)
                {
                    case 1:
                        monsterBox.Add(new BloodrootSentinel(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new HollowCaller(condition, giveCode, level));
                        break;

                    case 2:
                        monsterBox.Add(new HollowshadeBeast(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new HollowshadeBeast(condition, giveCode, level));
                        break;
                    case 3:
                        monsterBox.Add(new BloodrootSentinel(condition, giveCode, level));
                        giveCode++;
                        monsterBox.Add(new HollowshadeBeast(condition, giveCode, level));
                        break;
                }
            }
            else //보스전 생성
            {
                monsterBox.Add(new EclipseTyrant(condition, giveCode, 99));
            }

        }

    }
}
