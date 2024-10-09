using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
      [TextArea] // Optional: Adds a larger text area for easier editing in the Inspector.
      public string itemDescription = "This is a default description"; // Set a unique description for each item.
}
