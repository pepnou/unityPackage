using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Canvas))]
public class MyCursor : MonoBehaviour
{
    private static MyCursor instance;

    [SerializeField] private Sprite defaultCursor;
    [SerializeField] private Vector2 defaultCursorHotspot;

    [SerializeField] private RectTransform cursor;
    private Image cursorImage;

    private Canvas canvas;

    private Vector2 hotspot;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        canvas = GetComponent<Canvas>();
        cursorImage = cursor.GetComponent<Image>();

        cursorImage.sprite = defaultCursor;

        Cursor.visible = false;
    }
    private void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), mousePos, Camera.main, out Vector2 mousePos);
        //cursor.anchoredPosition = mousePos /* + hotspot*/;

        //cursor.position = Camera.main.ScreenToWorldPoint(mousePos);
        cursor.position = RectTransformUtility.WorldToScreenPoint(Camera.main, Camera.main.ScreenToWorldPoint(mousePos)) + hotspot;
    }

    private void SetCursor_internal(Sprite sprite, Vector2 hotspot)
    {
        if (sprite != null)
        {
            cursorImage.sprite = sprite;
            this.hotspot = hotspot;
        }
        else
        {
            cursorImage.sprite = defaultCursor;
            this.hotspot = defaultCursorHotspot;
        }

        cursorImage.preserveAspect = true;
        cursorImage.SetNativeSize();

    }
    static public void SetCursor(Sprite sprite, Vector2 hotspot)
    {
        instance.SetCursor_internal(sprite, hotspot);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        Cursor.visible = !hasFocus;
    }
}
