using Unity.Mathematics;
using UnityEngine;

//プレイヤーの死亡判定_チェックポイントへのワープ
public class IsDead : MonoBehaviour
{
    Vector3 checkPointPosition = Vector3.zero; //チェックポイントの位置

    //当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        //敵との接触
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("gagaga");
        }
    }

    //当たり判定_trigger
    private void OnTriggerEnter(Collider other)
    {
        //チェックポイントに接触
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            var position = other.transform.position;
            if (checkPointPosition != position)
            {
                checkPointPosition = position;
                TouchCheckPoint();
            }
        }

        //死亡するゾーンに侵入
        if (other.gameObject.CompareTag("DeadZone"))
        {
            transform.position = checkPointPosition;
        }
    }

    void TouchCheckPoint()
    {
        Debug.Log("更新");
    }
}
