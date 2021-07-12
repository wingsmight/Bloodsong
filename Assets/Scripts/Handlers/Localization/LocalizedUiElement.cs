using UnityEngine;

public abstract class LocalizedUiElement : MonoBehaviour
{
    [SerializeField] protected string key;

    protected Localization.OnLanguageChangedHandler onLanguageChangedHandler;

    private void Awake()
    {
        Init();

        onLanguageChangedHandler = (newLanguage) => Refresh();
        Localization.OnLanguageChanged += (_) => onLanguageChangedHandler?.Invoke(_);
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