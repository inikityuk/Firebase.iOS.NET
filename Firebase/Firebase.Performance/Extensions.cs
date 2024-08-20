using System;
namespace Firebase.Performance
{
    public partial class HttpMetric
    {
        public HttpMetric(string url, Firebase.Performance.HttpMethod httpMethod) : this(new NSUrl(url), httpMethod)
        {
        }
    }
}

