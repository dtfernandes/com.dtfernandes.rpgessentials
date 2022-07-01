using System;
using System.Collections.Generic;
using System.Linq;


namespace RpgEssentials.TurnBased
{
    public interface IBattleBehaviour
    {
        void StartBehaviour();
        bool UpdateBehaviour();
        void EndBehaviour();
    }

    public class PlayerBattleBehaviour : IBattleBehaviour
    {
        public void StartBehaviour()
        {
            //Open player menu
        }

        public bool UpdateBehaviour()
        {
            return true;
        }
        public void EndBehaviour()
        {
           
        }

    }


    public class AIBattleBehaviour<T> : IBattleBehaviour where T: IBattleBoard
    {

        private T board;

        public AIBattleBehaviour(T board)
        {
            this.board = board;
        }

        int t = 0;
        public void StartBehaviour()
        {
            //Mcts and stuff
            t = 30;
        }

        //Helper function to delay the AI's turn. Only for testing 
        private bool Delay()
        {
            t--;
            if (t == 0)
                return false;
            else return true;
        }

        public bool UpdateBehaviour()
        {
            return Delay();
        }

        public void EndBehaviour()
        {
         
        }

    }

}
