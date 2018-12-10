﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class SimpleCharacterControl : MonoBehaviour
{

    public GameObject MessagePanel;
    public GameObject heartone;
    public Image hearttwo;
    public Image heartthree;

    private enum ControlMode
    {
        Tank,
        Direct
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;

    public int papercount = 0;
    public int glasscount = 0;
    public int plasticcount = 0;

    public int papercountlast = 0;
    public int glasscountlast = 0;
    public int plasticcountlast = 0;

    private  int trashtag;
    public int errormessage=0;


    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    public AudioSource putaudiosource;
    public AudioSource paperaudio;
    public AudioSource glassaudio;
    public AudioSource plasticaudio;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }




    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    void Update()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
      
    }
   
    private void TankUpdate()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        }
        else if (walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }




    private void OnTriggerStay(Collider other)
    {
        Picking(other);
        Putting(other);


    }

   




    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }

    

    
    public void Picking(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) & (other.gameObject.CompareTag("paperpickup") || other.gameObject.CompareTag("plasticpickup") ||  other.gameObject.CompareTag("glasspickup") ))
        {
            
            m_animator.SetTrigger("Pickup");
           


            if (other.gameObject.CompareTag("paperpickup"))
            {

                papercount++;
                papercountlast++;
                paperaudio.Play();
                
            }
            if (other.gameObject.CompareTag("glasspickup"))
            {

                glasscount++;
                glasscountlast++;
                glassaudio.Play();
               
            }
            if (other.gameObject.CompareTag("plasticpickup"))
            {

                plasticcount++;
                plasticcountlast++;
                plasticaudio.Play();
                   
                
            }


            Destroy(other.gameObject);

            



        }
        if (papercountlast == 9 && glasscountlast == 12 && plasticcountlast == 12 && papercount == 0 && glasscount == 0 && plasticcount == 0)
        {
            winmenu();
        }




    }

    public void Putting(Collider trashcan)
    {
        if (trashcan.gameObject.CompareTag("papertrashcan") & Input.GetKeyDown(KeyCode.B))
        {
            
            if (papercount != 0)
            {
                papercount = papercount -1;
                putaudiosource.Play();

            }
        }
        if (trashcan.gameObject.CompareTag("papertrashcan") & (Input.GetKeyDown(KeyCode.N) || Input.GetKeyDown(KeyCode.M)))
        {
            OpenMessagePanel("");            
            errormessage++;
            if (errormessage == 1)
            {
                Destroy(heartone);

            }
            else if (errormessage == 2)
            {
                Destroy(hearttwo);

            }
            else if (errormessage == 3)
            {
                Destroy(heartthree);
            }

            StartCoroutine(Second());


        }

        if (trashcan.gameObject.CompareTag("glasstrashcan") & Input.GetKeyDown(KeyCode.N))
        {            
            if (glasscount !=0)
            {
                glasscount--;
                putaudiosource.Play();

            }
       
        }
        if (trashcan.gameObject.CompareTag("glasstrashcan") & (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.M)))
        {
            OpenMessagePanel("");            
            errormessage++;
            if (errormessage == 1)
            {
                Destroy(heartone);

            }
            else if (errormessage == 2)
            {
                Destroy(hearttwo);

            }
            else if (errormessage == 3)
            {
                Destroy(heartthree);
            }
        
            StartCoroutine(Second());

        }

        if (trashcan.gameObject.CompareTag("plastictrashcan") & Input.GetKeyDown(KeyCode.M))
        {
            if (plasticcount !=0)
            {
                plasticcount--;
                putaudiosource.Play();
            }

        }
        if (trashcan.gameObject.CompareTag("plastictrashcan") & (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.N)))
        {
            OpenMessagePanel("");
            errormessage++;
            if (errormessage == 1)
            {
                Destroy(heartone);

            }
            else if (errormessage == 2)
            {
                Destroy(hearttwo);

            }
            else if (errormessage == 3)
            {
                Destroy(heartthree);
            }
           
               
            StartCoroutine( Second());
            
        }

        



        if (errormessage == 3)
        {

            
            lostmenu();
        }

    }

    public void lostmenu()
    {

        SceneManager.LoadScene(3);

    }

    public void winmenu()
    {

        SceneManager.LoadScene(2);

    }



    public void OpenMessagePanel(string text) {

        MessagePanel.SetActive(true);
    }


    public void CloseMessagePanel()
    {
        
        MessagePanel.SetActive(false);
    }


    IEnumerator Second() {

        if (MessagePanel.activeInHierarchy)
        {
            yield return new WaitForSeconds(3.0f);
            MessagePanel.SetActive(false);



        }


        
    }

}
