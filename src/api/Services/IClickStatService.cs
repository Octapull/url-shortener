namespace api.Services;

public interface IClickStatService
{
    public Task RecordClickAsync(string code);

}