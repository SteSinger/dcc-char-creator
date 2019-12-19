using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DccCharCreator.web
{
    public class CounterMiddleware
    {
        private static long count = 0;

        private readonly RequestDelegate _next;

        private const string CounterFile = "counter.txt";

        static CounterMiddleware()
        {
            if (File.Exists(CounterFile))
            {
                var lastLine = File.ReadLines(CounterFile).Last();
                count = int.Parse(lastLine);
            }
        }

        public CounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            count++;
            context.Items["count"] = count;

            try
            {
                if(count % 100 == 0)
                    await File.WriteAllLinesAsync(CounterFile, new[] { count.ToString() });
                else
                    await File.AppendAllLinesAsync(CounterFile, new[] { count.ToString() });
            }
            catch
            {

            }
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
}
