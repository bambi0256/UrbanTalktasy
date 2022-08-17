using System.Collections;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class IdRoutine : MonoBehaviour
    {
        private static TextAsset curChap;
        private static int curId;
        
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


        private void LoadSave()
        {
            
        }
        
        private IEnumerator VisualNovelRoutine()
        {
            yield return new WaitForEndOfFrame();
            
            // curChap, Id 설정, Save가 없을 경우 
            LoadSave();

            while (curId < DialogueParse.csvData[curChap].Count)
            {
                // Parsing 정보 가져오기
                
                // cmd 진행
                
                // 루틴 중지 지점, 복귀 지점 1
                
                // Text 출력
                
                // 루틴 복귀 지점 2
                
                // id++;
            }
        }
        
        
        
        // cmd 함수들
        public void Choose(params int[] id)
        {
            // 입력받은 개수 확인
            // 개수에 따라 box 활성화
            // 선택지의 내용을 불러와 출력
            // 입력 대기, 입력받으면 MoveTo() 작동
        }

        public void MoveTo(int id)
        {
            // CurId 를 (id - 1)로 변경
        }

        public void Love(string charName)
        {
            // GameManager의 InGame Love 값 증가
        }

        public void CharSprite(params string[] sprite)
        {
            // Sprite 개수 확인
            // 개수에 맞는 패널 활성화
            // Sprite 변경
        }

        public void SetBackSprite(string sprite)
        {
            // Sprite 변경
        }

        public void SetBGM(string BGM)
        {
            // BGM Audio 변경
        }

        public void SetEffect(string Effect)
        {
            // Effect Audio 변경
        }

        public void DarkPanelActiveOn()
        {
            DarkPanel.SetActive(true);
        }

        public void DarkPanelActiveOff()
        {
            DarkPanel.SetActive(false);
        }

        public void FadeIn()
        {
            // Fade In
        }

        public void FadeOut()
        {
            // Fade Out
        }

        public void NameCardOn()
        {
            NameCard.SetActive(true);
        }

        public void NameCardOff()
        {
            NameCard.SetActive(false);
        }

        public void BoxOff()
        {
            Box.SetActive(false);
        }

        public void BoxOn()
        {
            Box.SetActive(true);
        }

        public void SetBoxNar()
        {
            Box.GetComponent<SpriteRenderer>().sprite = Box_Nar;
        }

        public void SetBoxDefault()
        {
            Box.GetComponent<SpriteRenderer>().sprite = Box_Default;
        }

        public void ExtraSpriteOn(string fileName)
        {
            // PopUp Active True
            // PopUp Sprite를 fileName Sprite로 변경
        }

        public void ExtraSpriteOff()
        {
            // Sprite 초기화
            // PopUp Active False
        }

        public void ShakeSprite()
        {
            // Animation 적용하기
        }

        public void SetScroll(int chapter)
        {
            Scroll.value = 0;
            // chapter id Length로 MaxValue 변경
        }

        public void StartMessenger(string charName)
        {
            MessengerBox.SetActive(true);
            MessengerName.text = charName;
        }

        public void MessengerLeft()
        {
            //콘텐츠 추가, 좌 정렬
        }

        public void MessengerRight()
        {
            //콘텐츠 추가, 우 정렬
        }

        public void MessengerExit()
        {
            MessengerBox.SetActive(false);
        }

        public void SkipTo()
        {
            //2번 복귀지점으로 돌아가기
        }

        public void LoadChoose(int chap, int id, params int[] ids)
        {
            //chap, id 의 선택 정보 가져오기 => myChoose
            //MoveTo(ids[myChoose]);
        }
    }
}
