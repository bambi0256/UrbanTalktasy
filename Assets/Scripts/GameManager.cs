using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //호감도 레벨 저장
    public float fox_level;
    public float dok_level;
    public float woo_level;
    
    //이전 씬 저장
    public string PrevScene;

    
    //파괴불가
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject); return;
            
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}