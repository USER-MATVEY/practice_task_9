using System;
using System.Text.RegularExpressions;

namespace practice_task_9
{
    class Program
    {
        static void Main()
        {
            Regex chessCoordReg = new Regex(@"[a-h][1-8]");
            
            Console.Write("введите размер доски, согласно шахматной записи (до h8 включительно): ");
            string boardDimentions = Console.ReadLine().ToLower();

            Console.Write("Введите позицию чёрной фигуры: ");
            string blackPiecePosition = Console.ReadLine().ToLower();
            
            Console.Write("Введите позицию белой фигуры: ");
            string whitePiecePosition = Console.ReadLine().ToLower();
            
            Console.Write("Введите конечную точку белой фигуры: ");
            string whitePieceDestination = Console.ReadLine().ToLower();
            
            Console.WriteLine("Введите тип чёрной фигуры: ");
            Console.WriteLine("1 - лодья; 2 - конь; 3 - слон; 4 - ферзь; 5 - король;");
            int blackPieceType = Convert.ToInt32(Console.ReadLine());
            
            Console.WriteLine("Введите тип белой фигуры: ");
            Console.WriteLine("1 - лодья; 2 - конь; 3 - слон; 4 - ферзь; 5 - король;");
            int whitePieceType = Convert.ToInt32(Console.ReadLine());
            
            string[] strings = new string[4] {boardDimentions, blackPiecePosition, whitePiecePosition, whitePieceDestination};

            if (!ValidFormat(chessCoordReg, strings))
            {
                Console.WriteLine("Введены данные, неправильные по формату!");
                return;
            }

            blackPiecePosition = ConvertChessCoordsToNormalCoords(blackPiecePosition);
            whitePiecePosition = ConvertChessCoordsToNormalCoords(whitePiecePosition);
            whitePieceDestination = ConvertChessCoordsToNormalCoords(whitePieceDestination);
            boardDimentions = ConvertChessCoordsToNormalCoords(boardDimentions);

            strings = new string[4] { boardDimentions, blackPiecePosition, whitePiecePosition, whitePieceDestination };

            for (int i = 1; i < 4; i++)
            {
                if (CoordsNotValid(strings[i], boardDimentions))
                {
                    Console.WriteLine("Введеы некорректные данные");
                    return;
                }
            }

            if (CanMove(whitePiecePosition, whitePieceDestination, whitePieceType))
            {
                Console.WriteLine("Фигура может дойти до поля.");
            }
            else if (CanMove(blackPiecePosition, whitePieceDestination, blackPieceType))
                Console.WriteLine("Фигура может дойти до поля, но оно под боем.");
            else Console.WriteLine("Фигура не дойдёт до поля.");
        }

        static bool ValidFormat(Regex strFormat, string[] strToCheck)
        {
            for (int i = 0; i < strToCheck.Length; i++)
            {
                if (!strFormat.IsMatch(strToCheck[i])) return false;
            }
            return true;
        }

        static string ConvertChessCoordsToNormalCoords(string chessCoords)
        {
            int xCoord = chessCoords[0] - 'a';
            int yCoord = int.Parse(chessCoords[1].ToString()) - 1;

            return new string(xCoord.ToString() + yCoord.ToString());
        }

        static Boolean CoordsNotValid(string piecePosition, string boardDimentions)
        {
            int pieceX = piecePosition[0] - '0';
            int pieceY = piecePosition[1] - '0';
            int boardX = boardDimentions[0] - '0';
            int boardY = boardDimentions[1] - '0';

            if ((pieceX > boardX || pieceY > boardY) ||
                (pieceX < 0 || pieceY < 0))
            { return true; }

            return false;
        }

        static bool CanMove(string piecePosition, string piceDestination, int pieceType)
        {
            int pieceX = Convert.ToInt32(piecePosition[0] - '0');
            int pieceY = Convert.ToInt32(piecePosition[1] - '0');
            int DestX = Convert.ToInt32(piceDestination[0] - '0');
            int DestY = Convert.ToInt32(piceDestination[1] - '0');

            switch (pieceType)
            {
                case 1: return (pieceX == DestX || pieceY == DestY); 
                case 2: return ((Math.Abs(pieceX - DestX) == 2 && Math.Abs(pieceY - DestY) == 1)
                        || (Math.Abs(pieceX - DestX) == 1 && Math.Abs(pieceY - DestY) == 2)); 
                case 3: return (Math.Abs(pieceX-DestX) == Math.Abs(pieceY - DestY)); 
                case 4: return ((Math.Abs(pieceX - DestX) == Math.Abs(pieceY - DestY))
                        || (pieceX == DestX || pieceY == DestY)); 
                case 5: return ((Math.Abs(pieceX - DestX) == 1 && Math.Abs(pieceY - DestY) == 1)); 
            }
            return true;
        }
    }
}