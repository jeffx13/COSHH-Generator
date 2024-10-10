using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace COSHH_Generator.Core
{
    public interface SDSProvider
    {
        public int Timeout
        {
            get;
            set;
        }
        public string Name { get; }
        public Task<List<Result>> SearchAsync(string query, CancellationToken cancelToken = default);
        public Task<SafetyData> ExtractAsync(string url, CancellationToken cancelToken, Action<string> errorCallback);
    }
}
