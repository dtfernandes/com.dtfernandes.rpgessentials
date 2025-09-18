namespace RpgEssentials.TurnBased
{
    /// <summary>
    /// Interface that defines an entity during a Battle
    /// </summary>
    public interface IBattleEntity
    {
        void StartTurn(BattleBoard board);
        void EndTurn();

        int Turn { get; set; }

        float OrderFunction();
    }

}


