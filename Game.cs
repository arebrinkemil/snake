namespace snake;


class Game
{
    //sätt upp alla variabler
    private readonly int Width;
    private readonly int Height;

    private const int InitialLength = 5;
    private const int LoopSpeed = 50;

    private bool isGameOver;
    private Direction direction;
    private List<Point> snake;
    private Point food;
    private Point poison;
    private Random random;
    private int score;

    public Game(int width, int height)
    {
        //sätt alla värden
        Width = width;
        Height = height;

        isGameOver = false;
        direction = Direction.Right;
        snake = new List<Point>();
        random = new Random();
        score = 0;

        PlaceFood();
        // PlacePoison();
        InitializeSnake();
    }

    public void Start()
    {
        //kör till gameover
        while (!isGameOver)
        {
            //om input 
            if (Console.KeyAvailable)
            {
                HandleInput();
            }
            
            //kör detta varje loop
            Move();
            Draw();
            
             //pause i loopen
        }

        //game over screen
        Console.Clear();
        Console.WriteLine("\n                 *          )            (     \n (       (     (  `      ( /(            )\\ )  \n )\\ )    )\\    )\\))(  (  )\\())(   (  (  (()/(  \n(()/( ((((_)( ((_)()\\ )\\((_)\\ )\\  )\\ )\\  /(_)) \n /(_))_)\\ _ )\\(_()((_|(_) ((_|(_)((_|(_)(_))   \n(_)) __(_)_\\(_)  \\/  | __/ _ \\ \\ / /| __| _ \\  \n  | (_ |/ _ \\ | |\\/| | _| (_) \\ V / | _||   /  \n   \\___/_/ \\_\\|_|  |_|___\\___/ \\_/  |___|_|_\\  \n                                               \n");
        Console.WriteLine($"Your score: {score}");
        Thread.Sleep(4000);
    }

    private void InitializeSnake()
    {
        //sätt ormen i mitten
        int startX = Width / 2;
        int startY = Height / 2;

        for (int i = 0; i < InitialLength; i++)
        {
            snake.Add(new Point(startX - i, startY));
        }
    }

    private void PlaceFood()
    {
        //placera mat
        int x = random.Next(5, Width - 5);
        int y = random.Next(5, Height - 5);

        food = new Point(x, y);
    }

    // private void PlacePoison()
    // {
    //     
    //     int x = random.Next(0, Width);
    //     int y = random.Next(0, Height);
    //
    //     
    //     poison = new Point(x, y);
    // }
    
    

    private void HandleInput()
    {
        //alternativ för alla knapp tryck
        ConsoleKeyInfo key = Console.ReadKey(true);

        switch (key.Key)
        {   
            case ConsoleKey.W:
            case ConsoleKey.UpArrow:
                if (direction != Direction.Down)
                    direction = Direction.Up;
                break;
            case ConsoleKey.A:
            case ConsoleKey.LeftArrow:
                if (direction != Direction.Right)
                    direction = Direction.Left;
                break;
            case ConsoleKey.S:
            case ConsoleKey.DownArrow:
                if (direction != Direction.Up)
                    direction = Direction.Down;
                break;
            
            case ConsoleKey.D:
            case ConsoleKey.RightArrow:
                if (direction != Direction.Left)
                    direction = Direction.Right;
                break;
            case ConsoleKey.Escape:
                isGameOver = true;
                break;
        }
    }

    private void Move()
    {
        Point head = snake[0];
        Point newHead = new Point(head.X, head.Y);
        
        //byt direction baserat på input
        switch (direction)
        {
            case Direction.Up:
                newHead.Y--;
                break;
            case Direction.Down:
                newHead.Y++;
                break;
            case Direction.Left:
                newHead.X--;
                break;
            case Direction.Right:
                newHead.X++;
                break;
        }

        //träffa väggen = död
        if (newHead.X < 0 || newHead.X >= Width || newHead.Y < 0 || newHead.Y >= Height)
        {
            isGameOver = true;
            return;
        }

        //träffa sig själv = död
        if (snake.Any(p => p.X == newHead.X && p.Y == newHead.Y))
        {
            isGameOver = true;
            return;
        }
        
        //nuddar huvvet mat. lägg till poäng + placera ny
        if (newHead.Equals(food))
        {
            score++;
            PlaceFood();
        }
        // else if (newHead.Equals(poison))
        // {
        //     isGameOver = true;
        //     return;
        // }
        else
        {
            snake.RemoveAt(snake.Count - 1);
        }

        snake.Insert(0, newHead);
        
        // if (score % 10 == 0)
        // {
        //     PlacePoison();
        // }
    }


    private void Draw()
    {
        Console.Clear();  

        Console.WriteLine(new string('■', Width + 2));

        for (int y = 0; y < Height; y++)
        {
            Console.Write('■'); 

            for (int x = 0; x < Width; x++)
            {
                char symbol = ' '; 

                if (snake.Any(p => p.X == x && p.Y == y))
                {
                    
                    if (x == snake[0].X && y == snake[0].Y) 
                    {
                        symbol = '@';  
                    }
                    else
                    {
                        symbol = '■';  
                    }
                }

                if (food.X == x && food.Y == y)
                {
                    symbol = 'O';  
                }
                
                // if (poison.X == x && poison.Y == y)
                // {
                //     symbol = 'X';  
                // }

                Console.Write(symbol);  
            }

            Console.WriteLine('■');  
        }

        Console.WriteLine(new string('■', Width + 2));

        Console.WriteLine($"Score: {score}");
        Thread.Sleep(LoopSpeed);
    }

}