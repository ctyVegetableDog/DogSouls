using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���ǵ��ƶ�ģ�飬�����������ת
/// �����﹫����ҵ�transform����Ϊstatic��
/// </summary>
public class PlayerMotor : MonoBehaviour
{
    private Transform mainCam; //���λ��
    private Vector3 forwardDirection; // ������Ϊ���-�����Y��Ϊ����ƽ���ϵ�ͶӰ

    private Transform trans; // �ڴ˴�������ҵ�transform
    private void Awake()
    {
        mainCam = Camera.main.transform;
        trans = this.transform;
        PlayerInfo.GetInstance().playerTransform = trans; // ����ҵ�transformί�и���̬�����Ϣ
    }

    /// <summary>
    /// ǰ��
    /// </summary>
    /// <param name="v">�ٶȱ���</param>
    public void MoveForward(float v)
    {
        trans.Translate(0, 0, v * PlayerInfo.GetInstance().BaseMoveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// ��ת����Ҫ���¼���������������Ϊ���-�����Y��Ϊ����ƽ���ϵ�ͶӰ
    /// </summary>
    /// <param name="x">��Input��ȡ</param>
    /// <param name="z">��Input��ȡ</param>
    public void Rotate(float x, float z)
    {
        UpdateForward();
        trans.rotation = Quaternion.LookRotation(forwardDirection) * Quaternion.LookRotation(new Vector3(x, 0, z));
        //trans.rotation = Quaternion.LookRotation(new Vector3(x, 0, z));
    }

    /// <summary>
    /// ����������
    /// </summary>
    private void UpdateForward()
    {
        forwardDirection = Vector3.ProjectOnPlane(trans.position - mainCam.position, Vector3.up);
    }
    
}
