using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeProject
{
    public struct Obj
    {
        public int x;
        public int y;
        public string button;
        public Obj(int _x, int _y, string _button)
        {
            this.x = _x;
            this.y = _y;
            this.button = _button;
        }
    }
    internal class Forge
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Forge forge = new Forge();
            forge.Selection();
        }
        //UpGrade upGrade = new UpGrade();

        Obj obj = new Obj(1, 1, "▶");
        public void Selection()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("- Welcome - \n   1. 강화 \n   2. 제작 \n   3. 수리");

                Console.SetCursorPosition(obj.x, obj.y);
                Console.WriteLine(obj.button);

                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (obj.y > 1)
                        {
                            obj.y--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (obj.y < 3)
                        {
                            obj.y++;
                        }
                        break;
                    case ConsoleKey.Enter:
                        switch (obj.y)
                        {
                            case 1:
                                //upGrade.ItemSelection();
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                        }
                        break;
                }
            }
        }
    }
    
}
//아이템 강화
//돈, 다른 재화로 착용가능한 장비를 강화
//강화 래밸은 최대 10까지
//1lv100%, 2lv90%,  3lv80%
//4lv70%, 5lv60%,  6lv50%
//(강화 단계 하락)7lv40%, 8lv30%, 9lv20%
//(장비 파괴?)10lv10%
//10% 강화2단계 상승
