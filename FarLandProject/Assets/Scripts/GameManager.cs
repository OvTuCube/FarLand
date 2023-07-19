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

    //�Է°���
    private MainController MainController = null;

    private float HorizontalValue = 0f;
    private float VertialValue = 0f;
    private float MoveSpeed = 4.0f;

    private void Awake()
    {
        _instance = this;

        //���⼭ �÷��̾ŸƮ�±״޸� ������Ʈ ã�Ƽ� ��ġ�ʱ�ȭ��
        PlayerStart = GameObject.Find("PlayerStart").transform;

        PlayerPrefab = Resources.Load<GameObject>("Prefabs/Beginner");
        PlayerObject = Instantiate(PlayerPrefab, PlayerStart.position, PlayerStart.rotation);

        Camera.main.transform.parent = PlayerObject.transform;
        Camera.main.transform.position += PlayerStart.position;
    }

    void Start()
    {

    }

    void Update()
    {
        HorizontalValue = Input.GetAxis("Horizontal");
        VertialValue = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(HorizontalValue, 0.0f, VertialValue);

        PlayerObject.transform.Translate(direction * MoveSpeed * Time.deltaTime);
    }
}
