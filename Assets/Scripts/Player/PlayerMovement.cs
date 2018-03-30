using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;
    public GameObject m_CameraObject;
    
    private string m_VerticalAxisName;     
    private string m_HorizontalAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_VerticalInputValue;    
    private float m_HorizontalInputValue;        
    private float m_OriginalPitch;
    private bool m_Moving;

    private Vector3 up, left, right, down;
    private Vector3 m_CurrentDirection;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_VerticalInputValue = 0f;
        m_HorizontalInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_VerticalAxisName = "Vertical" + m_PlayerNumber;
        m_HorizontalAxisName = "Horizontal" + m_PlayerNumber;

        if (m_MovementAudio)
        {
            m_OriginalPitch = m_MovementAudio.pitch;
        }

        transform.rotation = Camera.main.transform.rotation;
        up = new Vector3(0, transform.eulerAngles.y, 0);
        transform.eulerAngles = up;

        right = new Vector3(0, 90, 0) + up;
        left = new Vector3(0, -90, 0) + up;
        down = new Vector3(0, 180, 0) + up;

    }
    

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_VerticalInputValue = Input.GetAxis(m_VerticalAxisName);
        m_HorizontalInputValue = Input.GetAxis(m_HorizontalAxisName);

        EngineAudio();

        if (Input.GetButton(m_HorizontalAxisName))
        {
            if (Input.GetAxisRaw(m_HorizontalAxisName) > 0)
            {
                m_CurrentDirection = right;
            }
            if (Input.GetAxisRaw(m_HorizontalAxisName) < 0)
            {
                m_CurrentDirection = left;
            }
            
            m_Moving = true;
        }

        if (Input.GetButton(m_VerticalAxisName))
        {
            if (Input.GetAxisRaw(m_VerticalAxisName) > 0)
            {
                m_CurrentDirection = up;
            }
            if (Input.GetAxisRaw(m_VerticalAxisName) < 0)
            {
                m_CurrentDirection = down;
            }
            m_Moving = true;
        }

        if (Input.GetButtonUp(m_HorizontalAxisName) || Input.GetButtonUp(m_VerticalAxisName))
        {
            m_Moving = false;
        }

    }


    private void EngineAudio()
    {
        if (!m_EngineDriving || !m_EngineIdling || !m_MovementAudio)
        {
            return;
        }
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        if (Mathf.Abs(m_VerticalInputValue) < 0.1f && Mathf.Abs(m_HorizontalInputValue) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
        if (m_Moving)
        {
            Turn();
            Move();
        }
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = transform.forward * m_Speed * Time.deltaTime;

        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }



    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        transform.eulerAngles = m_CurrentDirection;
    }
}