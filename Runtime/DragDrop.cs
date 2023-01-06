using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]
public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{

    [SerializeField] private Canvas canvas;
    [SerializeField] private float followSpeed = 1f;
    [SerializeField] private RectTransform swap;

    private Transform initialParent;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private bool dragable = true;
    private bool isDraging = false;

    private Vector2 offset;

    private RectTransform target = null;
    private Vector2 targetPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        initialParent = transform.parent;
    }

    private void Update()
    {
        if (!isDraging)
            return;

        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * followSpeed);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!dragable)
            return;

        isDraging = true;
        transform.SetParent(swap, true);
        canvasGroup.alpha = 0.60f;
        canvasGroup.blocksRaycasts = false;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Mouse.current.position.ReadValue(), Camera.main, out offset);
        offset = rectTransform.anchoredPosition - offset;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!dragable)
            return;

        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        if(target == null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Mouse.current.position.ReadValue(), Camera.main, out targetPosition);
            targetPosition += offset;
        } else
        {
            Vector2 tmp =  RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), tmp, Camera.main, out targetPosition);
        }

        //rectTransform.anchoredPosition = targetPosition; 
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        target = null;
        isDraging = false;
        transform.SetParent(initialParent, true);
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = Vector2.zero;
        
        if (!dragable)
            return;

        canvasGroup.alpha = 1f;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void SetDragable(bool dragable)
    {
        this.dragable = dragable;
    }

    public bool GetIsDraging()
    {
        return isDraging;
    }

    public void SetTarget(RectTransform t)
    {
        target = t;
    }

    public void UnsetTarget(RectTransform t)
    {
        if (target == t)
            target = null;
    }
}
