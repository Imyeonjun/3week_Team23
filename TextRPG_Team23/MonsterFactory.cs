//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Team23_Dungeon
//{
//    class MonsterFactory
//    {
//        List<Monster> monsterPool = new List<Monster>();

//        public MonsterFactory()
//        {
//            int count = DataManager.DManager.MonsterInfo.Length;

//            for (int i = 0; i < count; i++)
//            {
//                SettingMonster(new Monster(), DataManager.DManager.MonsterInfo[i]);
//            }
//        }

//        public void SettingMonster(Monster m, string mStat)
//        {
//            string[] stat = mStat.Split('/');
//            m.Level = DungeonMaganer.random.Next(1, 7);
//            m.Name = stat[0];
//            m.Atk = int.Parse(stat[1]);
//            m.Def = int.Parse(stat[2]);
//            m.MaxHp = (int.Parse(stat[3]) + (m.Level * 2));
//            m.CurrentHp = m.MaxHp;
//            m.SkillCount = int.Parse(stat[4]);

//            monsterPool.Add(m);
//        }

//        public void MakeMonster(int count)
//        {
//            for (int i = 0; i < count; i++)
//            {
//                int select = DungeonMaganer.random.Next(0, 6);

//                DungeonMaganer.monsterBox.Add(monsterPool[select]);
//            }

//        }
//    }
//}
