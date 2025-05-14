using System.Collections;
using UnityEngine;

namespace My2D
{
    //발사체 기능: 이동하기, 충돌하기, 대미지 입히기
    public class Projectile : MonoBehaviour
    {
        #region Variables
        //참조
        private Rigidbody2D rb2D;

        //이동속도 - 좌 우로 이동, 위아래로 이동하지 않는다
        [SerializeField] private Vector2 moveVelocity = new Vector2(10f, 0);

        //공격력
        [SerializeField] private float attackDamage = 20f;
        //넉백 효과 - 뒤로 이동
        [SerializeField] private Vector2 knockback;

        //대미지 효과vfx, sfx
        public GameObject projectileEffectPrefab;
        public Transform effectPos;
        #endregion

        #region Unity Event Mehod
        private void Awake()
        {
            //참조
            rb2D = this.GetComponent<Rigidbody2D>();
        }

        private void Start()
        { 
            rb2D.linearVelocity = new Vector2( moveVelocity.x * transform.localScale.x, moveVelocity.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collision에서 Damageable 컴포넌트를 찾아서 TakeDamage
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable)
            {
                //공격하는 방향에 따라 밀리는 방향 설정
                Vector2 deliveredKnockback = this.transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool isHit = damageable.TakeDamage(attackDamage, deliveredKnockback);
                //대미지를 주었으면
                if(isHit)
                {
                    //vfx, sfx
                    GameObject effectGo = Instantiate(projectileEffectPrefab, effectPos.position, Quaternion.identity);
                    Destroy(effectGo, 0.4f);
                }
                
            }
            //화살 킬
            Destroy(gameObject);
        }
        #endregion
    }

}
