//modal potwierdzenia akcji
function ShowConfirmationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('bsConfirmationModal')).show();
}

function HideConfirmationModal() {
    bootstrap.Modal.getOrCreateInstance(document.getElementById('bsConfirmationModal')).hide();
}

//karuzela ksiazek
window.scrollCarousel = (id, direction, loop = true) => {
    const wrapper = document.getElementById(id);
    if (!wrapper) return;

    const container = wrapper.querySelector('.scroll-container');
    if (!container) return;

    const card = container.querySelector('.book-card');
    if (!card) return;

    const cardWidth = card.offsetWidth + 24; //wartość odstępu (gap)
    const scrollAmount = cardWidth * 1;

    const newScroll = container.scrollLeft + (direction * scrollAmount);

    if (loop) {
        const maxScroll = container.scrollWidth - container.clientWidth;

        if (newScroll > maxScroll) {
            container.scrollTo({ left: 0, behavior: 'smooth' });
        } else if (newScroll < 0) {
            container.scrollTo({ left: maxScroll, behavior: 'smooth' });
        } else {
            container.scrollBy({ left: direction * scrollAmount, behavior: 'smooth' });
        }
    } else {
        container.scrollBy({ left: direction * scrollAmount, behavior: 'smooth' });
    }
};
