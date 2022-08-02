using System.Collections.Generic;
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

        public static Dictionary<TextAsset, Dictionary<int, TalkData>> DialogueDictionary =
            new Dictionary<TextAsset, Dictionary<int, TalkData>>();
        
        private void Parsing(TextAsset textAsset)
        {
            // 아래 한 줄 빼기
            var csvText = textAsset.text.Substring(0, textAsset.text.Length - 1);
            // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
            var rows = csvText.Split(new char[] { '\n' });
            //1부터 함 > Header 무시 + 열 번호로 체크
            for (var i = 1; i < rows.Length; i++) 
            {
                // 열 별로 구분
                var rowValues = rows[i].Split(new char[] { ',' });
                // 구조체 정보 저장
                TalkData talkData;
                talkData.name = rowValues[1];
                talkData.context = rowValues[2];
                talkData.Duration = float.Parse(rowValues[3]);
                talkData.cmd = rowValues[4];
                DialogueDictionary[textAsset][i] = talkData;
            } // for 문 끝나는 중괄호
        }
        
        public TalkData GetDialogue(TextAsset textAsset, int id)
        {
            Parsing(textAsset);
            return DialogueDictionary[textAsset][id];
        }
    }
}
