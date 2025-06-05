using UnityEngine;
using System.Collections;

namespace My2D
{
    //캐릭터 이동 후 나타나는 잔상 이펙트
    //잔상 이펙트 : 2초동안 잔상 효과 발생
    //0.1초 간격으로 고스트 Material을 가진 오브젝트 생성하고
    //생성된 고스트 오브젝트는 1초 후에 자동으로 킬한다
    public class TrailEffect : MonoBehaviour
    {
        #region Variables
        //잔상 Material
        public Material ghostMaterial;

        //잔상 타이머
        [SerializeField] private float trailActiveTime = 2f; //잔상 효과 발생 시간
        [SerializeField] private float trailRefreshRate = 0.1f;  //잔상 오브젝트 발생 시간 간격
        [SerializeField] private float trailDestroyDelay = 1f;   //잔상 오브젝트 킬 딜레이 시간

        //잔상 알파값 조정
        [SerializeField] private float alphaRefreshRate = 0.1f;  //0.1초 간격으로
        [SerializeField] private float alphaRefreshValue = 0.05f; //0.05만큼 감소

        //플레이어 렌더러
        public SpriteRenderer playerRenderer;

        //고스트 이펙트 효과 체크
        private bool isTrailActive = false;
        #endregion

        #region Unity Event Method
        #endregion

        #region Custom Method
        public void StartActiveTrail()
        {
            //고스트 이펙트 효과 체크
            if (isTrailActive)
                return;

            StartCoroutine(ActiveTrail(trailActiveTime));
        }

        //activeTime(2초)동안 잔상효과
        IEnumerator ActiveTrail(float activeTime)
        {
            isTrailActive = true;

            while (activeTime > 0f)
            {
                activeTime -= trailRefreshRate; //0.1초씩 감산

                //잔상 효과
                //1. 빈 게임 오브젝트 만들기
                //2. 빈 게임 오브젝트의 위치, 회전, 크기를 플레이어(this.transform)와 동일하게 한다
                //3. 빈 게임오브젝트에 스프라이트 렌더러 컴포넌트 붙이고
                //4. 추가한 렌더러의 스프라이트 이미지를 플레이어 스프라이트 렌더러의 스프라이트 이미지로 가져온다.
                //5. 플레이어 렌더러와 고스트 렌더러의 그리는 순서 정하기
                //6. 고스트 Material 적용
                GameObject gObject = new GameObject();
                gObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
                gObject.transform.localScale = transform.localScale;

                SpriteRenderer ghostRenderer = gObject.AddComponent<SpriteRenderer>();
                ghostRenderer.sprite = playerRenderer.sprite;
                ghostRenderer.sortingLayerName = playerRenderer.sortingLayerName;
                ghostRenderer.sortingOrder = playerRenderer.sortingOrder - 1;
                ghostRenderer.material = ghostMaterial;

                StartCoroutine(AnimateSpriteColor(ghostRenderer, 0f, alphaRefreshRate, alphaRefreshValue));

                //7. 고스트 오브젝트 킬
                Destroy(gObject, trailDestroyDelay);

                yield return new WaitForSeconds(trailRefreshRate);
            }

            isTrailActive = false;
        }

        //알파값 애니 효과
        IEnumerator AnimateSpriteColor(SpriteRenderer renderer, float goal, float rate, float value)
        {
            float valueToAnimate = renderer.color.a;
            while (valueToAnimate > goal)
            {
                valueToAnimate -= value;
                renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a - value);
                yield return new WaitForSeconds(rate);
            }
        }
        #endregion
    }

}
