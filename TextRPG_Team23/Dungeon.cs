using System;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;




namespace TextRPG_Team23
{

    class DungeonMaganer
    {
        public static Random random = new Random();

        public static List<Monster> monsterBox = new List<Monster>();

        MonsterFactory facktory = new MonsterFactory();

        


    }

    class Battle
    {
        public void BattleStart()
        {
            Console.Clear();
            Console.Write("몬스터가 등장했다!\n\n" +
              "전투를 시작하려면 아무키나 누르세요.\n" +
              ">>>");

            Console.ReadKey();
            int spawnCount = DungeonMaganer.random.Next(2, 5);
        }


        public void BattleAi()
        {

        }

    }

    

}

