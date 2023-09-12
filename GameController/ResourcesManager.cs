using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesManager : MonoBehaviour
{
    static public ResourcesManager instance;
    static public int _resourcesIron;
    static public int _resourcesNickel;
    static public int _resourcesGold;


    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _textIron;
    [SerializeField] private TextMeshProUGUI _textNickel;
    [SerializeField] private TextMeshProUGUI _textGold;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        _textIron.text = "Железо             " + _resourcesIron.ToString();
        _textNickel.text = "Никель             " + _resourcesNickel.ToString();
        _textGold.text = "Золото             " + _resourcesGold.ToString();
    }

    public static void AddResources(int i, ResourceType type)
    {
        if (type == ResourceType.Iron)
        {
            instance.StartCoroutine(AddResourcesCoroutine(i, type, _resourcesIron));
        }
        else if (type == ResourceType.Nickel)
        {
            instance.StartCoroutine(AddResourcesCoroutine(i, type, _resourcesNickel));
        }
        else if (type == ResourceType.Gold)
        {
            instance.StartCoroutine(AddResourcesCoroutine(i, type, _resourcesGold));
        }
        else
        {
            instance.StartCoroutine(AddResourcesCoroutine(i, type, _resourcesIron));
        }
    }

    private static IEnumerator AddResourcesCoroutine(int amount, ResourceType type, int targetValue)
    {
        int currentValue = 0;
        int step = amount / 20; // Количество шагов для добавления ресурсов
        if (step < 1) step = 1; // Не допустим нулевые шаги

        while (currentValue < amount)
        {
            currentValue += step;
            if (currentValue > amount) currentValue = amount;

            if (type == ResourceType.Iron)
            {
                _resourcesIron += step;
            }
            else if (type == ResourceType.Nickel)
            {
                _resourcesNickel += step;
            }
            else if (type == ResourceType.Gold)
            {
                _resourcesGold += step;
            }
            // Возможно, что-то нужно сделать для других типов ресурсов

            yield return new WaitForSeconds(0.05f); // Задержка между шагами
        }
    }

    public static void SetResources(int iron, int nickel, int gold)
    {
        _resourcesIron -= iron;
        _resourcesNickel -= nickel;
        _resourcesGold -= gold;
    }
}
