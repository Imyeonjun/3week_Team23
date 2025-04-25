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

        public void MakeMonster(int mobCount)
        {
            int giveCode = 1;

            for (int i = 0; i < mobCount; i++)
            {
                int mobType = Program.random.Next(0, 6);
                int level = Program.random.Next(1, 6);
                switch (mobType)
                {
                    case 0:
                        monsterBox.Add(new Gravemoss(condition, giveCode, level));
                        break;

                    case 1:
                        monsterBox.Add(new Roothelm(condition, giveCode, level));
                        break;

                    case 2:
                        //monsterBox.Add(new Blightmaw(condition, giveCode, level));
                        monsterBox.Add(new Gravemoss(condition, giveCode, level));
                        break;

                    case 3:
                        monsterBox.Add(new Duskrend(condition, giveCode, level));
                        break;

                    case 4:
                        monsterBox.Add(new Gloomseer(condition, giveCode, level));
                        break;

                    case 5:
                        monsterBox.Add(new Rainwisp(condition, giveCode, level));
                        break;
                }
                giveCode++;
            }

        }

    }
}
