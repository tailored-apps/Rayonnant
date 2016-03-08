namespace Wise.Framework.Rest
{
    public interface IRestResponse
    {
        int ErrorCode { get; set; }
        bool Status { get; set; }
    }
}