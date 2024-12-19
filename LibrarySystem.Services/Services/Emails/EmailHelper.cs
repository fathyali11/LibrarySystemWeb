namespace LibrarySystem.Services.Services.Emails
{
    public static class EmailHelper
    {
        public static string PrepareBodyTemplate(string path,string name,Dictionary<string,string> data)
        {
            var fullPath=Path.Combine(path,name);
            string body=File.ReadAllText(fullPath);

            foreach(var key in data.Keys) 
                body=body.Replace(key, data[key]);

            return body ;

        }
    }
}
