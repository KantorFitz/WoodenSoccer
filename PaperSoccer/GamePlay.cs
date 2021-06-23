namespace PaperSoccer.GameSettings
{
    public enum GameState : ushort
    {
        NotStarted,
        Started,
        Finished        
    }
    public enum GameResult : ushort
    {
        InProgress,
        Draw,
        Player1Won,
        Player2Won
    }

    public enum Player : ushort
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
        Border,
        Player1Goal,
        Player2Goal,
        OldBall,
        Ball
    }
}
