using UnityEngine;
public static class NameGenerator
{
    public static string[] firstNames = { "John", "Emma", "Michael", "Sophia", "William" };
    public static string[] lastNames = { "Smith", "Johnson", "Brown", "Taylor", "Davis" };
    public static string GenerateRandomName()
    {
        string firstName = firstNames[Random.Range(0, firstNames.Length)];
        string lastName = lastNames[Random.Range(0, lastNames.Length)];

        string fullName = firstName + " " + lastName;

        return fullName;
    }
}