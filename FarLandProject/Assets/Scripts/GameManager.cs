using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    static private GameManager _instance;
    public static GameManager instance
    {
        get { return _instance; }
    }

    [SerializeField]
    private Transform PlayerStart = null;

    private GameObject PlayerPrefab = null;

    [HideInInspector]
    public GameObject PlayerObject = null;

    //메인카메라 위치저장
    private Vector3 _MainCamaeraStartPosision;


    //실제 다루는 캐릭터 정보============================================================
    private UnitSkill CurSkill = null;

    void SetUnitSkill()
    {
        if(PlayerObject != null)
        {
            CurSkill = PlayerObject.GetComponent<UnitSkill>();
        }
    }

    private void Awake()
    {
        _instance = this;

        //여기서 플레이어스타트태그달린 오브젝트 찾아서 위치초기화함
        PlayerStart = GameObject.Find("PlayerStart").transform;

        PlayerPrefab = Resources.Load<GameObject>("Prefabs/Beginner");
        PlayerObject = Instantiate(PlayerPrefab, PlayerStart.position, PlayerStart.rotation);

        //Camera.main.transform.parent = PlayerObject.transform;
        _MainCamaeraStartPosision = Camera.main.transform.position;

        SetUnitSkill();
    }

    void Start()
    {

    }

    //스킬 관련 컨트롤
    void KeyControl()
    {
        if (PlayerPrefab != null && CurSkill != null) 
        {
            //좌클릭시 스킬(공격)
            if(Input.GetMouseButtonDown(0))
            {
                CurSkill.Skill();
            }
        }
    }

    void SetCameraPosition()
    {
        Camera.main.transform.position = _MainCamaeraStartPosision + PlayerObject.transform.position;
    }

    //업데이트 관리
    void Update()
    {
        KeyControl();
        SetCameraPosition();
    }
}
