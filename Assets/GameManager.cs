using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public enum TutorialState
{
    Jump,Arms,Grab,Lift,Done
}
public class GameManager : MonoBehaviour
{
    TutorialState currState;
    [SerializeField] Animator _instructor, title;
    [SerializeField] GameObject prop, mainProp, plyr;
    [SerializeField] TMP_Text inst;
    [SerializeField] Camera _pCam;
    int index = -1;
    public List <TutorialState> states = new List <TutorialState>();
    public List<string> instruction = new List<string>();
    Vector3 ogPos;
    Renderer _plyr;
    void Start()
    {
        _plyr = plyr.GetComponent<Renderer>();
        ogPos = _plyr.transform.position;
       
    }
    private void Update()
    {
        /*if (title.GetCurrentAnimatorStateInfo(0).IsName("TitleSpin"))
        {
           Debug.Log( title.GetCurrentAnimatorStateInfo(0).normalizedTime);
            Debug.Log("!!!");
        }*/

        Plane[] CameraPlanes = GeometryUtility.CalculateFrustumPlanes(_pCam);//put other camera reference here;
        bool isInCamera = GeometryUtility.TestPlanesAABB(CameraPlanes, _plyr.bounds);
        if (!isInCamera)
        {
            plyr.transform.position = ogPos;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Climb", LoadSceneMode.Single);
    }
    public void Progress()
    {
        index++;
        if(index <= states.Count)
        { 
            ChangeState(states[index]);

        }
       
    }

    public void ChangeState(TutorialState state)
    {
        currState = state;  

        switch (currState)
        {
            case TutorialState.Jump:
                _instructor.SetBool("JumpPart", true);
                StartCoroutine(TitleSpin());
                break;
            case TutorialState.Arms:
                _instructor.SetBool("ArmMove", true);
                StartCoroutine(TitleSpin());
                break;
            case TutorialState.Grab:

                prop.SetActive(true);
                mainProp.SetActive(true);
                _instructor.SetBool("ArmMove", false);
                _instructor.SetBool("JumpPart", false);
                StartCoroutine(TitleSpin());
                break;
            case TutorialState.Lift:
                _instructor.SetBool("Lifting", true);
                StartCoroutine(TitleSpin());
                break;
            case TutorialState.Done:
                StartGame();
                break;
            default:
                break;

        }

    }

    IEnumerator TitleSpin()
    {
        title.Play("TitleSpin",0,0);

        yield return null;

        while (title.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.25f && title.GetCurrentAnimatorStateInfo(0).IsName("TitleSpin"))
        {
            //Debug.Log(title.GetCurrentAnimatorStateInfo(0).normalizedTime);
            yield return null;
        }

        inst.text = instruction[index];
        yield break;


    }
}
