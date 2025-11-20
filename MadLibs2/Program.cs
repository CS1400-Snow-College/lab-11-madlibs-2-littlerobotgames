//Alex Gardner | Madlibs 2 | 11/19/25
class Progam
{
    static string fileLocation = Environment.CurrentDirectory + "../../../../";
    static void Main(string[] filenames)
    {
        Dictionary<string, List<string>> newWords = new Dictionary<string, List<string>>();
        newWords.Add("adjective", new List<string>() {"loud", "cringe", "tiny", "yellow", "stupid", "flat", "goofy", "hot", "sticky", "yummy"});
        newWords.Add("past-tense-verb", new List<string>() { "hit", "ran", "cried", "flew", "fell", "chopped", "sliced", "drove", "laughed"});
        newWords.Add("noun", new List<string>() {"wood", "pencil", "book", "chocolate", "flower", "monster", "goose", "bed", "fire", "brick"});
        newWords.Add("plural-noun", new List<string>() {"carrots", "dogs", "bugs", "poops", "blocks", "robots", "dinosaurs", "rocks", "cards", "geese"});
        newWords.Add("verb", new List<string>() {"hit", "run", "jump", "attack", "throw", "fall", "smack", "hide", "break", "kill"});

        Dictionary<string, List<string>> stories = new Dictionary<string, List<string>>();
        Random random = new Random();

        //Get the stories
        foreach(string filename in filenames)
        {
            stories.Add(filename, OpenFile(filename));
        }

        foreach(KeyValuePair<string, List<string>> story in stories)
        {
            for (int i = 0; i < story.Value.Count; i++)
            {
                if (IsKeyword(story.Value[i]))
                {
                    string keyword = GetKeyword(story.Value[i]);
                    List<string> randomWords = newWords[keyword];

                    string randomWord = randomWords[random.Next(randomWords.Count)];

                    story.Value[i] = randomWord;
                }
            }

            SaveFile(story.Value, "generated." + story.Key);
        }
    }

    static List<string> OpenFile(string filename)
    {
        string allWords = File.ReadAllText(fileLocation + "/" + filename);
        string[] words = allWords.Split(' ');

        return words.ToList();
    }
    static void SaveFile(List<string> story, string filename)
    {
        string storyString = "";

        foreach(string word in story)
        {
            storyString += " " + word;
        }

        File.WriteAllText(fileLocation + "/" + filename, storyString);
    }
    static bool IsKeyword(string word)
    {
        return word.Contains(":");
    }
    static string GetKeyword(string word)
    {
        int startIndex = word.IndexOf(':') + 2;

        return word.Substring(startIndex, word.Length - startIndex);
    }
}