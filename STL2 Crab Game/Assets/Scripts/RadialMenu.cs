using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        for (int i = 0; i < 8; i++)
        {
            AddEntry("Button" + i.ToString(), icons[i]);
        }
        Rearrage();
    }
    
    public void Close()
    {
        for (int i = 0; i < 8; i++)
        {
            RectTransform rect = entries[i].GetComponent<RectTransform>();
            GameObject entry = entries[i].gameObject;
            
            rect.DOAnchorPos(Vector3.zero, 0.3f).SetEase(Ease.OutQuad).SetDelay(0.05f * i).onComplete = delegate
            {
                Destroy(entry);
            };
        }
        
        entries.Clear();
    }

    public void Toggle()
    {
        if (entries.Count == 0)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    void Rearrage()
    {
        float radiansOfSeperation = (Mathf.PI * 2) / entries.Count;
        for (int i = 0; i < entries.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeperation * i) * radius;
            float y = Mathf.Cos(radiansOfSeperation * i) * radius;


            RectTransform rect = entries[i].GetComponent<RectTransform>();
            
            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad).SetDelay(0.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0), 0.3f).SetEase(Ease.OutQuad).SetDelay(0.05f * i);
        }
    }
}
