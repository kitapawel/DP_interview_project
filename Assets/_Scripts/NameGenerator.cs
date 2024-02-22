using UnityEngine;
public static class NameGenerator
{
    public static string[] firstNames = { "John", "Adam", "Michael", "Andrew", "William", "Lancelot", "Lancealittle", "Graham" };
    public static string[] lastNames = { "Smith", "Johnson", "Brown", "Tailor", "Davis", " of Loxley", "of Arc", "son of Durin", "Ramsey", "Snow"};
    public static string GenerateRandomName()
    {
        string firstName = firstNames[Random.Range(0, firstNames.Length)];
        string lastName = lastNames[Random.Range(0, lastNames.Length)];

        string fullName = firstName + " " + lastName;

        if (fullName == "John Snow")
        {
            fullName = "John 'Knows nothing' Snow";
        }

        return fullName;
    }
}