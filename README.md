# AutocadPlugin
Написать приложение-плагин к AUTOCAD:
Необходимо реализовать программу для редактирования слоёв и примитивов (отрезок, окружность, точка).
Свойства объектов:
− Слой: цвет, название, видимость;
− Точка: координаты, высота;
− Отрезок: координаты двух точек, высота;
− Окружность: координаты центра, радиус, высота.
Диалог должен быть реализован с использованием технологии WPF на языке С#.
Объекты должны быть сгруппированы по тем слоям, к которым они принадлежат.
Идентифицировать для пользователя слои с помощью названия, примитивы с помощью слов: отрезок, окружность, точка.
Диалог вызывается командой AUTOCAD.
Программа считывает информацию с текущего чертежа только перед отображением диалога, записывает в текущий чертёж после нажатия на кнопку подтверждения в диалоге.
