using System.Threading.Tasks;
using System;

public interface ICityBikeDataFetcher
{
    public Task<int> GetBikeCountInStation(string stationName);
}