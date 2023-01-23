namespace MultiShopEnd.Extensions
{
    public static class FileExtension
    {
        public static bool CheckSize(this IFormFile file, double kb)
            => kb*1024 > file.Length;
        public static bool CheckType(this IFormFile file, string type)
            => file.ContentType.Contains(type);


        public static string SaveFile(this IFormFile file,string path)
        {
            string filename = ChangeName(file.FileName);
            using(FileStream fs = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                file.CopyTo(fs);
            };
            return filename;
        }

        public static string SaveFileWithName(this IFormFile file, string path,string filename)
        {
            
            using (FileStream fs = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                file.CopyTo(fs);
            };
            return filename;
        }
        static string ChangeName(string name)
        {
            name = Guid.NewGuid() + name.Substring(name.LastIndexOf(".")); ;
            return name;
        }


        public static void DeleteFile(this string filename,string rootpath,string path)
        {
            path = Path.Combine(rootpath,path,filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }


        public static string CheckValidation(this IFormFile file,double kb,string type)
        {
            string result = "";
            if (!file.CheckSize(kb)) result += $"The size of the {file.FileName} file should not be greater than {kb} kb";
            if (!file.CheckType(type)) result += $"The {file.FileName} file type should be a {type}";
            return result;
        }
    }
}
