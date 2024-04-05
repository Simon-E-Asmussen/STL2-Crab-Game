using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using DG.Tweening;

public class RadialMenuEntry : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI label;
    
    [SerializeField] private RawImage icon;

    private RectTransform rect;

    private void Start()
    {
        rect = icon.GetComponent<RectTransform>();
    }

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
        rect.DOComplete();
        rect.DOScale(Vector3.one * 1.5f, 0.3f).SetEase(Ease.OutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.DOComplete();
        rect.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuad);
    }
}
