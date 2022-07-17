using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnOnclick : MonoBehaviour
{
    public GameObject Info1;
    public GameObject Info2;
    public GameObject Info3;
    public GameObject Esc;

    private bool BoolPopup;

    private void Awake()
    {
        BoolPopup = false;
    }

    public void SetInfo_1()
    {
        Info1.SetActive(true);
        Info2.SetActive(false);
        Info3.SetActive(false);
        BoolPopup = true;
    }

    public void SetInfo_2()
    {
        Info2.SetActive(true);
        Info1.SetActive(false);
        Info3.SetActive(false);
        BoolPopup = true;
    }

    public void SetInfo_3()
    {
        Info3.SetActive(true);
        Info1.SetActive(false);
        Info2.SetActive(false);
        BoolPopup = true;
    }

    public void SetSaveLoad()
    {
        GameManager.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("SaveLoad");
    }
    
    public void SetDic()
    {
        GameManager.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Dictionary");
    }

    public void SetSelect()
    {
        GameManager.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Select Scene");
    }

    public void SetFriend()
    {
        GameManager.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Friends");
    }

    public void SetStart()
    {
        GameManager.Instance.PrevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Start");
    }

    public void SetBack()
    {
        Debug.Log(GameManager.Instance.PrevScene);
        SceneManager.LoadScene(GameManager.Instance.PrevScene);
    }

    public void YesOutGame()
    {
        Application.Quit();
    }

    public void NoOutGame()
    {
        Esc.SetActive(false);
        BoolPopup = false;
    }
    
    public void Update()
    {
        if (!Input.GetKeyDown("escape")) return;
        if (!BoolPopup)
        {
            Esc.SetActive(!Esc.activeSelf);
            BoolPopup = true;
        }
        else
        {
            GameObject.FindWithTag("PopUp").gameObject.SetActive(false);
            BoolPopup = false;
        }
    }
}
