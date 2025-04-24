using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextRPG_Team23
{
    internal class Forge
    {
        UpGrade upGrade = new UpGrade();
        Inventory inventory = new Inventory();
        public void Selection()
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("- Welcome - \n   1. 강화 \n   2. 제작 \n   3. 수리");
                int.TryParse(Console.ReadLine(), out int input);
                switch (input)
                {
                    case 1:
                        //Console.Clear();
                        upGrade.ItemSelection();
                        return;
                        break;
                    case 2:
                        break;
                    case 3:
                        
                        break;
                }
            }
        }
    }
    
}
