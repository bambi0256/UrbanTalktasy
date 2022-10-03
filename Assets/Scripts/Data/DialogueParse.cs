using System;
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
        private const string LINE_SPLIT_RE = "\n|\r\n";

        public Dictionary<int, Dictionary<int, TalkData>> csvData;

        private void Start()
        {
            IdRoutine.GameState = false;
            Parsing(csvFile0, 0);
            /*
            Parsing(csvFile1, 1);
            Parsing(csvFile2, 2);
            Parsing(csvFile3, 3);
            Parsing(csvFile4, 4);
            Parsing(csvFile5, 5);
            */
            IdRoutine.GameState = true;
        }

        private void Parsing(TextAsset textAsset, int Number)
        {
            // 줄 별 분리
            var dataLines = Regex.Split(textAsset.text, LINE_SPLIT_RE);
            Debug.Log("DataLines");

            //각 줄 별 값 분리, num 0은 헤더
            for (var id = 1; id <= dataLines.Length; id++) 
            {
                var values = Regex.Split(dataLines[id], SPLIT_RE);
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

                csvData[Number][id] = entry;

                Debug.Log("Setting End");
            }
        }
    }
}
