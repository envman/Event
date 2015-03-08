using UnityEngine;
using System.Collections;

public class PlayerController : Photon.MonoBehaviour
{

    public float SensitivityX = 15f;
    public float SensitivityY = 15f;

    public int Speed = 5;

    public float MinimumY = -60f;
    public float MaximumY = 60f;
    private float _rotationY;

    public void Start()
    {
        if (photonView.isMine)
        {
            var newCamera = gameObject.AddComponent<Camera>();
            newCamera.enabled = true;
        }
    }

    public void Update()
    {
        if (photonView.isMine)
        {
            var rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * SensitivityX;

            _rotationY += Input.GetAxis("Mouse Y") * SensitivityY;
            _rotationY = Mathf.Clamp(_rotationY, MinimumY, MaximumY);

            transform.localEulerAngles = new Vector3(-_rotationY, rotationX, 0);

            var x = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
            var z = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            transform.position += transform.forward * z;
            transform.position += transform.right * x;
        }
        else
        {
            syncTime += Time.deltaTime;
            transform.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
            transform.rotation = Quaternion.Slerp(syncStartRotation, syncEndRotation, syncTime / syncDelay);
        }
    }

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;

    private Vector3 syncStartPosition = Vector3.zero;
    private Vector3 syncEndPosition = Vector3.zero;

    private Quaternion syncEndRotation;
    private Quaternion syncStartRotation;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            syncEndPosition = (Vector3)stream.ReceiveNext();
            syncStartPosition = transform.position;

            syncEndRotation = (Quaternion)stream.ReceiveNext();
            syncStartRotation = transform.rotation;

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;
        }
    }
}
