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

    //����ī�޶� ��ġ����
    private Vector3 _MainCamaeraStartPosision;


    //���� �ٷ�� ĳ���� ����============================================================
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

        //���⼭ �÷��̾ŸƮ�±״޸� ������Ʈ ã�Ƽ� ��ġ�ʱ�ȭ��
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

    //��ų ���� ��Ʈ��
    void KeyControl()
    {
        if (PlayerPrefab != null && CurSkill != null) 
        {
            //��Ŭ���� ��ų(����)
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

    //������Ʈ ����
    void Update()
    {
        KeyControl();
        SetCameraPosition();
    }
}
