using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Game;
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
        private const string LINE_SPLIT_RE = "\n|\r\n";
        private const char TRIM_CHARS = '\"';

        public Dictionary<int, List<string>> csvData = new Dictionary<int, List<string>>();
        
        private void Start()
        {
            IdRoutine.GameState = false;
            csvData = Parsing(csvFile0, 0);
            /*
            csvData += Parsing(csvFile1, 100);
            csvData += Parsing(csvFile2, 200);
            csvData += Parsing(csvFile3, 300);
            csvData += Parsing(csvFile4, 400);
            csvData += Parsing(csvFile5, 500);
            */
            IdRoutine.GameState = true;
        }

        private static Dictionary<int, List<string>> Parsing(TextAsset textAsset, int StartNum)
        {
            var output = new Dictionary<int, List<string>>();

            // 줄 별 분리
            var dataLines = Regex.Split(textAsset.text, LINE_SPLIT_RE);

            //각 줄 별 값 분리, num 0은 헤더
            for (var id = 1; id < dataLines.Length; id++)
            {
                var values = Regex.Split(dataLines[id], SPLIT_RE);
                if (values.Length == 0) continue;

                //Data Trimming
                for (var i = 0; i < values.Length; i++)
                {
                    var value = values[i];
                    value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                    values[i] = value;
                }

                var entry = values.ToList();
                output[id + StartNum] = entry;
            }
            return output;
        }
    }
}
