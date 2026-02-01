namespace Server;

public class Client
{
    public string name { get; set; }
    public string clientId { get; set; 
    
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
    public string clientId { get; set; }

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
        chat = new List<Message>(); 
        actions = new List<Action>();
        round = 1;
    }
    public List<string> randomWords { get; set; } = new List<string>();
    public int drawing { get; set; }
    public string currentWord { get; set; } = "";
    public int round { get; set;  } = 1;
    public List<Client> players { get; set; } = new List<Client>();
    public int turn { get; set; } = 0;
    public int drawerID { get; set; }
    public List<Message> chat { get; set; } = new List<Message>();
    public List<Action> actions { get; set; } = new List<Action>();
    public DateTime StartTimestamp { get; set; }


}
public class GameStateResponse
{
    public List<Action> NewActions { get; set; }
    public List<Message> NewMessages { get; set; }
    public int RoundNumber { get; set; }
    public int drawerID { get; set; }
    public string Word { get; set; }
    public int TimeRemaining { get; set; }
}

public class Point
{
    public Point() { }
    public int x { get; set; }
    public int y { get; set; }
}
public class Line
{
    public Line() { }
    public Line(Point a1, Point b1)
    {
        a = a1;
        b = b1;
    }
    public Point a { get; set; }
    public Point b { get; set; }
}

public enum TOOL {
        ERASER,
        PENCIL,
        BUCKET
    }
public class Action
{
    public Line line { get; set; }
    public TOOL tool { get; set; }
    public int width { get; set; }
    public string color { get; set; }
        public Action() { }

        public Action(Point a, Point b, int w, TOOL t, string c) {
		line = new Line(a, b);
		width = w;
		tool = t;
		color = c;
	}

}

public class Actions
{
    public List<Action> actions { get; set; } = new List<Action>();
}

public class Message
{ 
    public int userId { get; set;  }
    public string guess { get; set; }
    public bool correct { get; set;  }

}
public class WordChoiceRequest {
    public int clientId { get; set; }
    public string word { get; set; }
}
