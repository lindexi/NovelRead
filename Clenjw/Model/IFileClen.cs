using Windows.Storage;

namespace lindexi.uwp.Clenjw.Model
{
    public interface IFileClen
    {
        StorageFile File { get; set; }
        string Name { get; set; }
        int Poit { get; set; }
    }
}