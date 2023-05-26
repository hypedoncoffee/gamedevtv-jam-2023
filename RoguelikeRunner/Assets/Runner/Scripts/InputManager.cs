using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A simple Input Manager for a Runner game.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Returns the InputManager.
        /// </summary>
        public static InputManager Instance => s_Instance;
        static InputManager s_Instance;

        [SerializeField]
        float m_InputSensitivity = 1.5f;

        bool m_HasInput;
        Vector3 m_InputPosition;
        Vector3 m_PreviousInputPosition;
        private float horizontalInput;

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
        }


        void Update()
        {
            if (PlayerController.Instance == null)
            {
                return;
            }
            
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                horizontalInput = -1;

            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                horizontalInput = 1;
            }
            else
            { 
                horizontalInput = 0; 
            }

            m_PreviousInputPosition = PlayerController.Instance.transform.position;

            if (horizontalInput != 0)
            {
                if (!m_HasInput)
                {
                    Vector3 offset = horizontalInput * PlayerController.Instance.Speed * Vector3.right;
                    m_InputPosition = PlayerController.Instance.transform.position + offset ;

                    print(offset);
                }
                m_HasInput = true;
            }
            else
            {
                m_HasInput = false;
            }

            if (m_HasInput)
            {
                float normalizedDeltaPosition = (m_InputPosition.x - m_PreviousInputPosition.x) / Screen.width * m_InputSensitivity;
                PlayerController.Instance.SetDeltaPosition(normalizedDeltaPosition);

                print(m_InputPosition.x);
                print(m_PreviousInputPosition.x);
                print(normalizedDeltaPosition);
            }
            else
            {
                PlayerController.Instance.CancelMovement();
            }

            m_PreviousInputPosition = m_InputPosition;
        }
    }
}

