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

    // Foliage image representing dirt/dead foliage
    public Image deadFoliage; // 2D sprite for dead foliage

    // Bug image and states
    public Image bugImage; // Reference to the UI Image for the bug
    public Sprite happyBugSprite; // Sprite for happy bug
    public Sprite neutralBugSprite; // Sprite for neutral bug
    public Sprite sadBugSprite; // Sprite for sad bug

    public float hungerDecayRate = 1.5f; // Hunger decays faster
    public float happinessDecayRate = 1f; // Happiness decays at a moderate rate
    public float cleanlinessDecayRate = 0.5f; // Cleanliness decays slower

    private float hunger = 100f;
    private float happiness = 100f;
    private float cleanliness = 100f;

    private bool canFeed = true;
    private bool canPlay = true;
    private bool canClean = true;

    private float actionCooldown = 1f; // Cooldown for actions

    private void Start()
    {
        // Initial state for thought bubbles (hidden at start)
        foodBubble.CrossFadeAlpha(0, 0.001f, true);
        playBubble.CrossFadeAlpha(0, 0.001f, true);
        cleanBubble.CrossFadeAlpha(0, 0.001f, true);

        // Initial state for dead foliage (hidden at start)
        deadFoliage.CrossFadeAlpha(0, 0.001f, true); // Hide dead foliage initially

        // Set the default bug sprite to happy
        //bugImage.sprite = happyBugSprite; // Set UI Image sprite

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
        // Decrease the needs over time
        hunger -= hungerDecayRate * Time.deltaTime;
        happiness -= happinessDecayRate * Time.deltaTime;
        cleanliness -= cleanlinessDecayRate * Time.deltaTime;

        hunger = Mathf.Clamp(hunger, 0, 100);
        happiness = Mathf.Clamp(happiness, 0, 100);
        cleanliness = Mathf.Clamp(cleanliness, 0, 100);

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
        // Show food thought bubble if hunger is below 50%
        if (hunger < 50f)
        {
            foodBubble.CrossFadeAlpha(1, 0.5f, true); // Fade in when hungry
        }
        else
        {
            foodBubble.CrossFadeAlpha(0, 0.5f, true); // Fade out when hunger is satisfied
        }

        // Show play thought bubble if happiness is below 50%
        if (happiness < 50f)
        {
            playBubble.CrossFadeAlpha(1, 0.5f, true); // Fade in when needs play
        }
        else
        {
            playBubble.CrossFadeAlpha(0, 0.5f, true); // Fade out when happy
        }

        // Show clean thought bubble if cleanliness is below 50%
        if (cleanliness < 50f)
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
        // Show dead foliage if cleanliness is below 50%
        if (cleanliness < 50f)
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
        if (happiness > 60f)
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
            hunger += 30f; // Increase hunger when feeding
            hunger = Mathf.Clamp(hunger, 0, 100); // Keep hunger within bounds
            UpdateHungerBar();
            StartCoroutine(CooldownFeed());
        }
    }

    public void PlayWithThePet()
    {
        if (canPlay)
        {
            happiness += 30f; // Increase happiness when playing
            happiness = Mathf.Clamp(happiness, 0, 100); // Keep happiness within bounds
            UpdateHappinessBar();
            StartCoroutine(CooldownPlay());
        }
    }

    public void CleanTheTerrarium()
    {
        if (canClean)
        {
            cleanliness += 30f; // Increase cleanliness when cleaning
            cleanliness = Mathf.Clamp(cleanliness, 0, 100); // Keep cleanliness within bounds
            UpdateCleanlinessBar();
            StartCoroutine(CooldownClean());
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