using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;//set in unity (0,0.1) so doesnt move LR only up
    Vector2 offset;//store offset for amount of movement each fram

    Material material;
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;//gets material so we can move its offset to make it scroll
    }

    void Update()
    {
        offset = moveSpeed * Time.deltaTime;//smoothly moves and wont tick each fram
        material.mainTextureOffset += offset;//move BG every frame
                //this is built in method for material
    }
}
