using UnityEngine;

public abstract class LocalizedUiElement : MonoBehaviour
{
    [SerializeField] protected string key;

    protected Localization.OnLanguageChangedHandler onLanguageChangedHandler;

    private void Awake()
    {
        Init();

        onLanguageChangedHandler = (newLanguage) => Refresh();
        Localization.OnLanguageChanged += onLanguageChangedHandler;
    }
    private void Start()
    {
        Refresh();
    }
    private void OnDestroy()
    {
        Localization.OnLanguageChanged -= onLanguageChangedHandler;
    }

    public abstract void Refresh();

    protected abstract void Init();
}