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
        // ��ȡ���ʲ�����Ϊ��ƽ��ģʽ
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        cloudMaterial = renderer.material;
        cloudMaterial.mainTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void Update()
    {
        // ����ƫ��UV����
        currentOffset.x += scrollSpeed * Time.deltaTime;
        cloudMaterial.mainTextureOffset = currentOffset;
    }
}