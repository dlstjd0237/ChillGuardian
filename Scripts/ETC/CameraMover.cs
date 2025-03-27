using BIS.Inputs;
using UnityEngine;

namespace BIS.ETC
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private InputReaderSO _inputSO;
        [SerializeField] private float _moveSpeed = 3;

        private void FixedUpdate()
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + (_moveSpeed * _inputSO.Direction.x * Time.deltaTime), -13f, 12f), transform.position.y, -100);
        }
    }
}
