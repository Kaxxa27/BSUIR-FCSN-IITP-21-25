namespace Class.Interface {
    public interface IStreamService<T> {
        public Task WriteToStreamAsync(Stream stream, IEnumerable<T> data, IProgress<string> progress);

        public Task CopyFromStreamAsync(Stream stream, string fileName, IProgress<string> progress);

        public Task<int> GetStatisticsAsync(string fileName, Func<T, bool> filter);
    }
}
