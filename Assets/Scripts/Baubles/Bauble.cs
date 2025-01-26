using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bauble : MonoBehaviour
{
    public string label = "Bauble";
    public string description = "Lorem Ipsum Dolor";
    public Sprite image;

    public string tooltip = "Select a unit";
    [SerializeField]
    private TMP_Text tooltipText;

    private TMP_Text[] texts;

    void Awake()
    {
        transform.GetComponentsInChildren<Image>()[0].sprite = image;

        texts = transform.GetComponentsInChildren<TMP_Text>();
        texts[0].text = label;
        texts[1].text = description;

        tooltipText = GameObject.Find("ToolTip Text").GetComponent<TMP_Text>();
    }

    public void showTooltip()
    {
        tooltipText.text = tooltip;
    }
}
