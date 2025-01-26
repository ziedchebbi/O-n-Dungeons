using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmiteBauble : Bauble
{
    private PreCombatManager preCombatManager;
    private LevelManager levelManager;

    [SerializeField]
    private int dammage = 3;

    private void Start()
    {
        preCombatManager = GameObject.Find("GM").GetComponent<PreCombatManager>();
        levelManager = GameObject.Find("GM").GetComponent<LevelManager>();
        GetComponent<Button>().onClick.AddListener(Effect);
    }

    private void Effect()
    {
        showTooltip();

        List<GameObject> selected = preCombatManager.getSelected(1);
        List<GameObject> affected = new List<GameObject>();
        if (selected.Count == 1 && selected.Count > 0)
        {
            foreach (GameObject unit in levelManager.actualBoard)
            {
                if (unit == selected[0])
                {
                    affected.Add(unit);
                }
            }

            for (int i = 0; i < affected.Count; i++)
            {
                affected[i].GetComponent<Unit>().Dammage(dammage);
                if (affected[i].GetComponent<Unit>().health <= 0)
                {
                    levelManager.Kill(affected[i]);
                }
            }

            preCombatManager.KillBauble(gameObject.GetComponent<Bauble>().label);
            Destroy(gameObject);
        }
    }
}
