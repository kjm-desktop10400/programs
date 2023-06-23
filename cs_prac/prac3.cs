using System;

 namespace prac2
{

    class Chess
    {
        private string[,] board = new string[8, 8];

        public Chess()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if((i + j) % 2 == 0)
                    {
                        board[i, j] = "Black";
                    }
                    else
                    {
                        board[i, j] = "White";
                    }
                }
            }
        }
        public void Look(int x, int y)
        {
            Console.WriteLine("({0},{1}) : {2}", x, y , board[x, y]);
        }
    }

    class Test
    {
        public static void Run()
        {
            Chess game = new Chess();
            int x, y;
            Console.Write("x : ");
            x = int.Parse(Console.ReadLine());
            Console.Write("y : ");
            y = int.Parse(Console.ReadLine());
            game.Look(x, y);
        }

        static void Main()
        {
            Test.Run();
        }
    }

}