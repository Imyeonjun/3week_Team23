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
            // 기능 추가 수정
            Console.WriteLine(" - @@ 신전에 어서오세요 - \n");
            while (true)
            {
                Console.WriteLine("1. 헌금 2. 세레 3. 축복 0. 나가기 \n");

                int.TryParse(Console.ReadLine(), out int input);// 헌금만 해서 돈이 싸이면 버프의 효과가 상승 세례(버프) 축복(상태이상해제)
                if (input > 0 || input <= 3)
                {
                    switch (input)
                    {
                        case 1:
                            break;
                    }
                }
                else if (input == 0)
                {
                    Console.WriteLine("마을로 돌아갑니다.");
                    Console.WriteLine("Enter키를 눌러주세요");
                    return;
                }
                else
                {
                    BranchManager.ErrorMessage("잘못된 번호입니다, Enter를 누른 후 다시 입력해주세요.");
                    //Console.WriteLine("잘못 입력하였습니다.");
                }
            }
        }
    }
}
