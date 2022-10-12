using JetBrains.Annotations;

namespace Struct
{
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
