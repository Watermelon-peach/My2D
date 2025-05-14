using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    // Health를 관리하는 클래스, takedamage, die 구현
    public class Damageable : MonoBehaviour
    {
        #region Variables
        //참조
        public Animator animator;
        

        private float currentHealth;
        [SerializeField] private float maxHealth = 100f;
        //죽음 체크
        private bool isDeath = false;

        //무적 타이머
        private bool isInvincible = false;  //true면 대미지를 입지 않는다
        [SerializeField] private float invincibleTime = 3f; //무적 타임
        private float countdown = 0f;

        //델리게이트 이벤트 함수
        //매개변수로 float, Vector2가 있는 함수
        public UnityAction<float, Vector2> hitAction;
        #endregion

        #region Property
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            private set
            {
                currentHealth = value;

                if(currentHealth <= 0 && !IsDeath)
                {
                    Die();
                }
            }
        }

        //최대체력
        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }
            private set
            {
                maxHealth = value;
            }
        }

        //죽음 체크
        public bool IsDeath
        {
            get
            {
                return isDeath;
            }
            set
            {
                isDeath = value;
            }
        }
         //이동속도 잠그기
        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.lockVelocity);
            }
            set
            {
                animator.SetBool(AnimationString.lockVelocity, value);
            }
        }

        //hp 풀 체크
        public bool IsHealthFull => CurrentHealth >= MaxHealth;
        #endregion

        #region Unity Event Method
        private void Start()
        {
            //참조

            //초기화
            CurrentHealth = MaxHealth;
        }

        private void Update()
        {
            //무적타이머
            if (isInvincible)
            {
                countdown += Time.deltaTime;
                if (countdown >= invincibleTime)
                {
                    isInvincible = false;
                }
            }
        }
        #endregion

        #region Custom Method
        //Health 가산
        //매개변수로 대미지량과 뒤로 밀리는 값을 받아온다
        public bool TakeDamage(float damage, Vector2 knockback)
        {
            if (IsDeath || isInvincible)
                return false;

            CurrentHealth -= damage;
            //Debug.Log($"CurrentHealth : {CurrentHealth}");

            //무적 모드 셋팅 - 타이머 초기화
            isInvincible = true;
            countdown = 0;

            //애니메이션
            animator.SetTrigger(AnimationString.hitTrigger);
            LockVelocity = true;

            //효과: SFX, VFX, 넉백효과, UI효과
            //델리게이트 함수에 등록된 함수들 호출: 효과 연출에 필요한 함수 등록
            /* if (hitAction != null)
             {
                 hitAction.Invoke(damage, knockback);
             }*/
            hitAction?.Invoke(damage, knockback);

            //UI 효과 - 대미지text 프리팹 생성하는 함수가 등록된 이벤트 함수 호출
            //Health Text, Health Bar 관련함수 호출
            CharacterEvents.characterDamaged?.Invoke(gameObject, damage);

            return true;
        }

        private void Die()
        {
            IsDeath = true;
            animator.SetBool(AnimationString.isDeath, true);
        }

        //Health 가산 - 매개변수만큼 Health 충전
        //health를 실질적으로 충전하면 참을 반환, 충전하지 않았다면 거짓반환.
        public bool Heal(float healAmount)
        {
            //죽음 체크
            if (IsDeath || IsHealthFull)
            {
                return false;
            }
            

            CurrentHealth += healAmount;
            if (CurrentHealth > maxHealth)
            {
                healAmount -= CurrentHealth - maxHealth;
                CurrentHealth = maxHealth;
            }
            //Debug.Log($"CurrentHealth: {CurrentHealth}");

            //UI 효과 - 힐text 프리팹 생성하는 팜수가 등록된 이벤트 함수 호출
            //Health Text, Health Bar 관련함수 호출
            CharacterEvents.characterHealed?.Invoke(gameObject, healAmount);

            return true;
        }
        #endregion
    }

}
