using Amazon;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace Blogs;

public class SqsProducer(ISqsClientFactory factory) : ISqsProducer
{
    private readonly IAmazonSQS _client = factory.GetClient();

    public async Task SendMessage(string groupId, string message)
    {
        var request = new SendMessageRequest
        {
            QueueUrl = "test.fifo",
            MessageBody = $"{{\r\n\"Message\": \"{message}\"}}",
            MessageGroupId = groupId
        };

        await _client.SendMessageAsync(request);
    }
}

public interface ISqsProducer
{
    Task SendMessage(string groupId, string message);
}

public class SqsClientFactory : ISqsClientFactory
{
    private readonly IAmazonSQS _client = GetNewClient();

    public IAmazonSQS GetClient() => _client;

    private static AmazonSQSClient GetNewClient()
    {
        var chain = new CredentialProfileStoreChain();
        if (!chain.TryGetAWSCredentials("staging", out var credentials))
        {
            throw new InvalidOperationException("Failed to find the aws sso profile");
        }

        var devClient = new AmazonSQSClient(credentials, RegionEndpoint.EUWest1);

        return devClient;
    }
}

public interface ISqsClientFactory
{
    IAmazonSQS GetClient();
}
