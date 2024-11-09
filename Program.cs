using Snake;

Coord gridDimensions = new(50, 30);
Coord snakePos = new(10, 1);
Random random = new();
Coord rewardPos = new(random.Next(1, gridDimensions.X), random.Next(1, gridDimensions.Y));
int frameDelayMilli = 50;
Direction movement = Direction.Down;
int score = 0;

List<Coord> snakePosHistory = [];
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
            Coord currentCoord = new(x, y);


            if (snakePos.Equals(currentCoord))
            {
                Console.Write("c");
            }
            else if (snakePosHistory.Contains(currentCoord))
            {
                Console.Write("o");
            }
            else if (rewardPos.Equals(currentCoord))
            {
                Console.Write("o");
            }
            else if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
            {
                Console.Write("#");
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

    DateTime time = DateTime.UtcNow;

    while ((DateTime.UtcNow - time).Milliseconds < frameDelayMilli)
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