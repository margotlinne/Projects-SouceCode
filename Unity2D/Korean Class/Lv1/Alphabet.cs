using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alphabet : MonoBehaviour
{
    private Vector3 dragOffset;

    Camera cam;
    Animator anim;
    GameManager gm;

    public int pos;

    void Awake()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        gm = GameManager.instance;

        // 게임 시작 시 나타나기
        anim.SetTrigger("Emerge");
    }

    private void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
    }

    private void OnMouseDrag()
    {
        // 마우스로 누른 시점에서 알파벳과 마우스 위치 차이를 더해주어 마우스로 클릭을 한 상대적인 위치 그대로 드래그 구현
        transform.position = GetMousePos() + dragOffset;
    }

    private Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.gameObject.CompareTag("Consonant") && other.CompareTag("ConsonantBox"))
        {
            sfxManager.instance.correctSoundPlay();
            anim.SetTrigger("Disappear");
            Score.score += 10;
        }

        if(this.gameObject.CompareTag("Vowel") && other.CompareTag("VowelBox"))
        {
            sfxManager.instance.correctSoundPlay();
            anim.SetTrigger("Disappear");
            Score.score += 10;
        }

        if (this.gameObject.CompareTag("Vowel") && other.CompareTag("ConsonantBox"))
        {
            sfxManager.instance.wrongSoundPlay();
            Score.score -= 20; 
        }

        if (this.gameObject.CompareTag("Consonant") && other.CompareTag("VowelBox"))
        {
            sfxManager.instance.wrongSoundPlay();
            Score.score -= 20;
        }
    }

    // 사라지는 애니메이션 끝에서 실행될 함수

    public void Delete()
    {
        Destroy(this.gameObject);
        gm.spawnAlphabets.total++;
        gm.spawnAlphabets.check -= 1;
        gm.spawnAlphabets.emptyPos = pos;
    }
}
