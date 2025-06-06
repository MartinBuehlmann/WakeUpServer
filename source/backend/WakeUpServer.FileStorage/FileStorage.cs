using System.Collections.Concurrent;
using System.Reflection;
using Newtonsoft.Json;

namespace WakeUpServer.FileStorage;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

internal class FileStorage : IFileStorage
{
    private readonly string directory;
    private readonly ConcurrentDictionary<string, SemaphoreSlim> fileLocks;

    public FileStorage()
    {
        this.fileLocks = new ConcurrentDictionary<string, SemaphoreSlim>();
        this.directory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "../Data");
        this.EnsureDataDirectoryExists();
    }

    public async Task<T?> ReadAsync<T>(string file)
    {
        string filePath = Path.Combine(this.directory, $"{file}.json");
        var semaphore = this.fileLocks.GetOrAdd(filePath, new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync();
        try
        {
            if (!File.Exists(filePath))
            {
                return default;
            }

            string jsonResult = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<T>(jsonResult);
        }
        finally
        {
            semaphore.Release();
        }
    }

    public async Task WriteAsync<T>(T? data, string file)
    {
        string filePath = Path.Combine(this.directory, $"{file}.json");
        SemaphoreSlim semaphore = this.fileLocks.GetOrAdd(filePath, new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync();
        try
        {
            string jsonResult = JsonConvert.SerializeObject(data);
            await File.WriteAllTextAsync(filePath, jsonResult);
        }
        finally
        {
            semaphore.Release();
        }
    }

    public async Task UpdateAsync<T>(string file, Action<T> updateAction)
        where T : new()
    {
        string filePath = Path.Combine(this.directory, $"{file}.json");
        SemaphoreSlim semaphore = this.fileLocks.GetOrAdd(filePath, new SemaphoreSlim(1, 1));
        await semaphore.WaitAsync();
        try
        {
            var data = new T();
            if (File.Exists(filePath))
            {
                string content = await File.ReadAllTextAsync(filePath);
                data = JsonConvert.DeserializeObject<T>(content)!;
            }

            updateAction(data);

            await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(data));
        }
        finally
        {
            semaphore.Release();
        }
    }

    private void EnsureDataDirectoryExists()
    {
        if (!Directory.Exists(this.directory))
        {
            Directory.CreateDirectory(this.directory);
        }
    }
}