using UnityEngine;
using TMPro;

namespace My2D
{
    //UI를 관리하는 클래스
    public class UIManager : MonoBehaviour
    {
        #region Vareiables
        //대미지 텍스트 프리팹
        public TextMeshProUGUI damageText;

        //힐 텍스트 프리팹
        public TextMeshProUGUI healText;

        //캔바스
        public Canvas gameCanvas;

        //캔바스 위의 스폰 위치 갖고오기
        private Camera camera;
        [SerializeField] private Vector3 offset;    //캐릭터 머리 위로 위치 보정값
        #endregion

        #region Unity Event Method
        private void Awake()
        {
            //참조
            camera = Camera.main;
            //damageText = damageTextPrefab.GetComponent<TextMeshProUGUI>();
        }
        private void OnEnable()
        {
            //이벤트 함수 등록   
            CharacterEvents.characterDamaged += CharacterTakeDamage;
            CharacterEvents.characterHealed += CharacterHeal;
        }

        private void OnDisable()
        {
            //이벤트 함수에 등록된 함수 제거
            CharacterEvents.characterDamaged -= CharacterTakeDamage;
            CharacterEvents.characterHealed -= CharacterHeal;
        }
        #endregion

        #region Cutom Method
        //대미지 텍스트 프리팹 생성
        public void CharacterTakeDamage(GameObject character, float damage)
        {
            //프리팹 생성 - 대미지량 세팅
            //Vector3 spawnPosition = new Vector3(Screen.width/2, Screen.height/2, 0f);
            Vector3 spawnPosition = camera.WorldToScreenPoint(character.transform.position);

            if (damageText)
            {
                damageText.text = damage.ToString();
            }
            Instantiate(damageText, spawnPosition + offset, Quaternion.identity, gameCanvas.transform);
        }

        //힐 텍스트 프리팹 생성, character:힐한 캐릭터
        public void CharacterHeal(GameObject character, float healAmount)
        {
            //프리팹 생성- 생성된 프리팹의 부모를 Canvas로 지정
            //텍스트에 매개변수로 들어온 힐량 셋팅
            Vector3 spawnPosition = camera.WorldToScreenPoint(character.transform.position);

            if (healText)
            {
                healText.text = healAmount.ToString();
            }
            Instantiate(healText, spawnPosition + offset, Quaternion.identity, gameCanvas.transform);
        }
        #endregion
    }
}

