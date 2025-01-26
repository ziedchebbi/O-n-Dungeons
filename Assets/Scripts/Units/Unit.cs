using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Unit: MonoBehaviour
{
    [SerializeField]
    private string label = "Unit";
    [SerializeField]
    private Sprite image;

    [Space]
    [SerializeField]
    private int dammage = 999;
    [SerializeField]
    public int health = 999;

    [Space]
    public int weight = 1;
    public int level = 1;

    private TMP_Text[] texts;

    public bool isSelected = false;
    private Image border;
    [SerializeField]
    private Sprite unselectedBorder;
    [SerializeField]
    private Sprite selectedBorder;

    private void Awake()
    {
        border = transform.GetComponentsInChildren<Image>()[0];
        transform.GetComponentsInChildren<Image>()[1].sprite = image;

        texts = transform.GetComponentsInChildren<TMP_Text>();
        texts[1].text = dammage.ToString();
        texts[0].text = health.ToString();
        texts[2].text = weight.ToString();
    }

    public void UpdateStats()
    {
        texts[1].text = dammage.ToString();
        texts[0].text = health.ToString();
    }

    public void Dammage(int dammage)
    {
        health -= dammage;
        UpdateStats();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SwitchBorder()
    {
        if (isSelected)
        {
            border.sprite = unselectedBorder;
            isSelected = false;
        } else {
            border.sprite = selectedBorder;
            isSelected = true;
        }
    }
}
