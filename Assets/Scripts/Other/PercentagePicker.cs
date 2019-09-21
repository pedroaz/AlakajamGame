using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


class PercentagePicker
{
    private class PercentageOption
    {
        public string id;
        public float percentage;

        public PercentageOption(string id, float percentage)
        {
            this.id = id;
            this.percentage = percentage;
        }
    }

    List<PercentageOption> listOfOptions;
    Random randomGenerator;

    float totalPercentage;
    public enum PickerType
    {
        OneHundredMax,
        NoMaxValue
    }


    public PercentagePicker()
    {
        totalPercentage = 0;
        randomGenerator = new Random();
        listOfOptions = new List<PercentageOption>();
    }

    public void AddOption(string id, float percentage)
    {
        totalPercentage += percentage;
        listOfOptions.Add(new PercentageOption(id, percentage));
    }

    public void RemoveOption(string id)
    {
        PercentageOption option = listOfOptions.Find(x => x.id == id);
        totalPercentage -= option.percentage;
        listOfOptions.Remove(option);
    }

    private void OrderList()
    {
        listOfOptions = listOfOptions.OrderBy(x => x.percentage).ToList();
    }


    public string PickRandom()
    {
        OrderList();
        double randomValue = randomGenerator.NextDouble() * totalPercentage;
        Console.WriteLine(randomValue);
        float accumulatedValue = 0;
        foreach (PercentageOption option in listOfOptions) {
            accumulatedValue += option.percentage;
            if (randomValue <= accumulatedValue) {
                return option.id;
            }
        }
        return listOfOptions[listOfOptions.Count - 1].id;
    }
}

