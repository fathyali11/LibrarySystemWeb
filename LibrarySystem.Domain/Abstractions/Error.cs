namespace LibrarySystem.Domain.Abstractions;
public class Error
{
    public string Code {  get; set; }=string.Empty;
    public string Description {  get; set; }=string.Empty;
    public int StatusCode {  get; set; }
    public Error(string code,string decription,int statusCode)
    {
        Code = code;
        Description = decription;
        StatusCode = statusCode;
    }
    public static Error None = new("", "", 0);
}
