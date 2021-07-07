using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Album<V, D> : MonoBehaviour, IPagable
    where V : IShowable<D>, IHidable
{
    private const int MIN_PAGE_INDEX = 0;


    [SerializeField] private List<V> elementViews;
    [SerializeField] [RequireInterface(typeof(IShowHidable))] private Object prevPageButton;
    [SerializeField] [RequireInterface(typeof(IShowHidable))] private Object nextPageButton;
    [SerializeField] private TextMeshProUGUI pageNumber;


    private List<D> elements;
    private int pageIndex;
    private int pageCapacity;
    private int pagesCount;


    protected virtual void Awake()
    {
        pageCapacity = elementViews.Count;
    }


    public virtual void Show(List<D> elements, int pageIndex = MIN_PAGE_INDEX)
    {
        this.pageIndex = pageIndex;
        this.elements = elements;
        pagesCount = 1 + elements.Count / pageCapacity;

        ShowPage(pageIndex);
    }
    public void PrevPage()
    {
        ShowPage(pageIndex - 1);
    }
    public void NextPage()
    {
        ShowPage(pageIndex + 1);
    }

    private void ShowPage(int index)
    {
        pageIndex = MathfExt.Clamp(index, MIN_PAGE_INDEX, pagesCount - 1);

        int elementIndex;
        int viewIndex;
        for (elementIndex = pageCapacity * pageIndex, viewIndex = MIN_PAGE_INDEX;
            elementIndex < pageCapacity * (pageIndex + 1) && elementIndex < elements.Count && viewIndex < pageCapacity;
            elementIndex++, viewIndex++)
        {
            elementViews[viewIndex].Show(elements[elementIndex]);
        }
        for (; viewIndex < elementViews.Count; viewIndex++)
        {
            elementViews[viewIndex].Hide();
        }

        pageNumber.text = (pageIndex + 1) + " / " + pagesCount;

        SetupButtons();
    }
    private void SetupButtons()
    {
        if (pageIndex == MIN_PAGE_INDEX)
        {
            PrevPageButton.Hide();
        }
        else
        {
            PrevPageButton.Show();
        }

        if (pageIndex == pagesCount - 1)
        {
            NextPageButton.Hide();
        }
        else
        {
            NextPageButton.Show();
        }
    }


    public int PageIndex => pageIndex;
    public int PageCount => pagesCount;

    private IShowHidable PrevPageButton => prevPageButton as IShowHidable;
    private IShowHidable NextPageButton => nextPageButton as IShowHidable;
}
