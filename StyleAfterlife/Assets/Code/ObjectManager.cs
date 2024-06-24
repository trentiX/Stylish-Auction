using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject HeavenLight;
    [SerializeField] private GameObject HellLight;
    [SerializeField] private GameObject[] itemPrefabs;
    private Dictionary<GameObject, GameObject> spawnedItems; // Словарь для хранения созданных предметов

    private Item obj;
    private Note note;
    
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
            note.jobDone = true;

            // Уведомление компонента Item
            obj.goToEarth();

            // Активация света и ожидание
            HellLight.SetActive(true);
            HellLight.transform.DOMoveY(4.5f, 1);
            yield return new WaitForSeconds(1);
            HellLight.transform.DOMoveY(-11, 4).OnComplete(() =>
            { 
                HellLight.SetActive(false);
            });

        }
    }
    
    public IEnumerator GoUp()
    {
        if (!note.jobDone)
        {
            note.jobDone = true;

            // Уведомление компонента Item
            obj.goToHeaven();

            // Активация света и ожидание
            HeavenLight.SetActive(true);
            HeavenLight.transform.DOMoveY(11, 1);
            yield return new WaitForSeconds(1);
            HeavenLight.transform.DOMoveY(-11, 4).OnComplete(() =>
            {
                HeavenLight.SetActive(false);
            });
        }
    }
}
