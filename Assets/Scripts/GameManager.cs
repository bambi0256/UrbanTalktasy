using System.Collections.Generic;
using Struct;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //호감도 수치 저장
    public static float fox_love;
    public static float dob_love;
    public static float woo_love;
    
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
}