using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerBall : MonoBehaviour
{
    public int itemCount;
    public float jumpPower;
    public GameManagerLogic manager;
    bool isJump;
    Rigidbody rigid;
    AudioSource audio;
    public AudioClip jumpClip;
    public AudioClip CoinClip;
    void Awake() {
        isJump = false;
        rigid = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        if(!isJump && Input.GetButtonDown("Jump")) {
            isJump = true;
            rigid.AddForce(new Vector3(0,jumpPower,0), ForceMode.Impulse);
            audio.PlayOneShot(jumpClip);
        }
    }
    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rigid.AddForce(new Vector3(h,0,v), ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Floor");
            isJump = false;
    }
    void OnTriggerEnter(Collider other){
        if (other.tag == "Coin") {
            itemCount++;
            audio.PlayOneShot(CoinClip);
            other.gameObject.SetActive(false);
            manager.GetItem(itemCount);
        }
        else if (other.tag == "Finish") {
            if(itemCount == manager.totalItemCount) {
                //Game Clear! && Next Stage
                if (manager.stage == 3)
                    SceneManager.LoadScene(0);
                else
                SceneManager.LoadScene(manager.stage+1);
            }
            else {
                //Restart..
                SceneManager.LoadScene(manager.stage);
            }
        }
    }

}
