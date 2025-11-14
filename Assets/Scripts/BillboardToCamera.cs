using UnityEngine;

public class BillboardToCamera : MonoBehaviour
{
    public Camera targetCamera;

    void LateUpdate()
    {
        if (!targetCamera)
            targetCamera = Camera.main;
        if (!targetCamera) return;

        // 面向鏡頭方向
        Vector3 forward = targetCamera.transform.forward;
        forward.y = 0; // 保持水平

        // 設定旋轉
        transform.rotation = Quaternion.LookRotation(forward);

        // ⭐ UI 正面如果是反的，加 180 度
        //transform.Rotate(0, 180f, 0);
    }
}
