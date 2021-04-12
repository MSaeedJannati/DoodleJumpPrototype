using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Variables
    [SerializeField] float Garvity = 5f;
    float xSpeed;
    [SerializeField]float speedFactor = .2f;
    [SerializeField]float maxXSpeed = 8.0f;
    [SerializeField] Transform mTransform;
    [SerializeField] Transform topObj;
    [SerializeField] Transform downObj;

    float minX;
    float maxX;

    bool isJumping;
    #region playGround Criteriens
    public static float topY;
    public static float downY;

    Vector3 delatPos = new Vector3();
    Vector3 currentPos = new Vector3();
    #endregion
    #endregion
    #region Functions
    public void Jump()
    {
        StartCoroutine(JumpCoroutine());
    }
    public float GetRightX()
    {
        return topObj.position.x;

    }
    public float GetLeftX()
    {
        return downObj.position.x;

    }
    #endregion
    #region Monobehaviour Callbacks
    private void Start()
    {
        minX = GetLeftX();
        maxX = GetRightX();
        xSpeed = 0.0f;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {

            xSpeed -= speedFactor;

        }
        if (Input.GetKey(KeyCode.D))
        {
            xSpeed += speedFactor;
        }
        xSpeed = Mathf.Clamp(xSpeed, -maxXSpeed, maxXSpeed);
        delatPos.x = xSpeed * Time.deltaTime;
        if (!isJumping)
            delatPos.y = -Garvity * Time.deltaTime;
        else
        {
            delatPos.y = 0;
        }
        currentPos = mTransform.position;
        currentPos += delatPos;
        currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
        mTransform.position = currentPos;
        topY = topObj.position.y;
        downY = downObj.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (collision.gameObject.transform.position.y <= mTransform.position.y)
            {
                StopAllCoroutines();
                isJumping = false;
                Jump();
            }
            else
            {
                StopAllCoroutines();
                isJumping = false;
            }

        }


    }

    #endregion
    #region coroutine
    IEnumerator JumpCoroutine()
    {
        float t = 0.0f;
        float period = .4f;
        float delatVelocity = 6.0f;
        Vector3 currentPos = mTransform.position;
        Vector3 initPos = mTransform.position;
        isJumping = true;
        while (t < period)
        {
            currentPos.y = initPos.y + delatVelocity * t - .5f * Garvity * t * t;
            currentPos.x += xSpeed * Time.deltaTime;
            currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
            mTransform.position = currentPos;
            t += Time.deltaTime;
            yield return null;
        }
        isJumping = false;
    }
    #endregion
}
