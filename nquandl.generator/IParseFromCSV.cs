
namespace NQuandl.Generator
{
    public interface IParseFromCSV
    {
        ICSVModel ParseToCSVModel { get; set; }
    }


    public interface IReturnCSVModel<T> where T : ICSVModel
    {
        T GetModelFromCSV();
    }

    public interface ICSVModel
    {
        string ApiCategory { get; }
        string DatabaseCode { get; }
        string TableCode { get; }
    }
}
