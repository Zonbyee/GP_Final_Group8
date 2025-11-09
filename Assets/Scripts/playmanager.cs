using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playmanager : MonoBehaviour
{

    public Slider slider;
    public AudioSource bgm;
    public GameObject setpanel;
    public GameObject blendpanel;
    public bagpool pool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = data.bgmvol;
        bgm.volume = data.bgmvol;
        slider.onValueChanged.AddListener(setvolume);

        foreach (var remains in data.inbag)
        {
            GameObject uiObj = pool.GetObject();
            uiObj.GetComponent<ingredient>().setingredient(remains.name, remains.quantity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("finishcounting");
        }
    }

    public void opensetting()
    {
        setpanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void closesetting()
    {
        setpanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void setvolume(float value)
    {
        bgm.volume = value;
        data.bgmvol = value;
    }

    public void openblendpanel()
    {
        blendpanel.SetActive(true);
    }

    public void closeblendpanel()
    {
        blendpanel.SetActive(false);
    }
}
