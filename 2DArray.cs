using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DArray
{
    class Program
    {

       
         
        private char[,] field = new char[12, 6];
        private struct Objects

        {
            public const char PLAYER = 'M';
            public const char EMPTY = '=';
            public const char WALL = 'W';
            public const char ITEM = '6';

        }

        private char objectUnder;
        private bool posChanged;
        private int collected;

        private struct Point

        {
            public int X { get; set; }
            public int Y { get; set; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }

            public void Set(int x, int y)
            {
                X = x;
                Y = y;
            }

        }

        private Point playerPos;

        public void Start()
        {
            Init();

            while (true)
            {
                PrintBoard();
                CheckInput();
            }
        }

        private void Init()
        {
            Random r = new Random();
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    char obj;

                    switch (r.Next(3))
                    {
                        case 0:
                            obj = Objects.ITEM;

                            break;

                        case 1:
                            obj = Objects.WALL;
                            break;

                        case 2:
                            obj = Objects.EMPTY;
                            break;

                        default:
                            obj = Objects.EMPTY;
                            break;
                    }

                    field[x, y] = obj;
                }
            }

            field[1, 1] = Objects.PLAYER;
            playerPos = new Point(1, 1);
            objectUnder = Objects.EMPTY;
        }

        private void PrintBoard()
        {
            for (int y = 0; y < field.GetLength(1); y++)
            {
                for (int x = 0; x < field.GetLength(0); x++)
                {
                    Console.Write(field[x, y]);
                }

                Console.WriteLine();

            }

            Console.WriteLine(string.Format("\nCurrent Position: [{0}, {1}]", playerPos.X, playerPos.Y));
            Console.WriteLine();
        }

        private void CheckInput()
        {
            var input = Console.ReadKey(true);
            Console.Clear();
            switch (input.Key)
            {

                case ConsoleKey.W:
                    Move(0, -1);
                    break;

                case ConsoleKey.S:
                    Move(0, 1);
                    break;

                case ConsoleKey.A:
                    Move(-1, 0);
                    break;

                case ConsoleKey.D:
                    Move(1, 0);
                    break;

                case ConsoleKey.E:
                    PickupItem();
                    break;

                case ConsoleKey.Q:
                    DropItem();
                    break;





            }
        }

        private void Move(int x, int y)
        {
            if (!CheckMove(x, y)) return;

            posChanged = !(playerPos.X + x == playerPos.X && playerPos.Y + y == playerPos.Y);

            if (posChanged)
            {
                field[playerPos.X, playerPos.Y] = objectUnder;
                objectUnder = field[playerPos.X + x, playerPos.Y + y];
                field[playerPos.X + x, playerPos.Y + y] = Objects.PLAYER;

            }

            playerPos.Set(playerPos.X + x, playerPos.Y + y);
        }

        private bool CheckMove(int x, int y)
        {
            int posX = playerPos.X + x;
            int posY = playerPos.Y + y;

            if (posX < 0 || posY < 0) return false;
            if (posX > field.GetLength(0) - 1) return false;
            if (posY > field.GetLength(1) - 1) return false;

            if (field[posX, posY] == Objects.WALL) return false;

            return true;
        }

        private void PickupItem()
        {
            if(objectUnder == Objects.ITEM)
            {
                objectUnder = Objects.EMPTY;

                collected++;
                    
            }
        }

        private void DropItem()
        {
            if(collected > 0)
            {
                if(objectUnder==Objects.EMPTY)
                {
                    objectUnder = Objects.ITEM;
                    collected--;
                }
            }
                   
        }
    }
}