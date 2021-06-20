namespace PaperSoccer.GameSettings
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
}

namespace PaperSoccer.BoardSettings
{
    public enum PlayerState : ushort
    {
        CanStopHere,
        CanNotStopHere,
        HasBounce,
        FinishedMove
    }
    public enum BoardPoint : ushort
    {
        Empty,
        Outer,
        Occupied,
        BorderTop,
        BorderBottom,
        BorderLeft,
        BorderRight,
        Player1Goal,
        Player2Goal,
        OldBall,
        Ball
    }
}
