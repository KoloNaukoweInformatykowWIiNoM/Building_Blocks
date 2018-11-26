using System;
using UnityEngine;
using CS.Namespace.Player;
using Random = UnityEngine.Random;

// ################################################################################
namespace CS.Namespace.Player {
    
	[RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]

    public class FirstPersonController : MonoBehaviour {
        
		public	GameObject			map;

		[SerializeField]	private					bool		m_IsWalking;
        [SerializeField]	private 				float		m_WalkSpeed;
        [SerializeField]	private					float		m_RunSpeed;
        [SerializeField]	[Range(0f, 1f)]	private	float		m_RunstepLenghten;
        [SerializeField]	private					float		m_JumpSpeed;
        [SerializeField]	private					float		m_StickToGroundForce;
        [SerializeField]	private					float		m_GravityMultiplier;
        [SerializeField]	private					MouseLook	m_MouseLook;
        [SerializeField]	private 				float		m_StepInterval;
        [SerializeField]	private					AudioClip[]	m_FootstepSounds;
        [SerializeField]	private					AudioClip	m_JumpSound;
        [SerializeField]	private					AudioClip	m_LandSound;
		[SerializeField]	private					Canvas		m_ui;

        private	Camera				m_Camera;
        private	bool				m_Jump;
        private float				m_YRotation;
        private Vector2 			m_Input;
        private Vector3 			m_MoveDir					=	Vector3.zero;
        private CharacterController	m_CharacterController;
        private CollisionFlags		m_CollisionFlags;
        private bool				m_PreviouslyGrounded;
        private Vector3				m_OriginalCameraPosition;
        private float				m_StepCycle;
        private float				m_NextStep;
        private bool				m_Jumping;
        private AudioSource			m_AudioSource;

		// ------------------------------------------------------------------------
        private void Start() {
            m_CharacterController		=	GetComponent<CharacterController>();
            m_Camera					=	Camera.main;
            m_OriginalCameraPosition	=	m_Camera.transform.localPosition;
            m_StepCycle					=	0f;
            m_NextStep					=	m_StepCycle/2f;
            m_Jumping					=	false;
            m_AudioSource				=	GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
        }


		// ------------------------------------------------------------------------
        private void Update() {
			///////////////////////////////////////////////////////////////////////
			if (!m_ui.GetComponent<UIScript>().mouseLock) { return; }

			if (Input.GetKeyDown(KeyCode.LeftShift)) { m_IsWalking = false; }
			if (Input.GetKeyUp(KeyCode.LeftShift)) { m_IsWalking = true; }
			///////////////////////////////////////////////////////////////////////

			RotateView();
			if (!m_Jump) { m_Jump = CrossPlatformInputManager.GetButtonDown("Jump"); }
            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded) {
                PlayLandingSound();
                m_MoveDir.y			=	0f;
                m_Jumping			=	false;
            }
			if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded) { m_MoveDir.y = 0f; }
            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }

		// ------------------------------------------------------------------------
        private void PlayLandingSound() {
            m_AudioSource.clip		=	m_LandSound;
            m_AudioSource.Play();
            m_NextStep				=	m_StepCycle + .5f;
        }

		// ------------------------------------------------------------------------
        private void FixedUpdate() {
			///////////////////////////////////////////////////////////////////////
			if (!m_ui.GetComponent<UIScript>().mouseLock) { return; }
			///////////////////////////////////////////////////////////////////////

            float speed;
			GetInput(out speed);
            Vector3 desiredMove		=	transform.forward*m_Input.y + transform.right*m_Input.x;
            RaycastHit hitInfo;
            Physics.SphereCast( transform.position, m_CharacterController.radius, Vector3.down, out hitInfo, m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore );
            desiredMove				=	Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
            m_MoveDir.x				=	desiredMove.x*speed;
            m_MoveDir.z				=	desiredMove.z*speed;
            if (m_CharacterController.isGrounded) {
                m_MoveDir.y = -m_StickToGroundForce;
                if (m_Jump) {
                    m_MoveDir.y		=	m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump			=	false;
                    m_Jumping		=	true;
                }
            } else {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags		=	m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);
            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

			mapTransform();
        }

		///////////////////////////////////////////////////////////////////////////
		void mapTransform() {
			int actualX = (int) transform.position.x;
			int actualY = (int) transform.position.y;
			int actualZ = (int) transform.position.z;

			if (actualY < 1) { 
				int newY = 1;
				for ( int i=1; i<map.GetComponent<Generator>().height; i++ ) {
					if ( map.GetComponent<Generator>().world[actualX,i,actualZ] == null ) { newY = i+2; }
				}

				Vector3 newPosition = new Vector3( actualX, newY, actualZ );
				transform.position = newPosition;
			}

			if ( actualX < 1 ) { 
				Vector3 newPosition = new Vector3( 2, actualY, actualZ );
				transform.position = newPosition;
			}

			if ( actualX >= map.GetComponent<Generator>().width-1 ) { 
				Vector3 newPosition = new Vector3( map.GetComponent<Generator>().width-3, actualY, actualZ );
				transform.position = newPosition;
			}

			if ( actualZ < 1 ) { 
				Vector3 newPosition = new Vector3( actualX, actualY, 2 );
				transform.position = newPosition;
			}

			if ( actualZ >= map.GetComponent<Generator>().depth-1 ) { 
				Vector3 newPosition = new Vector3( actualX, actualY, map.GetComponent<Generator>().depth-3 );
				transform.position = newPosition;
			}
		}
		///////////////////////////////////////////////////////////////////////////

		// ------------------------------------------------------------------------
        private void PlayJumpSound() {
            m_AudioSource.clip		=	m_JumpSound;
            m_AudioSource.Play();
        }

		// ------------------------------------------------------------------------
        private void ProgressStepCycle(float speed) {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0)) {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten))) * Time.fixedDeltaTime;
            }
            if (!(m_StepCycle > m_NextStep)) { return; }
            m_NextStep				=	m_StepCycle + m_StepInterval;
            PlayFootStepAudio();
        }

		// ------------------------------------------------------------------------
        private void PlayFootStepAudio() {
            
			if (!m_CharacterController.isGrounded) { return; }
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip		=	m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            m_FootstepSounds[n]		=	m_FootstepSounds[0];
            m_FootstepSounds[0]		=	m_AudioSource.clip;
        }

		// ------------------------------------------------------------------------
        private void UpdateCameraPosition(float speed) {
			Vector3		newCameraPosition;
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded) {
                newCameraPosition				=	m_Camera.transform.localPosition;
                newCameraPosition.y				=	m_Camera.transform.localPosition.y;
            } else {
                newCameraPosition 				=	m_Camera.transform.localPosition;
				newCameraPosition.y				=	m_OriginalCameraPosition.y;
            }
            m_Camera.transform.localPosition	=	newCameraPosition;
        }

		// ------------------------------------------------------------------------
        private void GetInput(out float speed) {
            float	horizontal					=	CrossPlatformInputManager.GetAxis("Horizontal");
            float	vertical					=	CrossPlatformInputManager.GetAxis("Vertical");
            bool	waswalking					=	m_IsWalking;
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input								=	new Vector2(horizontal, vertical);
			if (m_Input.sqrMagnitude > 1) { m_Input.Normalize(); }
            if (m_IsWalking != waswalking && m_CharacterController.velocity.sqrMagnitude > 0) {
                StopAllCoroutines();
            }
        }

		// ------------------------------------------------------------------------
        private void RotateView() {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }

		// ------------------------------------------------------------------------
        private void OnControllerColliderHit(ControllerColliderHit hit) {
            Rigidbody	body	=	hit.collider.attachedRigidbody;
            if (m_CollisionFlags == CollisionFlags.Below) { return; }
            if (body == null || body.isKinematic) { return; }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }

		// ------------------------------------------------------------------------
    }
}
// ################################################################################