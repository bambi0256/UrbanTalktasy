using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //호감도 수치 저장
    public float fox_level;
    public float dob_level;
    public float woo_level;
    
    //스크립트 Save
    public Dictionary<int, CScript> Save;

    //

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