namespace GameSettings
{
    enum GamePlay : ushort
    {
        NotStarted = 0,
        Move,
        Bounce,
        EndedMove,
        PositionConfirmed,
        CanNotEndMove,
        Draw,
        FinishedMove
    }
}
