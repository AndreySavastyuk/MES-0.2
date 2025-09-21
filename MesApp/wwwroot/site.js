// Горячие клавиши для MES системы
window.mesHotkeys = {
    // Регистрация обработчиков горячих клавиш
    initialize: function () {
        document.addEventListener('keydown', function (e) {
            // Ctrl+S - Сохранить форму
            if (e.ctrlKey && e.key === 's') {
                e.preventDefault();
                mesHotkeys.triggerSave();
                return false;
            }

            // F5 - Обновить список
            if (e.key === 'F5') {
                e.preventDefault();
                mesHotkeys.triggerRefresh();
                return false;
            }
        });
    },

    // Вызов кнопки сохранения
    triggerSave: function () {
        // Ищем активную кнопку сохранения в форме
        const saveButtons = document.querySelectorAll('button[type="submit"], button:contains("Сохранить"), button:contains("Создать")');

        for (let button of saveButtons) {
            if (button.offsetParent !== null && !button.disabled) { // видимая и активная
                button.click();
                console.log('Hotkey: Save triggered');
                return;
            }
        }

        // Если не найдено, ищем по тексту
        const allButtons = document.querySelectorAll('button');
        for (let button of allButtons) {
            const text = button.textContent.trim().toLowerCase();
            if ((text.includes('сохранить') || text.includes('создать')) &&
                button.offsetParent !== null && !button.disabled) {
                button.click();
                console.log('Hotkey: Save triggered by text match');
                return;
            }
        }
    },

    // Вызов обновления через .NET
    triggerRefresh: function () {
        if (window.dotnetHelper) {
            window.dotnetHelper.invokeMethodAsync('RefreshData');
            console.log('Hotkey: Refresh triggered');
        } else {
            // Fallback - перезагрузка страницы
            location.reload();
        }
    },

    // Регистрация .NET helper для refresh
    setDotNetHelper: function (helper) {
        window.dotnetHelper = helper;
    }
};

// Автоинициализация при загрузке
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', mesHotkeys.initialize);
} else {
    mesHotkeys.initialize();
}