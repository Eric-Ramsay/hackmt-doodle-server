namespace Server;

public class Models
{
    public Models()
    {

    }

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
    public PlayerCreateRequest() { }

    public string data { get; set; }
    public Models playerData { get; set; }
    public List<Models> players { get; set; }
}

public class GameState
{
    public GameState() {   }
    
    public string drawing { get; set; }
    public string currentWord { get; set; }
    public int round { get; set;  }
    public List<Models> players { get; set; }


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