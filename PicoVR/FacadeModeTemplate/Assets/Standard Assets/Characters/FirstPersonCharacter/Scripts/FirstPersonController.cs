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
        /// 是否可以行走
        /// </summary>
        [SerializeField] private bool m_IsWalking;
        /// <summary>
        /// 行走速度
        /// </summary>
        [SerializeField] private float m_WalkSpeed;
        /// <summary>
        /// 奔跑速度
        /// </summary>
        [SerializeField] private float m_RunSpeed;
        /// <summary>
        /// 奔跑步长
        /// </summary>
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        /// <summary>
        /// 跳的速度
        /// </summary>
        [SerializeField] private float m_JumpSpeed;
        /// <summary>
        /// 掉向地面的力
        /// </summary>
        [SerializeField] private float m_StickToGroundForce;
        /// <summary>
        /// 重力系数，重力乘数
        /// </summary>
        [SerializeField] private float m_GravityMultiplier;
        /// <summary>
        /// 鼠标视角，鼠标观察
        /// </summary>
        [SerializeField] private MouseLook m_MouseLook;
        /// <summary>
        /// 是否使用视角后座力
        /// </summary>
        [SerializeField] private bool m_UseFovKick;
        /// <summary>
        /// 视角后座力
        /// </summary>
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        /// <summary>
        /// 是否使用头部晃动
        /// </summary>
        [SerializeField] private bool m_UseHeadBob;
        /// <summary>
        /// 摆动曲线控制
        /// </summary>
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        /// <summary>
        /// 振荡线性控制（上下跳动）
        /// </summary>
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        /// <summary>
        /// 步长间隔
        /// </summary>
        [SerializeField] private float m_StepInterval;
        /// <summary>
        /// 走动声音
        /// </summary>
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        /// <summary>
        /// 跳声音
        /// </summary>
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        /// <summary>
        /// 落地声音
        /// </summary>
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.
        /// <summary>
        /// 摄像机
        /// </summary>
        private Camera m_Camera;
        /// <summary>
        /// 是否跳起
        /// </summary>
        private bool m_Jump;
        /// <summary>
        /// Y轴旋转角度
        /// </summary>
        private float m_YRotation;
        /// <summary>
        /// 鼠标位置（在屏幕上的）
        /// </summary>
        private Vector2 m_Input;
        /// <summary>
        /// 移动方向
        /// </summary>
        private Vector3 m_MoveDir = Vector3.zero;
        /// <summary>
        /// 角色控制器
        /// </summary>
        private CharacterController m_CharacterController;
        /// <summary>
        /// 碰撞标识
        /// </summary>
        private CollisionFlags m_CollisionFlags;
        /// <summary>
        /// 是否预先着地
        /// </summary>
        private bool m_PreviouslyGrounded;
        /// <summary>
        /// 原始摄像头位置
        /// </summary>
        private Vector3 m_OriginalCameraPosition;
        /// <summary>
        /// 步长圆
        /// </summary>
        private float m_StepCycle;
        /// <summary>
        /// 下一步长（脚步，步）
        /// </summary>
        private float m_NextStep;
        /// <summary>
        /// 是否跳起中
        /// </summary>
        private bool m_Jumping;
        /// <summary>
        /// 声音
        /// </summary>
        private AudioSource m_AudioSource;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);//设置视角后座力类
            m_HeadBob.Setup(m_Camera, m_StepInterval);//设置头部摆动类
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);//鼠标视角初始化（旋转值）
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            //跳转状态需要在这里读取以确保它不会被遗漏
            // 不在跳跃状态下，读取跳跃按键
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            //上一帧没有先前落地 且 角色控制器着地(刚落地的情况）
            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());//线程 跳起来的晃动周期
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            //角色控制器没有着地 且 不是在跳起来中 且 上一帧先前落地过（检查有没有不着地情况，刚跳起的情况）
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }
            //保存是否落地数据，下一帧使用
            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }

        /// <summary>
        /// 播放落地声音
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
            GetInput(out speed);//获得速度（走或跑的速度）
            // always move along the camera forward as it is the direction that it being aimed at
            // 方向永远跟随相机朝向的正方向
            // 根据人物的前方和右方，再乘以输入值，获得最终的方向
            // 所谓人物的前方，根据手册的说法，就是编辑器中蓝色箭头的方向（z轴正方向）；而右方就是红色箭头（x轴正方向）
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;//期望的移动向量

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            //以人物为中心向周围作球形投射，半径角色控制器的半径，世界空间向下，长度为角色控制器高度的一半
            /*
        * Physics.SphereCast, 进行一次球形的碰撞
        * param:
        *  origin, 触碰的起始点
        *  radius, 球形的半径
        *  direction, 碰撞的目标
        *  hitInfo, 碰撞的结果
        *  maxDistance, 碰撞到的最大距离
        *  layerMask, 层次标识，~0按位取反就是全1，也就是所有的都可以碰撞
        *  queryTriggerInteraction, 是否要触发triiger
        */
            // 相当于从胶囊中心放了一个求，然后往下扔，看到有没有碰撞到什么东西
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            //期望移动向量与和角色碰撞的平面的向量
            // hitInfo.normal 触碰表面的法向量
            // Vector3.ProjectOnPlane, 将一个向量投射到一个垂直于平面的法线所定义的平面上，如果路是有坡度的，角色的y轴就会有速度 
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            //转成移动向量
            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)//落地
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)//要跳跃，给与一个向上的速度
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                //不在地上时，作用一个重力影响
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);//移动

            ProgressStepCycle(speed);//处理脚步声音
            UpdateCameraPosition(speed);//更新相机的摆

            m_MouseLook.UpdateCursorLock();//直观印象就是更新鼠标会不会隐藏
        }

        /// <summary>
        /// 播放开始跳跃的声音
        /// </summary>
        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }

        /// <summary>
        /// 处理脚步循环
        /// </summary>
        /// <param name="speed"></param>
        private void ProgressStepCycle(float speed)
        {
            //就是在移动的情况下个时间量
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }
            //时间量没到达跳出
            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }
            //时间量到达了播放一个脚步声音
            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }

        /// <summary>
        /// 脚步声音
        /// </summary>
        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            //随机选择播放一个声音，不包括0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            //把播放的声音放到0位置，不次不会播放
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }

        /// <summary>
        /// 更新相机的位置
        /// </summary>
        /// <param name="speed"></param>
        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            //使用头部摆动的情况下处理
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
        /// 获得速度（走或跑），方向（键盘的水平和垂直轴），视角后座力
        /// 获取输入，计算方向和速度
        /// </summary>
        /// <param name="speed"></param>
        private void GetInput(out float speed)
        {
            // Read input获取输入值
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            //有没有按下LeftShift
            //在PC平台上运行，根据LeftShift按键改变移动状态（行走，奔跑）
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            // 如果长度超过1，则将其归一化
            // 这边使用了 m_Input.sqrMagnitude 求出了长度的平方，相比于 m_Input.magnitude 少求一个平方根，效率高
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            // 没有按下左Shift（按下或松开Shift的时候） 且 使用视角后座力 且 角色速度大于0
            // 根据相机的角度，处理速度。只有人物在奔跑的时候才处理
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();//
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());//视角后座力向上或向下
            }
        }

        /// <summary>
        /// 旋转视野(视野随鼠标旋转，控制鼠标图标隐藏显示)
        /// 处理鼠标移动后镜头的位置
        /// </summary>
        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }

        /// <summary>
        /// 处理角色碰撞
        /// </summary>
        /// <param name="hit"></param>
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)//刚体在下面的时候不会移动
            {
                return;
            }

            if (body == null || body.isKinematic)//刚体为空或者不作用物理的时候不处理
            {
                return;
            }
            //给予一个人物方向的冲力
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);//
        }
    }
}
