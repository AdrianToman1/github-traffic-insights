using System.Text;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp.Services;

public class StorageService(BlobContainerClient container, TimeProvider timeProvider, ILogger<StorageService> logger)
{
    public async Task SaveRawResponseAsync(
        string apiName,
        HttpResponseMessage response)
    {
        var timestamp = timeProvider.GetUtcNow().ToString("yyyy-MM-ddTHH-mm-ssZ");
        var blobPath = $"{apiName}/{timestamp}.json";

        logger.LogInformation("Saving GitHub response to blob: {BlobPath}", blobPath);

        var transcript = await BuildHttpTranscript(response);
        
        var blobClient = container.GetBlobClient(blobPath);

        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(transcript)); 
        await blobClient.UploadAsync(stream, overwrite: true);
    }

    private static async Task<string> BuildHttpTranscript(HttpResponseMessage response)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"HTTP/{response.Version} {(int)response.StatusCode} {response.ReasonPhrase}");

        foreach (var header in response.Headers)
            sb.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");

        foreach (var header in response.Content.Headers)
            sb.AppendLine($"{header.Key}: {string.Join(", ", header.Value)}");

        sb.AppendLine(); // blank line before body

        var body = await response.Content.ReadAsStringAsync();
        sb.AppendLine(body);

        return sb.ToString();
    }
}