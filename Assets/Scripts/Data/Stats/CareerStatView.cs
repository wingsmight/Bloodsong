public class CareerStatView : UserStatView, IShowable<CareerStat>
{
    public void Init(CareerStat careerStat)
    {
        currentData = careerStat;
        careerStat.OnDataChanged += (value) => Show(value as CareerStat);

        Refresh();
    }
    public void Show(CareerStat careerStat)
    {
        text.text = careerStat.AmountInCurrentBound + " / " + careerStat.Bounds[careerStat.BoundIndex];
        shadowText.text = text.text;
    }
    public override void Refresh()
    {
        Show(currentData as CareerStat);
    }
}
