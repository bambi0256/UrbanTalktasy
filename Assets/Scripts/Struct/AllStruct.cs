using System.Collections.Generic;

namespace Struct
{
    public struct SaveData
    {
        public int curId;
        public int curLove_woo;
        public int curLove_fox;
        public int curLove_dob;
        public Dictionary<int, int> ChooseData; // <Id, Choose> 데이터
    }
}
