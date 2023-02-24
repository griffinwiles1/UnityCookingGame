using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilsClass {
    public const int SORTING_ORDER_DEFAULT = 5000;

    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = SORTING_ORDER_DEFAULT) {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.Rotate(90f, 0f, 0f, Space.Self);
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    public static Vector3 GetMouseWorldPosition() {
        return GetMouseWorldPosition(Input.mousePosition, Camera.main);
    }

    public static Vector3 GetMouseWorldPosition(Camera worldCamera) {
        return GetMouseWorldPosition(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera worldCamera) {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static Vector3 GetMouseWorldPositionNoY() {
        Vector3 vec = GetMouseWorldPosition(Input.mousePosition, Camera.main);
        vec.y = 0f;
        return vec;
    }
}
