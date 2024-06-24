using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private TextMeshProUGUI cash;

    private Dictionary<GameObject, GameObject> spawnedItems; // Словарь для хранения созданных предметов

    private Item obj;
    private Note note;
    private string desicion;
    private int score;

    private void Awake()
    {
        note = FindObjectOfType<Note>();

        spawnedItems = new Dictionary<GameObject, GameObject>();

        foreach (GameObject prefab in itemPrefabs)
        {
            // Инициализация словаря созданными предметами, исключаем их с отключением состояния
            spawnedItems[prefab] = null;
        }
    }

    public void Spawn()
    {
        // Выбор случайного префаба
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        GameObject randomPrefab = itemPrefabs[randomIndex];

        if (spawnedItems[randomPrefab] == null)
        {
            // Создание экземпляра случайного предмета
            GameObject spawnedItem = Instantiate(randomPrefab, transform.position, Quaternion.identity);
            spawnedItems[randomPrefab] = spawnedItem; // Сохранение ссылки на созданный предмет

            // Отметка о завершении задачи
            note.jobDone = false;
        }

        obj = FindObjectOfType<Item>();
    }

    public IEnumerator GoDown()
    {
        if (!note.jobDone)
        {
            desicion = "earth";
            note.jobDone = true;

            Cash();
            // Уведомление компонента Item
            obj.goToEarth();
            yield break;
        }
    }

    public IEnumerator GoUp()
    {
        if (!note.jobDone)
        {
            desicion = "heaven";
            note.jobDone = true;

            Cash();
            // Уведомление компонента Item
            obj.goToHeaven();
            yield break;
        }
    }

    private void Cash()
    {
        switch (desicion)
        {
            case "earth":
                if (note.oldScore <= 200)
                {
                    score += note.oldScore;
                    cash.text = score.ToString();
                }
                else
                {
                    if (score > note.oldScore)
                    {
                        score -= note.oldScore;
                        cash.text = score.ToString();
                    }
                }
                break;
            case "heaven":
                if (note.oldScore >= 200)
                {
                    score += note.oldScore;
                    cash.text = score.ToString();
                }
                else
                {
                    if (score > note.oldScore)
                    {
                        score -= note.oldScore;
                        cash.text = score.ToString();
                    }
                }
                break;
            default:
                break;
        }
    }
}
