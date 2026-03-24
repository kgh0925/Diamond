using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    [Header("전환될 2개의 카메라 [ F3 ]")]
    [SerializeField]
    private Camera CamOne;
    [SerializeField]
    private Camera CamThree;

    private void Awake()
    {
        SetCamera();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (CamOne.enabled)
            {
                CamThree.enabled = true;
                CamOne.enabled = false;
            }
            else if (CamThree.enabled)
            {
                CamOne.enabled = true;
                CamThree.enabled = false;
            }
        }
    }

    void SetCamera()
    {
        CamOne.enabled = true;
        CamThree.enabled = false;
    }
}
