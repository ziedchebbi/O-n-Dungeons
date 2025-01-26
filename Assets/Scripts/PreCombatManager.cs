using System.Collections.Generic;
using UnityEngine;

public class PreCombatManager : MonoBehaviour
{   
    [SerializeField]
    private GameObject baubleContainer;

    [SerializeField]
    private List<GameObject> ownedBaubles = new List<GameObject>();

    // [HideInInspector]
    public List<GameObject> selectedUnits = new List<GameObject>();

    public void drawBaubles()
    {
        foreach (GameObject bauble in ownedBaubles)
        {
            Instantiate(bauble, baubleContainer.transform);
        }
    }

    public bool isSelected(GameObject unit)
    {
        foreach(GameObject selection in selectedUnits)
        {
            if (selection == unit)
            {
                return true;
            }
        }

        return false;
    }

    public List<GameObject> getSelected(int amount)
    {
        if (selectedUnits.Count == amount)
        {
            return selectedUnits;
        }
        return new List<GameObject>();
    }

    public void Kill(GameObject unit)
    {
        selectedUnits.Remove(unit);
    }

    public void KillBauble(string name)
    {
        foreach (GameObject bauble in ownedBaubles)
        {
            if (bauble.GetComponent<Bauble>().label == name)
            {
                ownedBaubles.Remove(bauble);
                return;
            }
        }        
    }
}
