using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Data
{
    [System.Serializable]
    public struct TalkData
    {
        public string name;
        public string context;
        public float Duration;
        public string cmd;
    }

    public class CScript
    {
        [SerializeField] static int id;
        [SerializeField] static TalkData talkDatas;
    }
}