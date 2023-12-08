namespace WakeUpServer.FileStorage;

using System;
using System.Threading.Tasks;

public interface IFileStorage
{
    Task<T?> ReadAsync<T>(string file);

    Task WriteAsync<T>(T? data, string file);

    Task UpdateAsync<T>(string file, Action<T> updateAction)
        where T : new();
}