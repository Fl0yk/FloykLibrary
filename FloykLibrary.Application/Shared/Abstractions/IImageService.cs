namespace FloykLibrary.Application.Shared.Abstractions
{
    public interface IImageService
    {
        public Task<string> SaveImageAsync(Stream source);
    }
}
