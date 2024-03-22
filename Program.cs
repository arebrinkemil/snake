
using snake;

class Program
{
    static void Main(string[] args)
    {
        const int width = 40;
        const int height = 20;

        //Console inställningar 
        Console.SetWindowSize(width + 2, height + 4); 
        Console.SetBufferSize(width + 2, height + 4);
        Console.CursorVisible = false;

        Game game = new Game(width, height);
        game.Start();
    }
}


enum Direction
{
    Up,
    Down,
    Left,
    Right
}



