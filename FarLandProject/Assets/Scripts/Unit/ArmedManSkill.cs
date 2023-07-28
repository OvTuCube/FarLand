using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArmedManSkill : UnitSkill
{
    private float _horizontalValue = 0f;
    private float _vertialValue = 0f;
    private float _moveSpeed = 4.0f;
    private float _speedVelocity = 0.0f;
    float _speedOffset = 200.0f;

    private int _rayLayerMask;
    private Vector3 _destPos;

    private Animator _animator;

    //�ִϸ��̼� ó���� ���� ����
    [SerializeField]
    private float _attackStunTime = 0.2f;

    private float _attackAnimationTime;

    //���� ó���� ���� ����
    private bool _isDead = false;
    private bool _isStun = false;
    private bool _isAttack = false;

    bool CanMove()
    {
        if (_isDead || _isStun || _isAttack) return false;
        return true;
    }

    void MoveControl()
    {
        if (CanMove() == false) return;

        _horizontalValue = Input.GetAxis("Horizontal");
        _vertialValue = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(_horizontalValue, 0.0f, _vertialValue);

        if (direction.magnitude == 0) return;

        _speedVelocity = (direction * _moveSpeed * Time.deltaTime).magnitude * _speedOffset;
        _speedVelocity = Mathf.Clamp(_speedVelocity, 0.0f,1.0f);

        direction.Normalize();
        TurnControl(direction);
        transform.position = transform.position + transform.forward * _moveSpeed * Time.deltaTime;
        //������ȯ
    }

    void TurnControl(Vector3 curDirection)
    {
        transform.rotation = Quaternion.LookRotation(curDirection);
    }

    private void Awake()
    {
        _rayLayerMask = LayerMask.GetMask("Terrain");
        _animator = GetComponent<Animator>();
        _attackAnimationTime = _attackStunTime;
    }

    void PunchAttack()
    {
        //�̵��Ұ�
        _isAttack = true;
        _speedVelocity = 0.0f;
    }

    //����� ��� ��ų
    public override void Skill()
    {
        if (_isAttack == true) return;

        Debug.Log("ArmedManSkill");
        Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,1000.0f, _rayLayerMask) )
        {
            _destPos = hit.point;

            Vector3 attackDir = _destPos - transform.position;
            attackDir.Normalize();
            attackDir.y = 0.0f;

            TurnControl(attackDir);
            //��ġ ã������ �׹��� �ٶ󺸰�
        }
        _animator.SetTrigger("Attack");
        //_attackAnimationTime = _animator.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(_attackAnimationTime);

        //����
        PunchAttack();
    }

    //Ư����Ȳ�� ������ ���
    void StateCounter()
    {
        if(_isAttack == true)
        {
            _attackAnimationTime -= Time.deltaTime;
            if(_attackAnimationTime <=0.0f)
            {
                _isAttack = false;
                _attackAnimationTime = _attackStunTime;
            }
        }
    }

    void StateControl()
    {
        StateCounter();
        MoveControl();
    }

    void AnimationControl()
    {
        _animator.SetFloat("Speed", _speedVelocity);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StateControl();
        AnimationControl();
    }
}
