using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject HeavenLight;
    [SerializeField] private GameObject HellLight;
    [SerializeField] private GameObject[] itemPrefabs;
    private Dictionary<GameObject, bool> spawnedItems; // Словарь предметов и их состояний

    private Item obj;
    private Note note;
    
    private void Awake()
    {
        obj = FindObjectOfType<Item>();
        note = FindObjectOfType<Note>();
        
        spawnedItems = new Dictionary<GameObject, bool>();
        foreach (GameObject prefab in itemPrefabs)
        {
            spawnedItems[prefab] = false; // Инициализация предмета как не созданного
        }
    }

    public void Spawn()
    {
        int randomIndex = UnityEngine.Random.Range(0, itemPrefabs.Length);
        GameObject randomPrefab = itemPrefabs[randomIndex];

        if (!spawnedItems[randomPrefab])
        {
            // Создаем экземпляр случайного предмета
            GameObject spawnedItem = Instantiate(randomPrefab, transform.position, Quaternion.identity);
            spawnedItems[randomPrefab] = true; // Устанавливаем флаг созданного предмета
        }
        
        note.jobDone = false;
    }
    public IEnumerator GoDown()
    {
        if (!note.jobDone)
        {
            obj.goToEarth();
            HellLight.SetActive(true);

            yield return new WaitForSeconds(3);
            HellLight.SetActive(false);
            note.jobDone = true;
        }
    }
    
    public IEnumerator GoUp()
    {
        if (!note.jobDone)
        {
            obj.goToHeaven();
            HeavenLight.SetActive(true);

            yield return new WaitForSeconds(3);
            HeavenLight.SetActive(false);
            note.jobDone = true;
        }
    }
}
