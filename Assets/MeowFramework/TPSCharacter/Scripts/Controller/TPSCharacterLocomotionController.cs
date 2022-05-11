// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 16/03/2022 16:53
// 最后一次修改于: 26/04/2022 9:49
// 版权所有: CheapMeowStudio
// 描述:
// ----------------------------------------------

using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MeowFramework.TPSCharacter
{
	/// <summary>
	/// 第三人称运动控制器
	/// </summary>
    public partial class TPSCharacterLocomotionController : SerializedMonoBehaviour
    {
	    // 常量
	    
	    /// <summary>
	    /// 微量
	    /// </summary>
	    private const float Threshold = 0.01f;
	    
	    // 组件相关

	    /// <summary>
	    /// 角色控制器
	    /// </summary>
	    [BoxGroup("Component")]
	    [Required]
	    [Tooltip("角色控制器")]
	    public CharacterController CharacterCtr;

	    /// <summary>
	    /// ACT 输入控制器
	    /// </summary>
	    [BoxGroup("Component")]
	    [Required]
	    [Tooltip("ACT 输入控制器")]
	    public TPSCharacterInputController ACTInput;
	    
	    /// <summary>
	    /// 摄像机跟随点
	    /// </summary>
	    [BoxGroup("Component")]
	    [Required]
	    [Tooltip("摄像机跟随点")]
	    public GameObject CMCameraFollowTarget;

	    /// <summary>
	    /// 主摄像机
	    /// </summary>
	    [BoxGroup("Component")]
	    [Sirenix.OdinInspector.ReadOnly]
	    [Tooltip("主摄像机")]
	    public Camera MainCamera;
	    
	    /// <summary>
	    /// 跟随主角的摄像机
	    /// </summary>
	    [BoxGroup("Component")]
	    [Sirenix.OdinInspector.ReadOnly]
	    [Tooltip("跟随主角的摄像机")]
	    public CinemachineVirtualCamera PlayerFollowCamera;
	    
	    public void Awake()
	    {
		    MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		    PlayerFollowCamera = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
		    PlayerFollowCamera.Follow = CMCameraFollowTarget.transform;
		    InitSwitchableList();
	    }
	    
	    protected void Update()
	    {
		    // 之所以不使用订阅委托的方式调用 Move RotateToMoveDir CameraRotate
		    // 是因为他们有着 Update LateUpdate 的先后顺序要求
		    // 同时平滑功能也要求它们是每帧调用的
		    
		    ApplyGravity();
		    GroundedCheck();
		    Move();
	    }

	    protected void LateUpdate()
	    {
		    CameraRotate();
	    }
    }
}

