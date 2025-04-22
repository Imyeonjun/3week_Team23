using System.Numerics;
using System;

namespace TextRPG_Team23
{
    internal class Program
    {
        //게임 종료를 위한 변수
        static private bool isRunning = true;

        static void Main(string[] args)
        {
            CreateCharacter();
            ShowMainMenu();

            //isRunning = false;가 되면 while 조건에 벗어나면서 프로그램 중단 = 게임 종료
            while (isRunning == true)
            {
                Console.Write("메뉴 선택 > ");
                string input = Console.ReadLine();
                //MenuSelect(input);
            }
        }

        static private void ShowMainMenu()
        {
            Console.Clear();
        }

        /*static private void MenuSelect(string input)
        {
            switch (input)
            {
                case "1": // 상태 보기

                    break;
                case "2": // 인벤토리

                    break;
                case "3": // 상점

                    break;
                case "4": // 던전

                    break;
                case "5": // 휴식 or 여관

                    break;
                case "0":
                    Console.WriteLine
                        ("== 저장 메뉴 ==" +
                        "\n" +
                        "\n1. 저장" +
                        "\n2. 저장 후 종료" +
                        "\n" +
                        "\n0. 돌아가기");
                    string saveInput = Console.ReadLine();

                    switch (saveInput)
                    {
                        case "1":
                            Console.WriteLine("게임을 저장합니다.");
                            //게임 저장 호출
                            ShowMainMenu();
                            break;
                        case "2":
                            Console.WriteLine("게임을 저장합니다.");
                            //게임 저장 호출
                            isRunning = false;
                            break;
                        case "0":
                            break;
                    }
                    break;

                default:
                    Console.WriteLine("\n잘못된 입력입니다.");
                    break;
            }

            if (isRunning)
            {
                Console.WriteLine("\n아무 키나 눌러 진행");
                Console.ReadKey();
                ShowMainMenu();
            }
        }*/

        static Player player;
        static void CreateCharacter()
        {
            Console.WriteLine("이름을 입력하세요.");
            string name = Console.ReadLine();
            Console.WriteLine("직업을 선택하세요. (0: 전사, 1: 마법사)");
            int jobChoice = int.Parse(Console.ReadLine());
            Job job;

            switch (jobChoice)
            {
                case 0:
                    job = new Warrior();
                    break;
                case 1:
                    job = new Magician();
                    break;
                default:
                    Console.WriteLine("잘못된 선택입니다. 기본 직업으로 전사를 선택합니다.");
                    job = new Warrior();
                    break;
            }
            player = new Player(name, job);
        }
    }

    public class BranchManager //선택지 생성기
    {
        //Branch 함수는 ReturnSelect를 위한 보조 함수. 호출은 ReturnSelect 권장.
        public void Branch(string[] options, bool isCancel, string cancelText) // 선택지 내용, 취소문 유무, 취소문 텍스트
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

        public int ReturnSelect(string[] options, bool isCancel, string cancelTex)
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
    }
}