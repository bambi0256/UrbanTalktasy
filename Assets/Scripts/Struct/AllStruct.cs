using JetBrains.Annotations;

namespace Struct
{
    public struct TalkData
    {
        //[CanBeNull]
        public string name;
        //[CanBeNull]
        public string context;
        //[CanBeNull]
        public string Duration;
        //[CanBeNull]
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
