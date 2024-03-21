using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI label;
    
    [SerializeField] private RawImage icon;
    
    public void SetLabel(string pText)
    {
        label.text = pText;
    }
    
    public void SetIcon(Texture pIcon)
    {
        icon.texture = pIcon;
    }

    public Texture GetIcon()
    {
        return (icon.texture);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
