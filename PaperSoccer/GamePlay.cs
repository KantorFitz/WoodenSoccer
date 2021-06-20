namespace GameSettings
{
    enum GameState : ushort
    {
        NotStarted,
        Started,
        Finished        
    }
    enum Result : ushort
    {
        Draw,
        Player1Won,
        Player2Won
    }

    enum Player : ushort
    {
        Player1,
        Player2
    }

    namespace Board
    {
        enum PlayerState : ushort
        {
            CanStopHere,
            CanNotStopHere,
            HasBounce,
            FinishedMove
        }
        enum BoardPoint : ushort
        {
            Empty,
            Occupied,
            BorderTop,
            BorderBottom,
            BorderLeft,
            BorderRight,
            Player1Goal,
            Player2Goal,
            Ball
        }
    }    
}
