using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class RecipeBookManualUI : MonoBehaviour
{
    [Header("Detail (After Click)")]
    public GameObject detailRoot;      // 詳情模式總容器（可選，但推薦拖）
    public Image leftResultImage;      // 左頁大圖
    public Button backButton;          // 返回按鈕（要拖）

    [Header("Fade (List Mode)")]
    public CanvasGroup listGroup;      // 放料理卡片按鈕+Close 的那坨
    public float fadeDuration = 0.35f;

    [Serializable]
    public class Entry
    {
        public string recipeName;
        public Sprite resultSprite;
        public GameObject rightPage;   // 右頁對應的配方 panel（你已經手排好的）
    }

    [Header("Manual Mapping")]
    public Entry[] entries;

    Coroutine fadeCo;

    void Start()
    {
        // 返回按鈕事件（不想寫在 Inspector 也沒關係）
        if (backButton != null)
        {
            backButton.onClick.RemoveAllListeners();
            backButton.onClick.AddListener(BackToList);
        }

        ResetView();
    }

    // =========================
    // List Mode（初始/返回後）
    // =========================
    public void ResetView()
    {
        // 關掉詳情整坨（大圖/材料/返回）
        if (detailRoot != null) detailRoot.SetActive(false);

        // 左頁大圖藏起來（保險）
        if (leftResultImage != null)
            leftResultImage.gameObject.SetActive(false);

        // 右頁材料全部關
        if (entries != null)
        {
            foreach (var e in entries)
                if (e != null && e.rightPage != null)
                    e.rightPage.SetActive(false);
        }

        // 列表顯示 + 可互動
        if (listGroup != null)
        {
            listGroup.gameObject.SetActive(true);
            listGroup.alpha = 1f;
            listGroup.interactable = true;
            listGroup.blocksRaycasts = true;
        }
    }

    public void BackToList()
    {
        // 關掉詳情
        if (detailRoot != null) detailRoot.SetActive(false);

        // 右頁材料全關
        if (entries != null)
        {
            foreach (var e in entries)
                if (e != null && e.rightPage != null)
                    e.rightPage.SetActive(false);
        }

        // 左頁圖藏
        if (leftResultImage != null)
            leftResultImage.gameObject.SetActive(false);

        // 列表淡入回來
        FadeList(show: true);
    }

    // =========================
    // Detail Mode（點料理後）
    // =========================
    public void Show(string recipeName)
    {
        // 找到對應 entry
        Entry target = null;
        foreach (var e in entries)
        {
            if (e == null) continue;
            if (e.recipeName == recipeName) { target = e; break; }
        }
        if (target == null)
        {
            Debug.LogWarning("Recipe not mapped: " + recipeName);
            return;
        }

        // 右頁材料先全關，再開目標
        foreach (var e in entries)
            if (e != null && e.rightPage != null)
                e.rightPage.SetActive(false);

        if (target.rightPage != null)
            target.rightPage.SetActive(true);

        // 左頁大圖顯示
        if (leftResultImage != null)
        {
            leftResultImage.gameObject.SetActive(true);
            leftResultImage.sprite = target.resultSprite;
            leftResultImage.preserveAspect = true;
        }

        // 打開詳情容器（包含返回按鈕/大圖/右頁文字）
        if (detailRoot != null)
            detailRoot.SetActive(true);
        Debug.Log("DetailRoot active = " + detailRoot.activeSelf);


        // 列表淡出 + 不能按
        FadeList(show: false);
    }

    // =========================
    // Fade helper
    // =========================
    void FadeList(bool show)
    {
        if (listGroup == null) return;

        if (fadeCo != null) StopCoroutine(fadeCo);
        fadeCo = StartCoroutine(FadeCanvasGroup(listGroup, show ? 1f : 0f, fadeDuration, disableWhenZero: !show));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup g, float target, float dur, bool disableWhenZero)
    {
        // 互動開關
        if (target <= 0f)
        {
            g.interactable = false;
            g.blocksRaycasts = false;
        }
        else
        {
            g.gameObject.SetActive(true);
            g.interactable = true;
            g.blocksRaycasts = true;
        }

        float start = g.alpha;
        float t = 0f;
        while (t < dur)
        {
            t += Time.unscaledDeltaTime;
            float p = (dur <= 0.0001f) ? 1f : Mathf.Clamp01(t / dur);
            g.alpha = Mathf.Lerp(start, target, p);
            yield return null;
        }

        g.alpha = target;

        if (disableWhenZero && Mathf.Approximately(target, 0f))
            g.gameObject.SetActive(false);
    }
}