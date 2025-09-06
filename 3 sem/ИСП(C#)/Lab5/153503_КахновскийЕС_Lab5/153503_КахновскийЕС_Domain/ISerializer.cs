using System.Collections.Generic;


namespace _153503_КахновскийЕС_Domain
{
    public interface ISerializer
    {
        IEnumerable<Book> DeSerializeByLINQ(string fileName);
        IEnumerable<Book> DeSerializeXML(string fileName);
        IEnumerable<Book> DeSerializeJSON(string fileName);
        void SerializeByLINQ(IEnumerable<Book> Lib, string fileName);
        void SerializeXML(IEnumerable<Book> Lib, string fileName);
        void SerializeJSON(IEnumerable<Book> Lib, string fileName);
    }
}
