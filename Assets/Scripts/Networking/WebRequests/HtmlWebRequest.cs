using System;
using System.Threading.Tasks;

partial class WebRequests
{
    public static class HTML
    {
        public static void GetBodyAsync(Uri url, Action<string> onSuccess, Action<string> onError = null)
        {
            WebRequests.Text.GetAsync(url, OnSuccess, OnError);

            void OnSuccess(string text)
            {
                Task<string> task = Task.Run(() => GetBodyFromText(text));
                
                string GetBodyFromText(string htmlText)
                {
                    string from = "<body>";
                    string to = "</body>";

                    int indexFrom = htmlText.IndexOf(from, StringComparison.Ordinal) + from.Length;
                    int indexTo = htmlText.LastIndexOf(to, StringComparison.Ordinal);

                    return htmlText.Substring(indexFrom, indexTo - indexFrom);
                }
                
                task.Wait();
                
                onSuccess?.Invoke(task.Result);
            }

            void OnError(string error)
            {
                onError?.Invoke(error);
            }
        }
    }
}