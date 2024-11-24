namespace Business.ExtensionMethods;

public static class ChangeCultrueExtension
{
  public static string ChangeCharacters(this string word)
  {
    return word.ToLower()
      .Replace('ı', 'i')
      .Replace('ö', 'o')
      .Replace('ü', 'u')
      .Replace('ğ', 'g')
      .Replace('ş', 's')
      .Replace('ç', 'c');
  }
}