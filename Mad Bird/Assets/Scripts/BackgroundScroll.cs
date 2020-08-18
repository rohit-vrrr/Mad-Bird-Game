using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float bgSpeed;
    public Renderer bgRend;
    public Renderer groundRend;

    public void StartScrolling()
    {
        InvokeRepeating("Scrollbackground", 0.1f, 0.02f);
    }

    public void StopScrolling()
    {
        CancelInvoke("Scrollbackground");
    }

    private void Scrollbackground()
    {
        bgRend.material.mainTextureOffset += new Vector2(bgSpeed * Time.deltaTime, 0f);
        groundRend.material.mainTextureOffset += new Vector2(bgSpeed * Time.deltaTime, 0f);
    }
}
