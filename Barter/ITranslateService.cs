namespace Barter
{
    public interface ITranslateService
    {
        /// <summary>
        /// Перепечатывает указанный текст в другой раскладке
        /// </summary>
        string ToAnotherKeyboardLayout(string value);
    }
}
