using SQLite.Net.Interop;

namespace XVendas.Data
{
    public interface IConfig
    {
        string DiretorioSQLite { get; }
        ISQLitePlatform Plataforma { get; }
    }
}
