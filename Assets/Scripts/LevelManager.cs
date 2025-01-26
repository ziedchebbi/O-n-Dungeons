using Random=System.Random;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{

    public GameObject[] currentAllyUnits;

    [SerializeField]
    private GameObject[] ennemiesList;
    private List<LevelMap> ennemiesLevelMap = new List<LevelMap>(); // level:gameobject

    private List<GameObject> board = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> actualBoard = new List<GameObject>();

    [HideInInspector]
    public int level = 1;

    [SerializeField]
    private GameObject unitsContainer;
    [SerializeField]
    private TMP_Text levelText;

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

    private void InstantiateBoard()
    {
        foreach (GameObject unit in board)
        {
            actualBoard.Add(Instantiate(unit, unitsContainer.transform));
        }

        levelText.text = "Area Level: " + level.ToString();
    }

    private void ClearBoard()
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

    public void Kill(GameObject unit)
    {
        actualBoard.Remove(unit);
        board.Remove(unit);
        preCombatManager.Kill(unit);
        unit.GetComponent<Unit>().Die();
    }
}

public class LevelMap
{
    public int level;
    public GameObject gameobject;
}
