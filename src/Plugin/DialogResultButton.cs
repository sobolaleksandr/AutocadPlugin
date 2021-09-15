namespace ACADPlugin
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;

    /// <summary>
    /// Класс, позволяющий навешивать на кнопку возможность задания "DialogResult".
    /// </summary>
    public static class DialogResultButton
    {
        /// <summary>
        /// Реализация свойства зависимости.
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty = DependencyProperty.RegisterAttached(
            "DialogResult",
            typeof(bool?),
            typeof(DialogResultButton),
            new UIPropertyMetadata
            {
                PropertyChangedCallback = (dependencyObject, e) =>
                {
                    if (!(dependencyObject is Button button))
                        throw new InvalidOperationException("Можно использовать для объектов типа Button");

                    button.Click += (sender, e2) =>
                    {
                        var window = Window.GetWindow(button);
                        if (window == null)
                            return;

                        var dialogResult = GetDialogResult(button);

                        if (window.DataContext is DialogViewModelBase dialogVm)
                            dialogVm.DialogResult = dialogResult;

                        if (ComponentDispatcher.IsThreadModal)
                            window.DialogResult = dialogResult;
                        else
                            window.Close();
                    };
                }
            });

        /// <summary>
        /// Задание значения свойства DialogResult, которое будет выставляться после нажатия на кнопку.
        /// </summary>
        public static void SetDialogResult(DependencyObject obj, bool? value)
        {
            obj.SetValue(DialogResultProperty, value);
        }

        /// <summary>
        /// Получение значение свойства DialogResult.
        /// </summary>
        private static bool? GetDialogResult(DependencyObject obj)
        {
            return (bool?)obj.GetValue(DialogResultProperty);
        }
    }
}