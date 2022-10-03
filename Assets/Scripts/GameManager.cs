using System.Collections.Generic;
using Struct;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //호감도 수치 저장
    public float fox_level;
    public float dob_level;
    public float woo_level;
    
    //스크립트 Save
    public bool ThereAnySave;
    public SaveData SaveData;

    //선택지 Save
    public List<ChooseData> ChooseData;
    

    //이전 씬 저장
    public string PrevScene;

    //인풋 이벤트
    private void Update()
    {
        // 스페이스 입력 시
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
        
        // 마우스 클릭 시 = 클릭받음 상태만 인지, 각 클릭은 해당 패널에서 KeyDown으로 작동시킬 것임.
        if (Input.GetMouseButtonDown(0))
        {
            
        }
        
    }

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
    
    //PlayerPref 정보교환
    private void Start()
    {
        GetPref();
    }

    private void GetPref()
    {
        if (!PlayerPrefs.HasKey("CurChap") || !PlayerPrefs.HasKey("CurId"))
        {
            ThereAnySave = false;
            return;
        }
        SaveData.curChap = PlayerPrefs.GetInt("CurChap");
        SaveData.curId = PlayerPrefs.GetInt("CurId");
        ThereAnySave = true;
    }

    public void SavePref(int chap, int id)
    {
        if (chap != SaveData.curChap)
        {
            SaveData.curChap = chap;
            PlayerPrefs.SetInt("CurChap", chap);
        }

        if (id == SaveData.curId) return;
        SaveData.curId = id;
        PlayerPrefs.SetInt("CurId", id);
    }
}