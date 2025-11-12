using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class shopmanager : MonoBehaviour
{
    public TextMeshProUGUI show;
    public Slider slider;
    public AudioSource bgm;
    public GameObject setpanel;
    public bagpool pool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        show.text = "value : " + data.val;
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

    }

    public void nextscene()
    {
        SceneManager.LoadScene("Game");
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

    public void AddItem(string item)
    {
        data.ingreds_data isexist = data.inbag.Find(x => x.name == item);
        if (isexist != null)
        {
            isexist.quantity++;
            GameObject numup = pool.pool.Find(obj => obj.GetComponent<ingredient>().thename == item);
            if (numup != null)
                numup.GetComponent<ingredient>().updatenum(isexist.quantity);
        }
        else
        {
            data.inbag.Add(new data.ingreds_data(item));

            GameObject uiObj = pool.GetObject();
            uiObj.GetComponent<ingredient>().setingredient(item, 1);
        }
    }
}
