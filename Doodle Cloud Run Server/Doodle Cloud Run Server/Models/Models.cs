namespace Server;

public class Client
{
    public string name { get; set; }
    public int clientId { get; set; 
    
    }
    public int playerScore{ get; set;

    }

    public bool connected { get; set; }
    public DateTime lastTime { get; set; }

}

public class PlayerCreateRequest
{
    public string name { get; set; }
 
}

public class PlayerCreateResponse
{
    public int clientId { get; set; }

}

public class RandomWordsResponse
{
    public List<string> chosenWords { get; set; } = new List<string>();
}
public class TransitionResponse
{
    public Dictionary<int, int> scores { get; set; } = new Dictionary<int, int>();
    public List<string> chosenWords { get; set; } = new List<string>();
    public int drawerId { get; set; }

}
public class RoundStartResponse
{
    public string censoredWord { get; set; }
    public string uncensoredWord { get; set; }
    public int drawerId { get; set; }
}
public class RoundStartRequest
{
    public string word { get; set; }
}
public class GuessCreateRequest
{
    public string guess { get; set; }
}
public class GuessResponse
{
    public bool correct { get; set; }
}
public class GameState
{
    public GameState()
    {
        string filePath = @"myWords.txt";
        try
        {
            randomWords = File.ReadAllLines(filePath).ToList();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: The file '{filePath}' was not found.");
        }
    }
    public List<string> randomWords { get; set; } = new List<string>();
    public int drawing { get; set; }
    public string currentWord { get; set; } = "";
    public int round { get; set;  }
    public List<Client> players { get; set; } = new List<Client>();
    public int turn { get; set; } = 0;
    public int drawerId { get; set; }



}
public class GameStateResponse
{
    public int drawing { get; set; }
    public string currentWord { get; set; } = "";
    public int round { get; set; }

}

public class Draw
{
    public Draw() { }

    public enum TOOL {
        ERASER,
        PENCIL,
        BUCKET
    }
    public int brushSize { get; set; }
    public string color { get; set; }
    public List<Line> points{ get; set; }

}

public class Line
{
    public Line() { }
    public Points a { get; set; }
    public Points b { get; set; }
}

public class Points
{
    public Points() { }
    public int x { get; set; }
    public int y { get; set; }
}