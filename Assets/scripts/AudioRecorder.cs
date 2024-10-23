using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMeshPro namespace

public class AudioRecorder : MonoBehaviour
{
    public AudioSource audioSource;            // AudioSource to play the recorded sound
    public Button recordButton;                // UI button to start recording
    public GameObject recordingUI;             // The UI panel for recording
    public GameObject compositionUI;           // The UI panel for composing
    public Button[] pitchButtons;              // Array of buttons for different pitches
    public TextMeshProUGUI isRecordingText;    // TextMeshProUGUI to show "Recording..." when recording

    private AudioClip recordedClip;            // The recorded audio clip
    private bool isRecording = false;

    void Start()
    {
        // Add listener to record button to trigger recording
        recordButton.onClick.AddListener(StartRecording);
        
        // Ensure the composition UI is hidden at the start
        compositionUI.SetActive(false);
        
        // Ensure the recording text is hidden at the start
        isRecordingText.gameObject.SetActive(false);
    }

    // Start recording when the record button is pressed
    void StartRecording()
    {
        if (!isRecording)
        {
            // Display the "Recording..." text
            isRecordingText.gameObject.SetActive(true);

            // Start recording for 5 seconds
            recordedClip = Microphone.Start(null, false, 5, 44100);
            isRecording = true;
            Invoke("StopRecording", 5f);  // Automatically stop after 5 seconds
        }
    }

    // Stop recording after 5 seconds and switch UI
    void StopRecording()
    {
        if (isRecording)
        {
            // Stop the microphone and store the clip in the audio source
            Microphone.End(null);
            audioSource.clip = recordedClip;
            isRecording = false;

            // Hide the "Recording..." text
            isRecordingText.gameObject.SetActive(false);

            // Close the recording UI and open the composition UI
            recordingUI.SetActive(false);
            OpenInstrumentUICanvas();
        }
    }

    // Function to open the composition UI after recording is finished
    void OpenInstrumentUICanvas()
    {
        compositionUI.SetActive(true); // Open the UI for composing music
        AssignAudioToButtons();
    }

    // Assign the recorded audio clip to the buttons and modify the pitch
    void AssignAudioToButtons()
    {
        if (pitchButtons.Length == 5) // Ensure there are 5 buttons
        {
            for (int i = 0; i < pitchButtons.Length; i++)
            {
                // Calculate pitch for each button (index 0 has pitch 1, index 4 has pitch 1.4, etc.)
                float pitch = 1f + (i * 0.1f);  // Increase pitch progressively (1.0, 1.1, 1.2, ...)

                // Assign behavior to each button
                int buttonIndex = i; // Capture the index for use in the lambda
                pitchButtons[buttonIndex].onClick.AddListener(() =>
                {
                    PlayRecordedAudio(pitch);  // Play the recorded audio with the corresponding pitch
                });
            }
        }
    }

    // Play the recorded audio with a specific pitch
    void PlayRecordedAudio(float pitch)
    {
        if (recordedClip != null)
        {
            audioSource.pitch = pitch;   // Adjust the pitch
            audioSource.Play();          // Play the recorded clip with the set pitch
        }
    }
}
