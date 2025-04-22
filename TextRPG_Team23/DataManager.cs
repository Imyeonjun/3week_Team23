using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Team23_Dungeon
{
    class DataManager
    {
        private static DataManager dateManager;

        public static DataManager DManager
        {
            get 
            { 
                if( dateManager == null)
                    dateManager = new DataManager();
                
                return dateManager; 
            } 
        }
        public string[] MonsterInfo { get; set; } = { // [index]이름/공격력/방어력/체력/스킬카운트   녹던 몹 정보데이터
                "바늘등까마귀/9/3/30/2", "습지울음꾼/12/7/42/2", "바위턱도마뱀/6/12/37/2",
                "유령무스/6/0/1/2", "시퍼런털짐승/10/10/50/2", "덩굴곰/14/22/80/2" };




    }

}
