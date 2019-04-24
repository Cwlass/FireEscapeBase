using UnityEngine;
public class RenderCubeMap : MonoBehaviour
{
    public RenderTexture cubemapLeftEye;
    public RenderTexture cubemapRightEye;
    public RenderTexture equirect;
    public bool renderStereo = true;
    public float stereoSeparation = 0.064f;
    void LateUpdate()
    {
        Camera cam = GetComponent<Camera>();
        if (cam)
        {
            Debug.Log("Camera Found");
        }
        if (cam == null)
        {
            cam = GetComponentInParent<Camera>();
        }
        if (cam == null)
        {
            Debug.Log("stereo 360 capture node has no camera or parent camera");
        }
        if (renderStereo)
        {
            cam.stereoSeparation = stereoSeparation;
            cam.RenderToCubemap(cubemapLeftEye, 63, Camera.MonoOrStereoscopicEye.Left);
            cam.RenderToCubemap(cubemapRightEye, 63, Camera.MonoOrStereoscopicEye.Right);
        }
        else
        {
            cam.RenderToCubemap(cubemapLeftEye, 63, Camera.MonoOrStereoscopicEye.Mono);
        }
        //optional: convert cubemaps to equirect
        if (equirect == null)
            return;
        cubemapLeftEye.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Left);
        cubemapRightEye.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Right);
    }
}