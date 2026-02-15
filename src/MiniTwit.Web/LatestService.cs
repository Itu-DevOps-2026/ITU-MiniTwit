namespace MiniTwit.Web;

public class LatestService
{
    private int _latest;

    public LatestService()
    {
        _latest = 0;
    }

    public void SetLatest(int? latest)
    {
        _latest = latest ?? _latest;
    }

    public int GetLatest()
    {
        return _latest;
    }
}