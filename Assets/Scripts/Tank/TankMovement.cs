using UnityEngine;

public class TankMovement : MonoBehaviour
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

        m_OriginalPitch = m_MovementAudio.pitch;

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

        if (Input.GetButtonDown(m_HorizontalAxisName))
        {
            if (Input.GetAxisRaw(m_HorizontalAxisName) > 0)
            {
                Debug.Log("Right button pressed");
                transform.eulerAngles = right;
            }
            else
            {
                Debug.Log("Left button pressed");
                transform.eulerAngles = left;
            }
        }

        if (Input.GetButtonDown(m_VerticalAxisName))
        {
            if (Input.GetAxisRaw(m_VerticalAxisName) > 0)
            {
                Debug.Log("Up button pressed");
                transform.eulerAngles = up;
            }
            else
            {
                Debug.Log("Down button pressed");
                transform.eulerAngles = down;
            }
        }

        if (Input.GetButton(m_HorizontalAxisName) || Input.GetButton(m_VerticalAxisName))
        {
            m_Moving = true;
        }

        if (Input.GetButtonUp(m_HorizontalAxisName) || Input.GetButtonUp(m_VerticalAxisName))
        {
            m_Moving = false;
        }

    }


    private void EngineAudio()
    {
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
        //float turn = m_HorizontalInputValue * m_TurnSpeed * Time.deltaTime;

        //Quaternion turnRotation = Quaternion.Euler(0.0f, turn, 0.0f);

        //m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}