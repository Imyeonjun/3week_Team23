using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TextRPG_Team23
{
    public class Forge
    {
        UpGrade upGrade = new UpGrade();
        public void Selection()
        {
            while (true)
            {
                //Console.Clear();
                Console.WriteLine("- @@대장간에 어서오세요 - \n   1. 강화 \n   2. 제작 \n   3. 수리");
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
