using UnityEngine;

public class CameraY : MonoBehaviour
{
    [Header("사용자 값 입력 후 사용")]
    public float RotateSpeed;
    private float mouseMoveX;
    private float mouseMoveY;
    private float tempX;
    private void Update()
    {
        mouseMoveY = Input.GetAxisRaw("Mouse Y");

        transform.Rotate(-mouseMoveY * RotateSpeed * Time.deltaTime, 0, 0);
        if (transform.eulerAngles.x > 180) tempX = transform.eulerAngles.x - 360;
        else
        {
            tempX = transform.eulerAngles.x;
        }
        tempX = Mathf.Clamp(tempX, -60, 60);

        transform.eulerAngles = new Vector3(tempX, transform.eulerAngles.y,0);
    }
}
