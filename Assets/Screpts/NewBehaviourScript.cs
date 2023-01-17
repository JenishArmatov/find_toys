using UnityEngine;
using UnityEngine.EventSystems;



public class NewBehaviourScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject planeForDestroy;
    public GameObject mainPlane;
    public ParticleSystem BigBoomAfterDestroy;
    public ParticleSystem SmollBoomAfterDestroy1;
    public ParticleSystem SmollBoomAfterDestroy2;


    private Camera m_cam;

    private float _resX;
    private float _resZ;
    private float _oldScaleX;

    private bool _isInAreaDestroyPlane = false;
    private bool _startAnimationDestroy = false;
    private bool _startAnimationChoseWrong = false;


    private Animator _choseWrongAnimator;

    private AudioSource _soundDestroy;
    private AudioSource _soundChoseWrong;

    private  Material _material;


    public void OnDrag(PointerEventData eventData)
    {
        move();

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Rigidbody>().useGravity = true;
        Rigidbody rb2d = GetComponent<Rigidbody>();
        rb2d.constraints = RigidbodyConstraints.None;
        if (_isInAreaDestroyPlane) {
            GetComponent<Rigidbody>().useGravity = false;

            if (!Controller.flag)
            {
                Controller.flag = true;
                _isInAreaDestroyPlane = false;
                Rigidbody r = GetComponent<Rigidbody>();
                r.constraints = RigidbodyConstraints.FreezeAll; 
                Controller.oldGameObject = gameObject;


            }
            else {
                if (Controller.oldGameObject.name.Equals(gameObject.name))
                {

                    _startAnimationDestroy = true;

                }
                else {
                    _soundChoseWrong.Play();

                    _choseWrongAnimator.Play("Start wrong animation");

                    _startAnimationChoseWrong = true;
                }

            }
        }

    }

    private void Start()
    {

        _choseWrongAnimator = planeForDestroy.GetComponent<Animator>();
        _soundDestroy = planeForDestroy.GetComponent<AudioSource>();
        _soundChoseWrong = GetComponentInParent<AudioSource>();
        _oldScaleX = gameObject.transform.localScale.x;

        MeshRenderer mesh = planeForDestroy.GetComponent<MeshRenderer>();
        _material = mesh.materials[0];

        gameObject.transform.position = new Vector3(Random.Range(mainPlane.transform.position.x-1f, mainPlane.transform.position.x + 1f),
                                                    Random.Range(mainPlane.transform.position.y + 10, mainPlane.transform.position.y +1),
                                                    Random.Range(mainPlane.transform.position.z - 4, mainPlane.transform.position.z + 4));


        if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
            Debug.Log("Camera doesn't ahve a physics raycaster.");

        m_cam = Camera.main;
    }
    private void Update() 
    {
        float posY = transform.position.y;

        if (posY < -1) 
        {

            transform.position = new Vector3(0, 1, 0);

        }

        if (_startAnimationDestroy) 
        {

            AnimationDestroidPair();

        }

        if (_startAnimationChoseWrong)
        {
            AnimationChoseWrong();
        }
/*
        if (Application.platform == RuntimePlatform.Android)
        {

            if (Input.GetKeyDown(KeyCode.Escape))
            {

                Application.Quit();

            }
        }
*/
    }
    private void AnimationDestroidPair() {

        _material.color = Color.green;


        gameObject.transform.localScale = Vector3.MoveTowards(
            gameObject.transform.localScale, 
            new Vector3(0, 0, 0),
            Time.deltaTime* _oldScaleX * 10);

       Controller.oldGameObject.transform.localScale = Vector3.MoveTowards(
            Controller.oldGameObject.transform.localScale, 
            new Vector3(0, 0, 0),
            Time.deltaTime* _oldScaleX * 10);

        if (gameObject.transform.localScale.x < _oldScaleX - _oldScaleX / 2)
        {

             SmollBoomAfterDestroy1.transform.position = new Vector3(
                gameObject.transform.position.x,
                gameObject.transform.position.y,
                gameObject.transform.position.z);


            SmollBoomAfterDestroy2.transform.position = new Vector3(
                Controller.oldGameObject.transform.position.x,
                Controller.oldGameObject.transform.position.y,
                Controller.oldGameObject.transform.position.z);
            _soundDestroy.Play();

            SmollBoomAfterDestroy1.Play();
            SmollBoomAfterDestroy2.Play();
            BigBoomAfterDestroy.Play();

            Destroy(gameObject);
            Destroy(Controller.oldGameObject);
            float score = PlayerPrefs.GetFloat(Controller.SCORE_STING) + 10;

            PlayerPrefs.SetFloat(Controller.SCORE_STING, score);
            Controller.SCORE_FLOAT = score;
            Controller.flag = false;

            _material.color = Color.white;

            _startAnimationDestroy = false;

        }

    }

    private void AnimationChoseWrong()
    {
        _material.color = Color.red;
        GetComponent<Rigidbody>().useGravity = false;
        gameObject.transform.position = Vector3.MoveTowards(
            gameObject.transform.position, 
            new Vector3(2.18f, 2, 0),
            Time.deltaTime*50);


        if (gameObject.transform.position.z >= -0.1f)
        {
            GetComponent<Rigidbody>().useGravity = true;

            _material.color = Color.white;

            _startAnimationChoseWrong = false;

        }
    }
    private void move() 
    {
        GetComponent<Rigidbody>().useGravity = false;
        Rigidbody rb2d = GetComponent<Rigidbody>();
        rb2d.constraints = RigidbodyConstraints.FreezePosition;

        Ray R = m_cam.ScreenPointToRay(Input.mousePosition); // Get the ray from mouse position
        Vector3 PO = transform.position; // Take current position of this draggable object as Plane's Origin
        Vector3 PN = -m_cam.transform.forward; // Take current negative camera's forward as Plane's Normal
        float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN); // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
        Vector3 P = R.origin + R.direction * t; // Find the new point.

        _resX = P.x;
        _resZ = P.z;

        transform.position = new Vector3(P.x, 1, P.z);

        checkIsInAreaDestroyPlane();



    }

    private void checkIsInAreaDestroyPlane()
    {
        if (Controller.flag)
        {
            if (Controller.oldGameObject.GetHashCode() == gameObject.GetHashCode())
            {

                Controller.flag = false;

                return;


            }
            if (_resX > planeForDestroy.transform.position.x - 4.5f &&
                _resZ < planeForDestroy.transform.position.z + 2.5f)
            {
                transform.SetPositionAndRotation(new Vector3(
                    planeForDestroy.transform.position.x - 1,
                    planeForDestroy.transform.position.y + 1.0f,
                    planeForDestroy.transform.position.z), new Quaternion(0, 0, 0, 0));

                _isInAreaDestroyPlane = true;
            }
            else
            {
                _isInAreaDestroyPlane = false;
            }


        }
        else
        {
            if (_resX > planeForDestroy.transform.position.x - 4.5f &&
                _resZ < planeForDestroy.transform.position.z + 2.5f)
            {
                transform.SetPositionAndRotation(new Vector3(
                    planeForDestroy.transform.position.x + 1,
                    planeForDestroy.transform.position.y + 1.0f,
                    planeForDestroy.transform.position.z), new Quaternion(0, 0, 0, 0));

                _isInAreaDestroyPlane = true;
            }
            else
            {
                _isInAreaDestroyPlane = false;
            }

        }
    }
}



