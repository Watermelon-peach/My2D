using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    //플레이어를 제어하는 클래스
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        //강체
        private Rigidbody2D rb2D;

        //이동
        //걷는 속도 - 좌우로 걷는다
        [SerializeField] private float walkSpeed = 5f;

        //이동 입력값
        private Vector2 inputMove;
        #endregion

        private void Awake()
        {
            //참조
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {

            rb2D.linearVelocity = new Vector2(inputMove.x * walkSpeed, rb2D.linearVelocity.y);
        }

        public void Move(InputAction.CallbackContext context)
        {
                inputMove = context.ReadValue<Vector2>();
        }

    }
}

