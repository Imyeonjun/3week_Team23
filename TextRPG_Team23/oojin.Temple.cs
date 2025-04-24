using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class Temple
    {
        public void Selection()
        {
            Console.WriteLine(" - @@신전에 어서오세요 - ");
            Console.WriteLine("1. 헌금 2. 세레 3. 축복 4. 나가기");

            int.TryParse(Console.ReadLine(), out int input);// 헌금만 가능하며 돈이 싸이면 버프의 효과가 상승 세례(버프) 축복(상태이상해제)
            if (input >= 1 || input <= 4)
            {
                switch(input)
                {
                    case 0:
                        break;
                }
            }            
            else
            {
                Console.WriteLine("잘못 입력하였습니다.");
                return;
            }

        }
    }
}
