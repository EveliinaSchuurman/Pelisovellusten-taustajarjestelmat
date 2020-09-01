

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace vscode_test
{
    
    public class BikeRentalStationList{
        public List<Stations> stations{get; set;}
    }
    public class Stations{
        public int Id{get; set;}
        public string Name{get; set;}
        public float x {get; set;}
        public float y {get; set;}
        public int bikesAvailable{get; set;}
        public int spacesAvailable{get; set;}
        public bool allowDropOff {get; set;}
        public bool isFloatingBike {get; set;}
        public bool isCarstation{get; set;}
        public string state{get; set;}
        public List<string> networks { get; set; }
        public bool realTimeData { get; set; }

        }
    class NotFoundException : Exception
    {
        public NotFoundException(string message)
        {
            Console.WriteLine(message);
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            
            RealTimeCityBikeDataFetcher rl = new RealTimeCityBikeDataFetcher();
            OfflineCityBikeDataFetcher ol = new OfflineCityBikeDataFetcher();
            Console.WriteLine("Offline(0) or realtime(1)?");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Station: ");
            string y = Console.ReadLine();
            int yep;
            if(x==1){
                yep = await rl.GetBikeCountInStation(y);
                Console.WriteLine(yep);
            }
            else if(x==0){
                yep = await ol.GetBikeCountInStation(y);
                Console.WriteLine(yep);
            }
        }
    }

    public class OfflineCityBikeDataFetcher : ICityBikeDataFetcher{
        
        public Task<int> GetBikeCountInStation(string stationName){
             try{
                 if(stationName.Any(char.IsDigit)){
                    throw new ArgumentException("Invalid argument: no numbers pls");
                }
                string[] lines = File.ReadAllLines(@"bikeData.txt");  
                foreach (string line in lines){  
                    if(line.Contains(stationName)){
                        string stringAfterChar = line.Substring(line.IndexOf(":") + 2); 
                        return Task.FromResult(Int32.Parse(stringAfterChar));
                    }
                }
             }
             catch(ArgumentException yeet){
                Console.WriteLine(yeet.Message);
            }
            catch(NotFoundException gotYeeted){
                Console.WriteLine(gotYeeted.Message);
            }   
            return Task.FromResult(0);
            }

        }
    
   
    public class  RealTimeCityBikeDataFetcher : ICityBikeDataFetcher{
         
        static HttpClient httpClient = new HttpClient();
        const string URL = @"http://api.digitransit.fi/routing/v1/routers/hsl/bike_rental";
        public async Task<int> GetBikeCountInStation(string stationName){
            
            try{
                if(stationName.Any(char.IsDigit)){
                    throw new ArgumentException("Invalid argument: no numbers pls");
                }
                
                string responseBody = await httpClient.GetStringAsync(URL);
                BikeRentalStationList bl = JsonConvert.DeserializeObject<BikeRentalStationList>(responseBody);
                foreach(Stations stations in bl.stations){
                    if(stations.Name == stationName){
                        return stations.bikesAvailable;
                    }
                    else
                    {
                        throw new NotFoundException("Parameter cannot be null");
                    }
                }   
            }
            catch(ArgumentException yeet){
                Console.WriteLine(yeet.Message);
            }
            catch(NotFoundException gotYeeted){
                Console.WriteLine(gotYeeted.Message);
            }   
            return 0;
        }
    }
            
}
