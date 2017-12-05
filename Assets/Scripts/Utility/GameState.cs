/*
	GameState.cs
	Created 9/27/2017 11:37:41 AM
	Project Resource Collector by Base Games
*/

namespace Utility
{
    //The current game state of the game.
    public enum CurrentGameState { Playing, Paused }

    /// <summary>
    /// The current state of the game to be changed at will.
    /// </summary>
    public class GameState
    {
        public static CurrentGameState CGameState = CurrentGameState.Playing;
    }
}