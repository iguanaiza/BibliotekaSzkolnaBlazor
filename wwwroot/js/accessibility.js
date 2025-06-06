//zmiana motywu, kontrastu, wielkosci czcionki
window.applyUserPreferences = () => {
    const body = document.body;
    if (!body) return;

    const savedTheme = localStorage.getItem('theme');
    const savedFontSize = localStorage.getItem('fontSize');

    body.classList.remove(...Array.from(body.classList).filter(cls => cls.startsWith('theme-')));
    if (savedTheme) {
        body.classList.add(savedTheme);
    }

    body.classList.remove(...Array.from(body.classList).filter(cls => cls.startsWith('font-')));
    if (savedFontSize) {
        body.classList.add(savedFontSize);
    }
};

window.setTheme = (themeClass) => {
    localStorage.setItem('theme', themeClass);
    window.applyUserPreferences();
};

window.setFontSize = (fontSizeClass) => {
    localStorage.setItem('fontSize', fontSizeClass);
    window.applyUserPreferences();
};
