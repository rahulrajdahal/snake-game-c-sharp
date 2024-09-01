using Snake;

Coord gridDimensions = new Coord(50, 20);
Coord snakePos = new Coord(10, 1);
Random random = new Random();
Coord rewardPos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
int frameDelayMilli = 50;
Direction movement = Direction.Down;
int score = 0;

List<Coord> snakePosHistory = new List<Coord>();
int tailLength = 0;

while (true)
{
    Console.Clear();
    Console.WriteLine("Score: " + score);
    snakePos.ApplyMovementDirection(movement);

    for (int y = 0; y < gridDimensions.Y; y++)
    {
        for (int x = 0; x < gridDimensions.X; x++)
        {
            Coord currentCoord = new Coord(x, y);

            if (snakePos.Equals(currentCoord) || snakePosHistory.Contains(currentCoord))
            {
                Console.Write("s");
                //  Console.Write("🐍");
            }
            else if (rewardPos.Equals(currentCoord))
            {
                Console.Write("r");
                // Console.Write("🐀");
            }
            else if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
            {
                Console.Write("#");
                // Console.Write("🧱");
            }
            else
            {
                Console.Write(' ');
            }
        }
        Console.WriteLine();
    }

    if (snakePos.Equals(rewardPos))
    {
        tailLength++;
        score++;
        rewardPos = new Coord(random.Next(1, gridDimensions.X - 1), random.Next(1, gridDimensions.Y - 1));
    }
    else if (snakePosHistory.Contains(snakePos) || snakePos.X == 0 || snakePos.Y == 0 || snakePos.X == gridDimensions.X - 1 || snakePos.Y == gridDimensions.Y - 1)
    {
        score = 0;
        tailLength = 1;
        snakePos = new Coord(10, 1);
        snakePosHistory.Clear();
        movement = Direction.Down;
        continue;
    }

    snakePosHistory.Add(new Coord(snakePos.X, snakePos.Y));
    if (snakePosHistory.Count > tailLength)
    {
        snakePosHistory.RemoveAt(0);
    }

    DateTime time = DateTime.Now;

    while ((DateTime.Now - time).Milliseconds < frameDelayMilli)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    movement = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    movement = Direction.Right;
                    break;
                case ConsoleKey.UpArrow:
                    movement = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    movement = Direction.Down;
                    break;
            }
        }
    }

    Thread.Sleep(frameDelayMilli);


}