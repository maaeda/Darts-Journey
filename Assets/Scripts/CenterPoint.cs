using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Sprite crosshairSprite; // クロスバー用のスプライト

    void Start()
    {
        // キャンバスの作成
        GameObject canvasObject = new GameObject("CrosshairCanvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        // クロスバーイメージの作成
        GameObject crosshairObject = new GameObject("Crosshair");
        crosshairObject.transform.SetParent(canvas.transform);
        Image crosshairImage = crosshairObject.AddComponent<Image>();
        crosshairImage.sprite = crosshairSprite;

        // クロスバーのサイズと位置を設定
        RectTransform rectTransform = crosshairObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(10, 10); // クロスバーのサイズ
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero; // 画面の中央に配置
    }
}