using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    internal class Temple
    {
        public void Selection()
        {
            Console.WriteLine(" - @@신전에 어서오세요 - ");
            Console.WriteLine("1. 헌금 2. 전직 3. 나가기");

            int.TryParse(Console.ReadLine(), out int input);
            switch(input)
            {// 헌금만 가능하며 돈이 싸이면 버프의 효과가 상승 세례(버프) 축복(상태이상해제)
                case 1:
                    Console.WriteLine("1. 헌금 2. 세례 3. 축복");

                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("잘못 입력하였습니다.");
                    break;
            }
        }
    }
}
