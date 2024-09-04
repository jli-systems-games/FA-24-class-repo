using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerImageManager : MonoBehaviour
{
    public Player currentPlayer = Player.Player1;

    public Skin skinP1 = Skin.skin1;
    public Skin skinP2 = Skin.skin2;

    public Face faceP1 = Face.face1;
    public Face faceP2 = Face.face2;

    public enum Player
    {
        Player1, Player2
    }
    public enum Skin
    {
        skin1, skin2, skin3, skin4, skin5, skin6, skin7, skin8, skin9, skin10,
    }

    public enum Face
    {
        face1, face2, face3, face4, face5, face6, face7, face8, face9, face10,
    }

    public GameObject player1Profile;
    public GameObject player2Profile;

    public Image player1SkinImage;
    public Image player1FaceImage;
    public Image player2SkinImage;
    public Image player2FaceImage;

    public Image player1SkinImageGame;
    public Image player1FaceImageGame;
    public Image player2SkinImageGame;
    public Image player2FaceImageGame;

    public Button skinButton1;
    public Button skinButton2;
    public Button skinButton3;
    public Button skinButton4;
    public Button skinButton5;
    public Button skinButton6;
    public Button skinButton7;
    public Button skinButton8;
    public Button skinButton9;
    public Button skinButton10;

    public Button faceButton1;
    public Button faceButton2;
    public Button faceButton3;
    public Button faceButton4;
    public Button faceButton5;
    public Button faceButton6;
    public Button faceButton7;
    public Button faceButton8;
    public Button faceButton9;
    public Button faceButton10;

    private CameraShake cameraShake;
    private PlayerImageCarrier imageCarrier;
    private AudioManager audioManager;
    private GameStatusManager gameStatusManager;
    public Animator buttonsAnim;
    public Animator avatarsMenuAnim;
    public Animator avatarsGameAnim1;
    public Animator avatarsGameAnim2;
    public Animator barAnim1;
    public Animator barAnim2;
    private void Start()
    {
        imageCarrier = GetComponent<PlayerImageCarrier>();
        audioManager = GetComponent<AudioManager>();
        gameStatusManager = GetComponent<GameStatusManager>();
        cameraShake = FindObjectOfType<CameraShake>();

        player1Profile.SetActive(true);
        player2Profile.SetActive(false);

        // 添加监听
        skinButton1.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin1));
        skinButton2.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin2));
        skinButton3.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin3));
        skinButton4.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin4));
        skinButton5.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin5));
        skinButton6.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin6));
        skinButton7.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin7));
        skinButton8.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin8));
        skinButton9.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin9));
        skinButton10.onClick.AddListener(() => OnSkinButtonClicked(Skin.skin10));
        
        faceButton1.onClick.AddListener(() => OnFaceButtonClicked(Face.face1));
        faceButton2.onClick.AddListener(() => OnFaceButtonClicked(Face.face2));
        faceButton3.onClick.AddListener(() => OnFaceButtonClicked(Face.face3));
        faceButton4.onClick.AddListener(() => OnFaceButtonClicked(Face.face4));
        faceButton5.onClick.AddListener(() => OnFaceButtonClicked(Face.face5));
        faceButton6.onClick.AddListener(() => OnFaceButtonClicked(Face.face6));
        faceButton7.onClick.AddListener(() => OnFaceButtonClicked(Face.face7));
        faceButton8.onClick.AddListener(() => OnFaceButtonClicked(Face.face8));
        faceButton9.onClick.AddListener(() => OnFaceButtonClicked(Face.face9));
        faceButton10.onClick.AddListener(() => OnFaceButtonClicked(Face.face10));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentPlayer == Player.Player1)
        {           
            player1Profile.SetActive(false);
            player2Profile.SetActive(true);
            currentPlayer = Player.Player2;
            audioManager.PlayPlayerSet();
            cameraShake.TriggerShake();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentPlayer == Player.Player2 && !gameStatusManager.gameStarted)
        {
            StartGame();
        }
    }

    private void OnSkinButtonClicked(Skin selectedSkin)
    {
        if (currentPlayer == Player.Player1)
        {
            skinP1 = selectedSkin;
            player1SkinImage.sprite = imageCarrier.GetSkinSprite(selectedSkin);
            player1SkinImageGame.sprite = imageCarrier.GetSkinSprite(selectedSkin);
        }
        else if (currentPlayer == Player.Player2)
        {
            skinP2 = selectedSkin;
            player2SkinImage.sprite = imageCarrier.GetSkinSprite(selectedSkin);
            player2SkinImageGame.sprite = imageCarrier.GetSkinSprite(selectedSkin);
        }
    }

    private void OnFaceButtonClicked(Face selectedFace)
    {
        if (currentPlayer == Player.Player1)
        {
            faceP1 = selectedFace;
            player1FaceImage.sprite = imageCarrier.GetFaceSprite(selectedFace);
            player1FaceImageGame.sprite = imageCarrier.GetFaceSprite(selectedFace);
        }
        else if (currentPlayer == Player.Player2)
        {
            faceP2 = selectedFace;
            player2FaceImage.sprite = imageCarrier.GetFaceSprite(selectedFace);
            player2FaceImageGame.sprite = imageCarrier.GetFaceSprite(selectedFace);
        }
    }

    void StartGame()
    {       
        player2Profile.SetActive(false);
        gameStatusManager.gameStarted = true;
        gameStatusManager.ResetGame();
        audioManager.PlayPlayerSet();
        cameraShake.TriggerShake();
        buttonsAnim.SetTrigger("Start");

        StartCoroutine(DelayedExecution());
    }

    IEnumerator DelayedExecution()
    {
        // 等待1秒后执行：
        yield return new WaitForSeconds(1f);        
        avatarsMenuAnim.SetTrigger("Start");
        audioManager.PlayGameStart();

        // 等待2秒后执行：
        yield return new WaitForSeconds(1f);
        avatarsGameAnim1.SetTrigger("Start");
        avatarsGameAnim2.SetTrigger("Start");
        barAnim1.SetTrigger("Start");
        barAnim2.SetTrigger("Start");
    }

}
