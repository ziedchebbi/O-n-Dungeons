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
        preCombatManager.drawBaubles();
    }

    private void Update()
    {
        if (gameState == gameStates.Gen)
        {
            levelManager.PopulateBoard();

            gameState = gameStates.PreCombat;
        }

        if (gameState == gameStates.Combat)
        {
            Combat();
        }

        if (gameState == gameStates.Shop)
        {
            levelManager.Sync();
        }
    }

    private void Combat()
    {
        bool swap = true;
        while (swap)
        {
            Debug.Log("sorting");
            swap = false;
            for (int i = 0; i < levelManager.actualBoard.Count - 1; i++)
            {
                if (levelManager.actualBoard[i].GetComponent<Unit>().weight > levelManager.actualBoard[i + 1].GetComponent<Unit>().weight)
                {
                    levelManager.SwapOnBoard(i, i + 1);
                    if(levelManager.actualBoard[i].GetComponent<Unit>().type != levelManager.actualBoard[i + 1].GetComponent<Unit>().type)
                    {
                        levelManager.actualBoard[i].GetComponent<Unit>().Dammage(levelManager.actualBoard[i + 1].GetComponent<Unit>().dammage);
                        levelManager.actualBoard[i + 1].GetComponent<Unit>().Dammage(levelManager.actualBoard[i].GetComponent<Unit>().dammage);
                    }
                    swap = true;
                }

                // if (levelManager.actualBoard[i].GetComponent<Unit>().weight == levelManager.actualBoard[i + 1].GetComponent<Unit>().weight)
                // {
                //     levelManager.SwapOnBoard(i, i + 1);
                //     if(levelManager.actualBoard[i].GetComponent<Unit>().type != levelManager.actualBoard[i + 1].GetComponent<Unit>().type)
                //     {
                //         levelManager.actualBoard[i].GetComponent<Unit>().Dammage(levelManager.actualBoard[i + 1].GetComponent<Unit>().dammage);
                //         levelManager.actualBoard[i + 1].GetComponent<Unit>().Dammage(levelManager.actualBoard[i].GetComponent<Unit>().dammage);
                //     }
                //     swap = false;
                // }

                if(levelManager.CheckKill())
                {
                    Debug.Log("retake");
                    Combat();
                    return;
                }
            }
        }

        levelManager.updateSI();
        gameState = gameStates.Shop;            
    }

    public void startCombat()
    {
        if (gameState == gameStates.PreCombat)
        {
            gameState = gameStates.Combat;
        }

        if (gameState == gameStates.Shop)
        {
            gameState = gameStates.Gen;
        }
    }
}
