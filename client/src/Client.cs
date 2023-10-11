using System;
using neonet.lib;

namespace neonet.client;


public class Client
{
    public static void Main(string[] args)
    {

        var gameClient = new RectangleTest("test", 500, 500);
        gameClient.Run();
    }
}