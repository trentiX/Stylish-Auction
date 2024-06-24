using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Random = System.Random;

public class ItemCharacteristics
{
    // Each characteristic is accompanied by a rarity rating (points)
    public KeyValuePair<string, int> Creator; // Item creator
    public KeyValuePair<string, int> Age; // Item age
    public KeyValuePair<string, int> Condition; // Item condition
    public KeyValuePair<string, int> LastOwner; // Last item owner
    public KeyValuePair<string, int> Materials; // Item materials
    public KeyValuePair<string, int> AmountOfCopies; // Number of copies of the item
    public KeyValuePair<string, int> Remark; // Remark about the item

    // Constructor to initialize characteristics
    public ItemCharacteristics(KeyValuePair<string, int> creator, KeyValuePair<string, int> age, KeyValuePair<string, int> condition,
        KeyValuePair<string, int> lastOwner, KeyValuePair<string, int> materials,
        KeyValuePair<string, int> amountOfCopies, KeyValuePair<string, int> remark)
    {
        Creator = creator;
        Age = age;
        Condition = condition;
        LastOwner = lastOwner;
        Materials = materials;
        AmountOfCopies = amountOfCopies;
        Remark = remark;
    }
    
    public int GetTotalScore()
    {
        return Creator.Value + Age.Value + Condition.Value + LastOwner.Value + Materials.Value + AmountOfCopies.Value + Remark.Value;
    }
}

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject textNote;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private FirstPersonController fps;
    [SerializeField] private TextMeshProUGUI itemInfoText;
    [SerializeField] public TMP_InputField _inputField;

    private ItemCharacteristics currentCharacteristics;
    public int oldScore = 0;
    private bool interacted = false;
    public bool jobDone = true;
    private string name = "Note" + "\n [E]";

    // Dictionary to hold all characteristics options
    private Dictionary<string, KeyValuePair<string, int>[]> characteristics = new Dictionary<string, KeyValuePair<string, int>[]>
    {
        { "Creator", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("Mad Scientist", 50),
                new KeyValuePair<string, int>("Ralph Lauren", 20),
                new KeyValuePair<string, int>("Fashion Wizard", 40),
                new KeyValuePair<string, int>("Coco Chanel", 15),
                new KeyValuePair<string, int>("Space Cowboy", 35),
                new KeyValuePair<string, int>("Kenyo Asan", 10),
                new KeyValuePair<string, int>("Time Traveler", 30),
                new KeyValuePair<string, int>("Master", 5),
                new KeyValuePair<string, int>("Pizza Connoisseur", 25),
                new KeyValuePair<string, int>("Random dude", 3),
                new KeyValuePair<string, int>("Ninja Chef", 22),
                new KeyValuePair<string, int>("Robot Overlord", 18),
                new KeyValuePair<string, int>("Pirate Captain", 16),
                new KeyValuePair<string, int>("Unicorn Wrangler", 14),
                new KeyValuePair<string, int>("Wizard of Oz", 12),
                new KeyValuePair<string, int>("Leonardo da Vinci", 40),
                new KeyValuePair<string, int>("Superhero Sidekick", 28),
                new KeyValuePair<string, int>("Dragon Tamer", 26),
                new KeyValuePair<string, int>("Alien Diplomat", 24),
                new KeyValuePair<string, int>("Yeti Hunter", 20),
                new KeyValuePair<string, int>("Georgia O'Keeffe", 16),
                new KeyValuePair<string, int>("Sorcerer's Apprentice", 18),
                new KeyValuePair<string, int>("Frida Kahlo", 14),
                new KeyValuePair<string, int>("Ghost Whisperer", 15),
                new KeyValuePair<string, int>("Rembrandt", 28),
                new KeyValuePair<string, int>("Zombie Slayer", 14),
                new KeyValuePair<string, int>("Auguste Rodin", 26),
                new KeyValuePair<string, int>("Vampire Hunter", 10),
                new KeyValuePair<string, int>("Edvard Munch", 24),
                new KeyValuePair<string, int>("Banksy", 20),
            }
        },
        { "Age", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("1000+ years old", 1000),
                new KeyValuePair<string, int>("500 years old", 200),
                new KeyValuePair<string, int>("100 years old", 100),
                new KeyValuePair<string, int>("50 years old", 50),
                new KeyValuePair<string, int>("30 years old", 30),
                new KeyValuePair<string, int>("20 years old", 20),
                new KeyValuePair<string, int>("10 years old", 10),
                new KeyValuePair<string, int>("5 years old", 5),
                new KeyValuePair<string, int>("1 years old", 1),
            }
        },
        { "Condition", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("Torned", 9),
                new KeyValuePair<string, int>("Used", 5),
                new KeyValuePair<string, int>("Clear", 3),
                new KeyValuePair<string, int>("Crystal clear", 1),
                new KeyValuePair<string, int>("Completely dirty", 7),
            }
        },
        { "LastOwner", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("Neil Armstrong", 10),
                new KeyValuePair<string, int>("Mahatma Gandhi", 15),
                new KeyValuePair<string, int>("LeBron James", 15),
                new KeyValuePair<string, int>("Winston Churchill", 20),
                new KeyValuePair<string, int>("Alexander the Great", 25),
                new KeyValuePair<string, int>("Cleopatra", 25),
                new KeyValuePair<string, int>("Julius Caesar", 25),
                new KeyValuePair<string, int>("Leonardo DiCaprio", 12),
                new KeyValuePair<string, int>("Lionel Messi", 14),
                new KeyValuePair<string, int>("Napoleon Bonaparte", 23),
                new KeyValuePair<string, int>("Steve Jobs", 13),
                new KeyValuePair<string, int>("Muhammad Ali", 14),
                new KeyValuePair<string, int>("Fyodor Dostoevsky", 20),
                new KeyValuePair<string, int>("Albert Einstein", 22),
                new KeyValuePair<string, int>("Leonardo da Vinci", 26),
                new KeyValuePair<string, int>("Pele", 15),
                new KeyValuePair<string, int>("Audrey Hepburn", 9),
                new KeyValuePair<string, int>("Elon Musk", 15),
                new KeyValuePair<string, int>("Random dude", 2),
                new KeyValuePair<string, int>("Beethoven", 20),
                new KeyValuePair<string, int>("Charles Chaplin", 10)
            }
        },
        { "Materials", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("From another world", 100),
                new KeyValuePair<string, int>("Legendary", 50),
                new KeyValuePair<string, int>("Mythic", 30),
                new KeyValuePair<string, int>("Epic", 15),
                new KeyValuePair<string, int>("Common", 5),
            }
        },
        { "AmountOfCopies", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("One of a Kind", 15),
                new KeyValuePair<string, int>("Limited Edition", 8),
                new KeyValuePair<string, int>("Mass Produced", 3),
            }
        },
        { "Remark", new KeyValuePair<string, int>[]
            {
                new KeyValuePair<string, int>("Loves FC Milan", 10),
                new KeyValuePair<string, int>("Wants to live more", 2),
                new KeyValuePair<string, int>("Hates last owner", 2),
                new KeyValuePair<string, int>("Loves last owner", 5),
                new KeyValuePair<string, int>("Wants to meet owner", 8),
                new KeyValuePair<string, int>("Has a mysterious history", 6),
                new KeyValuePair<string, int>("Carried through generations", 4),
                new KeyValuePair<string, int>("Bears a hidden message", 5),
                new KeyValuePair<string, int>("Known for its unique design", 3),
                new KeyValuePair<string, int>("Carries an ancient curse", 7),
                new KeyValuePair<string, int>("Touched by a famous artist", 9),
                new KeyValuePair<string, int>("Longs for adventure", 4),
                new KeyValuePair<string, int>("Holds a forgotten secret", 6)
            }
        }
    };

    private void Awake()
    {
        fps = FindObjectOfType<FirstPersonController>();
    }

    public void GenerateRandomCharacteristics()
    {
        // Generate random characteristics with rarity ratings
        KeyValuePair<string, int> randomCreator = GetRandomCharacteristic("Creator");
        KeyValuePair<string, int> randomAge = GetRandomCharacteristic("Age");
        KeyValuePair<string, int> randomCondition = GetRandomCharacteristic("Condition");
        KeyValuePair<string, int> randomLastOwner = GetRandomCharacteristic("LastOwner");
        KeyValuePair<string, int> randomMaterials = GetRandomCharacteristic("Materials");
        KeyValuePair<string, int> randomAmountOfCopies = GetRandomCharacteristic("AmountOfCopies");
        KeyValuePair<string, int> randomRemark = GetRandomCharacteristic("Remark");

        // Create new characteristics
        currentCharacteristics = new ItemCharacteristics(randomCreator, randomAge, randomCondition,
            randomLastOwner, randomMaterials,
            randomAmountOfCopies, randomRemark);

        // Update the UI text
        UpdateItemInfoText(currentCharacteristics);
    }

    // Function to get a random characteristic and its rarity rating
    private KeyValuePair<string, int> GetRandomCharacteristic(string category)
    {
        KeyValuePair<string, int>[] possibleCharacteristics = characteristics[category];
        Random rnd = new Random();
        int randomIndex = rnd.Next(0, possibleCharacteristics.Length);
        return possibleCharacteristics[randomIndex];
    }

    public void UpdateItemInfoText(ItemCharacteristics characteristics)
    {
        currentCharacteristics = characteristics;

        // Construct the text based on the new characteristics
        string newText = $"{characteristics.Creator.Key} ({characteristics.Creator.Value})\n" +
                         $"{characteristics.Age.Key} ({characteristics.Age.Value})\n" +
                         $"{characteristics.Condition.Key} ({characteristics.Condition.Value})\n" +
                         $"{characteristics.LastOwner.Key} ({characteristics.LastOwner.Value})\n" +
                         $"{characteristics.Materials.Key} ({characteristics.Materials.Value})\n" +
                         $"{characteristics.AmountOfCopies.Key} ({characteristics.AmountOfCopies.Value})\n" +
                         $"{characteristics.Remark.Key} ({characteristics.Remark.Value})";

        // Update the TextMeshProUGUI component
        itemInfoText.text = newText;
    }

    private int GetScore()
    {
        return currentCharacteristics.GetTotalScore();
    }
    public string getName()
    {
        return name;
    }

    public void Interact()
    {
        if (!interacted)
        {
            name = null;
            _canvasGroup.DOFade(0.5f, 1);
            textNote.SetActive(true);
            fps.cameraCanMove = false;
            interacted = true;
            _inputField.ActivateInputField();

            if (!jobDone)
            {
                GenerateRandomCharacteristics();
                oldScore = GetScore();
                Debug.Log(oldScore);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && interacted)
        {
            name = "Note" + "\n [E]";
            _canvasGroup.DOFade(0, 1);
            textNote.SetActive(false);
            fps.cameraCanMove = true;
            interacted = false;
            _inputField.DeactivateInputField();
        }
    }
}
