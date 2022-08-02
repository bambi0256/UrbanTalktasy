using System.Globalization;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Functions : MonoBehaviour
    {
        public Text Sentence;
        public Text Name;
        
        private void ReadAll(TextAsset textAsset)
        {
            var dialogue = DialogueParse.DialogueDictionary[textAsset];
            for (var i = 0; i < dialogue.Count; i++)
            {
                Sentence.text = dialogue[i].context;
                Name.text = dialogue[i].name;
                Texing(Sentence);
                MakeStringFunc(dialogue[i].cmd);
            }
        }

        private void Texing(Text _text)
        {
            
        }

        private void MakeStringFunc(string cmd)
        {
            
        }
    }
}
