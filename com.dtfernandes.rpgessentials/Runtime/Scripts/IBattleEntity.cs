namespace RpgEssentials.TurnBase
{
    /// <summary>
    /// Interface that defines an entity during a Battle
    /// </summary>
    public interface IBattleEntity
    {
        void StartTurn();
        void UpdateTurn();
        void EndTurn();

        int Turn { get; set; }

        float OrderFunction();
    }

}


