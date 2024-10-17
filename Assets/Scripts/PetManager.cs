using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetManager : MonoBehaviour
{
    // Pet Needs UI (Sliders)
    public Slider hungerSlider;
    public Slider happinessSlider;
    public Slider cleanlinessSlider;

    // Thought Bubbles for the needs
    public Image foodBubble;
    public Image playBubble;
    public Image cleanBubble;

    // Buttons for player actions
    public Button FeedButton;
    public Button PlayButton;
    public Button CleanButton;

    private AudioManager audioManager; // Reference to the AudioManager

    // Foliage image representing dirt/dead foliage
    public Image deadFoliage; // 2D sprite for dead foliage

    // Bug image and states
    public Image bugImage; // Reference to the UI Image for the bug
    public Sprite happyBugSprite; // Sprite for happy bug
    public Sprite neutralBugSprite; // Sprite for neutral bug
    public Sprite sadBugSprite; // Sprite for sad bug

    // Audio Clips for button sounds
    public AudioClip feedSound;   // Sound for feeding
    public AudioClip playSound;   // Sound for playing
    public AudioClip cleanSound;  // Sound for cleaning

    public float hungerDecayRate = 4f; // Hunger decays faster
    public float happinessDecayRate = 3.2f; // Happiness decays at a moderate rate
    public float cleanlinessDecayRate = 2.5f; // Cleanliness decays slower

    private float hunger = 100f;
    private float happiness = 100f;
    private float cleanliness = 100f;

    private bool canFeed = true;
    private bool canPlay = true;
    private bool canClean = true;

    private float actionCooldown = 1f; // Cooldown for actions
    private float decayPauseDuration = 1.5f; // Duration to pause before decay starts

    private void Start()
    {
        // Initial state for thought bubbles (hidden at start)
        foodBubble.CrossFadeAlpha(0, 0.001f, true);
        playBubble.CrossFadeAlpha(0, 0.001f, true);
        cleanBubble.CrossFadeAlpha(0, 0.001f, true);

        // Get the AudioManager component from the scene
        audioManager = FindObjectOfType<AudioManager>();

        // Initialize button listeners
        FeedButton.onClick.AddListener(FeedThePet);
        PlayButton.onClick.AddListener(PlayWithThePet);
        CleanButton.onClick.AddListener(CleanTheTerrarium);

        UpdateHungerBar();
        UpdateHappinessBar();
        UpdateCleanlinessBar();
        UpdateBugSprite(); // Initialize bug sprite
    }

    private void Update()
    {
        // Base decay rates for hunger, happiness, and cleanliness
        hunger -= hungerDecayRate * Time.deltaTime;
        cleanliness -= cleanlinessDecayRate * Time.deltaTime;

        // Check conditions for faster happiness decay
        float happinessDecayMultiplier = 1f; // Default multiplier

        if (hunger < 60f) // When hunger is below %, increase happiness decay
        {
            happinessDecayMultiplier += 0.5f; // Increase happiness decay by 50%
        }

        if (cleanliness < 60f) // When cleanliness is below %, increase happiness decay
        {
            happinessDecayMultiplier += 0.5f; // Increase happiness decay by another 50%
        }

        // Apply faster decay to happiness based on current hunger and cleanliness levels
        happiness -= happinessDecayRate * happinessDecayMultiplier * Time.deltaTime;

        // Clamp values to ensure they stay within bounds
        hunger = Mathf.Clamp(hunger, 0, 100);
        cleanliness = Mathf.Clamp(cleanliness, 0, 100);
        happiness = Mathf.Clamp(happiness, 0, 100);

        // Update UI sliders
        UpdateHungerBar();
        UpdateHappinessBar();
        UpdateCleanlinessBar();

        // Handle thought bubbles and dead foliage visibility based on current need levels
        HandleThoughtBubbles();
        HandleFoliageAppearance();

        // Update the bug sprite based on current happiness
        UpdateBugSprite();

    }

    private void HandleThoughtBubbles()
    {
        // Show food thought bubble if hunger is below % 
        if (hunger < 75f)
        {
            foodBubble.CrossFadeAlpha(1, 0.5f, true); // Fade in when hungry
        }
        else
        {
            foodBubble.CrossFadeAlpha(0, 0.5f, true); // Fade out when hunger is satisfied
        }

        // Show play thought bubble if happiness is below %
        if (happiness < 65f)
        {
            playBubble.CrossFadeAlpha(1, 0.5f, true); // Fade in when needs play
        }
        else
        {
            playBubble.CrossFadeAlpha(0, 0.5f, true); // Fade out when happy
        }

        // Show clean thought bubble if cleanliness is below %
        if (cleanliness < 65f)
        {
            cleanBubble.CrossFadeAlpha(1, 0.5f, true); // Fade in when dirty
        }
        else
        {
            cleanBubble.CrossFadeAlpha(0, 0.5f, true); // Fade out when clean
        }
    }

    private void HandleFoliageAppearance()
    {
        // Show dead foliage if cleanliness is below %
        if (cleanliness < 67f)
        {
            deadFoliage.CrossFadeAlpha(1, 0.5f, true); // Fade in dead foliage
        }
        else
        {
            deadFoliage.CrossFadeAlpha(0, 0.5f, true); // Fade out dead foliage
        }
    }

    private void UpdateBugSprite()
    {
        // Change bug sprite based on happiness
        if (happiness > 65f)
        {
            bugImage.sprite = happyBugSprite; // Set happy sprite
        }
        else if (happiness >= 40f)
        {
            bugImage.sprite = neutralBugSprite; // Set neutral sprite
        }
        else
        {
            bugImage.sprite = sadBugSprite; // Set sad sprite
        }
    }

    // Button actions to increase pet's stats with cooldowns
    public void FeedThePet()
    {
        if (canFeed)
        {
            audioManager.PlaySound(feedSound); // Play feed sound
            hunger += 30f; // Increase hunger when feeding
            hunger = Mathf.Clamp(hunger, 0, 100); // Keep hunger within bounds
            UpdateHungerBar();
            StartCoroutine(CooldownFeed());
            StartCoroutine(PauseDecay("hunger")); // Start the decay pause for hunger
        }
    }

    public void PlayWithThePet()
    {
        if (canPlay)
        {
            audioManager.PlaySound(playSound); // Play play sound
            happiness += 25f; // Increase happiness when playing
            happiness = Mathf.Clamp(happiness, 0, 100); // Keep happiness within bounds
            UpdateHappinessBar();
            StartCoroutine(CooldownPlay());
            StartCoroutine(PauseDecay("happiness")); // Start the decay pause for happiness
        }
    }

    public void CleanTheTerrarium()
    {
        if (canClean)
        {
            audioManager.PlaySound(cleanSound); // Play clean sound
            cleanliness += 40f; // Increase cleanliness when cleaning
            cleanliness = Mathf.Clamp(cleanliness, 0, 100); // Keep cleanliness within bounds
            UpdateCleanlinessBar();
            StartCoroutine(CooldownClean());
            StartCoroutine(PauseDecay("cleanliness")); // Start the decay pause for cleanliness
        }
    }

    // Cooldown handling for actions
    private System.Collections.IEnumerator CooldownFeed()
    {
        canFeed = false;
        yield return new WaitForSeconds(actionCooldown); // 1-second cooldown
        canFeed = true;
    }

    private System.Collections.IEnumerator CooldownPlay()
    {
        canPlay = false;
        yield return new WaitForSeconds(actionCooldown); // 1-second cooldown
        canPlay = true;
    }

    private System.Collections.IEnumerator CooldownClean()
    {
        canClean = false;
        yield return new WaitForSeconds(actionCooldown); // 1-second cooldown
        canClean = true;
    }

    // Coroutine to pause decay of specific need
    private System.Collections.IEnumerator PauseDecay(string stat)
    {
        if (stat == "hunger")
        {
            float originalHungerDecayRate = hungerDecayRate;
            hungerDecayRate = 0f;
            yield return new WaitForSeconds(decayPauseDuration); // Wait for specified duration
            hungerDecayRate = originalHungerDecayRate;
        }
        else if (stat == "happiness")
        {
            float originalHappinessDecayRate = happinessDecayRate;
            happinessDecayRate = 0f;
            yield return new WaitForSeconds(decayPauseDuration); // Wait for specified duration
            happinessDecayRate = originalHappinessDecayRate;
        }
        else if (stat == "cleanliness")
        {
            float originalCleanlinessDecayRate = cleanlinessDecayRate;
            cleanlinessDecayRate = 0f;
            yield return new WaitForSeconds(decayPauseDuration); // Wait for specified duration
            cleanlinessDecayRate = originalCleanlinessDecayRate;
        }
    }

    private void UpdateHungerBar()
    {
        hungerSlider.value = hunger / 100f; // Update UI slider
    }

    private void UpdateHappinessBar()
    {
        happinessSlider.value = happiness / 100f; // Update UI slider
    }

    private void UpdateCleanlinessBar()
    {
        cleanlinessSlider.value = cleanliness / 100f; // Update UI slider
    }
}