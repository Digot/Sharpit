using System;

namespace Sharpit.Configuration
{

    public class FilePath : Attribute
    {
        public string Path { get; set; }

        public FilePath(string path)
        {
            this.Path = path;
        }
    }

    public class Comment : Attribute
    {
        public string Value { get; set; }

        public Comment(string value)
        {
            this.Value = value;
        }
    }
}