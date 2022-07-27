using System.Collections.Generic;
using UnityEngine;

public class DialogueParse : MonoBehaviour
{
    public TextAsset tsvFile;
    
    private static Dictionary<int, TalkData[]> DialogueDictionary = 
        new Dictionary<int, TalkData[]>();
                    
    public static TalkData[] GetDialogue(int id)
    {
        return DialogueDictionary[id];
    }

    private void Start()
    {
        // 아래 한 줄 빼기
        var tsvText = tsvFile.text.Substring(0, tsvFile.text.Length - 1);
        // 줄바꿈(한 줄)을 기준으로 csv 파일을 쪼개서 string배열에 줄 순서대로 담음
        var rows = tsvText.Split(new char[] { '\n' });
        //1부터 함 > Header 무시
        for (var i = 1; i < rows.Length; i++) 
        {
            // 열 별로 구분
            var rowValues = rows[i].Split(new char[] { '\t' });
            // 구조체 정보 저장
            TalkData talkData; 
            talkData.type = int.Parse(rowValues[1].Trim());
            talkData.name = rowValues[2];
            talkData.context = rowValues[3];
            talkData.CharSprite = rowValues[4];
            talkData.BackSprite = rowValues[5];
            talkData.BackSound = rowValues[6];
            talkData.EffectSound = rowValues[7];
            talkData.EffectSprite = rowValues[8];
            talkData.Duration = float.Parse(rowValues[9]);
            talkData.Choose1 = rowValues[10]; 
            talkData.Choose1_id = int.Parse(rowValues[11]);
            talkData.Choose2 = rowValues[12]; 
            talkData.Choose2_id = int.Parse(rowValues[13]);
            talkData.FoxLove = int.Parse(rowValues[14]);
            talkData.DobLove = int.Parse(rowValues[15]);
            talkData.WooLove = int.Parse(rowValues[16]);
            
            
        } // for 문 끝나는 중괄호
    }
}
