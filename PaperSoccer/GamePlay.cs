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
        Unknown,
        InProgress,
        Draw,
        Player1Won,
        Player2Won
    }

    public enum Player : ushort
    {
        Unknown,
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
    public enum Direction : int
    {
        UNKNOWN = -1,
        NW,
        N,
        NE,
        W,
        E,
        SW,
        S,
        SE
    }
}
