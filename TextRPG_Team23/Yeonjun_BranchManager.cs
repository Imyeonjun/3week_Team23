using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team23
{
    public class BranchManager //선택지 생성기
    {
        //Branch 함수는 ReturnSelect를 위한 보조 함수. 호출은 ReturnSelect 권장.
        public static void Branch(string[] options, bool isCancel, string cancelText) // 선택지 내용, 취소문 유무, 취소문 텍스트
        {
            for (int i = 0; i < options.Length; i++)
            {
                // i를 번호로 지정하고, 뒤쪽에 가져온 선택지 불러오기
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            if (isCancel)
            {
                Console.WriteLine("\n0. " + cancelText);
            }
        }

        public static int ReturnSelect(string[] options, bool isCancel, string cancelTex)
        {
            //선택지 출력
            Branch(options, isCancel, cancelTex);

            //사용자 입력 받기
            Console.Write("\n선택 >> ");
            string input = Console.ReadLine();

            //입력값 판단
            if (int.TryParse(input, out int result))
            {
                int maxOptions = options.Length;

                if (isCancel && result == 0)
                    return 0;

                else if (result >= 1 && result <= maxOptions)
                    return result;

                return -1;
            }
            return -1;
        }

        public static void ErrorMessage(string input)
        {
 /*           Console.Clear();*/
            Console.Write(input);
            Console.WriteLine();
            Console.Write("\nPress any key to continue\n" +
                          ">>>");
            Console.ReadKey();
        }
    }
}
