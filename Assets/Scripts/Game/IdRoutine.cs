using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Data;
using Struct;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class IdRoutine : MonoBehaviour
    {
        // Regex
        private const string CMD_NAME = @"(^\w+)";
        private const string CMD_FACTOR = @"(?<=,| |\()(\'?\w+)(\'?)";

        // Parsing
        public GameObject DialogueManager;
        private DialogueParse dialogueParse;
        private int curId = 1;
        
        // Id Control
        private bool IdUpFlag;
        
        // Save Component
        private SaveData Auto;
        public SaveData SaveFile;
        
        // Text Effect Control
        public GameObject NextArrow;
        private bool isTextEffect;
        public Text Context;
        public Text Name;
        private const float TextSpeed = 0.07f;

        // Game Event Control
        public static bool GameState;
        public bool AutoMode;
        
        // Routine WaitControl
        
        // Love Point
        public int woo_love;
        public int fox_love;
        public int dob_love;
        
        // in Functions
        public GameObject DarkPanel;
        public GameObject FadePanel;
        public GameObject NameCard;
        public GameObject Box;
        public GameObject MessengerBox;
        public Image BackGround;
        
        public Sprite Box_Default;
        public Sprite Box_Nar;
        
        public Scrollbar Scroll;
        
        public Text MessengerName;
        public AudioSource Audio;

        private void Start()
        {
            dialogueParse = DialogueManager.GetComponent<DialogueParse>();
            PlayerPrefs.DeleteAll();
            GetPref();
            StartCoroutine(VisualNovelRoutine());
        }

        private IEnumerator VisualNovelRoutine()
        {
            // 루틴 시작
            while (curId < dialogueParse.csvData.Count)
            {
                // 게임 중 상태를 체크
                if (GameState == false) yield return new WaitUntil(() => GameState);

                // Flag 초기화
                isTextEffect = true;
                IdUpFlag = true;

                // Parsing 정보 가져오기
                var Now = dialogueParse.csvData[curId];
                // Now[0] = id, [1] = name, [2] = context, [3] = Duration, [4] = CMD

                // cmd 진행
                CallCmd(Now[4]);

                // NameTag Text 변경
                Name.text = Now[1];

                // Text 출력
                if (!isTextEffect) Context.text = Now[2];
                else yield return StartCoroutine(Typing(Context, Now[2], TextSpeed));

                // AutoMode, Duration 만큼 기다리고 다음으로 진행
                // UnAuto, 좌클릭 또는 스페이스 입력을 기다림
                if (float.TryParse(Now[3], out var floatValues))
                {
                    if (AutoMode) yield return new WaitForSecondsRealtime(floatValues);
                    else
                        yield return new WaitUntil(() =>
                            Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0));
                }

                //AutoSave 관리
                Save(false);
                
                // id++;
                if (IdUpFlag)
                {
                    curId++;
                }
            }
        }
        
        // 타이핑 효과
        private IEnumerator Typing(Text typingText, string message, float speed)
        {
            GameState = false;
            NextArrow.SetActive(false);
            for (var i = 0; i < message.Length; i++)
            {
                typingText.text = message.Substring(0, i + 1);
                yield return new WaitForSeconds(speed);
            }
            NextArrow.SetActive(true);
            GameState = true;
        }
        
        // cmd 스플릿 후 호출
        private void CallCmd(string WholeCmd)
        {
            var EachCmd = WholeCmd.Split(';');
            var num = EachCmd.Length;

            for (var i = 0; i < num; i++)
            {
                var F_name = Regex.Match(EachCmd[i], CMD_NAME);
                var factors = Regex.Matches(EachCmd[i], CMD_FACTOR);
                var Function = F_name.ToString();
                var F_factor = SetFactors(factors);
                Debug.Log(Function);
                Execute(Function, F_factor);
            }
        }

        // Factors 구성 함수
        private static object[] SetFactors(MatchCollection factors)
        {
            var output = new object[factors.Count];
            for (var i = 0; i< factors.Count; i++)
            {
                var _stringValue = factors[i].Groups[0].ToString();
                var isInt = int.TryParse(_stringValue, out var _intValue);
                var isFloat = float.TryParse(_stringValue, out var _floatValue);
                if (isFloat)
                {
                    if (isInt) output[i] = _intValue;
                    else output[i] = _floatValue;
                }
                else output[i] = _stringValue;
            }
            return output;
        }

        // Cmd 실행 함수
        private void Execute(string method, object[] factors)
        {
            var Function = typeof(IdRoutine).GetMethod(method);
            if (Function == null) return;
            try
            {
                Function.Invoke(this, factors);
            }
            catch (ArgumentException)
            {
                var infos = Function.GetParameters();
                var newFactors = new object[factors.Length];
                var array = new int[3];
                var i = 0;
                foreach (var t in infos)
                {
                    if (t.ParameterType == typeof(float)) newFactors[i] = float.Parse(factors[i].ToString());
                    else if (t.ParameterType == typeof(string)) newFactors[i] = factors[i].ToString();
                    else if (t.ParameterType == typeof(int)) newFactors[i] = int.Parse(factors[i].ToString());
                    else if (t.ParameterType == typeof(int[]))
                    {
                        for (var j = 0; j < newFactors.Length - i; j++)
                        {
                            array[j] = int.Parse(factors[i].ToString());
                            newFactors[i] = array;
                        }
                        break;
                    }
                    i++;
                }
                Function.Invoke(this, newFactors);
            }
            catch (TargetParameterCountException)
            {
                var infos = Function.GetParameters();
                var newFactors = new object[infos.Length];
                var i = 0;
                foreach (var t in infos)
                {
                    if (t.ParameterType == typeof(float)) newFactors[i] = float.Parse(factors[i].ToString());
                    else if (t.ParameterType == typeof(string)) newFactors[i] = factors[i].ToString();
                    else if (t.ParameterType == typeof(int)) newFactors[i] = int.Parse(factors[i].ToString());
                    else if (t.ParameterType == typeof(int[]))
                    {
                        var array = new int[3];
                        for (var j = 0; j < newFactors.Length - i; j++) array[j] = int.Parse(factors[i + j].ToString());
                        newFactors[i] = array;
                        break;
                    }
                    i++;
                }
                Function.Invoke(this, newFactors);
            }
        }
        
        //AutoSave 함수
        private void Save(bool IsLoveChange)
        {
            Auto.curId = curId;
            if (IsLoveChange)
            {
                Auto.curLove_woo = woo_love;
                Auto.curLove_dob = dob_love;
                Auto.curLove_fox = fox_love;
            }
            Debug.Log("세이브 작동");
            SavePref();
        }

        private void ChooseData(int id, int Choose)
        {
            Auto.ChooseData.Add(id, Choose);
            var Key = "Choose" + id;
            PlayerPrefs.SetInt(Key, Choose);
        }
        
        // PlayerPrefs 접근 함수 : 기본 시작 시에 저장된 커서와 호감도(마지막 Auto)를 가져옴.
        // 만약 처음부터 실행 버튼을 누르면, 해당 기능에서 PlayerPrefs 값들을 초기화할 것.
        private void GetPref()
        {
            if(PlayerPrefs.HasKey("CurId")) curId = PlayerPrefs.GetInt("CurId");
            if(PlayerPrefs.HasKey("CurLove_woo")) woo_love = PlayerPrefs.GetInt("CurLove_woo");
            if(PlayerPrefs.HasKey("CurLove_fox")) fox_love = PlayerPrefs.GetInt("CurLove_fox");
            if(PlayerPrefs.HasKey("CurLove_dob")) dob_love = PlayerPrefs.GetInt("CurLove_dob");

            for (var i = 1; i < curId; i++)
            {
                var Key = "Choose" + i;
                if (PlayerPrefs.HasKey(Key))
                {
                    Auto.ChooseData[i] = PlayerPrefs.GetInt(Key);
                }
            }
        }
        
        // PlayerPrefs 저장 함수 : Auto 변화 시 커서, 호감도를 저장해둠.
        private void SavePref()
        {
            PlayerPrefs.SetInt("CurId", Auto.curId);
            PlayerPrefs.SetInt("CurLove_woo", Auto.curLove_woo);
            PlayerPrefs.SetInt("CurLove_fox", Auto.curLove_fox);
            PlayerPrefs.SetInt("CurLove_dob", Auto.curLove_dob);
        }
        
        // cmd 함수들
        public void Choose(params int[] id)
        {
            // 입력받은 개수 확인
            var Count = id.Length;

            // 개수에 따라 box 활성화 >> 최대 몇 개를 할 것 같은지? Case 쓸지 ScrollBox 쓸지 고민 중
            // 선택지의 내용을 불러와 출력

            // 입력 대기, 입력받으면 Save Dictionary에 저장
            const int Ch = 1;
            ChooseData(curId, Ch);
            
            Debug.Log("Choose 작동");
        }

        public void MoveTo(int id)
        {
            IdUpFlag = false;
            curId = id;
            Debug.Log("MoveTo 작동");
        }

        public void Love(string charName)
        {
            // 호감도 값 변경, Save 함수 작동
            switch (charName)
            {
                case "Woo":
                    woo_love += 1;
                    Save(true);
                    break;
                case "Dob":
                    dob_love += 1;
                    Save(true);
                    break;
                case "Fox":
                    fox_love += 1;
                    Save(true);
                    break;
            }
            Debug.Log("Love 작동");
        }

        public void CharSprite(params int[] input)
        {
            var Count = input[0];
            if (int.TryParse(Count.ToString(), out var intResult))

            // 개수에 맞게 패널 On/Off, 
            switch (Count)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
            
            // Sprite 변경
            for (var i = 0; i < intResult; i++)
            {
                
            }
            Debug.Log("CharSprite 작동");
        }

        public void SetBackSprite(int Number)
        {
            // Sprite 변경
            var Back = Resources.LoadAll<Sprite>("Background");
            BackGround.sprite = Back[Number];
            Debug.Log("SetBackSprite 작동");
        }

        public void SetBGM(int Number)
        {
            // BGM Audio 변경
            var BGM = Resources.LoadAll<AudioClip>("BGM");
            Audio.clip = BGM[Number];
            Debug.Log("SetBGM 작동");
        }

        public void SetEffect(int Number)
        {
            // Effect Audio 변경
            var Effect = Resources.LoadAll<AudioClip>("Effect");
            Audio.clip = Effect[Number];
            Debug.Log("SetEffect 작동");
        }

        public void DarkPanelActiveOn()
        {
            DarkPanel.SetActive(true);
            Debug.Log("DarkPanelActiveOn 작동");
        }

        public void DarkPanelActiveOff()
        {
            DarkPanel.SetActive(false);
            Debug.Log("DarkPanelActiveOff 작동");
        }

        public void FadeIn()
        {
            // Fade In
            FadePanel.SetActive(true);
            StartCoroutine(FadeInCoroutine());
            Debug.Log("Fade In 작동");
        }

        private IEnumerator FadeInCoroutine()
        {
            var image = FadePanel.GetComponent<Image>();
            float Count = 0;
            while (Count < 1.0f)
            {
                Count += 0.01f;
                yield return new WaitForSeconds(0.01f);
                image.color = new Color(0, 0, 0, Count);
            }
        }

        public void FadeOut()
        {
            // Fade Out
            StartCoroutine(FadeOutCoroutine());
            FadePanel.SetActive(false);
            Debug.Log("Fade Out 작동");
        }
        
        private IEnumerator FadeOutCoroutine()
        {
            var image = FadePanel.GetComponent<Image>();
            var Count = 1.0f;
            while (Count > 0)
            {
                Count -= 0.01f;
                yield return new WaitForSeconds(0.01f);
                image.color = new Color(0, 0, 0, Count);
            }
        }

        public void NameCardOn()
        {
            NameCard.SetActive(true);
            Debug.Log("NameCardOn 작동");
        }

        public void NameCardOff()
        {
            NameCard.SetActive(false);
            Debug.Log("NameCardOff 작동");
        }

        public void BoxOff()
        {
            Box.SetActive(false);
            Debug.Log("BoxOff 작동");
        }

        public void BoxOn()
        {
            Box.SetActive(true);
            Debug.Log("BoxOn 작동");
        }

        public void SetBoxNar()
        {
            Box.GetComponent<Image>().sprite = Box_Nar;
            Debug.Log("SetBoxNar 작동");
        }

        public void SetBoxDefault()
        {
            Box.GetComponent<Image>().sprite = Box_Default;
            Debug.Log("SetBoxDefault 작동");
        }

        public void ExtraSpriteOn(string fileName)
        {
            // PopUp Active True
            // PopUp Sprite를 fileName Sprite로 변경
            Debug.Log("ExtraSpriteOn 작동");
        }

        public void ExtraSpriteOff()
        {
            // Sprite 초기화
            // PopUp Active False
            Debug.Log("ExtraSpriteOff 작동");
        }

        public void ShakeSprite()
        {
            // Animation 적용하기
            Debug.Log("ShakeSprite 작동");
        }

        public void SetScroll(int chapter)
        {
            Scroll.value = 0;
            // chapter id Length로 MaxValue 변경
            Debug.Log("SetScroll 작동");
        }

        public void StartMessenger(string charName)
        {
            MessengerBox.SetActive(true);
            MessengerName.text = charName;
            Debug.Log("StartMessenger 작동");
        }

        public void MessengerLeft()
        {
            //콘텐츠 추가, 좌 정렬
            Debug.Log("MessengerLeft 작동");
        }

        public void MessengerRight()
        {
            //콘텐츠 추가, 우 정렬
            Debug.Log("MessengerRight 작동");
        }

        public void MessengerExit()
        {
            MessengerBox.SetActive(false);
            Debug.Log("MessengerExit 작동");
        }

        public void SkipTo()
        {
            isTextEffect = false;
            Debug.Log("SkipTo 작동");
        }

        public void LoadChoose(int id, params int[] ids)
        {
            // AutoSave 내에 있는 선택지 Dictionary를 읽어오기
            Debug.Log("LoadChoose 작동");
        }
    }
}
