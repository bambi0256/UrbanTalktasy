using System.Collections.Generic;
using System.Text.RegularExpressions;
using Game;
using Struct;
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

        public static Dictionary<int, Dictionary<int, TalkData>> csvData;

        private void Start()
        {
            Parsing(csvFile0);
            Parsing(csvFile1);
            Parsing(csvFile2);
            Parsing(csvFile3);
            Parsing(csvFile4);
            Parsing(csvFile5);
            IdRoutine.GameState = false;
        }

        private void Parsing(TextAsset textAsset)
        {
            // 줄 별 분리
            var dataLines = Regex.Split(textAsset.text, LINE_SPLIT_RE);
            Debug.Log("DataLines");

            //각 줄 별 값 분리, num 0은 헤더
            for (var num = 1; num <= dataLines.Length; num++) 
            {
                var values = Regex.Split(dataLines[num], SPLIT_RE);
                if (values.Length <= 0) continue;
                Debug.Log("values");

                // 줄 별 정보 딕셔너리
                var entry = new TalkData
                {
                    name = values[1],
                    context = values[2],
                    Duration = values[3],
                    cmd = values[4]
                };
                Debug.Log(values[1]);
                Debug.Log(values[2]);
                Debug.Log(values[3]);
                Debug.Log(values[4]);
                Debug.Log("entry");

                switch (textAsset)
                {
                    case var value when value == csvFile0:
                        csvData[0][num] = entry;
                        break;
                    case var value when value == csvFile1:
                        csvData[1][num] = entry;
                        break;
                    case var value when value == csvFile2:
                        csvData[2][num] = entry;
                        break;
                    case var value when value == csvFile3:
                        csvData[3][num] = entry;
                        break;
                    case var value when value == csvFile4:
                        csvData[4][num] = entry;
                        break;
                    case var value when value == csvFile5:
                        csvData[5][num] = entry;
                        break;
                }
                Debug.Log("End");
            }
        }
    }
}
