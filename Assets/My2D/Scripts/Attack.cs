using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    //공격시 충돌하는 상대에게 대미지를 준다
    public class Attack : MonoBehaviour
    {
        #region Variables
        //공격력
        [SerializeField] private float attackDamage = 10f;

        //넉백 효과 - 뒤로 이동
        [SerializeField] private Vector2 knockback = Vector2.zero;
        #endregion

        #region Unity Event Method
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log($"플레이어에게 대미지 {attackDamage}을 준다");
            // collision에서 Damageable 컴포넌트를 찾아서 TakeDamage
            Damageable damageable = collision.GetComponent<Damageable>();

            if (damageable != null)
            {
                //공격하는 방향에 따라 밀리는 방향 설정
                Vector2 deliveredknockback = this.transform.parent.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool isHit = damageable.TakeDamage(attackDamage, deliveredknockback);

                if (isHit)
                {
                    Debug.Log(collision.name + "hit for" + attackDamage);
                }
            }
        }
        #endregion

    }

}
