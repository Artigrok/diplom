using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //AudioSource walkingSound;
    //public AudioClip walkingClip;
    public CharacterController controller;
    public Transform groundCheck;
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;
    public LayerMask GroundMask;
    bool isGrounded;
    // Start is called before the first frame update
    public float speed = 12f;
    [SerializeField]float speed_config;
    Vector3 velocity;
    public float gravity = -9.81f;
    Vector3 curpos, predpos;
    public float x, z;
    // Update is called once per frame
    void Start()
    {
       // walkingSound = GetComponent<AudioSource>();
        curpos = transform.position;
        predpos = transform.position;
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        curpos = transform.position;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, GroundMask);
        if (isGrounded && velocity.y < -2f)
        {
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        Physics.SyncTransforms();
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        Physics.SyncTransforms();
        controller.Move(velocity * Time.deltaTime);

        /*
        if (controller.isGrounded) //персонаж на земле
        {

            if (curpos != predpos) //персонаж двигается, используется квадратичная магнитуда,
                                   //т.к. это операция менее требовательна к ресурсам, и нам не нужна точная скорость – нам нужен сам факт передвижения

                if (!walkingSound.isPlaying)
                { //проигрываем новый звук, только если сейчас никакой звук не играет
                    walkingSound.clip = walkingClip;
                    walkingSound.Play();
                }




        }

        else //персонаж НЕ двигается


                if (walkingSound.isPlaying)  //если звук проигрывается

            walkingSound.Stop();  //выключаем проигрывание звуков

        predpos = curpos;*/
    }
    public float ArmorSpeedDebuff(float armor) 
    {
        return (speed_config - (speed* 0.25f * armor / 100));
    }
}
