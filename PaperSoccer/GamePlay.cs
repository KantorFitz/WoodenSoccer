namespace GameSettings
{
    enum GameState : ushort
    {
        NotStarted = 0,
        Moving,
        EndedMove,
        Draw,
        Player1Won,
        Player2Won
    }
    enum MoveState : ushort
    {
        CanStopHere,
        CanNotStopHere,
        HasBounce,
        FinishedMove
    }
}
