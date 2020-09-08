using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;


namespace vscode_test
{
    public static class Extenxion {
        public static int GetHighestLevelItem (this Player player) {
            int itemCount = player.Items.Count;
            Item best = new Item();
            for(int i=0; i<itemCount; i++){
                for(int j=0; j<itemCount; j++){
                    if(player.Items[i].Level>player.Items[j].Level){
                        best = player.Items[i];
                    }else if(player.Items[j].Level>player.Items[i].Level){
                        best = player.Items[j];
                }
            } 
        }
        Console.WriteLine("Best item got");
        return best.Level;
    }
    }
    class Program2
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");
            List<Player> players = new List<Player>();
            players = CreatePlayers(1000000, players);
            int count = players.Count;
            for(int i = 0; i < count; i++){
                players[i].Items = ItemCreator(players[i].Items);
            }
            
            int GetHighestLevelIte = players[1].GetHighestLevelItem();
            //Console.WriteLine(GetHighestLevelItem + "yeet");
            Console.WriteLine((GetHighestLevelIte).ToString());
            
             ProcessEachItem(players[0], PrintItem);
            ProcessEachItem(players[0], PrintBoio);
        }

        public static void ProcessEachItem(Player player, Action<Item> process){
            for(int i = 0; i<player.Items.Count;i++){
                process(player.Items[i]);
            }
        }
        public Item FirstItem(Player player){
            if(player.Items[0] != null)
                return player.Items[0];
            else return null;
        }
        public Item FirstItemWithLinq(Player player){
            bool hasElements = player.Items.Any();
            return hasElements ? player.Items[0] : null;
        }

        //GetItems: Transform the list to an array using normal C# loop to do the work and return it
        public Array GetItems(List<Item> items){
            Item[] array = items.ToArray();
            return array;
        }
        public Array GetItemsWithLinq(List<Item> items){
            Item[] array = items.Select(x=>x).ToArray();
            return array;
        }
        
        public static void PrintItem(Item item){
            Console.WriteLine(item.Id);
            Console.WriteLine(item.Level);
        }
        //Call the ProcessEachItem function (implemented in the previous exercise) with a lambda 
        //function that does the same thing as the function in the previous excercise (so print the Id and Level to the console).

        public static void PrintBoio(Item item){
            item.Id => (Console.WriteLine(item.Id);
            item.Level => Console.WriteLine(item.Level); 
        }

        public static Guid CreateGUID(){
            Guid guid = Guid.NewGuid();
            return guid;
        }
        public static List<Item> ItemCreator(List<Item> items){
            Random rand = new Random();
            int listsize = rand.Next(0,4);
            for(int i = 0;i<listsize;i++){
                Item item = new Item();
                item.Id = CreateGUID();
                item.Level = rand.Next();
                items.Add(item);
                
            }
            Console.WriteLine("Items created");
            return items;
        }
        

        public static List<Player> CreatePlayers(int numbr, List<Player> players){
            for(int i = 0; i<numbr;i++){
               players.Add(new Player());
               players[i].Id = CreateGUID(); 
            }
            for(int i=0; i<numbr; i++){
                for(int j=0; j<numbr; j++){
                    if(players[i].Id==players[j].Id && i!=j){
                        players[i].Id = CreateGUID();
                        //no I'm not going to check it again
                    }
                    else continue;
                }
            }
            Console.WriteLine("players created");
            return players;
        }
    }

        public class Player : IPlayer
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
    }
    public class Game<T> where T : IPlayer
{
    private List<T> _players;

    public Game(List<T> players) {
        _players = players;
    }

    //public T[] GetTop10Players() {
        // ... write code that returns 10 players with highest scores
    //    return ;
    //}
}
}
