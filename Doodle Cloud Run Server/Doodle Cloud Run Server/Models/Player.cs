namespace Server;

public class Player
{
    public Player()
    {

    }

    public string name { get; set; }
    public string clientId { get; set; 
    
    }
}

public class PlayerCreateRequest
{
    public PlayerCreateRequest() { }

    public string data { get; set; }
    public Player playerData { get; set; }
    public List<Player> players { get; set; }
}
