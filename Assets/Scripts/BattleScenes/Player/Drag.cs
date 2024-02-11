﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform parentToReturnTo = null;
    public Card cardInfo;
    public DeckManager deckManager;
    private bool isHovered = false;
    private bool isDragging = false;
    private Vector3 originalScale;
    private Vector3 enlargedScale = new Vector3(2f, 2f, 2f);
    private Vector3 hoverOffset = new Vector3(0f, 40f, 0f); // 向上移动的偏移量
    private void Start()
    {
        originalScale = transform.localScale;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("#####mouse press");
        isDragging = true;
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
        cardInfo = GetComponent<Card>();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
  
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("mouse release");
        isDragging = false;
        this.transform.SetParent(parentToReturnTo);     
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == gameObject)
        Debug.Log("mouse in card " + gameObject.name);
        transform.position += hoverOffset;
        //StartCoroutine(EnlargeCard());
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("mouse exit card " + gameObject.name);
        transform.position -= hoverOffset;
        //StartCoroutine(ShrinkCard());
        isHovered = false;
    }

    /*IEnumerator EnlargeCard()
    {       
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale; // 兩倍大的尺寸


        float duration = 0.001f; // 動畫持續時間
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
      
        transform.localScale = targetScale;
        transform.position += hoverOffset;

    }

    IEnumerator ShrinkCard()
    {        
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.one; // 回復成原本的大小



        float duration = 0.001f; // 動畫持續時間
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        transform.position -= hoverOffset;

    } */
    private void Update()
    {
        if (isHovered && !isDragging)
        {
            transform.localScale = enlargedScale;
        }
        else
        {
            transform.localScale = originalScale;
        }
    }


}