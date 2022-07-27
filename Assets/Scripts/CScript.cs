using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TalkData
{
    public int id;
    public int type;
    public string name;
    public string context;
    public string CharSprite;
    public string BackSprite;
    public string BackSound;
    public string EffectSound;
    public string EffectSprite;
    public float Duration;
    public string Choose1;
    public int Choose1_id;
    public string Choose2;
    public int Choose2_id;
    public int FoxLove;
    public int DobLove;
    public int WooLove;
}

public class CScript
{
    [SerializeField] int id;
    [SerializeField] TalkData[] talkDatas;
}

