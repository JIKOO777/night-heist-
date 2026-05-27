using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupScale : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private float popDuration = 0.25f;
    [SerializeField] private float overshoot = 1.08f;

    [Header("Victory Flow")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private float waitBeforeNextScene = 2f;
    [SerializeField] public string nextSceneName = "2_level";

    private void OnEnable()
    {
        StopAllCoroutines();
        transform.localScale = Vector3.zero;
        StartCoroutine(Pop());
    }

    private IEnumerator Pop()
    {
        float t = 0f;

        // grow to overshoot
        while (t < popDuration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Clamp01(t / popDuration);
            float s = Mathf.SmoothStep(0f, overshoot, a);
            transform.localScale = Vector3.one * s;
            yield return null;
        }

        // settle to 1
        t = 0f;
        while (t < popDuration * 0.6f)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Clamp01(t / (popDuration * 0.6f));
            float s = Mathf.SmoothStep(overshoot, 1f, a);
            transform.localScale = Vector3.one * s;
            yield return null;
        }

        transform.localScale = Vector3.one;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        // ⏳ ЖДЕМ И ПЕРЕХОДИМ НА СЛЕДУЮЩУЮ СЦЕНУ
        yield return new WaitForSecondsRealtime(waitBeforeNextScene);

        SceneManager.LoadScene(nextSceneName);
    }
}