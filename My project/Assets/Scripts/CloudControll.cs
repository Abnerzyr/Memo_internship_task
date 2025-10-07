using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CloudScroll : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Material cloudMaterial;
    private Vector2 currentOffset = Vector2.zero;

    void Start()
    {
        // 获取材质并设置为可平铺模式
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        cloudMaterial = renderer.material;
        cloudMaterial.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        // 持续偏移UV坐标
        currentOffset.x += scrollSpeed * Time.deltaTime;
        cloudMaterial.mainTextureOffset = currentOffset;
    }
}