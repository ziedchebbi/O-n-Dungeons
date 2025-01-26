using UnityEngine;
using UnityEngine.UI;

public class SwapBauble : Bauble
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Effect);
    }

    private void Effect()
    {
        showTooltip();
        Debug.Log("swap baub");
        Destroy(gameObject);
    }
}
