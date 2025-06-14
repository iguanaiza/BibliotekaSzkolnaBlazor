//dostepne zmienne
window.fontSizes = ['font-small', 'font-normal', 'font-large'];
window.themes = ['theme-green', 'theme-blue'];

//Zapis ustawień
window.setTheme = (themeClass) => {
    localStorage.setItem('theme', themeClass);
    window.applyUserPreferences();
};

window.setFontSize = (fontSizeClass) => {
    localStorage.setItem('fontSize', fontSizeClass);
    window.applyUserPreferences();
};

// Cykliczna zmiana rozmiaru czcionki
window.cycleFontSize = () => {
    const current = localStorage.getItem('fontSize') || 'font-normal';
    const sizes = window.fontSizes;
    const index = sizes.indexOf(current);
    const next = sizes[(index + 1) % sizes.length];
    localStorage.setItem('fontSize', next);
    window.applyUserPreferences();
};

// Cykliczna zmiana motywu 
window.cycleTheme = () => {
    const current = localStorage.getItem('theme') || 'theme-green';

    if (current === 'theme-contrast') {
        const fallback = localStorage.getItem('lastTheme') || 'theme-green';
        localStorage.setItem('theme', fallback);
    } else {
        const index = window.themes.indexOf(current);
        const next = window.themes[(index + 1) % window.themes.length];
        localStorage.setItem('lastTheme', next);
        localStorage.setItem('theme', next);
    }

    window.applyUserPreferences();
};

// Osobny tryb wysokiego kontrastu
window.toggleContrast = () => {
    const current = localStorage.getItem('theme');
    if (current === 'theme-contrast') {
        const fallback = localStorage.getItem('lastTheme') || 'theme-green';
        localStorage.setItem('theme', fallback);
    } else {
        localStorage.setItem('lastTheme', current);
        localStorage.setItem('theme', 'theme-contrast');
    }

    window.applyUserPreferences();
};

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

    console.log('Applied classes:', body.className);
};