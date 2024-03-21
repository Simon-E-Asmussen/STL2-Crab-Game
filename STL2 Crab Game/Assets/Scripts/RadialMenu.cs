using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    [SerializeField] private GameObject entryPrefab;

    [SerializeField] private float radius = 300f;

    [SerializeField] private List<Texture> icons;

    private List<RadialMenuEntry> entries;

    // Start is called before the first frame update
    void Start()
    {
        entries = new List<RadialMenuEntry>();
    }


    void AddEntry(string pLabel, Texture pIcon)
    {
        GameObject entry = Instantiate(entryPrefab, transform);

        RadialMenuEntry rme = entry.GetComponent<RadialMenuEntry>();
        rme.SetLabel(pLabel);
        rme.SetIcon(pIcon);
        
        entries.Add(rme);
    }

    public void Open()
    {
        for (int i = 0; i < 3; i++)
        {
            AddEntry("Button" + i.ToString(), icons[i]);
        }
        Rearrage();
    }

    void Rearrage()
    {
        float radiansOfSeperation = (Mathf.PI * 2) / entries.Count;
        for (int i = 0; i < entries.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeperation * i) * radius;
            float y = Mathf.Cos(radiansOfSeperation * i) * radius;

            entries[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0);
        }
    }
}
