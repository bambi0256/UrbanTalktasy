using JetBrains.Annotations;

namespace Struct
{
    public struct TalkData
    { 
        public string name;
        public string context;
        public float Duration;
        public string cmd;
    }

    public struct SaveData
    {
        public int curChap;
        public int curId;
    }
    
    public struct ChooseData
    {
        public SaveData WhereIn;
        public int choose;
    }
}
