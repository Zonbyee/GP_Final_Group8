using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private float timer = 60f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI moneyText;
    void Start()
    {
        if (moneyText != null)
        {
            moneyText.text = "$ " + data.money;  // <-- 開始時顯示
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timer).ToString() + "s";
        }

        if (moneyText != null)
        {
            moneyText.text = "$ " + data.money;   // <-- 持續顯示金錢
        }

        if (timer <= 0f)
        {
            SceneManager.LoadScene("Counting");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Counting");
        }
    }
}
