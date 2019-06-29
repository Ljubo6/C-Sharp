using System;
using System.Linq;

namespace P03_JediGalaxy
{
    public class StartUp
    {
        public static void Main()
        {
            int[] dimestion = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int rows = dimestion[0];
            int cols = dimestion[1];

            Board board = new Board(rows,cols);

            string command = Console.ReadLine();
            long sum = 0;
            while (command != "Let the Force be with you")
            {
                int[] playerCoordinates = command.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                int[] evilCoordinates = Console.ReadLine().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                Player evil = new Player
                {
                    Row = evilCoordinates[0],
                    Col = evilCoordinates[1]
                };

                while (evil.Row >= 0 && evil.Col >= 0)
                {
                    if (board.IsInside(evil.Row, evil.Col))
                    {
                        board.Matrix[evil.Row, evil.Col] = 0;
                    }
                    evil.Row--;
                    evil.Col--;
                }

                Player player = new Player
                {
                    Row = playerCoordinates[0],
                    Col = playerCoordinates[1]
                };

                while (player.Row >= 0 && player.Col < board.Matrix.GetLength(1))
                {
                    if (board.IsInside(player.Row, player.Col))
                    {
                        sum += board.Matrix[player.Row, player.Col];
                    }

                    player.Col++;
                    player.Row--;
                }

                command = Console.ReadLine();
            }

            Console.WriteLine(sum);

        }
    }
}
