namespace Brainbay.DataRelay.Sync;

public class APIClientUriBuilder
{
    public class Character
    {
        public static string GetCharactersUri(string baseUri, int page)
        {
            return $"{baseUri}/character?page={page}";
        }
    }
}