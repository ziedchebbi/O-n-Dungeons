using UnityEngine;

class GameManager : MonoBehaviour
{
    private LevelManager levelManager;
    private PreCombatManager preCombatManager;

    private enum gameStates
    {
        Gen,
        PreCombat,
        Combat,
        Shop
    }
    private gameStates gameState;

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
        preCombatManager = GetComponent<PreCombatManager>();
        gameState = gameStates.Gen; 
    }

    private void Update()
    {
        if (gameState == gameStates.Gen)
        {
            levelManager.PopulateBoard();
            preCombatManager.drawBaubles();

            gameState = gameStates.PreCombat;
        }
    }
}
