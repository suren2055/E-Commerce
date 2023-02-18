using System.Net;
using System.Text;

namespace E_Commerce.WEB.Helpers;


public static class HttpCaller
{
    private static HttpClient client;

    static HttpCaller()
    {
        client = new HttpClient();
    }

    public static async Task<HttpRequestOutput> SendAsync(HttpRequestInput input, short cancelTimeout = 30000)
    {
        try
        {
            if (input.Headers != null && input.Headers.Count != 0)
                foreach (var header in input.Headers)
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
         
         

            var cts = new CancellationTokenSource();
            var ctsToken = cts.Token;
            cts.CancelAfter(cancelTimeout);

            var request = new HttpRequestMessage(input.Methods, input.Url)
            {
                Content = string.IsNullOrWhiteSpace(input.Data)
                    ? null
                    : new StringContent(input.Data, Encoding.UTF8, input.ContentType)
            };


            var resp = await client.SendAsync(request, ctsToken);


            var responseFromServer = await resp.Content.ReadAsStreamAsync(ctsToken);
            return new HttpRequestOutput
            {
                Response = responseFromServer,
                StatusCode = (int) resp.StatusCode / 100 == 2 ? HttpStatusCode.OK : resp.StatusCode
            };
        }
        catch (TaskCanceledException exc)
        {
            return new HttpRequestOutput
            {
                StatusCode = HttpStatusCode.RequestTimeout,
            };
        }
        catch (WebException exc)
        {
            var res = (HttpWebResponse) exc.Response;

            return new HttpRequestOutput
            {
                StatusCode = res.StatusCode,
            };
        }
        catch (Exception exc)
        {
            return new HttpRequestOutput
            {
                StatusCode = HttpStatusCode.ExpectationFailed,
            };
        }
    }
}

public class HttpRequestInput
{
    public Dictionary<string, string> Headers { get; set; }
    public string Data { get; set; } = string.Empty;
    public string Url { get; set; }
    public HttpMethod Methods { get; set; }
    public string ContentType { get; set; } = string.Empty;
}
public class HttpRequestOutput
{
    public HttpStatusCode StatusCode { get; set; }
    public Stream Response { get; set; }
}