using System.Linq;
using System.Collections.Generic;

namespace RpgEssentials.TurnBased
{
    public class AIBattleBehaviour<T> : IBattleBehaviour where T: IBattleBoard
    {
        protected BattleEntity entity;
        protected T board;

        public AIBattleBehaviour(T board)
        {
            
            this.board = board;
        }

        public virtual void StartBehaviour()
        {
            entity = board.TurnEntity;

            //Select move
            IBattleMove selectedMove = entity.Mold.Moves[0];

            //Select all possible entities
            IEnumerable<BattleEntity> entities = 
                board.Entities.Where(x => x.IsPlayer && !x.IsDead);

            //Resolve Attack
            entity.UseMove(selectedMove,entities);
        }
    
        public virtual bool UpdateBehaviour()
        {
            return true;
        }

        public virtual void EndBehaviour()
        {
         
        }

    }

}
