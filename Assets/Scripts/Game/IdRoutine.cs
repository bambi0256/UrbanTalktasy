using System;
using System.Collections;
using System.Text.RegularExpressions;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class IdRoutine : MonoBehaviour
    {
        // Regex
        private const string CMD_NAME = @"(^\w+)";
        private const string CMD_FACTOR = @"(?<=,|\()(\""""?\w+)(\""""?)";

        // Parsing
        private int curChap;
        private int curId;
        
        // Id Control
        private bool IdUpFlag;
        
        // Text Effect Control
        private bool isTextEffect;
        public Text Context;
        public Text Name;
        private float TextSpeed = 0.1f;

        // Game Event Control
        public static bool GameState;
        public bool AutoMode;
        
        // Routine WaitControl
        
        // in Functions
        private int _id;
        public GameObject DarkPanel;
        public GameObject NameCard;
        public GameObject Box;
        public Sprite Box_Default;
        public Sprite Box_Nar;
        public Scrollbar Scroll;
        public GameObject MessengerBox;
        public Text MessengerName;
        
        private void Start()
        {
            StartCoroutine(VisualNovelRoutine());
        }

        private IEnumerator VisualNovelRoutine()
        {
            // 세이브된 내용 기반으로 Cursor 설정
            if (!GameManager.Instance.ThereAnySave)
            {
                curChap = 0;
                curId = 1;
            }
            else
            {
                curChap = GameManager.Instance.SaveData.curChap;
                curId = GameManager.Instance.SaveData.curId;
            }

            // 루틴 시작
            while (curId < DialogueParse.csvData[curChap].Count)
            {
                // 게임 중 상태를 체크
                if (GameState == false) yield return new WaitUntil(() => GameState);
                
                // Flag 초기화
                isTextEffect = true;
                IdUpFlag = true;
                
                // Parsing 정보 가져오기
                var Now = DialogueParse.csvData[curChap][curId];

                // cmd 진행
                var cmdData = Now.cmd;
                CallCmd(cmdData);

                // NameTag Text 변경
                Name.text = Now.name;

                // Text 출력
                if (!isTextEffect) Context.text = Now.context;
                else StartCoroutine(Typing(Context, Now.context, TextSpeed));
                
                // AutoMode, Duration 만큼 기다리고 다음으로 진행
                if (float.TryParse(Now.Duration, out var floatValues))
                {
                    if (AutoMode) yield return new WaitForSecondsRealtime(floatValues);
                }
                // UnAuto, 좌클릭 또는 스페이스 입력을 기다림
                else yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0));

                //AutoSave 관리
                GameManager.Instance.SaveData.curChap = curChap;
                GameManager.Instance.SaveData.curId = curId;
                
                // id++;
                if (IdUpFlag)
                {
                    curId++;
                }
            }
        }
        
        // 타이핑 효과
        private static IEnumerator Typing(Text typingText, string message, float speed)
        {
            for (var i = 0; i < message.Length; i++)
            {
                typingText.text = message.Substring(0, i + 1);
                yield return new WaitForSeconds(speed);
            }
        }
        
        // cmd 스플릿
        private void CallCmd(string WholeCmd)
        {
            var EachCmd = WholeCmd.Split(';');
            var num = EachCmd.Length;

            for (var i = 0; i < num; i++)
            {
                string F_name = null;
                F_name += Regex.Split(EachCmd[i], CMD_NAME);
                var factors = Regex.Split(EachCmd[i], CMD_FACTOR);
                var F_factor = SetFactors(factors);
                Execute(F_name, F_factor);
            }
        }

        // Factors 구성 함수
        private static object[] SetFactors(string[] factors)
        {
            object[] _factors = null;
            for (var i = 0; i < factors.Length; ++i)
            {
                var _stringValue = factors[i];
                var isInt = int.TryParse(_stringValue, out var _intValue);
                var isFloat = float.TryParse(_stringValue, out var _floatValue);
                var isString = isFloat == false && isInt == false;
                if (isString)
                {
                    _factors![i] = _stringValue;
                }
                else if (isFloat)
                {
                    _factors![i] = _floatValue;
                }
                else
                {
                    _factors![i] = _intValue;
                }
            }
            return _factors;
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
            catch (Exception)
            {
                var infos = Function.GetParameters();
                foreach (var t in infos)
                {
                    var newFactors = new object[factors.Length];
                    if (t.ParameterType == typeof(float[]))
                    {
                        for (var i = 0; i < newFactors.Length; ++i) newFactors[i] = (float)factors[i];
                        continue;
                    }
                    if (t.ParameterType == typeof(string)) 
                    {
                        for (var i = 0; i < newFactors.Length; ++i) newFactors[i] = factors[i];
                        continue;
                    }
                    if (t.ParameterType == typeof(int))
                    {
                        for (var i = 0; i < newFactors.Length; ++i) newFactors[i] = (int)factors[i];
                        continue;
                    }
                    Function.Invoke(this, new object[] { newFactors });
                }
            }
        }
        
        // cmd 함수들
        public void Choose(params int[] id)
        {
            // 입력받은 개수 확인
            // 개수에 따라 box 활성화
            // 선택지의 내용을 불러와 출력
            // 입력 대기, 입력받으면 MoveTo() 작동
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
            // GameManager의 InGame Love 값 증가
            Debug.Log("Love 작동");
        }

        public void CharSprite(params string[] sprite)
        {
            // Sprite 개수 확인
            // 개수에 맞는 패널 활성화
            // Sprite 변경
            Debug.Log("CharSprite 작동");
        }

        public void SetBackSprite(string sprite)
        {
            // Sprite 변경
            Debug.Log("SetBackSprite 작동");
        }

        public void SetBGM(string BGM)
        {
            // BGM Audio 변경
            Debug.Log("SetBGM 작동");
        }

        public void SetEffect(string Effect)
        {
            // Effect Audio 변경
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
            Debug.Log("Fade In 작동");
        }

        public void FadeOut()
        {
            // Fade Out
            Debug.Log("Fade Out 작동");
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
            Box.GetComponent<SpriteRenderer>().sprite = Box_Nar;
            Debug.Log("SetBoxNar 작동");
        }

        public void SetBoxDefault()
        {
            Box.GetComponent<SpriteRenderer>().sprite = Box_Default;
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

        public void LoadChoose(int chap, int id, params int[] ids)
        {
            //chap, id 의 선택 정보 가져오기 => myChoose
            //MoveTo(ids[myChoose]);
            Debug.Log("LoadChoose 작동");
        }
    }
}
