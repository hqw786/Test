using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        /// <summary>
        /// �Ƿ��������
        /// </summary>
        [SerializeField] private bool m_IsWalking;
        /// <summary>
        /// �����ٶ�
        /// </summary>
        [SerializeField] private float m_WalkSpeed;
        /// <summary>
        /// �����ٶ�
        /// </summary>
        [SerializeField] private float m_RunSpeed;
        /// <summary>
        /// ���ܲ���
        /// </summary>
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        /// <summary>
        /// �����ٶ�
        /// </summary>
        [SerializeField] private float m_JumpSpeed;
        /// <summary>
        /// ����������
        /// </summary>
        [SerializeField] private float m_StickToGroundForce;
        /// <summary>
        /// ����ϵ������������
        /// </summary>
        [SerializeField] private float m_GravityMultiplier;
        /// <summary>
        /// ����ӽǣ����۲�
        /// </summary>
        [SerializeField] private MouseLook m_MouseLook;
        /// <summary>
        /// �Ƿ�ʹ���ӽǺ�����
        /// </summary>
        [SerializeField] private bool m_UseFovKick;
        /// <summary>
        /// �ӽǺ�����
        /// </summary>
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        /// <summary>
        /// �Ƿ�ʹ��ͷ���ζ�
        /// </summary>
        [SerializeField] private bool m_UseHeadBob;
        /// <summary>
        /// �ڶ����߿���
        /// </summary>
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        /// <summary>
        /// �����Կ��ƣ�����������
        /// </summary>
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        /// <summary>
        /// �������
        /// </summary>
        [SerializeField] private float m_StepInterval;
        /// <summary>
        /// �߶�����
        /// </summary>
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        /// <summary>
        /// ������
        /// </summary>
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        /// <summary>
        /// �������
        /// </summary>
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.
        /// <summary>
        /// �����
        /// </summary>
        private Camera m_Camera;
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private bool m_Jump;
        /// <summary>
        /// Y����ת�Ƕ�
        /// </summary>
        private float m_YRotation;
        /// <summary>
        /// ���λ�ã�����Ļ�ϵģ�
        /// </summary>
        private Vector2 m_Input;
        /// <summary>
        /// �ƶ�����
        /// </summary>
        private Vector3 m_MoveDir = Vector3.zero;
        /// <summary>
        /// ��ɫ������
        /// </summary>
        private CharacterController m_CharacterController;
        /// <summary>
        /// ��ײ��ʶ
        /// </summary>
        private CollisionFlags m_CollisionFlags;
        /// <summary>
        /// �Ƿ�Ԥ���ŵ�
        /// </summary>
        private bool m_PreviouslyGrounded;
        /// <summary>
        /// ԭʼ����ͷλ��
        /// </summary>
        private Vector3 m_OriginalCameraPosition;
        /// <summary>
        /// ����Բ
        /// </summary>
        private float m_StepCycle;
        /// <summary>
        /// ��һ�������Ų�������
        /// </summary>
        private float m_NextStep;
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        private bool m_Jumping;
        /// <summary>
        /// ����
        /// </summary>
        private AudioSource m_AudioSource;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);//�����ӽǺ�������
            m_HeadBob.Setup(m_Camera, m_StepInterval);//����ͷ���ڶ���
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);//����ӽǳ�ʼ������תֵ��
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            //��ת״̬��Ҫ�������ȡ��ȷ�������ᱻ��©
            // ������Ծ״̬�£���ȡ��Ծ����
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            //��һ֡û����ǰ��� �� ��ɫ�������ŵ�(����ص������
            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());//�߳� �������Ļζ�����
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            //��ɫ������û���ŵ� �� �������������� �� ��һ֡��ǰ��ع��������û�в��ŵ������������������
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }
            //�����Ƿ�������ݣ���һ֡ʹ��
            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }

        /// <summary>
        /// �����������
        /// </summary>
        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }

        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);//����ٶȣ��߻��ܵ��ٶȣ�
            // always move along the camera forward as it is the direction that it being aimed at
            // ������Զ������������������
            // ���������ǰ�����ҷ����ٳ�������ֵ��������յķ���
            // ��ν�����ǰ���������ֲ��˵�������Ǳ༭������ɫ��ͷ�ķ���z�������򣩣����ҷ����Ǻ�ɫ��ͷ��x��������
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;//�������ƶ�����

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            //������Ϊ��������Χ������Ͷ�䣬�뾶��ɫ�������İ뾶������ռ����£�����Ϊ��ɫ�������߶ȵ�һ��
            /*
        * Physics.SphereCast, ����һ�����ε���ײ
        * param:
        *  origin, ��������ʼ��
        *  radius, ���εİ뾶
        *  direction, ��ײ��Ŀ��
        *  hitInfo, ��ײ�Ľ��
        *  maxDistance, ��ײ����������
        *  layerMask, ��α�ʶ��~0��λȡ������ȫ1��Ҳ�������еĶ�������ײ
        *  queryTriggerInteraction, �Ƿ�Ҫ����triiger
        */
            // �൱�ڴӽ������ķ���һ����Ȼ�������ӣ�������û����ײ��ʲô����
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            //�����ƶ�������ͽ�ɫ��ײ��ƽ�������
            // hitInfo.normal ��������ķ�����
            // Vector3.ProjectOnPlane, ��һ������Ͷ�䵽һ����ֱ��ƽ��ķ����������ƽ���ϣ����·�����¶ȵģ���ɫ��y��ͻ����ٶ� 
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            //ת���ƶ�����
            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)//���
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)//Ҫ��Ծ������һ�����ϵ��ٶ�
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                //���ڵ���ʱ������һ������Ӱ��
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);//�ƶ�

            ProgressStepCycle(speed);//����Ų�����
            UpdateCameraPosition(speed);//��������İ�

            m_MouseLook.UpdateCursorLock();//ֱ��ӡ����Ǹ������᲻������
        }

        /// <summary>
        /// ���ſ�ʼ��Ծ������
        /// </summary>
        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }

        /// <summary>
        /// ����Ų�ѭ��
        /// </summary>
        /// <param name="speed"></param>
        private void ProgressStepCycle(float speed)
        {
            //�������ƶ�������¸�ʱ����
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }
            //ʱ����û��������
            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }
            //ʱ���������˲���һ���Ų�����
            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }

        /// <summary>
        /// �Ų�����
        /// </summary>
        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            //���ѡ�񲥷�һ��������������0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            //�Ѳ��ŵ������ŵ�0λ�ã����β��Ქ��
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }

        /// <summary>
        /// ���������λ��
        /// </summary>
        /// <param name="speed"></param>
        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            //ʹ��ͷ���ڶ�������´���
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }

        /// <summary>
        /// ����ٶȣ��߻��ܣ������򣨼��̵�ˮƽ�ʹ�ֱ�ᣩ���ӽǺ�����
        /// ��ȡ���룬���㷽����ٶ�
        /// </summary>
        /// <param name="speed"></param>
        private void GetInput(out float speed)
        {
            // Read input��ȡ����ֵ
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            //��û�а���LeftShift
            //��PCƽ̨�����У�����LeftShift�����ı��ƶ�״̬�����ߣ����ܣ�
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            // ������ȳ���1�������һ��
            // ���ʹ���� m_Input.sqrMagnitude ����˳��ȵ�ƽ��������� m_Input.magnitude ����һ��ƽ������Ч�ʸ�
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            // û�а�����Shift�����»��ɿ�Shift��ʱ�� �� ʹ���ӽǺ����� �� ��ɫ�ٶȴ���0
            // ��������ĽǶȣ������ٶȡ�ֻ�������ڱ��ܵ�ʱ��Ŵ���
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();//
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());//�ӽǺ��������ϻ�����
            }
        }

        /// <summary>
        /// ��ת��Ұ(��Ұ�������ת���������ͼ��������ʾ)
        /// ��������ƶ���ͷ��λ��
        /// </summary>
        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }

        /// <summary>
        /// �����ɫ��ײ
        /// </summary>
        /// <param name="hit"></param>
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)//�����������ʱ�򲻻��ƶ�
            {
                return;
            }

            if (body == null || body.isKinematic)//����Ϊ�ջ��߲����������ʱ�򲻴���
            {
                return;
            }
            //����һ�����﷽��ĳ���
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);//
        }
    }
}
