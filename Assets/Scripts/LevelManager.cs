using Random=System.Random;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;

public class LevelManager : MonoBehaviour
{

    public List<GameObject> currentAllyUnits = new List<GameObject>();

    [SerializeField]
    private GameObject[] ennemiesList;
    private List<LevelMap> ennemiesLevelMap = new List<LevelMap>(); // level:gameobject

    public List<GameObject> board = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> actualBoard = new List<GameObject>();

    [HideInInspector]
    public int level = 1;

    [SerializeField]
    private GameObject unitsContainer;
    [SerializeField]
    private TMP_Text levelText;

    public GameObject noneGameObject;
    private PreCombatManager preCombatManager;

    private void Start()
    {
        ClearBoard();

        for (int i = 0; i < ennemiesList.Length; i++)
        {
            ennemiesLevelMap.Add(new LevelMap());
            ennemiesLevelMap[i].level = ennemiesList[i].GetComponent<Unit>().level;
            ennemiesLevelMap[i].gameobject = ennemiesList[i];
        }

        preCombatManager = GetComponent<PreCombatManager>();
    }

    public void PopulateBoard()
    {
        level++;
        ClearBoard();

        board = new List<GameObject>();
        // random Gen
        board.Add(ennemiesList[0]);
        board.Add(ennemiesList[1]);
        board.Add(ennemiesList[2]);
        // ---

        // add allies
        foreach(GameObject ally in currentAllyUnits)
        {
            board.Add(ally);
        }

        // shuffle
        Random random = new Random();
        board = board.OrderBy(x => random.Next()).ToList();

        // render
        InstantiateBoard();
        AddSelectListners();
    }

    public void InstantiateBoard()
    {
        foreach (GameObject unit in board)
        {
            if (unit != noneGameObject)
            {
                actualBoard.Add(Instantiate(unit, unitsContainer.transform));
            }
        }

        levelText.text = "Area Level: " + level.ToString();
    }

    public void ClearBoard()
    {
        board = new List<GameObject>();
        foreach (Transform child in unitsContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddSelectListners()
    {
        foreach (GameObject unit in actualBoard)
        {
            unit.GetComponent<Button>().onClick.AddListener(delegate{SelectUnit(unit);});
        }
    }

    private void SelectUnit(GameObject unit)
    {
        if (!preCombatManager.isSelected(unit))
        {
            preCombatManager.selectedUnits.Add(unit);
            unit.GetComponent<Unit>().SwitchBorder();
        } else {
            preCombatManager.selectedUnits.Remove(unit);
            unit.GetComponent<Unit>().SwitchBorder();
        }
    }

    public void Kill(string label)
    {
        foreach (GameObject unit in actualBoard)
        {
            if (unit.GetComponent<Unit>().label == label)
            {
                actualBoard.Remove(unit);
                preCombatManager.Kill(unit);
                unit.GetComponent<Unit>().Die();
                break;
            }
        }

        foreach (GameObject unit in currentAllyUnits)
        {
            if (unit.GetComponent<Unit>().label == label)
            {
                currentAllyUnits.Remove(unit);
                return;
            }
        }

        foreach (GameObject unit in board)
        {
            if (unit.GetComponent<Unit>().label == label)
            {
                board.Remove(unit);
                return;
            }
        }
        
    }

    public void SwapOnBoard(int idx1, int idx2)
    {
        GameObject tmp = actualBoard[idx1];
        actualBoard[idx1] = actualBoard[idx2];
        actualBoard[idx2] = tmp;

        tmp = board[idx1];
        board[idx1] = board[idx2];
        board[idx2] = tmp;

        updateSI();
    }

    public void updateSI()
    {   
        board = actualBoard;
        for (int i = 0; i < actualBoard.Count; i++)
        {
            actualBoard[i].transform.SetSiblingIndex(i);
        }
    }

    public bool CheckKill()
    {
        Debug.Log("checking kill");
        for (int i = 0; i < actualBoard.Count; i++)
        {
            if (actualBoard[i].GetComponent<Unit>().health <= 0)
            {
                preCombatManager.Kill(actualBoard[i]);
                actualBoard[i].GetComponent<Unit>().Die();
                Kill(actualBoard[i].GetComponent<Unit>().label);
                return true;
            }

        }

        Sync();
        return false;
    }

    public void Sync()
    {
        board = actualBoard;
    }
}

public class LevelMap
{
    public int level;
    public GameObject gameobject;
}
