using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Data
{
    public class DialogueParse : MonoBehaviour
    {
        public TextAsset csvFile0;
        public TextAsset csvFile1;
        public TextAsset csvFile2;
        public TextAsset csvFile3;
        public TextAsset csvFile4;
        public TextAsset csvFile5;
        
        private const string SPLIT_RE = ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)";
        private const string LINE_SPLIT_RE = "\r\n|\n\r|\n|\r";

        public static Dictionary<TextAsset, Dictionary<int, TalkData>> csvData;

        private void Start()
        {
            Parsing(csvFile0);
            Parsing(csvFile1);
            Parsing(csvFile2);
            Parsing(csvFile3);
            Parsing(csvFile4);
            Parsing(csvFile5);
        }

        private static void Parsing(TextAsset textAsset)
        {
            // 줄 별 분리
            var dataLines = Regex.Split(textAsset.text, LINE_SPLIT_RE);

            //각 줄 별 값 분리
            for (var num = 1; num <= dataLines.Length; num++) 
            {
                var values = Regex.Split(dataLines[num], SPLIT_RE);
                if (values.Length <= 0) continue;
                
                // 줄 별 정보 딕셔너리
                var entry = new TalkData
                {
                    name = values[1],
                    context = values[2],
                    Duration = float.Parse(values[3]),
                    cmd = values[4]
                };
                csvData[textAsset][num] = entry;
            }
        }
    }
}
